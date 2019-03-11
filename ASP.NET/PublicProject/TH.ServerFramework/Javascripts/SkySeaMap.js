; (function ($) {

    'use strict';

    dojo.require("esri.map");
    dojo.require("esri.dijit.OverviewMap");
    dojo.require("esri.dijit.BasemapToggle");
    dojo.require("esri.dijit.Scalebar");
    dojo.require("esri.dijit.Search");
    dojo.require("esri.dijit.Measurement");
    dojo.require("esri.dijit.HorizontalSlider");
    dojo.require("esri.dijit.editing.TemplatePicker");
    dojo.require("esri.toolbars.navigation");
    dojo.require("esri.toolbars.draw");
    dojo.require("esri.toolbars.edit");
    dojo.require("esri.tasks.GeometryService");
    dojo.require("esri.tasks.RelationParameters");
    dojo.require("esri.tasks.BufferParameters");
    dojo.require("esri.tasks.DistanceParameters");
    dojo.require("esri.geometry.Point");
    dojo.require("esri.graphic");
    dojo.require("esri.InfoTemplate");
    dojo.require("esri.layers.ArcGISTiledMapServiceLayer");
    dojo.require("esri.layers.FeatureLayer");
    dojo.require("esri.layers.ArcGISDynamicMapServiceLayer");
    dojo.require("dojo.dnd.move");


    var MyMap = new function () {
        var _this = this;
        _this.mapEntity = null;
        _this.searchEntity = null;
        _this.basemapToggle = null;
        _this.initView = function () {
            _this.setBaseMaps();

            InitView.initMap();
            InitView.overView();
            InitView.basemapToggle();
            InitView.scaleBar();
            InitView.searchbar();
            InitView.Compass();
            InitView.showlocaltion();
            CommonHelper.initSymbol();
            ToolBar.init();
            ToolBar.initDialog();
        }

        _this.setBaseMaps = function () {
            $.each(MapOptions.baseMaps, function (i, item) {
                if (typeof esri.basemaps[item.id] === 'object') {
                    esri.basemaps[item.id].baseMapLayers = [{ url: item.url }];
                }
            });
        }

        _this.clearResult = function () {
            MyMap.mapEntity.infoWindow.hide();
            MyMap.mapEntity.graphics.clear();
        }

        _this.layerAddEvent = function (evt) {
            var layerInfo = LayerCtrl.getFeatureLayerById(evt.layer.id);

            if (layerInfo == null) return;

            layerInfo.entity.on("click", graphicClickEvent);
        }

        function graphicClickEvent(evt) {
            if (ToolBar.actionType == "layerEdit") return;

            var layerInfo = LayerCtrl.curFeatureLayer();

            if (layerInfo == null || layerInfo.id != this.id || !layerInfo.isQuery) return;

            if (typeof MapOptions.callBacks.graphicClicked === 'function')
                MapOptions.callBacks.graphicClicked({ layerId: layerInfo.id, feature: evt.graphic });

            _this.clearResult();

            if (MapOptions.graphicClicked == null || !MapOptions.graphicClicked.ishighLight) return;

            var symbol = CommonHelper.getDefSymbol(layerInfo);
            var infoTemplate = CommonHelper.getInfoTemplate(layerInfo);
            var graphic = new esri.Graphic(evt.graphic.geometry, symbol, evt.graphic.attributes, infoTemplate);
            MyMap.mapEntity.graphics.add(graphic);

            if (!MapOptions.graphicClicked.isShowInfo) return;

            MyMap.mapEntity.infoWindow.setContent(graphic.getContent());
            MyMap.mapEntity.infoWindow.setTitle(graphic.getTitle());
            MyMap.mapEntity.infoWindow.show(evt.screenPoint, MyMap.mapEntity.getInfoWindowAnchor(evt.screenPoint));

        }
    }

    var InitView = new function () {
        var _this = this;
        _this.initMap = function () {
            $("#" + MapOptions.name).addClass("calcite skyseamap");
            MyMap.mapEntity = new esri.Map(MapOptions.name, {
                logo: false, showAttribution: false, basemap: MapOptions.baseMaps[0].id, slider: MapOptions.zoomSlider.visible
            });


            LayerCtrl.addAllLayer();

            MyMap.mapEntity.on('load', function (evt) {
                if (typeof MapOptions.callBacks.initMap === 'function')
                    MapOptions.callBacks.initMap({ status: "success", result: null });

                if (MapOptions.zoomSlider.visible && MapOptions.zoomSlider.style != null && typeof MapOptions.zoomSlider.style === 'object')
                    $('#myMap_zoom_slider').css(MapOptions.zoomSlider.style);

                if (MapOptions.extent != null && typeof MapOptions.extent === 'object') {
                    var extent = new esri.geometry.Extent(MapOptions.extent.xmin, MapOptions.extent.ymin, MapOptions.extent.xmax, MapOptions.extent.ymax, MyMap.mapEntity.spatialReference);
                    MyMap.mapEntity.setExtent(extent);
                }
            });

            MyMap.mapEntity.on("layer-add", MyMap.layerAddEvent);
        }
        _this.showlocaltion = function () {
            if (!MapOptions.localtionLabel.visible)
                return;
            $("#" + MapOptions.name).append('<lable id="showlocaltion"></lable>');
            if (MapOptions.localtionLabel.style != null && typeof MapOptions.localtionLabel.style === 'object')
                $('#showlocaltion').css(MapOptions.localtionLabel.style);
            MyMap.mapEntity.on("mouse-move", function (evt) {
                if (typeof evt.mapPoint.x !== 'number') return;
                $("#showlocaltion").text('X:' + evt.mapPoint.x + ' , Y:' + evt.mapPoint.y);
            });
        }
        _this.overView = function () {
            var overview = new esri.dijit.OverviewMap({
                map: MyMap.mapEntity,
                attachTo: MapOptions.overView.attachTo,
                width: MapOptions.overView.width,
                height: MapOptions.overView.height,
                visible: MapOptions.overView.visible,
            });
            overview.startup();
        }
        _this.basemapToggle = function () {
            $("#" + MapOptions.name).append('<div id="basemaptoggle"></div>');

            MyMap.basemapToggle = new esri.dijit.BasemapToggle({
                map: MyMap.mapEntity,
                visible: MapOptions.toggleView.visible,
                basemap: MapOptions.baseMaps[1].id
            }, "basemaptoggle");
            MyMap.basemapToggle.startup();

            if (MapOptions.toggleView.style != null && typeof MapOptions.toggleView.style === 'object')
                $('#basemaptoggle').css(MapOptions.toggleView.style);

        }
        _this.scaleBar = function () {
            if (!MapOptions.scaleBar.visible) return;

            var scalebar = new esri.dijit.Scalebar({
                map: MyMap.mapEntity,
                attachTo: MapOptions.scaleBar.attachTo,
                scalebarUnit: MapOptions.scaleBar.scalebarUnit
            });
        }
        _this.searchbar = function () {
            if (!MapOptions.searchView.visible) return;
            $("#" + MapOptions.name).append('<div id="searchbar"></div>');
            MyMap.searchEntity = new esri.dijit.Search({
                map: MyMap.mapEntity,
                sources: [],
            }, "searchbar");

            if (MapOptions.searchView.style != null && typeof MapOptions.searchView.style === 'object')
                $('#searchbar').css(MapOptions.searchView.style);

            MyMap.searchEntity.startup();
        }
        _this.Compass = function () {
            if (!MapOptions.compass.visible) return;
            var compass = new Compass();
            compass.map = MyMap.mapEntity;
            compass.loadCompass();
            if (MapOptions.compass.style != null && typeof MapOptions.compass.style === 'object')
                $('#compasshousing').css(MapOptions.compass.style);
        }
    }

    var Compass = function () {
        var _this = this;
        _this.map;
        var COMPASS_SIZE = 125;
        var pt;
        var graphic;
        var watchId;
        var compassFaceRadius, compassFaceDiameter;
        var needleAngle, needleWidth, needleLength, compassRing;
        var renderingInterval = -1;
        var currentHeading;
        var hasCompass;
        var compassHousing;
        var containerX;
        var containerY;
        var compassNeedleContext;
        // The HTML5 geolocation API is used to get the user's current position.
        _this.mapLoadHandler = function () {
            dojo.on(window, 'resize', _this.map, _this.map.resize);
            // check if geolocaiton is supported
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(zoomToLocation, locationError);
                // retrieve update about the current geographic location of the device
                watchId = navigator.geolocation.watchPosition(showLocation, locationError);
            } else {
                alert("Browser doesn't support Geolocation. Visit http://caniuse.com to discover browser support for the Geolocation API.");
            }
        }

        function zoomToLocation(location) {
            pt = esri.geometry.geographicToWebMercator(new Point(location.coords.longitude, location.coords.latitude));
            addGraphic(pt);
            _this.map.centerAndZoom(pt, 17);
        }

        function showLocation(location) {
            pt = esri.geometry.geographicToWebMercator(new Point(location.coords.longitude, location.coords.latitude));
            if (!graphic) {
                addGraphic(pt);
            } else {
                //move the graphic if it already exists
                graphic.setGeometry(pt);
            }
            _this.map.centerAt(pt);
        }

        function locationError(error) {
            //error occurred so stop watchPosition
            if (navigator.geolocation) {
                navigator.geolocation.clearWatch(watchId);
            }
            switch (error.code) {
                case error.PERMISSION_DENIED:
                    alert("Location not provided");
                    break;

                case error.POSITION_UNAVAILABLE:
                    alert("Current location not available");
                    break;

                case error.TIMEOUT:
                    alert("Timeout");
                    break;

                default:
                    alert("unknown error");
                    break;
            }
        }

        // Add a pulsating graphic to the map
        function addGraphic(pt) {
            var symbol = new esri.symbol.SimpleMarkerSymbol(SimpleMarkerSymbol.STYLE_CIRCLE, 12, new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID, new Color([210, 105, 30, 0.5]), 8), new Color([210, 105, 30, 0.9]));
            graphic = new Graphic(pt, symbol);
            map.graphics.add(graphic);
        }

        _this.loadCompass = function () {

            $("#" + MapOptions.name).append('<article id="compasshousing"><canvas id="compassface"></canvas><canvas id="compassneedle"></canvas></article>');

            compassHousing = dojo.byId("compasshousing");
            // assign the compass housing dimensions
            compassHousing.style.height = compassHousing.style.width = COMPASS_SIZE + "px";
            // return the absolute position of the compass housing

            containerX = compassHousing.offsetLeft;
            containerY = compassHousing.offsetTop;

            currentHeading = 0;
            needleAngle = 0;
            if (!buildCompassFace()) {
                return;
            }
            drawCompassFace();
            drawCompassNeedle();
            //hasWebkit();
        }

        // Creates the diameter of the compass face
        // Creates the radius
        function buildCompassFace() {
            // compass housing diameter and radius
            compassFaceDiameter = COMPASS_SIZE;
            compassFaceRadius = compassFaceDiameter / 2;
            // needle length
            needleLength = compassFaceDiameter;
            // needle width
            needleWidth = needleLength / 10;
            // tick marks
            compassRing = compassFaceDiameter / 50;
            return true;
        }

        var compassFaceContext;
        // Draw the coppass face, text labels and font, and tick marks
        function drawCompassFace() {
            var compassFaceCanvas = dojo.byId("compassface");
            compassFaceCanvas.width = compassFaceCanvas.height = compassFaceDiameter;
            compassFaceContext = compassFaceCanvas.getContext("2d");
            compassFaceContext.clearRect(0, 0, compassFaceCanvas.width, compassFaceCanvas.height);

            // draw the tick marks and center the compass ring
            var xOffset, yOffset;
            xOffset = yOffset = compassFaceCanvas.width / 2;
            for (var i = 0; i < 360; ++i) {
                var x = (compassFaceRadius * Math.cos(degToRad(i))) + xOffset;
                var y = (compassFaceRadius * Math.sin(degToRad(i))) + yOffset;
                var x2 = ((compassFaceRadius - compassRing) * Math.cos(degToRad(i))) + xOffset;
                var y2 = ((compassFaceRadius - compassRing) * Math.sin(degToRad(i))) + yOffset;
                compassFaceContext.beginPath();
                compassFaceContext.moveTo(x, y);
                compassFaceContext.lineTo(x2, y2);
                compassFaceContext.closePath();
                compassFaceContext.stroke();
                i = i + 4;
            }

            // The measureText method returns an object, with one attribute: width.
            // The width attribute returns the width of the text, in pixels.
            compassFaceContext.font = "10px Arial";
            compassFaceContext.textAlign = "center";
            var metrics = compassFaceContext.measureText('N');
            compassFaceContext.fillText('N', compassFaceRadius, 15);
            compassFaceContext.fillText('S', compassFaceRadius, compassFaceDiameter - 10);
            compassFaceContext.fillText('E', (compassFaceRadius + (compassFaceRadius - metrics.width)), compassFaceRadius);
            compassFaceContext.fillText('W', 10, compassFaceRadius);
        }

        // Draw the compass needle
        function drawCompassNeedle() {
            var compassNeedle = dojo.byId("compassneedle");
            compassNeedle.width = compassNeedle.height = compassFaceDiameter;
            compassNeedle.style.left = Math.floor(compassFaceContext.width / 2) + "px";
            compassNeedle.style.top = Math.floor(compassFaceContext.height / 2) + "px";
            compassNeedleContext = compassNeedle.getContext("2d");
            compassNeedleContext.translate(compassFaceRadius, compassFaceRadius);
            compassNeedleContext.clearRect((compassNeedleContext.canvas.width / 2) * -1, (compassNeedleContext.canvas.height / 2) * -1, compassNeedleContext.canvas.width, compassNeedleContext.canvas.height);

            // The first step to create a path is calling the beginPath method. Internally, paths are stored as a list of sub-paths
            // (lines, arcs, etc) which together form a shape. Every time this method is called, the list is reset and we can start
            // drawing new shapes.

            // SOUTH
            compassNeedleContext.beginPath();
            compassNeedleContext.lineWidth = 1;
            compassNeedleContext.moveTo(0, 5);
            compassNeedleContext.lineTo(0, compassFaceRadius);
            compassNeedleContext.stroke();
            // circle around label
            compassNeedleContext.beginPath();
            compassNeedleContext.arc(0, compassFaceRadius - 15, 8, 0, 2 * Math.PI, false);
            compassNeedleContext.fillStyle = "#FFF";
            compassNeedleContext.fill();
            compassNeedleContext.lineWidth = 1;
            compassNeedleContext.strokeStyle = "black";
            compassNeedleContext.stroke();
            // S
            compassNeedleContext.beginPath();
            compassNeedleContext.moveTo(0, 0);
            compassNeedleContext.font = "normal 10px Verdana";
            compassNeedleContext.fillStyle = "#000";
            compassNeedleContext.textAlign = "center";
            compassNeedleContext.fillText("S", 0, compassFaceRadius - 10);
            // needle
            compassNeedleContext.beginPath();
            compassNeedleContext.fillStyle = "#000";
            compassNeedleContext.moveTo(0, 0);
            compassNeedleContext.lineTo(0, needleLength / 4);
            compassNeedleContext.lineTo((needleWidth / 4) * -1, 0);
            compassNeedleContext.fill();
            compassNeedleContext.beginPath();
            compassNeedleContext.fillStyle = "#000";
            compassNeedleContext.moveTo(0, 0);
            compassNeedleContext.lineTo(0, needleLength / 4);
            compassNeedleContext.lineTo(needleWidth / 4, 0);
            compassNeedleContext.fill();


            // NORTH
            compassNeedleContext.beginPath();
            compassNeedleContext.lineWidth = 1;
            compassNeedleContext.moveTo(0, 0);
            compassNeedleContext.lineTo(0, -compassFaceRadius);
            compassNeedleContext.stroke();
            // circle
            compassNeedleContext.beginPath();
            compassNeedleContext.arc(0, -(compassFaceRadius - 16), 8, 0, 2 * Math.PI, false);
            compassNeedleContext.fillStyle = "#FFF";
            compassNeedleContext.fill();
            compassNeedleContext.lineWidth = 1;
            compassNeedleContext.strokeStyle = "black";
            compassNeedleContext.stroke();
            // N
            compassNeedleContext.beginPath();
            compassNeedleContext.moveTo(0, 0);
            compassNeedleContext.font = "normal 10px Verdana";
            compassNeedleContext.fillStyle = "#000";
            compassNeedleContext.textAlign = "center";
            compassNeedleContext.fillText("N", 0, -(compassFaceRadius - 20));
            // needle
            compassNeedleContext.beginPath();
            compassNeedleContext.fillStyle = "#000";
            compassNeedleContext.moveTo(0, 0);
            compassNeedleContext.lineTo(0, (needleLength / 4) * -1);
            compassNeedleContext.lineTo((needleWidth / 4) * -1, 0);
            compassNeedleContext.fill();
            compassNeedleContext.beginPath();
            compassNeedleContext.fillStyle = "#000";
            compassNeedleContext.moveTo(0, 0);
            compassNeedleContext.lineTo(0, (needleLength / 4) * -1);
            compassNeedleContext.lineTo(needleWidth / 4, 0);
            compassNeedleContext.fill();

            // center pin color
            compassNeedleContext.beginPath();
            compassNeedleContext.arc(0, 0, 10, 0, 2 * Math.PI, false);
            compassNeedleContext.fillStyle = "rgb(255,255,255)";
            compassNeedleContext.fill();
            compassNeedleContext.lineWidth = 1;
            compassNeedleContext.strokeStyle = "black";
            compassNeedleContext.stroke();

            compassNeedleContext.beginPath();
            compassNeedleContext.moveTo(0, 0);
            compassNeedleContext.arc(0, 0, (needleWidth / 4), 0, degToRad(360), false);
            compassNeedleContext.fillStyle = "#000";
            compassNeedleContext.fill();
        }

        var orientationHandle;
        function orientationChangeHandler() {
            // An event handler for device orientation events sent to the window.
            orientationHandle = dojo.on(window, "deviceorientation", onDeviceOrientationChange);
            // The setInterval() method calls rotateNeedle at specified intervals (in milliseconds).
            renderingInterval = setInterval(rotateNeedle, 100);
        }

        var compassTestHandle;
        function hasWebkit() {
            if (dojo.has("ff") || dojo.has("ie") || dojo.has("opera")) {
                hasCompass = false;
                orientationChangeHandler();
                alert("Your browser does not support WebKit.");
            } else if (window.DeviceOrientationEvent) {
                compassTestHandle = dojo.on(window, "deviceorientation", hasGyroscope);
            } else {
                hasCompass = false;
                orientationChangeHandler();
            }
        }

        // Test if the device has a gyroscope.
        // Instances of the DeviceOrientationEvent class are fired only when the device has a gyroscope and while the user is changing the orientation.
        function hasGyroscope(event) {
            dojo.disconnect(compassTestHandle);
            if (event.webkitCompassHeading !== undefined || event.alpha != null) {
                hasCompass = true;
            } else {
                hasCompass = false;
            }
            orientationChangeHandler();
        }

        // Rotate the needle based on the device's current heading
        function rotateNeedle() {
            var multiplier = Math.floor(needleAngle / 360);
            var adjustedNeedleAngle = needleAngle - (360 * multiplier);
            var delta = currentHeading - adjustedNeedleAngle;
            if (Math.abs(delta) > 180) {
                if (delta < 0) {
                    delta += 360;
                } else {
                    delta -= 360;
                }
            }
            delta /= 5;
            needleAngle = needleAngle + delta;
            var updatedAngle = needleAngle - window.orientation;
            // rotate the needle
            dojo.byId("compassneedle").style.webkitTransform = "rotate(" + updatedAngle + "deg)";
        }

        function onDeviceOrientationChange(event) {
            var accuracy;
            if (event.webkitCompassHeading !== undefined) {
                // Direction values are measured in degrees starting at due north and continuing clockwise around the compass.
                // Thus, north is 0 degrees, east is 90 degrees, south is 180 degrees, and so on. A negative value indicates an invalid direction.
                currentHeading = (360 - event.webkitCompassHeading);
                accuracy = event.webkitCompassAccuracy;
            } else if (event.alpha != null) {
                // alpha returns the rotation of the device around the Z axis; that is, the number of degrees by which the device is being twisted
                // around the center of the screen
                // (support for android)
                currentHeading = (270 - event.alpha) * -1;
                accuracy = event.webkitCompassAccuracy;
            }

            if (accuracy < 11) {
                compassNeedleContext.fillStyle = "rgba(0, 205, 0, 0.9)";
            } else if (accuracy >= 15 && accuracy < 25) {
                compassNeedleContext.fillStyle = "rgba(255, 255, 0, 0.9)";
            } else if (accuracy > 24) {
                compassNeedleContext.fillStyle = "rgba(255, 0, 0, 0.9)";
            }
            compassNeedleContext.fill();

            if (renderingInterval == -1) {
                rotateNeedle();
            }
        }

        // Convert degrees to radians
        function degToRad(deg) {
            return (deg * Math.PI) / 180;
        }

        // Handle portrait and landscape mode orientation changes
        function orientationChanged() {
            if (map) {
                map.reposition();
                map.resize();
            }
        }
    };

    var CommonHelper = new function () {
        var _this = this;
        _this.markerSymbol = null;
        _this.lineSymbol = null;
        _this.fillSymbol = null;

        _this.initDrag = function (params) {
            new dojo.dnd.Moveable(params, { handle: "dragme" });
        };

        _this.initSymbol = function () {
            _this.markerSymbol = new esri.symbol.SimpleMarkerSymbol(esri.symbol.SimpleMarkerSymbol.STYLE_CIRCLE, 15, new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([0, 0, 0]), 1), new dojo.Color([255, 0, 0, 5]));
            _this.lineSymbol = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 6);
            _this.fillSymbol = new esri.symbol.SimpleFillSymbol(esri.symbol.SimpleFillSymbol.STYLE_SOLID, new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 2), new dojo.Color([255, 255, 0, 0.25]));

            if (MapOptions.highLightSymbol == null || typeof MapOptions.highLightSymbol !== 'object') return;

            for (var key in MapOptions.highLightSymbol) {
                var value = MapOptions.highLightSymbol[key];
                if (value == null) continue;
                switch (key) {
                    case 'pointSymbol':
                        _this.markerSymbol = _this.getSymbol(value);
                        break;
                    case 'polylineSymbol':
                        _this.lineSymbol = _this.getSymbol(value);
                        break;
                    case 'polygonSymbol':
                        _this.fillSymbol = _this.getSymbol(value);
                        break;
                }
            }

        }

        _this.getSymbol = function (params) {
            var symbol = null;
            switch (params.type) {
                case 'esriSLS':
                    symbol = new esri.symbol.SimpleLineSymbol(params);
                    break;
                case 'esriSMS':
                    symbol = new esri.symbol.SimpleMarkerSymbol(params);
                    break;
                case 'esriSFS':
                    symbol = new esri.symbol.SimpleFillSymbol(params);
                    break;
                case 'esriPMS':
                    symbol = new esri.symbol.PictureMarkerSymbol(params);
                    break;
                case 'esriPFS':
                    symbol = new esri.symbol.PictureFillSymbol(params);
                    break;
                case 'esriTS':
                    symbol = new esri.symbol.TextSymbol(params);
                    break;
                default:
                    break;
            }
            return symbol;
        }

        _this.getRender = function (json) {
            //var json = {type: renderer类型,config:{rendererJson}}
            var render = null;
            switch (json.type) {
                case 'classBreaks':
                    render = new esri.renderer.ClassBreakRenderer(json);
                    break;
                case 'simple':
                    render = new esri.renderer.SimpleRenderer(json);
                    break;
                case 'uniqueValue':
                    render = new esri.renderer.UniqueValueRenderer(json);
                    break;
                case 'dotdensity':
                    render = new esri.renderer.DotDensityRenderer(json.config);
                    break;
                case 'heatmap':
                    render = new esri.renderer.HeatmapRenderer(json.config);
                    break;
            }
            return render;
        }

        _this.getInfoTemplate = function (layerInfo) {
            if (layerInfo.infoTemplate != null && typeof layerInfo.infoTemplate === "object") {
                return new esri.InfoTemplate(layerInfo.infoTemplate);
            }

            var showFields = layerInfo.showFields;
            var context = "";
            $.each(showFields, function (i, item) {
                context += item.label + " : ${" + item.fieldName + "}<br/>";
            });
            if (context.length == 0) {
                var fields = layerInfo.fields;
                $.each(fields, function (i, item) {
                    context += item.label + " : ${" + item.fieldName + "}<br/>";
                });
            }
            return new esri.InfoTemplate("属性", context);

        }

        _this.getDefSymbol = function (layerInfo) {
            var symbol = null;
            switch (layerInfo.entity.geometryType) {
                case 'esriGeometryPoint':
                    symbol = _this.markerSymbol;
                    break;
                case 'esriGeometryPolyline':
                    symbol = _this.lineSymbol;
                    break;
                case 'esriGeometryPolygon':
                    symbol = _this.fillSymbol;
                    break;
            }
            return symbol;
        }

        _this.guidGenerator = function () {
            var S4 = function () {
                return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
            };
            return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
        }

        _this.graphicClone = function (graphic) {
            var grap = new esri.Graphic();
            grap.setAttributes(graphic.attributes);
            var geometry = graphic.geometry.toJson();
            var geometry2 = esri.geometry.fromJson(geometry);
            grap.setGeometry(geometry2);
            return grap;
        }

        _this.getQuery = function (where, geometry) {
            var query = new esri.tasks.Query();
            if (where != null) query.where = where;
            if (geometry != null) query.geometry = geometry;
            query.outSpatialReference = MyMap.mapEntity.spatialReference;
            query.returnGeometry = true;
            query.outFields = ["*"];
            return query;
        }

        _this.whereInIds = function (key, ids) {
            var sqlStr = key + ' IN ( ';
            for (var i = 0; i < ids.length; i++) {
                if (i > 0)
                    sqlStr += " , '" + ids[i] + "'";
                else
                    sqlStr += "'" + ids[i] + "'";
            }
            return sqlStr + ' )';
        }

        _this.setBtnEnble = function ($btn, isEnble) {
            if (isEnble) {
                $btn.removeAttr("disabled");
                $btn.removeClass("not");
            } else {
                $btn.attr("disabled", true);
                $btn.addClass("not");
            }
        }
    }

    var ToolBar = new function () {
        var _this = this;
        _this.navEntity = null;
        _this.actionType = null;
        _this.geometryService = null;

        _this.init = function () {
            _this.navEntity = new esri.toolbars.Navigation(MyMap.mapEntity);
            _this.geometryService = new esri.tasks.GeometryService(MapOptions.geometryServiceUrl);
            esriConfig.defaults.geometryService = _this.geometryService;

            if (!MapOptions.toolBar.visible) return;
            var tools = MapOptions.toolBar.tools;
            if (tools == null || typeof tools !== 'object') return;

            $("#" + MapOptions.name).append('<div class="toolsbar"></div>');

            if (MapOptions.toolBar.style != null && typeof MapOptions.toolBar.style === 'object')
                $("#" + MapOptions.name).children(".toolsbar").css(MapOptions.toolBar.style);

            for (var i = 0; i < tools.length; i++) {
                if (typeof tools[i] === 'object') {
                    addToolBtnGroup(tools[i]);
                } else {
                    addToolBtn(tools[i]);
                }
            }
        }
        _this.initDialog = function () {
            var tools = ['layerCtrl', 'layerEdit', 'select', 'measure'];
            for (var i = 0; i < tools.length; i++) {
                if ($("#" + tools[i]).length > 0) continue;
                switch (tools[i]) {
                    case 'layerCtrl':
                        LayerCtrl.initDialog(true);
                        break;
                    case 'layerEdit':
                        LayerEdit.initDialog(true);
                        break;
                    case 'select':
                        Select.initDialog(true);
                        break;
                    case 'measure':
                        Measure.initDialog(true);
                        break;
                    default:
                }
            }
        }

        _this.initTool = function ($this) {
            if ($($this).parent().parent().is("section")) {
                var $section = $($this).parent().parent("section").parent().siblings().children("section");
                $section.siblings("a").removeClass("on");
                $section.siblings("i").removeClass("on");
                $section.hide();
                $($this).parent().parent().siblings("i").removeClass("on");
                $($this).parent().siblings().children("i").removeClass("on");
            } else {
                var $section = $($this).parent().siblings().children("section");
                $section.siblings("a").removeClass("on");
                $section.siblings("i").removeClass("on");
                $section.hide();
            }
            _this.deactivateLastAction();
        }

        _this.deactivateLastAction = function () {
            switch (_this.actionType) {
                case 'select':
                    Select.endSelect();
                    break;
                case 'measure':
                    Measure.endMeasure();
                    break;
                case 'zoomIn':
                    _this.navEntity.deactivate();
                    break;
                case 'zoomOut':
                    _this.navEntity.deactivate();
                    break;
                case 'layerEdit':
                    LayerEdit.endEdit();
                    break;
                default:
            }
            MyMap.clearResult();
        }

        _this.zoomIn = function () {
            if ($("#" + MapOptions.name).children('.toolsbar').find('div.zoomIn').length > 0) return;
            _this.deactivateLastAction();
            if (_this.actionType == "zoomIn") {
                _this.actionType = null;
            } else {
                _this.actionType = "zoomIn";
                _this.navEntity.activate(esri.toolbars.Navigation.ZOOM_IN);
            }
        }
        _this.zoomOut = function () {
            if ($("#" + MapOptions.name).children('.toolsbar').find('div.zoomOut').length > 0) return;
            _this.deactivateLastAction();
            if (_this.actionType == "zoomOut") {
                _this.actionType = null;
            } else {
                _this.actionType = "zoomOut";
                _this.navEntity.activate(esri.toolbars.Navigation.ZOOM_OUT);
            }
        }
        _this.pan = function () {
            _this.deactivateLastAction();
            _this.actionType = null;
            _this.navEntity.activate(esri.toolbars.Navigation.PAN);
        }
        _this.zoomFull = function () {
            _this.deactivateLastAction();
            _this.actionType = null;
            if (MapOptions.extent != null && typeof MapOptions.extent === 'object') {
                var extent = new esri.geometry.Extent(MapOptions.extent.xmin, MapOptions.extent.ymin, MapOptions.extent.xmax, MapOptions.extent.ymax, MyMap.mapEntity.spatialReference);
                MyMap.mapEntity.setExtent(extent);
            } else {
                _this.navEntity.zoomToFullExtent();
            }
        }

        function zoomIn() {
            if ($(this).hasClass("on")) {
                _this.navEntity.deactivate();
                _this.actionType = null;
            } else {
                _this.initTool(this);
                _this.actionType = "zoomIn";
                _this.navEntity.activate(esri.toolbars.Navigation.ZOOM_IN);
            }
            $(this).toggleClass('on');
        }
        function zoomOut() {
            if ($(this).hasClass("on")) {
                _this.navEntity.deactivate();
                _this.actionType = null;
            } else {
                _this.initTool(this);
                _this.actionType = "zoomOut";
                _this.navEntity.activate(esri.toolbars.Navigation.ZOOM_OUT);
            }
            $(this).toggleClass('on');
        }
        function pan() {
            _this.initTool(this);
            _this.actionType = null;
            _this.navEntity.activate(esri.toolbars.Navigation.PAN);
        }
        function zoomFull() {
            _this.initTool(this);
            _this.actionType = null;
            if (MapOptions.extent != null && typeof MapOptions.extent === 'object') {
                var extent = new esri.geometry.Extent(MapOptions.extent.xmin, MapOptions.extent.ymin, MapOptions.extent.xmax, MapOptions.extent.ymax, MyMap.mapEntity.spatialReference);
                MyMap.mapEntity.setExtent(extent);
            } else {
                _this.navEntity.zoomToFullExtent();
            }
        }

        function addToolBtn(btnName) {
            var btninfo = configs.tools[btnName];
            if (typeof btninfo !== 'object') return;
            var $toolbar = $("#" + MapOptions.name).children('.toolsbar');
            var btnhtml = configs.btnHtml.format({ className: btninfo.name, BtnTitle: btninfo.toolTip });
            $toolbar.append(btnhtml);
            var $toolbtn = $toolbar.children("." + btninfo.name);
            $toolbtn.children("i").on("click", btninfo.onClick);
            if (typeof btninfo.dialogBox === 'function')
                btninfo.dialogBox();
        }
        function addToolBtnGroup(BtnGroup) {
            var $btngroup = null;
            for (var i = 0; i < BtnGroup.length; i++) {
                var btninfo = configs.tools[BtnGroup[i]];
                if (typeof btninfo !== 'object') continue;
                var btnhtml = configs.btnHtml.format({ className: btninfo.name, BtnTitle: btninfo.toolTip });
                if ($btngroup != null) {
                    $btngroup.append(btnhtml);
                    $btngroup.children('.' + btninfo.name).children('i').on("click", btninfo.onClick);
                } else {
                    var $toolbar = $("#" + MapOptions.name).children('.toolsbar');
                    $toolbar.append(btnhtml);
                    var $toolbtn = $toolbar.children('.' + btninfo.name);
                    $toolbtn.children('i').on("click", btninfo.onClick);
                    var btngrouphtml = configs.btnGroupHtml.format({ id: btninfo.name });
                    $toolbtn.append(btngrouphtml);
                    $btngroup = $("#" + btninfo.name);
                    $btngroup.siblings("a.u-select").on("click", function () {
                        if (!$(this).hasClass('on')) {
                            $(this).siblings('section').find('i').removeClass('on');
                        }
                        $(this).toggleClass('on');
                        $btngroup.toggle();
                    });
                }
            }
        }

        var configs = {
            btnHtml: '<div class="u-nav {className}"><i title="{BtnTitle}"></i></div>',
            btnGroupHtml: '<a class="u-select"></a><section id={id} class="btnGroup"></section>',
            tools: {
                layerCtrl: { name: 'layerCtrl', toolTip: '图层控制', onClick: function () { LayerCtrl.onClick(this); }, dialogBox: function () { LayerCtrl.initDialog(); } },
                layerEdit: { name: 'layerEdit', toolTip: '编辑', onClick: function () { LayerEdit.onClick(this); }, dialogBox: function () { LayerEdit.initDialog(); } },
                select: { name: 'select', toolTip: '选择', onClick: function () { Select.onClick(this); }, dialogBox: function () { Select.initDialog(); } },
                measure: { name: 'measure', toolTip: '量距', onClick: function () { Measure.onClick(this); }, dialogBox: function () { Measure.initDialog(); } },
                saveConfig: { name: 'saveConfig', toolTip: '保存配置', onClick: function () { ConfigInfo.onClick(this) }, dialogBox: function () { ConfigInfo.initDialog(); } },
                zoomFull: { name: 'zoomFull', toolTip: '全屏', onClick: zoomFull },
                zoomIn: { name: 'zoomIn', toolTip: '放大', onClick: zoomIn },
                zoomOut: { name: 'zoomOut', toolTip: '缩小', onClick: zoomOut },
                pan: { name: 'pan', toolTip: '漫游', onClick: pan }
            }
        };
    }

    var LayerCtrl = new function () {
        var _this = this;
        _this.curLayerNum;
        var curlayerName = null;
        _this.initDialog = function (isCustom) {
            var $toolbtn = null;
            if (isCustom) {
                $toolbtn = $("#" + MapOptions.name);
            } else {
                $toolbtn = $("#" + MapOptions.name).children('.toolsbar').children(".layerCtrl");
                $toolbtn.prepend('<label></label>');
            }
            var html = '<section id="layerCtrl" class="toRight"><div class="u-title">图层控制<span class="u-close">x</span></div><ul></ul></section>';
            $toolbtn.append(html);

            $('#layerCtrl').find('.u-close').on('click', function () {
                $("#layerCtrl").hide();
                if (!isCustom)
                    $("#layerCtrl").siblings("i").removeClass("on");
                ToolBar.actionType = null;
            });
            CommonHelper.initDrag("layerCtrl");

            if (MapOptions.layerList.length > 0)
                LayerCtrl.updateList();
        }
        _this.onClick = function (_this) {
            if ($(_this).hasClass("on")) {
                ToolBar.actionType = null;
            } else {
                ToolBar.initTool(_this);
                ToolBar.actionType = "layerCtrl";
            }
            $("#layerCtrl").toggle();
            $(_this).toggleClass('on');
        }
        _this.onCustomClick = function () {
            if (!$("#layerCtrl").parent().hasClass('skyseamap')) return;
            if ($("#layerCtrl").is(":visible")) {
                ToolBar.actionType = null;
            } else {
                ToolBar.deactivateLastAction();
                ToolBar.actionType = "layerCtrl";
                $("#layerCtrl").siblings('section').hide();
            }
            $("#layerCtrl").toggle();
        }
        _this.updateList = function () {
            var $ul = $("#layerCtrl").children('ul');
            $ul.empty();
            var li_Model = '<li name="mapLegend_{num}" class="{isOn}"><input type="checkbox" {checked}/><span>{title}</span><div class="u-btn">{idel}<i class="up" title="上移"></i><i class="down" title="下移"></i><i class="opacity" title="透明度"></i></div></li>';
            var n = 1;
            $.each(MapOptions.layerList, function (i, item) {
                var isOn = false;
                if (curlayerName != null && curlayerName == item.title) isOn = true;
                var li = li_Model.format({ "num": i, "isOn": isOn ? 'on' : '', "checked": item.isVisible ? "checked" : "", idel: MapOptions.isEnbleDelLayer ? '<i class="del" title="删除"></i>' : '', title: item.title });
                $ul.prepend(li);
                MyMap.mapEntity.reorderLayer(item.entity, n);
                n++;
            });

            $ul.children('li').children('span').on('click', clickEvent);
            $ul.children('li').children('div').children("i").on("click", btnClickEvent);
            $ul.children('li').children("input[type='checkbox']").on("change", checkEvent);
            $ul.children('li').on("mouseover", function () {
                $(this).children("div").show();
            });
            $ul.children('li').on("mouseout", function () {
                $(this).children("div").hide();
            });
            if (curlayerName == null)
                setTimeout(function () {
                    $ul.children('li').children('span').eq(0).click();
                }, 1000);
        }
        _this.addAllLayer = function () {
            $.each(MapOptions.layerList, function (i, item) {
                _this.addLayer(item);
            });
        }
        _this.addLayer = function (param) {
            switch (param.type) {
                case "TiledMapService":
                    param.entity = new esri.layers.ArcGISTiledMapServiceLayer(param.url, { id: param.id });
                    break;
                case "FeatureServer":
                    param.entity = new esri.layers.FeatureLayer(param.url, {
                        id: param.id,
                        name: param.title,
                        mode: esri.layers.FeatureLayer.MODE_ONDEMAND,
                        outFields: ["*"]
                    });
                    if (param.renderer != null && typeof param.renderer === 'object') {
                        var render = CommonHelper.getRender(param.renderer);
                        if (render != null) param.entity.setRenderer(render);
                    }
                    if (param.labelingInfo != null && typeof param.labelingInfo === 'object') {
                        param.entity.setLabelingInfo(param.labelingInfo);
                    }
                    break;
                case "DynamicMapService":
                    param.entity = new esri.layers.ArcGISDynamicMapServiceLayer(param.url, { id: param.id });
                    break;
                default:
                    return;
            }
            MyMap.mapEntity.addLayer(param.entity);

            if (typeof param.opacity === "number" && param.opacity < 100)
                param.entity.setOpacity(param.opacity / 100);

            if (typeof param.isVisible === "boolean" && !param.isVisible)
                param.entity.setVisibility(param.isVisible);

        }
        _this.getFeatureLayerById = function (id) {
            var layer = null;
            $.each(MapOptions.layerList, function (i, item) {
                if (id == item.id && item.type == 'FeatureServer') {
                    layer = item;
                    return false;
                }
            });
            return layer;
        }
        _this.curFeatureLayer = function () {
            var layerInfo = MapOptions.layerList[_this.curLayerNum];
            if (layerInfo == null) return null;
            return layerInfo.type == 'FeatureServer' ? layerInfo : null;
        }

        _this.initCurrentlayer = function () {

            var layerInfo = _this.curFeatureLayer();

            CommonHelper.setBtnEnble($("#layerEdit").siblings("i"), layerInfo != null);
            CommonHelper.setBtnEnble($("#select").siblings("i"), layerInfo != null);

            if (!MapOptions.searchView.visible) return;
            var sources = [];
            if (layerInfo != null && layerInfo.isQuery) {
                var infoTemplate = CommonHelper.getInfoTemplate(layerInfo);
                sources.push({
                    featureLayer: layerInfo.entity,
                    outFields: ["*"],
                    displayField: layerInfo.queryFields[0],
                    placeholder: layerInfo.title,
                    enableSuggestions: true,
                    highlightSymbol: CommonHelper.getDefSymbol(layerInfo),
                    infoTemplate: infoTemplate
                });
            }
            //Set the sources above to the search widget
            MyMap.searchEntity.set("sources", sources);
        }

        function clickEvent() {
            $(this).parent().addClass('on');
            $(this).parent().siblings().removeClass('on');

            var arr = $(this).parents("li").attr("name").split("_");
            _this.curLayerNum = parseInt(arr[1]);
            curlayerName = MapOptions.layerList[_this.curLayerNum].title;
            $('#layerCtrl').siblings('label').text(curlayerName);
            $('#layerCtrl').siblings('label').attr('title', curlayerName);

            _this.initCurrentlayer();
        }
        function btnClickEvent() {
            var arr = $(this).parents("li").attr("name").split("_");
            var num = parseInt(arr[1]);
            var type = $(this).attr("class");
            switch (type) {
                case "opacity":
                    var opacity = MapOptions.layerList[num].opacity;
                    if (typeof opacity !== "number") opacity = 100;
                    opacity -= 15;
                    if (opacity <= 0) opacity += 100;

                    MapOptions.layerList[num].entity.setOpacity(opacity / 100);
                    MapOptions.layerList[num].opacity = opacity;
                    $(this).attr("title", "透明度:" + opacity);
                    break;
                case "del":
                    MyMap.mapEntity.removeLayer(MapOptions.layerList[num].entity);
                    MapOptions.layerList.splice(num, 1);
                    _this.updateList();
                    break;
                case "up":
                    if (MapOptions.layerList.length - 1 <= num) return;
                    var layerdata = MapOptions.layerList[num];
                    MapOptions.layerList.splice(num + 2, 0, layerdata);
                    MapOptions.layerList.splice(num, 1);
                    var n = 1;
                    $.each(MapOptions.layerList, function (i, item) {
                        MyMap.mapEntity.reorderLayer(item.entity, n);
                        n++;
                    });
                    _this.updateList();
                    break;
                case "down":
                    if (num <= 0) return;
                    var layerdata = MapOptions.layerList[num];
                    MapOptions.layerList.splice(num, 1);
                    MapOptions.layerList.splice(num - 1, 0, layerdata);
                    var n = 1;
                    $.each(MapOptions.layerList, function (i, item) {
                        MyMap.mapEntity.reorderLayer(item.entity, n);
                        n++;
                    });
                    _this.updateList();
                default:
            }
        }
        function checkEvent() {
            var isChecked = this.checked;
            var arr = $(this).parents("li").attr("name").split("_");
            var num = parseInt(arr[1]);
            MapOptions.layerList[num].isVisible = isChecked;
            MapOptions.layerList[num].entity.setVisibility(isChecked);
        }
    }

    var LayerEdit = new function () {
        var _this = this;
        var templatePicker = null;
        var graphicsLayer = null;
        var curlayerInfo = null;
        var undoManager = null;
        var drawEntity = null;
        var editEntity = null;
        var actionType = null;
        var graphiclist = [];
        var checkedGraphiclist = [];

        _this.initDialog = function (isCustom) {
            drawEntity = new esri.toolbars.Draw(MyMap.mapEntity);
            editEntity = new esri.toolbars.Edit(MyMap.mapEntity);
            drawEntity.on("draw-end", drawEnd);

            undoManager = new UndoManager();
            var $toolbtn = null;
            if (isCustom)
                $toolbtn = $("#" + MapOptions.name);
            else
                $toolbtn = $("#" + MapOptions.name).children('.toolsbar').children(".layerEdit");

            var headHtml = '<section id="layerEdit" class="toRight"><div class="u-title">要素编辑<span class="u-close">x</span></div></section>';
            $toolbtn.append(headHtml);

            initTab();
            initTabTagPanel();
            initTabEditPanel();
            initTabDelPanel();
            initBottomPanel();

            $('#layerEdit').find('.u-close').on('click', function () {
                $("#layerEdit").hide();
                if (!isCustom)
                    $("#layerEdit").siblings("i").removeClass("on");
                _this.endEdit();
                ToolBar.actionType = null;
            });

            CommonHelper.initDrag("layerEdit");
        }
        _this.onClick = function ($this) {
            if ($($this).hasClass("on")) {
                _this.endEdit();
                ToolBar.actionType = null;
            } else {
                ToolBar.initTool($this);
                ToolBar.actionType = "layerEdit";
                _this.startEdit();
            }
            $("#layerEdit").toggle();
            $($this).toggleClass('on');
        }
        _this.onCustomClick = function () {
            if (LayerCtrl.curFeatureLayer() == null) return;
            if (!$("#layerEdit").parent().hasClass('skyseamap')) return;
            ToolBar.deactivateLastAction();
            if ($("#layerEdit").is(":visible")) {
                ToolBar.actionType = null;
            } else {
                ToolBar.actionType = "layerEdit";
                _this.startEdit();
                $("#layerEdit").siblings('section').hide();
            }
            $("#layerEdit").toggle();
        }

        _this.startEdit = function () {
            curlayerInfo = LayerCtrl.curFeatureLayer();
            templatePicker.attr("featureLayers", curlayerInfo == null ? [] : [curlayerInfo.entity]);
            templatePicker.update();

            $("#layerEdit").children('div.tabContainer').children().removeClass("on");
            $("#layerEdit").children('div.tabPanel').children().hide();

            if (curlayerInfo == null) return;

            if (graphicsLayer == null) {
                graphicsLayer = new esri.layers.GraphicsLayer();
                MyMap.mapEntity.addLayer(graphicsLayer);
                graphicsLayer.on('click', layerClickEvent);
                MyMap.mapEntity.on('click', mapClickEvent);
            } else {
                graphicsLayer.clear();
            }
            graphicsLayer.setOpacity(curlayerInfo.opacity / 100);

            $('#layerEdit').children('.tabContainer').children('span').each(function () {
                var type = $(this).attr('name');
                switch (type) {
                    case 'tabTag':
                        CommonHelper.setBtnEnble($(this), curlayerInfo.isAdd);
                        break;
                    case 'tabEdit':
                        CommonHelper.setBtnEnble($(this), curlayerInfo.isEdit);
                        break;
                    case 'tabDel':
                        CommonHelper.setBtnEnble($(this), curlayerInfo.isDel);
                        break;
                    default:

                }
            });

            graphicsLayer.setRenderer(curlayerInfo.entity.renderer);
            graphicsLayer.setVisibility(true);
            curlayerInfo.entity.setVisibility(false);
            var graphics = curlayerInfo.entity.graphics;
            for (var i = 0; i < graphics.length; i++) {
                var grap = new esri.Graphic();
                grap.setAttributes(graphics[i].attributes);
                grap.setGeometry(graphics[i].geometry);
                graphicsLayer.add(grap);
            }
        }
        _this.endEdit = function () {
            if (undoManager.getPosition() >= 0)
                if (confirm('是否保存要素编辑！'))
                    _this.save();
            graphicsLayer.clear();
            graphicsLayer.setVisibility(false);
            undoManager.clear();
            MyMap.mapEntity.graphics.clear();
            if (curlayerInfo != null)
                curlayerInfo.entity.setVisibility(curlayerInfo.isVisible);
            drawEntity.deactivate();
            editEntity.deactivate();
            templatePicker.clearSelection();
            actionType = null;
        }

        _this.save = function () {
            if (undoManager.getPosition() < 0) return;

            var graphicList = [];
            for (var i = 0; i <= undoManager.getPosition() ; i++) {
                var operation = undoManager.operationList[i];
                graphicList.push(operation);
            }

            var isSave = true;
            if (typeof MapOptions.callBacks.beforeSaveEdit === 'function')
                isSave = MapOptions.callBacks.beforeSaveEdit({ status: 'success', result: graphicList });
            if (!isSave) {
                if (typeof MapOptions.callBacks.afterSaveEdit === 'function')
                    MapOptions.callBacks.afterSaveEdit({ status: 'error', result: '取消保存！' });
                return;
            }

            var addGraphic = [];
            var updatesGraphic = [];
            var deletesGraphic = [];
            for (var i = 0; i < graphicList.length ; i++) {
                var operation = graphicList[i];
                switch (operation.action) {
                    case "add":
                        var grap = CommonHelper.graphicClone(operation.newGraphics[0]);
                        addGraphic.push(grap);
                        break;
                    case "update":
                        var grap = CommonHelper.graphicClone(operation.newGraphics[0]);
                        if (!opData(addGraphic, 'update', grap))
                            updatesGraphic.push(grap);
                        break;
                    case "delete":
                        for (var i = 0; i < operation.oldGraphics.length; i++) {
                            var grap = CommonHelper.graphicClone(operation.oldGraphics[i]);
                            if (!opData(addGraphic, 'delete', grap))
                                deletesGraphic.push(grap);
                        }
                        break;
                    case "cut":
                        for (var i = 0; i < operation.newGraphics.length; i++) {
                            var grap = CommonHelper.graphicClone(operation.newGraphics[i]);
                            if (i > 0) {
                                addGraphic.push(grap);
                            } else {
                                if (!opData(addGraphic, 'update', grap))
                                    updatesGraphic.push(grap);
                            }
                        }
                        break;
                    case "union":
                        var grap = CommonHelper.graphicClone(operation.newGraphics[0]);
                        if (!opData(addGraphic, 'update', grap))
                            updatesGraphic.push(grap);

                        for (var i = 1; i < operation.oldGraphics.length; i++) {
                            var grap = CommonHelper.graphicClone(operation.oldGraphics[i]);
                            if (!opData(addGraphic, 'delete', grap))
                                deletesGraphic.push(grap);
                        }
                        break;
                }
            }
            curlayerInfo.entity.applyEdits(addGraphic, updatesGraphic, deletesGraphic, function (result) {
                undoManager.clear();
                if (typeof MapOptions.callBacks.afterSaveEdit === 'function')
                    MapOptions.callBacks.afterSaveEdit({ status: 'success', result: graphicList });
            }, function (result) {
                if (typeof MapOptions.callBacks.afterSaveEdit === 'function')
                    MapOptions.callBacks.afterSaveEdit({ status: 'error', result: result });
            });
        }
        function opData(addGraphic, opType, grap) {
            var key = curlayerInfo.idFieldName;
            var index = -1;
            for (var i = 0; i < addGraphic.length; i++) {
                if (addGraphic[i].attributes[key] == grap.attributes[key]) {
                    index = i;
                    break;
                }
            }

            if (index >= 0) {
                switch (opType) {
                    case "update":
                        addGraphic[index] = grap;
                        break;
                    case "delete":
                        addGraphic.splice(index, 1);
                        break;
                }
            }
            return index >= 0;
        }

        _this.cancel = function () {
            if (confirm('确定是否取消保存！')) {
                while (undoManager.getPosition() >= 0)
                    undoManager.undo();
                undoManager.clear();
            } else {
                _this.save();
            }
        }
        _this.del = function () {
            if (checkedGraphiclist.length == 0) return;
            var operation = { action: "delete", graphicsLayer: graphicsLayer, oldGraphics: checkedGraphiclist, newGraphics: [] };
            undoManager.add(operation);

            clearChecked();
            var $delTool = $('#layerEdit').children('.tabPanel').children('.delTool');
            CommonHelper.setBtnEnble($delTool.children('i'), false);
            $delTool.children('span').text(0);
        }
        _this.tag = function (evt) {
            var grap = new esri.Graphic();
            grap.setGeometry(evt.geometry);
            var attr = {};
            for (var i = 0; i < curlayerInfo.entity.fields.length; i++) {
                var field = curlayerInfo.entity.fields[i];
                attr[field.name] = null;
            }
            $.each(curlayerInfo.initFields, function (i, item) {
                if (item.isDefValue) {
                    attr[item.fieldName] = item.Value;
                } else {
                    if (item.Value == "id") {
                        attr[item.fieldName] = "{" + CommonHelper.guidGenerator() + "}";
                    } else if (item.Value == "esriFieldTypeDate") {
                        attr[item.fieldName] = new Date().getTime();
                    }
                }
            });

            if (typeof MapOptions.callBacks.beforeTag === 'function')
                MapOptions.callBacks.beforeTag(attr);

            grap.setAttributes(attr);
            var operation = { action: "add", graphicsLayer: graphicsLayer, oldGraphics: [], newGraphics: [grap] };
            undoManager.add(operation);

        }
        _this.cut = function (evt) {
            ToolBar.geometryService.cut([checkedGraphiclist[0].geometry], evt.geometry, function (result) {
                var operation = { action: "cut", graphicsLayer: graphicsLayer, oldGraphics: checkedGraphiclist, newGraphics: [] };
                var graphic = CommonHelper.graphicClone(checkedGraphiclist[0]);
                graphic.setGeometry(result.geometries[0]);
                operation.newGraphics.push(graphic);
                for (var i = 1; i < result.geometries.length; i++) {
                    var grap = new esri.Graphic();
                    grap.setGeometry(result.geometries[i]);
                    var attr = {};
                    for (var i = 0; i < curlayerInfo.entity.fields.length; i++) {
                        var field = curlayerInfo.entity.fields[i];
                        attr[field.name] = null;
                    }
                    $.each(curlayerInfo.initFields, function (i, item) {
                        if (item.isDefValue) {
                            attr[item.fieldName] = item.Value;
                        } else {
                            if (item.Value == "id") {
                                attr[item.fieldName] = "{" + CommonHelper.guidGenerator() + "}";
                            } else if (item.Value == "esriFieldTypeDate") {
                                attr[item.fieldName] = new Date().getTime();
                            }
                        }
                    });
                    grap.setAttributes(attr);
                    operation.newGraphics.push(grap);
                }
                undoManager.add(operation);

                clearChecked();
                actionType = 'editTool';
                setBtnState();
            });
        }
        _this.merge = function () {
            var geometries = [];
            for (var i = 0; i < checkedGraphiclist.length; i++) {
                geometries.push(checkedGraphiclist[i].geometry);
            }

            ToolBar.geometryService.union(geometries, function (result) {
                var operation = { action: "union", graphicsLayer: graphicsLayer, oldGraphics: graphiclist, newGraphics: [] };
                var graphic = CommonHelper.graphicClone(checkedGraphiclist[0]);
                graphic.setGeometry(result);
                operation.newGraphics.push(graphic);
                undoManager.add(operation);

                clearChecked();
                actionType = 'editTool';
                setBtnState();
            });
        }
        _this.borehole = function (evt) {
            ToolBar.geometryService.difference([checkedGraphiclist[0].geometry], evt.geometry, function (result) {
                var operation = { action: "update", graphicsLayer: graphicsLayer, oldGraphics: checkedGraphiclist, newGraphics: [] };
                var graphic = CommonHelper.graphicClone(checkedGraphiclist[0]);
                graphic.setGeometry(result[0]);
                operation.newGraphics.push(graphic);
                undoManager.add(operation);

                clearChecked();
                actionType = 'editTool';
                setBtnState();
            });
        }

        function drawEnd(evt) {
            switch (actionType) {
                case 'tag':
                    _this.tag(evt);
                    break;
                case 'cut':
                    drawEntity.deactivate();
                    _this.cut(evt);
                    break;
                case 'borehole':
                    drawEntity.deactivate();
                    _this.borehole(evt);
                    break;
                default:
            }
        }

        function initTab() {
            var btnGroupHtml = '<div class="tabContainer"><span name="tabTag">标注</span><span name="tabEdit">编辑</span><span name="tabDel">删除</span></div>';
            $('#layerEdit').append(btnGroupHtml);

            $('#layerEdit').children('.tabContainer').children().on('click', tabClickEvent);

            var tabPanelHtml = '<div class="tabPanel"></div>';
            $('#layerEdit').append(tabPanelHtml);
        }
        function initTabTagPanel() {
            var tabBZHtml = '<div id="templateDiv"></div>';
            $('#layerEdit').children('.tabPanel').append(tabBZHtml);

            templatePicker = new esri.dijit.editing.TemplatePicker({
                featureLayers: [],
                grouping: false,
                rows: "auto",
                showTooltip: false,
                columns: 4,
            }, "templateDiv");
            templatePicker.startup();

            templatePicker.on("selection-change", function () {
                var selected = templatePicker.getSelected();
                if (selected == null) {
                    drawEntity.deactivate();
                    return;
                }
                switch (selected.featureLayer.geometryType) {
                    case 'esriGeometryPoint':
                        drawEntity.activate(esri.toolbars.Draw.POINT);
                        break;
                    case 'esriGeometryPolyline':
                        drawEntity.activate(esri.toolbars.Draw.POLYLINE);
                        break;
                    case 'esriGeometryPolygon':
                        drawEntity.activate(esri.toolbars.Draw.POLYGON);
                        break;
                }
            });
        }
        function initTabEditPanel() {
            var $tabPanel = $('#layerEdit').children('.tabPanel');
            var btnHtml = '<div class="editTool" id="layerEditTool"><span name="move">移动</span><span name="edit">修改</span><span name="cut">剪切</span><span name="merge">合并</span><span name="borehole">挖洞</span></div>';
            $tabPanel.append(btnHtml);

            $('#layerEditTool').children().on('click', function () {
                var type = $(this).attr('name');
                if ($(this).hasClass('on')) {
                    $(this).removeClass('on');
                    CommonHelper.setBtnEnble($(this), false);
                    actionType = "editTool";
                    drawEntity.deactivate();
                    editEntity.deactivate();
                    clearChecked();
                    return;
                }
                $(this).siblings().each(function () {
                    CommonHelper.setBtnEnble($(this), false);
                });
                $(this).addClass('on');
                switch (type) {
                    case 'move':
                        if (checkedGraphiclist.length != 1) return;
                        var state = editEntity.getCurrentState();
                        if (state.graphic != null) return;
                        editEntity.activate(esri.toolbars.Edit.MOVE, checkedGraphiclist[0]);
                        break;
                    case 'edit':
                        if (checkedGraphiclist.length != 1) return;
                        var state = editEntity.getCurrentState();
                        if (state.graphic != null) return;
                        editEntity.activate(esri.toolbars.Edit.ROTATE | esri.toolbars.Edit.SCALE | esri.toolbars.Edit.EDIT_VERTICES, checkedGraphiclist[0]);
                        break;
                    case 'cut':
                        if (checkedGraphiclist.length != 1) return;
                        drawEntity.activate(esri.toolbars.Draw.POLYLINE);
                        break;
                    case 'merge':
                        if (checkedGraphiclist.length < 2) return;
                        _this.merge();
                        break;
                    case 'borehole':
                        if (checkedGraphiclist.length != 1) return;
                        drawEntity.activate(esri.toolbars.Draw.POLYGON);
                        break;
                    default:
                        return;
                }
                actionType = type;
            });
        }
        function initTabDelPanel() {
            var $tabPanel = $('#layerEdit').children('.tabPanel');
            var delPanel = '<div class="delTool">当前选中<span>0</span>个<i class="del" title="删除"></i></div>';
            $tabPanel.append(delPanel);

            $tabPanel.children('.delTool').children('i').on('click', _this.del);
        }
        function initBottomPanel() {
            var bottomHtml = '<div class="bottomTool"><i class="undo" title="撤销"></i></i><i class="redo" title="恢复"></i><i class="cancel" title="取消"></i><i class="save" title="保存"></i><div>';
            $('#layerEdit').append(bottomHtml);

            var $bottomTool = $('#layerEdit').children('.bottomTool');

            $bottomTool.children().on('click', function () {
                MyMap.mapEntity.graphics.clear();
                var type = $(this).attr('class');
                switch (type) {
                    case 'undo':
                        undoManager.undo();
                        break;
                    case 'redo':
                        undoManager.redo();
                        break;
                    case 'save':
                        _this.save();
                        break;
                    case 'cancel':
                        _this.cancel();
                        break;
                    default:
                }
            });

            $bottomTool.children().each(function () {
                CommonHelper.setBtnEnble($(this), false);
            });

            undoManager.onChange(function (result) {
                if (result.canUndo) {
                    CommonHelper.setBtnEnble($bottomTool.children('.undo'), true);
                } else {
                    CommonHelper.setBtnEnble($bottomTool.children('.undo'), false);
                }

                if (result.canRedo) {
                    CommonHelper.setBtnEnble($bottomTool.children('.redo'), true);
                } else {
                    CommonHelper.setBtnEnble($bottomTool.children('.redo'), false);
                }

                if (result.position >= 0) {
                    CommonHelper.setBtnEnble($bottomTool.children('.save'), true);
                    CommonHelper.setBtnEnble($bottomTool.children('.cancel'), true);
                } else {
                    CommonHelper.setBtnEnble($bottomTool.children('.save'), false);
                    CommonHelper.setBtnEnble($bottomTool.children('.cancel'), false);
                }
            });
        }

        function tabClickEvent() {
            $(this).toggleClass("on");
            $(this).siblings().removeClass("on");
            var type = $(this).attr('name');
            clearChecked();
            drawEntity.deactivate();
            editEntity.deactivate();
            switch (type) {
                case 'tabTag':
                    templatePicker.clearSelection();
                    if ($(this).hasClass('on')) {
                        actionType = 'tag';
                        $('#templateDiv').show();
                        $('#templateDiv').siblings().hide();
                    } else {
                        $('#templateDiv').hide();
                        actionType = null;
                    }
                    break;
                case 'tabEdit':
                    var $editTool = $('#layerEdit').children('.tabPanel').children('.editTool');
                    if ($(this).hasClass('on')) {
                        $editTool.show();
                        $editTool.siblings().hide();
                        actionType = "editTool";
                        setBtnState();
                    } else {
                        $editTool.hide();
                        actionType = null;
                    }
                    break;
                case 'tabDel':
                    var $delTool = $('#layerEdit').children('.tabPanel').children('.delTool');
                    $delTool.children('span').text(0);
                    if ($(this).hasClass('on')) {
                        $delTool.show();
                        actionType = 'del';
                        $delTool.siblings().hide();
                        setBtnState();
                    } else {
                        $delTool.hide();
                        actionType = null;
                    }
                    break;
            }
        }
        function layerClickEvent(evt) {
            switch (actionType) {
                case 'del':
                    if (evt.graphic.symbol != null) return;
                    var symbol = CommonHelper.getDefSymbol(curlayerInfo);
                    evt.graphic.setSymbol(symbol);
                    checkedGraphiclist.push(evt.graphic);
                    $('#layerEdit').children('.tabPanel').children('.delTool').children('span').text(checkedGraphiclist.length);
                    setBtnState();
                    break;
                case 'editTool':
                    if (evt.ctrlKey == false && evt.metaKey == false) {  //delete feature if ctrl key is depressed
                        clearChecked();
                    }
                    if (evt.graphic.symbol != null) return;
                    var graphic = CommonHelper.graphicClone(evt.graphic);
                    graphiclist.push(graphic);
                    var symbol = CommonHelper.getDefSymbol(curlayerInfo);
                    evt.graphic.setSymbol(symbol);
                    checkedGraphiclist.push(evt.graphic);
                    setBtnState();
                    break;
                default:
            }
        }
        function mapClickEvent(evt) {
            if (ToolBar.actionType != 'layerEdit') return;
            if (actionType != 'edit' && actionType != 'move') return;

            var state = editEntity.getCurrentState();
            if (state.isModified) {
                var operation = { action: "update", graphicsLayer: graphicsLayer, oldGraphics: graphiclist, newGraphics: [state.graphic] };
                undoManager.add(operation);
            }
            editEntity.deactivate();
            actionType = 'editTool';
            clearChecked();
            setBtnState();
        }

        function setBtnState() {
            if (actionType == 'del') {
                var $btn = $('#layerEdit').children('.tabPanel').children('.delTool').children('i');
                CommonHelper.setBtnEnble($btn, checkedGraphiclist.length > 0);
                return;
            }
            var geometryType = curlayerInfo.entity.geometryType;

            $('#layerEditTool').children('span').each(function () {
                $(this).removeClass('on');
                var type = $(this).attr('name');
                switch (type) {
                    case 'move':
                        CommonHelper.setBtnEnble($(this), checkedGraphiclist.length == 1);
                        break;
                    case 'edit':
                        var isEnble = geometryType != 'esriGeometryPoint' && checkedGraphiclist.length == 1;
                        CommonHelper.setBtnEnble($(this), isEnble);
                        break;
                    case 'cut':
                        var isEnble = geometryType != 'esriGeometryPoint' && checkedGraphiclist.length == 1;
                        CommonHelper.setBtnEnble($(this), isEnble);
                        break;
                    case 'merge':
                        var isEnble = geometryType != 'esriGeometryPoint' && checkedGraphiclist.length > 1;
                        CommonHelper.setBtnEnble($(this), isEnble);
                        break;
                    case 'borehole':
                        var isEnble = geometryType == 'esriGeometryPolygon' && checkedGraphiclist.length == 1;
                        CommonHelper.setBtnEnble($(this), isEnble);
                        break;
                    default:
                }
            });
        }

        function clearChecked() {
            for (var i = 0; i < checkedGraphiclist.length; i++) {
                checkedGraphiclist[i].setSymbol(null);
            }
            graphiclist = [];
            checkedGraphiclist = [];
        }
    }

    var Measure = new function () {
        var _this = this;
        var measurement = null;
        _this.initDialog = function (isCustom) {
            var $toolbtn = null;
            if (isCustom)
                $toolbtn = $("#" + MapOptions.name);
            else
                $toolbtn = $("#" + MapOptions.name).children('.toolsbar').children(".measure");

            var html = '<section id="measure" class="toRight"><div class="u-title">请选类型<span class="u-close">x</span></div><div id="measurement"></div></section>';
            $toolbtn.append(html);
            measurement = new esri.dijit.Measurement({
                map: MyMap.mapEntity
            }, dojo.byId('measurement'));
            CommonHelper.initDrag("measure");
            $('#measure').find('.u-close').on('click', function () {
                $("#measure").hide();
                if (!isCustom)
                    $("#measure").siblings("i").removeClass("on");
                _this.endMeasure();
                ToolBar.actionType = null;
            });
            measurement.startup();
        }
        _this.onClick = function ($this) {
            if ($($this).hasClass("on")) {
                _this.endMeasure();
                ToolBar.actionType = null;
            } else {
                ToolBar.initTool($this);
                ToolBar.actionType = "measure";
            }
            $("#measure").toggle();
            $($this).toggleClass('on');
        }
        _this.onCustomClick = function () {
            if (!$("#measure").parent().hasClass('skyseamap')) return;
            ToolBar.deactivateLastAction();
            if ($("#measure").is(":visible")) {
                ToolBar.actionType = null;
            } else {
                ToolBar.actionType = "measure";
                $("#measure").siblings('section').hide();
            }
            $("#measure").toggle();
        }

        _this.endMeasure = function () {
            measurement.clearResult();
            var tool = measurement.getTool();
            if (typeof tool === 'object')
                measurement.setTool(tool.toolName, false);
        }
    }

    var Select = new function () {
        var _this = this;
        var curLayerInfo = null;
        var horizontalSlider = null;
        var drawEntity = null;
        var cusConfigs = null;

        _this.initDialog = function (isCustom) {
            drawEntity = new esri.toolbars.Draw(MyMap.mapEntity);
            drawEntity.on("draw-end", drawEnd);

            var $toolbtn = null;
            if (isCustom)
                $toolbtn = $("#" + MapOptions.name);
            else
                $toolbtn = $("#" + MapOptions.name).children('.toolsbar').children(".select");

            var html = '<section id="select" class="toRight"><div class="u-title">要素选择<span class="u-close">x</span></div></section>';
            $toolbtn.append(html);
            var tabContainerHtml = '<div class="tabContainer"><span name="tabDX">点选</span><span name="tabKX">框选</span><span name="tabZYX">自由选</span></div>';
            $("#select").append(tabContainerHtml);
            initTabContainer();

            initTabClickEvent();
            $('#select').find('.u-close').on('click', function () {
                $("#select").hide();
                if (!isCustom)
                    $("#select").siblings("i").removeClass("on");
                ToolBar.deactivateLastAction();
                ToolBar.actionType = null;
            });

            CommonHelper.initDrag("select");
        }
        _this.onClick = function ($this) {
            if ($($this).hasClass("on")) {
                ToolBar.actionType = null;
            } else {
                ToolBar.initTool($this);
                initSelect();
            }
            $("#select").toggle();
            $($this).toggleClass('on');
        }
        _this.onCustomClick = function () {
            if (LayerCtrl.curFeatureLayer() == null) return;
            if (!$("#select").parent().hasClass('skyseamap')) return;
            ToolBar.deactivateLastAction();
            if ($("#select").is(":visible")) {
                ToolBar.actionType = null;
            } else {
                initSelect();
                $("#select").siblings('section').hide();
            }
            $("#select").toggle();
        }

        _this.selectFeature = function (params, callbacks) {
            //{ radius: 500, layerId: "cf3fb541-0618-4235-8448-0dcaabedc364", type: "FrameSelection", isHighLight: true, symbol: null, infoTemplate: null };
            curLayerInfo = params.layerId == null ? LayerCtrl.curFeatureLayer() : LayerCtrl.getFeatureLayerById(params.layerId);

            if (curLayerInfo == null || !curLayerInfo.isQuery) {
                if (typeof callbacks === 'function')
                    callbacks({ status: "error", result: "此图层不可查询或没有权限查询！" });
                return;
            }

            if (params.type == null || (params.type == 'PointSelection' && typeof params.radius !== 'number')) {
                if (typeof callbacks === 'function')
                    callbacks({ status: "error", result: "请传入正确的参数！" });
                return;
            }
            cusConfigs = params;
            switch (params.type) {
                case 'PointSelection':
                    drawEntity.activate(esri.toolbars.Draw.POINT);
                    break;
                case 'FrameSelection':
                    drawEntity.activate(esri.toolbars.Draw.EXTENT);
                    break;
                case 'FreeSelection':
                    drawEntity.activate(esri.toolbars.Draw.POLYGON);
                    break;
                default:
                    if (typeof callbacks === 'function')
                        callbacks({ status: "error", result: "不支持该选择工具类型！" });
                    return;
            }

            if (typeof callbacks === 'function')
                callbacks({ status: "success", result: "接口调用成功,请点击地图选择要素！" });
        }

        _this.endSelect = function () {
            drawEntity.deactivate();
        }

        function initSelect() {
            ToolBar.actionType = "select";
            var $tabBtn = $("#select").children('div.tabContainer').children();
            $tabBtn.removeClass("on");
            $("#horizontalSlider").hide();
            curLayerInfo = LayerCtrl.curFeatureLayer();

            $tabBtn.each(function () {
                CommonHelper.setBtnEnble($(this), curLayerInfo.isQuery);
            });
            cusConfigs = null;
        }
        function initTabClickEvent() {
            $("#select").children('.tabContainer').children('span').on('click', function () {
                $(this).toggleClass("on");
                $(this).siblings().removeClass("on");
                var type = $(this).attr('name');
                switch (type) {
                    case 'tabDX':
                        if ($(this).hasClass("on")) {
                            MyMap.clearResult();
                            $('#select').children(".tabResult").children('.result').text("");
                            drawEntity.activate(esri.toolbars.Draw.POINT);
                        } else {
                            drawEntity.deactivate();
                        }
                        $("#horizontalSlider").show();
                        break;
                    case 'tabKX':
                        if ($(this).hasClass("on")) {
                            MyMap.clearResult();
                            $('#select').children(".tabResult").children('.result').text("");
                            drawEntity.activate(esri.toolbars.Draw.EXTENT);
                        } else {
                            drawEntity.deactivate();
                        }
                        $("#horizontalSlider").hide();
                        break;
                    case 'tabZYX':
                        if ($(this).hasClass("on")) {
                            MyMap.clearResult();
                            $('#select').children(".tabResult").children('.result').text("");
                            drawEntity.activate(esri.toolbars.Draw.POLYGON);
                        } else {
                            drawEntity.deactivate();
                        }
                        $("#horizontalSlider").hide();
                        break;
                }
            });
        }
        function initTabContainer() {
            var tabDXHtml = '<div id="horizontalSlider"></div>';
            $('#select').append(tabDXHtml);
            horizontalSlider = new esri.dijit.HorizontalSlider({ labels: ["0", "0.5", "1.0", "1.5", "2.0"] }, "horizontalSlider");
            horizontalSlider.setValue(25);
            $('#horizontalSlider').find(".dijitSliderDecorationT").append('<p>km</p>');
            horizontalSlider.startup();
            var resultHtml = '<div class ="tabResult"><p class="resultTitle">选择结果</p><p class="result"></p><div/>';
            $("#select").append(resultHtml);
        }
        function drawEnd(evt) {
            MyMap.clearResult();
            if (evt.geometry.type == 'point') {
                var radius = cusConfigs == null ? parseFloat(horizontalSlider.value) * 20 : cusConfigs.radius;
                var params = new esri.tasks.BufferParameters();
                params.geometries = [evt.geometry];
                params.distances = [radius];
                params.unit = esri.tasks.GeometryService.UNIT_METER;
                params.bufferSpatialReference = evt.geometry.spatialReference;
                params.outSpatialReference = MyMap.mapEntity.spatialReference;
                ToolBar.geometryService.buffer(params, function (result) {
                    var circleSymb = new esri.symbol.SimpleFillSymbol(esri.symbol.SimpleFillSymbol.STYLE_NULL, new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SHORTDASHDOTDOT, new dojo.Color([105, 105, 105]), 2), new dojo.Color([255, 255, 0, 0.25]));
                    var graphic = new esri.Graphic(result[0], circleSymb);
                    MyMap.mapEntity.graphics.add(graphic);
                    queryByGeometry(result[0]);
                });

            } else {
                queryByGeometry(evt.geometry);
            }
        }
        function queryByGeometry(geometry) {
            var query = CommonHelper.getQuery();
            query.geometry = geometry;
            curLayerInfo.entity.queryFeatures(query, function (featureSet) {
                var features = featureSet.features;
                if (cusConfigs != null && !cusConfigs.isHighLight) {
                    if (typeof MapOptions.callBacks.selectFeature === 'function')
                        MapOptions.callBacks.selectFeature(features);
                    cusConfigs = null;
                    return;
                }
                if (cusConfigs == null)
                    $('#select').children(".tabResult").children('.result').text("共找到" + features.length + "个要素");

                var symbol = null;
                if (cusConfigs != null && cusConfigs.symbol != null)
                    symbol = CommonHelper.getSymbol(cusConfigs.symbol);
                else {
                    symbol = CommonHelper.getDefSymbol(curLayerInfo);
                }

                var infoTemplate = null;
                if (cusConfigs != null && cusConfigs.infoTemplate != null)
                    infoTemplate = new esri.InfoTemplate(cusConfigs.infoTemplate);
                else
                    infoTemplate = CommonHelper.getInfoTemplate(curLayerInfo);

                for (var i = 0; i < features.length; i++) {
                    var geomtryGraphic = new esri.Graphic();
                    geomtryGraphic.setGeometry(features[i].geometry);
                    geomtryGraphic.setSymbol(symbol);
                    geomtryGraphic.setInfoTemplate(infoTemplate);
                    geomtryGraphic.setAttributes(features[i].attributes);
                    MyMap.mapEntity.graphics.add(geomtryGraphic);
                }

                if (typeof MapOptions.callBacks.selectFeature === 'function')
                    MapOptions.callBacks.selectFeature(features);
                cusConfigs = null;
            });
        }
    }

    var ConfigInfo = new function () {
        var _this = this;

        _this.initDialog = function () {
            var $toolbtn = $("#" + MapOptions.name).children('.toolsbar').children(".saveConfig");
            var html = '<section id="saveConfig" class="toRight">'
                        + '<div class="u-title">添加新配置信息<span class="u-close">x</span></div>'
                        + '<div class="u-center"><label>配置名称：</label><input type="text" name="title" /></div>'
                        + '<div class="u-btns"><input type="button" name="sure" value="确定" />&emsp;&emsp;<input type="button" value="取消" class="u-close"/></div>'
                        + '</section>';
            $toolbtn.append(html);

            $('#saveConfig').find('input[name="sure"]').on('click', saveConfig);
            $('#saveConfig').find('.u-close').on('click', function () {
                $("#saveConfig").hide();
                $("#saveConfig").siblings("i").removeClass("on");
            });
        }
        _this.onClick = function ($this) {
            if ($($this).hasClass("on")) {
                ToolBar.actionType = null;
            } else {
                ToolBar.initTool($this);
                ToolBar.actionType = "saveConfig";
                $("#saveConfig input[name='title']").val(MapOptions.configInfo != null ? MapOptions.configInfo.PZMC : "");
            }
            $("#saveConfig").toggle();
            $($this).toggleClass('on');
        }

        function saveConfig() {
            var name = $("#saveConfig input[name='title']").val();
            if (name.length == 0) return;

            var oldname = MapOptions.configInfo != null ? MapOptions.configInfo.PZMC : "";

            var pzxx = getConfig();
            var config = { ID: oldname == name ? MapOptions.configInfo.ID : CommonHelper.guidGenerator(), PZMC: name, PZXX: encodeURI(JSON.stringify(pzxx)), CJR: null, CJSJ: new Date() };
            var str = JSON.stringify([config]);
            $.ajax({
                url: MapOptions.apiUrl + "/ConfigService?rd=" + Math.random(),
                type: "post", async: false, dataType: "text",
                data: { "op": "update", "token": CommonHelper.guidGenerator(), "objs": str, "tb": "AdminConfigDb" },
                success: function (data) {
                    var json = JSON.parse(data);
                    if (json.Status == 1) {//验证数据的完整性                            
                        $("#saveConfig").hide();
                        $("#saveConfig").siblings("i").removeClass("on");
                        MapOptions.configInfo = config;
                    } else {
                        console.log("map-->saveConfig:", json.Error);
                    }
                },
                error: function (data) {
                    // alert("status:" + data.status + ";error:" + data.statusText);
                    console.log("map-->saveConfig:", "status:" + data.status + ";error:" + data.statusText);
                }
            });
        }

        function getTools() {
            var toolBar = $.extend(true, {}, MapOptions.toolBar);
            toolBar.tools = ['layerCtrl', 'layerEdit', 'select', 'measure', ['zoomFull', 'zoomIn', 'zoomOut', 'pan']];
            return toolBar;
        }
        function getBaseMaps() {
            return MapOptions.baseMaps;
        }
        function getLayerInfos() {
            var allLayers = [];
            $.each(MapOptions.layerList, function (i, item) {
                var layer = $.extend(true, {}, item);
                layer.entity = null;
                allLayers.push(layer);
            });
            return allLayers;
        }
        function getOptions() {
            return { extent: MapOptions.extent, geometryServiceUrl: MapOptions.geometryServiceUrl, spatialReferenceType: MapOptions.spatialReferenceType, graphicClicked: MapOptions.graphicClicked };
        }
        function getComponent() {
            return { zoomSlider: MapOptions.zoomSlider, toolBar: getTools(), localtionLabel: MapOptions.localtionLabel, searchView: MapOptions.searchView, overView: MapOptions.overView, scaleBar: MapOptions.scaleBar, toggleView: MapOptions.toggleView, compass: MapOptions.compass };
        }
        function getConfig() {
            var config = { baseMaps: getBaseMaps(), layerList: getLayerInfos() };
            $.extend(config, getOptions());
            $.extend(config, getComponent());
            return config;
        }
    }

    var UndoManager = function () {
        var _this = this;
        var position = -1;
        var canRedo = false;
        var canUndo = false;
        var changeEvent = null;
        _this.operationList = [];

        _this.add = function (operation) {
            //var operation = { action: '', graphicsLayer: null, oldGraphics: [], newGraphics: [] };

            if (position < _this.operationList.length - 1) {
                _this.operationList.splice(position + 1, _this.operationList.length - position - 1);
            }
            _this.operationList.push(operation);
            position = _this.operationList.length - 1;
            redoExecute(operation);
            setState();
        }

        _this.redo = function () {
            if (!canRedo) return;
            position += 1;
            redoExecute(_this.operationList[position]);
            setState();
        }
        _this.undo = function () {
            if (!canUndo) return;
            undoExecute(_this.operationList[position]);
            position -= 1;
            setState();
        }

        _this.clear = function () {
            position = -1;
            _this.operationList = [];
            setState();
        }

        _this.getPosition = function () {
            return position;
        }

        _this.onChange = function (method) {
            changeEvent = method;
        }

        function redoExecute(operation) {
            switch (operation.action) {
                case 'add':
                    operation.graphicsLayer.add(operation.newGraphics[0]);
                    break;
                case 'update':
                    operation.graphicsLayer.remove(operation.oldGraphics[0]);
                    operation.graphicsLayer.add(operation.newGraphics[0]);
                    break;
                case 'delete':
                    for (var i = 0; i < operation.oldGraphics.length; i++) {
                        operation.graphicsLayer.remove(operation.oldGraphics[i]);
                    }
                    break;
                case 'union':
                    for (var i = 0; i < operation.oldGraphics.length; i++) {
                        operation.graphicsLayer.remove(operation.oldGraphics[i]);
                    }
                    operation.graphicsLayer.add(operation.newGraphics[0]);
                    break;
                case 'cut':
                    operation.graphicsLayer.remove(operation.oldGraphics[0]);
                    for (var i = 0; i < operation.newGraphics.length; i++) {
                        operation.graphicsLayer.add(operation.newGraphics[i]);
                    }
                    break;
                default:
            }
        }

        function undoExecute(operation) {
            switch (operation.action) {
                case 'add':
                    operation.graphicsLayer.remove(operation.newGraphics[0]);
                    break;
                case 'update':
                    //operation.curGraphic.setGeometry(operation.oldGraphics[0].geometry);
                    //operation.graphicsLayer.redraw();
                    operation.graphicsLayer.remove(operation.newGraphics[0]);
                    operation.graphicsLayer.add(operation.oldGraphics[0]);
                    break;
                case 'delete':
                    for (var i = 0; i < operation.oldGraphics.length; i++) {
                        operation.graphicsLayer.add(operation.oldGraphics[i]);
                    }
                    break;
                case 'union':
                    for (var i = 0; i < operation.oldGraphics.length; i++) {
                        operation.graphicsLayer.add(operation.oldGraphics[i]);
                    }
                    operation.graphicsLayer.remove(operation.newGraphics[0]);
                    break;
                case 'cut':
                    operation.graphicsLayer.add(operation.oldGraphics[0]);
                    for (var i = 0; i < operation.newGraphics.length; i++) {
                        operation.graphicsLayer.remove(operation.newGraphics[i]);
                    }
                    break;
                default:
            }
        }

        function setState() {
            canUndo = position >= 0;
            canRedo = position < _this.operationList.length - 1;
            if (typeof changeEvent === 'function')
                changeEvent({ canUndo: canUndo, canRedo: canRedo, position: position });
        }
    }

    var TagApi = new function () {
        var _this = this;
        var graphicsLayer = null;
        var layer = null;
        var drawEntity = null;
        var cusConfigs = null;

        _this.addTag = function (params, callbacks) {
            //{ layerId: null, attributes: null};

            layer = params.layerId == null ? LayerCtrl.curFeatureLayer() : LayerCtrl.getFeatureLayerById(params.layerId);

            if (layer == null || layer.isAdd != true) {
                if (typeof callbacks === 'function')
                    callbacks({ status: 'error', rusult: '此图层不可添加点要素或没有添加权限!' });
                return;
            }
            initDraw();

            cusConfigs = $.extend({ isSaveAll: false, callbacks: callbacks }, params);

            drawActivate();
        }

        _this.init = function (layerId, callbacks) {
            layer = layerId == null ? LayerCtrl.curFeatureLayer() : LayerCtrl.getFeatureLayerById(layerId);

            if (layer == null || layer.isAdd != true) {
                if (typeof callbacks === 'function')
                    callbacks({ status: 'error', rusult: '此图层不可添加要素或没有添加权限!' });
                return;
            }

            if (graphicsLayer == null) {
                graphicsLayer = new esri.layers.GraphicsLayer();
                MyMap.mapEntity.addLayer(graphicsLayer);
            } else {
                graphicsLayer.clear();
            }
            graphicsLayer.setOpacity(layer.opacity / 100);

            initDraw();

            if (typeof callbacks === 'function') {
                callbacks({ status: 'success', rusult: '初始化成功！' });
            }
        }
        _this.activate = function (params, callbacks) {
            if (graphicsLayer == null || layer == null) {
                if (typeof callbacks === 'function')
                    callbacks({ status: 'error', rusult: '请先初始化标注功能!' });
                return;
            }

            cusConfigs = $.extend(true, { isSaveAll: true, callbacks: callbacks }, params);

            drawActivate();
        }
        _this.end = function (beforeSaveCallbacks, afterSaveCallbacks) {
            if (graphicsLayer == null || layer == null) {
                if (typeof beforeSaveCallbacks === 'function')
                    beforeSaveCallbacks({ status: 'error', rusult: '请先初始化标注功能!' });
                return;
            }
            var isSave = true;
            var graphics = graphicsClone(graphicsLayer.graphics);
            if (typeof beforeSaveCallbacks === 'function')
                isSave = beforeSaveCallbacks({ status: 'success', rusult: graphics });

            if (isSave) {
                layer.entity.applyEdits(graphics, null, null, function (result) {
                    if (typeof afterSaveCallbacks === 'function')
                        afterSaveCallbacks({ status: 'success', result: graphics });
                    clear();
                }, function (result) {
                    if (typeof afterSaveCallbacks === 'function')
                        afterSaveCallbacks({ status: 'error', result: result });
                    clear();
                });
            } else {
                clear();
            }
        }

        function clear() {
            MyMap.mapEntity.removeLayer(graphicsLayer);
            layer = null;
            graphicsLayer = null;
            cusConfigs = null;
        }

        function graphicsClone(graphics) {
            var arr = [];
            for (var i = 0; i < graphics.length; i++) {
                var graphic = CommonHelper.graphicClone(graphics[i]);
                arr.push(graphic)
            }
            return arr;
        }

        function initDraw() {
            if (drawEntity == null) {
                drawEntity = new esri.toolbars.Draw(MyMap.mapEntity);
                drawEntity.on("draw-end", drawEnd);
            }
        }
        function drawEnd(evt) {
            drawEntity.deactivate();
            if (cusConfigs.isSaveAll) {
                addToTempLayer(evt);
            } else {
                addToLayer(evt);
            }
        }
        function drawActivate() {
            var type = layer.entity.geometryType;
            switch (type) {
                case 'esriGeometryPoint':
                    drawEntity.activate(esri.toolbars.Draw.POINT);
                    break;
                case 'esriGeometryPolyline':
                    drawEntity.activate(esri.toolbars.Draw.POLYLINE);
                    break;
                case 'esriGeometryPolygon':
                    drawEntity.activate(esri.toolbars.Draw.POLYGON);
                    break;
                default:
            }
        }

        function addToLayer(evt) {
            var grap = new esri.Graphic();
            grap.setGeometry(evt.geometry);
            var attr = {};
            for (var i = 0; i < layer.entity.fields.length; i++) {
                var field = layer.entity.fields[i];
                attr[field.name] = null;
            }
            $.each(layer.initFields, function (i, item) {
                if (item.isDefValue) {
                    attr[item.fieldName] = item.Value;
                } else {
                    if (item.Value == "id") {
                        attr[item.fieldName] = "{" + CommonHelper.guidGenerator() + "}";
                    } else if (item.Value == "esriFieldTypeDate") {
                        attr[item.fieldName] = new Date().getTime();
                    }
                }
            });
            if (cusConfigs.attributes != null && typeof cusConfigs.attributes === 'object')
                $.extend(true, attr, cusConfigs.attributes);

            grap.setAttributes(attr);
            layer.entity.applyEdits([grap], null, null, function (result) {
                if (typeof cusConfigs.callbacks === 'function')
                    cusConfigs.callbacks({ status: 'success', result: grap });
                cusConfigs = null;
            }, function (result) {
                if (typeof cusConfigs.callbacks === 'function')
                    cusConfigs.callbacks({ status: 'error', result: result });
                cusConfigs = null;
            });
        }
        function addToTempLayer(evt) {
            var grap = new esri.Graphic();
            grap.setGeometry(evt.geometry);
            var attr = {};
            for (var i = 0; i < layer.entity.fields.length; i++) {
                var field = layer.entity.fields[i];
                attr[field.name] = null;
            }
            $.each(layer.initFields, function (i, item) {
                if (item.isDefValue) {
                    attr[item.fieldName] = item.Value;
                } else {
                    if (item.Value == "id") {
                        attr[item.fieldName] = "{" + CommonHelper.guidGenerator() + "}";
                    } else if (item.Value == "esriFieldTypeDate") {
                        attr[item.fieldName] = new Date().getTime();
                    }
                }
            });
            if (cusConfigs.attributes != null && typeof cusConfigs.attributes === 'object')
                $.extend(true, attr, cusConfigs.attributes);
            grap.setAttributes(attr);

            var symbol = cusConfigs.symbol != null ? CommonHelper.getSymbol(cusConfigs.symbol) : CommonHelper.getDefSymbol(layer);
            grap.setSymbol(symbol);

            graphicsLayer.add(grap);
            if (typeof cusConfigs.callbacks === 'function')
                cusConfigs.callbacks({ status: 'success', result: grap });
            cusConfigs = null;
        }
    }

    var MapApi = new function () {
        var _this = this;
        _this.initConfigure = function (options) {
            if (!MapOptions.isEnbleDelLayer) {
                MapOptions.baseMaps = [];
                MapOptions.layerList = [];
                MapOptions.toolBar.tools = [];
            }
            $.extend(true, MapOptions, options);

            if (MapOptions.isMapLoaded) {
                ToolBar.navEntity.deactivate();
                ToolBar.deactivateLastAction();

                $("#" + MapOptions.name).empty();
                $("#" + MapOptions.name).removeAttr("calss");

                MyMap.initView();
            } else {
                dojo.ready(function () {
                   // esriConfig.defaults.io.proxyUrl = "../../proxy.ashx";
                   //esriConfig.defaults.io.alwaysUseProxy = false;

                    MyMap.initView();
                    MapOptions.isMapLoaded = true;
                });
            }
        }
        _this.defInitMap = function (key, callbacks) {
            if (typeof key === 'string' && key.length > 30) {
                MapOptions.key = key;
            } else {
                if (typeof callbacks === 'function')
                    callbacks({ status: "error", result: "请输入正确的Key！" });
                return;
            }

            var defConfigure = null;
            $.ajax({
                url: MapOptions.apiUrl + "/GetMapConfig?rd=" + Math.random(),
                type: "post", async: false, dataType: "json", data: { key: key },
                success: function (data) {
                    if (data.Status != 1) return;
                    var Result = data.Result;
                    if (Result != null && Result.length > 0) {
                        var configStr = decodeURI(Result);
                        defConfigure = JSON.parse(configStr);
                    }
                },
                error: function (data) {
                    console.log("map-->defInitMap:", "status:" + data.status + ";error:" + data.statusText);
                }
            });
            if (defConfigure == null) {
                if (typeof callbacks === 'function')
                    callbacks({ status: "error", result: "请输入正确的Key！" });
                return;
            }

            var layerlist = [];
            for (var i = 0; i < defConfigure.layerList.length; i++) {
                var defLayerInfo = defConfigure.layerList[i];
                if (typeof defLayerInfo.isCreate === 'boolean' && !defLayerInfo.isCreate) continue;
                layerlist.push(defLayerInfo);
            }
            defConfigure.layerList = layerlist;

            if (typeof callbacks === 'function')
                MapOptions.callBacks.initMap = callbacks;
            _this.initConfigure(defConfigure);
        }
        _this.customMap = function (params, callbacks) {
            if (typeof params.key !== 'string' || params.key.length < 30) {
                if (typeof callbacks === 'function')
                    callbacks({ status: "error", result: "请输入正确的Key！" });
                return;
            }

            var defConfigure = null;
            $.ajax({
                url: MapOptions.apiUrl + "/GetMapConfig?rd=" + Math.random(),
                type: "post", async: false, dataType: "json", data: { key: params.key },
                success: function (data) {
                    if (data.Status != 1) return;
                    var Result = data.Result;
                    if (Result != null && Result.length > 0) {
                        var configStr = decodeURI(Result);
                        defConfigure = JSON.parse(configStr);
                    }
                },
                error: function (data) {
                    console.log("map-->customMap:", "status:" + data.status + ";error:" + data.statusText);
                }
            });
            if (defConfigure == null) {
                if (typeof callbacks === 'function')
                    callbacks({ status: "error", result: "请输入正确的Key！" });
                return;
            }

            for (var type in params) {
                switch (type) {
                    case 'layerList':
                        var layerlist = [];
                        if (params.layerList == null || typeof params.layerList !== 'object') continue;
                        for (var i = 0; i < defConfigure.layerList.length; i++) {
                            var defLayerInfo = defConfigure.layerList[i];
                            for (var j = 0; j < params.layerList.length; j++) {
                                var layerInfo = params.layerList[j];
                                if (defLayerInfo.id == layerInfo.id) {
                                    if (typeof layerInfo.isCreate === 'boolean' && !layerInfo.isCreate) break;
                                    $.extend(true, defLayerInfo, layerInfo);
                                    layerlist.push(defLayerInfo);
                                    break;
                                }
                            }
                        }
                        defConfigure.layerList = layerlist;
                        break;
                    case 'baseMaps': break;
                    default:
                        if (defConfigure[type] == null)
                            defConfigure[type] = params[type];
                        else
                            $.extend(defConfigure[type], params[type]);
                }
            }

            if (params.layerList == null || typeof params.layerList !== 'object') {
                var layerlist = [];
                for (var i = 0; i < defConfigure.layerList.length; i++) {
                    var defLayerInfo = defConfigure.layerList[i];
                    if (typeof defLayerInfo.isCreate === 'boolean' && !defLayerInfo.isCreate) continue;
                    layerlist.push(defLayerInfo);
                }
                defConfigure.layerList = layerlist;
            }

            if (typeof callbacks === 'function')
                MapOptions.callBacks.initMap = callbacks;
            _this.initConfigure(defConfigure);
        }
        _this.addLayer = function (params) {
            LayerCtrl.addLayer(params);
            MapOptions.layerList.push(params);
            LayerCtrl.updateList();
        }
        _this.getLayerInfos = function (callbacks) {
            var layers = [];
            $.each(MapOptions.layerList, function (i, item) {
                var info = { layerId: item.id, type: item.type, name: item.title, fields: item.fields };                
                    layers.push(info);             
            });

            if (typeof callbacks === 'function')
                callbacks(layers);
        }
        _this.location = function (params, callbacks) {
            //{ location: { center:{ x: -29209294.2007, y: 2420591.46523 },zoom:10, isHighLight: true ,symbol:null,isAlwaysShow:true} };
            if (!params.center || typeof params.center.x !== 'number' || typeof params.center.y !== 'number') {
                if (typeof callbacks === 'function')
                    callbacks({ status: "error", result: "请传入正确的坐标！" });
                return;
            }

            var centerPoint = new esri.geometry.Point(params.center.x, params.center.y, MyMap.mapEntity.spatialReference);
            var zoom = typeof params.zoom === 'number' ? params.zoom : MyMap.mapEntity.getMaxZoom() - 2;
            MyMap.mapEntity.centerAndZoom(centerPoint, zoom);

            if (params.isHighLight || params.isAlwaysShow) {
                var sampleGraphic = new esri.Graphic();
                sampleGraphic.setGeometry(centerPoint);
                if (params.symbol != null && typeof params.symbol === 'object') {
                    var symbol = CommonHelper.getSymbol(params.symbol);
                    sampleGraphic.setSymbol(symbol);
                } else
                    sampleGraphic.setSymbol(CommonHelper.markerSymbol);
                var layer = MyMap.mapEntity.getLayer('locationLayer');
                if (params.isAlwaysShow) {
                    if (!layer) {
                        layer = new esri.layers.GraphicsLayer({ id: 'locationLayer' });
                        MyMap.mapEntity.addLayer(layer);
                    } else {
                        layer.clear();
                    }
                    layer.add(sampleGraphic);
                } else {
                    if (layer)
                        MyMap.mapEntity.removeLayer(layer);
                    MyMap.mapEntity.graphics.add(sampleGraphic);
                }
            }

            if (typeof callbacks === 'function')
                callbacks({ status: "success", result: "定位成功！" });
        }
        _this.extentAndCenter = function (callbacks) {
            if (typeof callbacks === 'function') {
                var extent = MyMap.mapEntity.extent;
                var center = MyMap.mapEntity.center;
                callbacks({ status: "success", result: { "extent": extent, "center": center } });
            }
        }
        _this.queryFeature = function (params, callbacks) {
            //{ queryFeature: { layerId: -29209294.2007, filters: 'where语句',filterByIds:[],filterByGeomtry:null, isHighLight: true ,symbol:null,infoTemplate:null} };
            var layerInfo = params.layerId == null ? LayerCtrl.curFeatureLayer() : LayerCtrl.getFeatureLayerById(params.layerId);

            if (layerInfo == null || !layerInfo.isQuery) {
                if (typeof callbacks === 'function')
                    callbacks({ status: "error", result: "此图层不可查询或没有权限查询！" });
                return;
            }

            var query = CommonHelper.getQuery();
            if (params.filterByIds != null) {
                query.where = CommonHelper.whereInIds(layerInfo.idFieldName, params.filterByIds);
            } else if (params.filters != null) {
                query.where = params.filters;
            } else if (params.filterByGeomtry != null) {
                query.geometry = params.filterByGeomtry;
            } else {
                if (typeof callbacks === 'function')
                    callbacks({ status: "error", result: "请输入查询条件！" });
                return;
            }
            layerInfo.entity.queryFeatures(query, function (featureSet) {
                var features = featureSet.features;
                if (typeof callbacks === 'function')
                    callbacks({ status: "success", result: features });

                if (typeof params.isHighLight !== 'boolean' || !params.isHighLight) return;

                MyMap.clearResult();

                var symbol = params.symbol != null ? CommonHelper.getSymbol(params.symbol) : CommonHelper.getDefSymbol(layerInfo);

                var infoTemplate = params.infoTemplate != null ? new esri.InfoTemplate(params.infoTemplate) : CommonHelper.getInfoTemplate(layerInfo);

                var rExtent = new esri.geometry.Extent();
                for (var i = 0; i < features.length; i++) {
                    var graphic = new esri.Graphic();
                    graphic.setGeometry(features[i].geometry);
                    graphic.setSymbol(symbol);
                    graphic.setInfoTemplate(infoTemplate);
                    graphic.setAttributes(features[i].attributes);
                    MyMap.mapEntity.graphics.add(graphic);

                    if (i == 0)
                        rExtent = graphic._extent;
                    else
                        rExtent = rExtent.union(graphic._extent);
                }
                MyMap.mapEntity.setExtent(rExtent);
            });
        }
        _this.selectedLayer = function (layerId, callbacks) {
            //{ selectedLayer:  layerId };
            if (typeof layerId !== 'string') {
                if (typeof callbacks === 'function')
                    callbacks({ status: "error", result: "layerId属性不为空！" });
                return;
            }
            var num = -1;
            for (var i = 0; i < MapOptions.layerList.length; i++) {
                var layerInfo = MapOptions.layerList[i];
                if (layerInfo.id == layerId) {
                    num = i;
                    break;
                }
            }

            if (num >= 0) {
                var $span = $("#layerCtrl").children('ul').children('li').children('span');
                LayerCtrl.curLayerNum = num;
                if ($span.length > 0) {
                    var n = MapOptions.layerList.length - num - 1;
                    $span.eq(n).click();
                } else {
                    LayerCtrl.initCurrentlayer();
                }
                if (typeof callbacks === 'function')
                    callbacks({ status: "success", result: "设置成功！" });
            } else {
                if (typeof callbacks === 'function')
                    callbacks({ status: "error", result: "此图层不存在！" });
            }
        }
        _this.layerFilters = function (params, callbacks) {
            //{ layerFilters: { layerId: -29209294.2007,filters:"where ID = '777BA5B8-8499-44AE-A4D7-5B24766D946C'",filterByIds:[]} };
            var layerInfo = params.layerId == null ? LayerCtrl.curFeatureLayer() : LayerCtrl.getFeatureLayerById(params.layerId);

            if (layerInfo == null) {
                if (typeof callbacks === 'function')
                    callbacks({ status: 'error', result: '该图层不存在或不可过滤!' });
                return;
            }

            if (params.filters == null && params.filterByIds == null) {
                if (typeof callbacks === 'function')
                    callbacks({ status: 'error', result: '过滤条件不能为空!' });

                return;
            }
            var filterStr = params.filters;
            if (params.filterByIds != null)
                filterStr = CommonHelper.whereInIds(layerInfo.idFieldName, params.filterByIds);

            var result = layerInfo.entity.setDefinitionExpression(filterStr);
            if (typeof callbacks === 'function')
                callbacks({ status: 'success', result: result });

        }
        _this.basemapToggle = function (callbacks) {
            MyMap.basemapToggle.toggle();
            if (typeof callbacks === 'function') {
                MyMap.basemapToggle.on('toggle', function (result) {
                    callbacks({ status: 'success', result: result });
                });
            }
        }
        _this.toMapPoint = function (params, callbacks) {
            if (params.x == null || params.y == null) {
                if (typeof callbacks === 'function')
                    callbacks({ status: "error", result: "请传入屏幕坐标！" });
                return;
            }
            var mapWidth = $("#" + MapOptions.name).width();
            var mapHeight = $("#" + MapOptions.name).height();
            var screenPoint = new esri.geometry.ScreenPoint(params.x, params.y);
            var mapPoint = esri.geometry.toMapPoint(MyMap.mapEntity.extent, mapWidth, mapHeight, screenPoint);
            if (typeof callbacks === 'function')
                callbacks({ status: "success", result: mapPoint });
        }
        _this.updateAttr = function (params, callbacks) {
            //{ layerId: null,featureId: null, attributes: {} };
            var layer = params.layerId == null ? LayerCtrl.curFeatureLayer() : LayerCtrl.getFeatureLayerById(params.layerId);

            if (layer == null || layer.isEdit != true) {
                if (typeof callbacks === 'function')
                    callbacks({ status: 'error', rusult: '此图层不可编辑或没有编辑权限!' });
                return;
            }

            if (params.featureId == null || params.attributes == null) {
                if (typeof callbacks === 'function')
                    callbacks({ statis: 'error', rusult: '请传入正确参数!' });
                return;
            }

            var query = CommonHelper.getQuery();
            query.where = layer.idFieldName + " = '" + params.featureId + "'";
            layer.entity.queryFeatures(query, function (featureSet) {
                if (featureSet.features.length == 0) {
                    if (typeof callbacks === 'function')
                        callbacks({ status: 'error', rusult: '此元素不存在！' });
                    return;
                }
                var feature = featureSet.features[0];
                var attributes = $.extend(false, {}, feature.attributes, params.attributes);
                feature.setAttributes(attributes);
                layer.entity.applyEdits(null, [feature], null, function (result) {
                    if (typeof callbacks === 'function')
                        callbacks({ status: 'success', rusult: feature });
                }, function (result) {
                    if (typeof callbacks === 'function')
                        callbacks({ status: 'error', rusult: result });
                });
            });
        }
        _this.delFeatures = function (params, callbacks) {
            // { layerId: null, featureIds: [] };
            var layerInfo = params.layerId == null ? LayerCtrl.curFeatureLayer() : LayerCtrl.getFeatureLayerById(params.layerId);

            if (layerInfo == null || layerInfo.isDel != true) {
                if (typeof callbacks === 'function')
                    callbacks({ status: 'error', rusult: '此图层不可删除要素或没有删除权限!' });
                return;
            }

            if (params.featureIds == null) {
                if (typeof callbacks === 'function')
                    callbacks({ status: 'error', rusult: '删除条件或要素不为空!' });
                return;
            }

            var query = CommonHelper.getQuery();
            query.where = CommonHelper.whereInIds(layerInfo.idFieldName, params.featureIds);
            layerInfo.entity.queryFeatures(query, function (featureSet) {
                var features = featureSet.features;
                if (features.length == 0) {
                    if (typeof callbacks === 'function')
                        callbacks({ status: 'error', rusult: '查询不到相关要素信息！' });
                    return;
                }
                layerInfo.entity.applyEdits(null, null, features, function (result) {
                    if (typeof callbacks === 'function')
                        callbacks({ status: 'success', rusult: features });
                }, function (result) {
                    if (typeof callbacks === 'function')
                        callbacks({ status: 'error', rusult: result });
                });
            });

        }
        _this.setRenderer = function (params, callbacks) {
            //{ layerId: null,renderJson:{type: renderer类型,params:{json}}};
            var layer = params.layerId == null ? LayerCtrl.curFeatureLayer() : LayerCtrl.getFeatureLayerById(params.layerId);

            if (layer == null) {
                if (typeof callbacks === 'function')
                    callbacks({ status: 'error', rusult: '此图层不存在或不可渲染!' });
                return;
            }
            if (params.renderJson == null) {
                if (typeof callbacks === 'function')
                    callbacks({ status: 'error', rusult: '请传入渲染器Json!' });
                return;
            }

            var renderer = CommonHelper.getRender(params.renderJson);
            layer.entity.setRenderer(renderer);
            if (typeof callbacks === 'function')
                callbacks({ status: 'success', rusult: '渲染成功!' });
        }
        _this.setSymbol = function (params, callbacks) {
            //var s = { layerId: null,featureIds: [], symbol: null, infoTemplate: null, isFocus: false, isShowInfo: false }
            var layer = params.layerId == null ? LayerCtrl.curFeatureLayer() : LayerCtrl.getFeatureLayerById(params.layerId);

            if (layer == null) {
                if (typeof callbacks === 'function')
                    callbacks({ status: 'error', rusult: '此图层不存在或不可设置样式!' });
                return;
            }
            if (params.featureIds == null || params.featureIds.length == 0) {
                if (typeof callbacks === 'function')
                    callbacks({ status: 'error', rusult: 'featureIds属性不为空!' });
                return;
            }

            var query = CommonHelper.getQuery();
            query.where = CommonHelper.whereInIds(layer.idFieldName, params.featureIds);
            layer.entity.queryFeatures(query, function (featureSet) {
                var features = featureSet.features;
                if (features.length == 0) {
                    if (typeof callbacks === 'function')
                        callbacks({ status: 'error', rusult: '查询不到相关要素信息！' });
                    return;
                }

                var symbol = params.symbol != null ? CommonHelper.getSymbol(params.symbol) : CommonHelper.getDefSymbol(layer);
                var infoTemplate = params.infoTemplate != null ? new esri.InfoTemplate(params.infoTemplate) : CommonHelper.getInfoTemplate(layer);

                var extent = new esri.geometry.Extent();
                for (var i = 0; i < features.length; i++) {
                    var graphic = CommonHelper.graphicClone(features[i]);
                    graphic.setSymbol(symbol);
                    graphic.setInfoTemplate(infoTemplate);
                    MyMap.mapEntity.graphics.add(graphic);

                    if (i > 0) {
                        extent = extent.union(graphic._extent);
                    } else {
                        extent = graphic._extent;
                        if (params.isShowInfo && layer.entity.geometryType == 'esriGeometryPoint') {
                            MyMap.mapEntity.infoWindow.setContent(graphic.getContent());
                            MyMap.mapEntity.infoWindow.setTitle(graphic.getTitle());
                            MyMap.mapEntity.infoWindow.show(graphic.getGeomtry(), MyMap.mapEntity.getInfoWindowAnchor(graphic.getGeomtry()));
                        }
                    }
                }
                if (!params.isShowInfo && params.isFocus)
                    MyMap.mapEntity.setExtent(extent);

                if (typeof callbacks === 'function')
                    callbacks({ status: 'success', rusult: '渲染成功!' });
            });
        }
        _this.geometryRelation = function (params, callbacks) {
            // var s = { relation: null, geometries1: [], geometries2: [] };
            if (params.relation == null || params.geometries1 || params.geometries2) {
                if (typeof callbacks === 'function')
                    callbacks({ status: 'error', rusult: '请传入正确的参数!' });
                return;
            }

            var relationParams = new esri.tasks.RelationParameters();
            relationParams.geometries1 = params.geometries1;
            relationParams.geometries2 = params.geometries2;
            var relation = esri.tasks.RelationParameters["SPATIAL_REL_" + params.relation];
            if (relation == null) {
                if (typeof callbacks === 'function')
                    callbacks({ status: 'error', rusult: params.relation + '此接口不存在!' });
                return;
            }
            relationParams.relation = relation;

            ToolBar.geometryService.relation(relationParams, function (result) {
                if (typeof callbacks === 'function')
                    callbacks({ status: 'success', rusult: result });
            }, function (result) {
                if (typeof callbacks === 'function')
                    callbacks({ status: 'error', rusult: result });
            });

        }
        _this.transform = function (params, callbacks) {
            // { x: null, y: null, type: null };
            if (typeof params.x !== 'number' || typeof params.y !== 'number') {
                if (typeof callbacks === 'function')
                    callbacks({ status: 'error', rusult: '请传入正确的X,Y参数!' });
                return;
            }
            switch (params.type) {
                case 'L2W':
                    var point = lonLat2WebMercator(params);
                    if (typeof callbacks === 'function')
                        callbacks({ status: 'success', rusult: point });
                    break;
                case 'W2L':
                    var point = WebMercator2lonLat(params);
                    if (typeof callbacks === 'function')
                        callbacks({ status: 'success', rusult: point });
                    break;
                default:
                    if (typeof callbacks === 'function')
                        callbacks({ status: 'error', rusult: '请传入正确的type参数!' });
                    return;
            }

            function lonLat2WebMercator(lonLat) {
                var x = lonLat.x * 20037508.34 / 180;
                var y = Math.log(Math.tan((90 + lonLat.y) * Math.PI / 360)) / (Math.PI / 180);
                y = y * 20037508.34 / 180;
                return { x: x, y: y };
            }
            function WebMercator2lonLat(mercator) {
                var x = mercator.x / 20037508.34 * 180;
                var y = mercator.y / 20037508.34 * 180;
                y = 180 / Math.PI * (2 * Math.atan(Math.exp(y * Math.PI / 180)) - Math.PI / 2);
                return { x: x, y: y };
            }
        }
        _this.queryNearFeature = function (params, callbacks) {
            // var a = { radius: 1, center: { x: 1, y: 2 }, isHighLight: true ,symbol:null,infoTemplate:null }
            if (params == null || typeof params.radius !== 'number' || typeof params.center !== 'object') {
                if (typeof callbacks === 'function')
                    callbacks({ status: 'error', rusult: '请传入正确参数!' });
                return;
            }
            var centerPoint = new esri.geometry.Point(params.center.x, params.center.y, MyMap.mapEntity.spatialReference);
            var buffer = new esri.tasks.BufferParameters();
            buffer.geometries = [centerPoint];
            buffer.distances = [params.radius];
            buffer.unit = esri.tasks.GeometryService.UNIT_METER;
            buffer.bufferSpatialReference = centerPoint.spatialReference;
            buffer.outSpatialReference = MyMap.mapEntity.spatialReference;
            ToolBar.geometryService.buffer(buffer, function (result) {
                params.filterByGeomtry = result[0];
                _this.queryFeature(params, callbacks);
            }, function (result) {
                if (typeof callbacks === 'function')
                    callbacks({ status: 'error', rusult: result });
            });
        }
        _this.distance = function (params, callbacks) {
            if (params == null || typeof params.point1 !== 'object' || typeof params.point2 !== 'object') {
                if (typeof callbacks === 'function')
                    callbacks({ status: 'error', rusult: '请传入正确参数!' });
                return;
            }
            var point1 = new esri.geometry.Point(params.point1.x, params.point1.y, MyMap.mapEntity.spatialReference);
            var point2 = new esri.geometry.Point(params.point2.x, params.point2.y, MyMap.mapEntity.spatialReference);
            var distParams = new esri.tasks.DistanceParameters();
            distParams.geometry1 = point1;
            distParams.geometry2 = point2;
            distParams.distanceUnit = esri.tasks.GeometryService.UNIT_METER;
            distParams.geodesic = true;
            ToolBar.geometryService.distance(distParams, function (data) {
                if (typeof callbacks === 'function')
                    callbacks({ status: 'success', rusult: data });
            }, function (data) {
                if (typeof callbacks === 'function')
                    callbacks({ status: 'error', rusult: data });
            });
        }
        _this.layerConfig = function (params, callbacks) {
            // { layerId: null, index: null, opacity: null,isVisible:false };
            var layer = params.layerId ? MyMap.mapEntity.getLayer(params.layerId) : MapOptions.layerList[LayerCtrl.curLayerNum];
            if (!layer) {
                if (typeof callbacks === 'function')
                    callbacks({ status: 'error', rusult: '请传入正确参数!' });
                return;
            }

            if (typeof params.isVisible === 'boolean')
                layer.setVisibility(params.isVisible);
            if (typeof params.opacity === 'number')
                layer.setOpacity(params.opacity / 100);
            if (typeof params.index === 'number')
                MyMap.mapEntity.reorderLayer(layer, params.index);

            if (typeof callbacks === 'function')
                callbacks({ status: 'success', rusult: '配置成功！' });
        }
    }


    var MapOptions = {
        name: 'myMap',
        extent: null,
        layerList: [],
        baseMaps: [],
        highLightSymbol: null,
        graphicClicked: { ishighLight: true, isShowInfo: true },
        zoomSlider: {
            visible: true, style: { top: "60px !important", left: "15px" }
        },
        localtionLabel: {
            visible: true, style: { bottom: "5px", left: "180px" }
        },
        toolBar: {
            visible: true, style: { top: "15px", right: "15px" },
            tools: ['layerCtrl', 'layerEdit', 'select', 'measure', 'saveConfig', ['zoomFull', 'zoomIn', 'zoomOut', 'pan']]
        },
        searchView: {
            visible: true, style: { top: "15px", left: "15px" }
        },
        overView: {
            visible: false, width: 150, height: 100, attachTo: 'bottom-right'
        },
        scaleBar: {
            visible: true, attachTo: 'bottom-left', scalebarUnit: 'dual'
        },
        toggleView: {
            visible: true, style: { top: "60px", right: "15px" }
        },
        compass: {
            visible: true, style: { bottom: "60px", left: "15px" }
        },
        isMapLoaded: false,
        isEnbleDelLayer: true,
        geometryServiceUrl: null,
        spatialReferenceType: null,
        apiUrl: "http://192.168.1.111/thth/map",
        key: "管理员",
        callBacks: {
            initMap: null,
            beforeTag: null,
            selectFeature: null,
            beforeSaveEdit: null,
            afterSaveEdit: null,
            graphicClicked: null
        }
    };

    String.prototype.format = function (args) {
        if (arguments.length > 0) {
            var result = this;
            if (arguments.length == 1 && typeof (args) == "object") {
                for (var key in args) {
                    var reg = new RegExp("({" + key + "})", "g");
                    result = result.replace(reg, args[key]);
                }
            } else {
                for (var i = 0; i < arguments.length; i++) {
                    if (arguments[i] == undefined) {
                        return "";
                    } else {
                        var reg = new RegExp("({[" + i + "]})", "g");
                        result = result.replace(reg, arguments[i]);
                    }
                }
            }
            return result;
        } else {
            return this;
        }
    }

    var Map = function (elementId) {
        var _this = this;
        _this.$element = dojo.byId(elementId);

        _this.initMap = function (params, callbacks) {
            if (!_this.$element) throw ('尚未创建SkySeaMap实例！');
            MapOptions.name = _this.$element.id;
            MapOptions.isEnbleDelLayer = false;
            if (params != null && typeof params === 'object')
                MapApi.customMap(params, callbacks);
            else if (typeof params === 'string')
                MapApi.defInitMap(params, callbacks);
        }

        _this.on = function (params, callbacks) {
            if (typeof params === 'object') {
                dojo.mixin(MapOptions.callBacks, params);
                return;
            }
            if (typeof callbacks !== 'function') return;

            if (typeof MapOptions.callBacks[params] !== 'undefined')
                MapOptions.callBacks[params] = callbacks;
            else
                alert('不支持此接口！');
        }

        dojo.mixin(_this, {
            'location': MapApi.location,
            'updateAttr': MapApi.updateAttr,
            'queryFeature': MapApi.queryFeature,
            'queryNearFeature': MapApi.queryNearFeature,
            'layerFilters': MapApi.layerFilters,
            'checkedLayer': MapApi.selectedLayer,
            'selectFeature': Select.selectFeature,
            'toMapPoint': MapApi.toMapPoint,
            'setRenderer': MapApi.setRenderer,
            'delFeatures': MapApi.delFeatures,
            'setSymbol': MapApi.setSymbol,
            'geometryRelation': MapApi.geometryRelation,
            'initTag': TagApi.init,
            'tag': TagApi.activate,
            'endTag': TagApi.end,
            'transform': MapApi.transform,
            'getLayerInfos': MapApi.getLayerInfos,
            'clearScreen': MyMap.clearResult,
            'basemapToggle': MapApi.basemapToggle,
            'distance': MapApi.distance,
            'layerCtrl': LayerCtrl.onCustomClick,
            'layerEdit': LayerEdit.onCustomClick,
            'select': Select.onCustomClick,
            'measure': Measure.onCustomClick,
            'zoomFull': ToolBar.zoomFull,
            'zoomIn': ToolBar.zoomIn,
            'zoomOut': ToolBar.zoomOut,
            'pan': ToolBar.pan,
            'layerConfig': MapApi.layerConfig
        });
        return this;
    }
    Map.prototype = {
        "initConfigure": MapApi.initConfigure,
        "addLayer": MapApi.addLayer
    };

    var SkySeaMap = {};
    SkySeaMap.Map = Map;
    SkySeaMap.RelationParameters = {};
    var arr = ['COINCIDENCE', 'CROSS', 'DISJOINT', 'IN', 'INTERIORINTERSECTION', 'INTERSECTION', 'LINETOUCH', 'OVERLAP', 'POINTTOUCH', 'RELATION', 'TOUCH', 'WITHIN'];
    for (var i = 0; i < arr.length; i++) {
        var type = arr[i];
        SkySeaMap.RelationParameters[type] = type;
    }

    SkySeaMap.TransformType = {};
    dojo.mixin(SkySeaMap.TransformType, {
        LonLat2WebMercator: 'L2W',
        WebMercator2LonLat: 'W2L'
    });

    dojo.mixin(window, {
        SkySeaMap: SkySeaMap,
        SKYSEAMAP_API_VERSION: "1.0.0"
    });
})(jQuery);