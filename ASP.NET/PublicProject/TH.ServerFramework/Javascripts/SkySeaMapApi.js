(function () {
    'use strict';

    var SkySeaMap = {};
    SkySeaMap.Map = function (elementId) {
        var _this = this;
        _this.$element = document.getElementById(elementId);
        extend(this.$element.style, { border: '0px', width: '100%', height: '100%' });
        if (_this.$element.src == null || _this.$element.src.length == 0)
            _this.$element.src = "SkySeaMap/SkySeaMap.html";
        _this.isLoading = false;
        var map;
        var content = _this.$element.contentWindow;

        _this.initMap = function (params, callbacks) {
            if (!_this.$element) throw ('尚未创建SkySeaMap实例！');

            if (_this.$element.attachEvent) {
                _this.$element.attachEvent("onload", function () {
                    //以下操作必须在iframe加载完后才可进行  
                    map = new content.SkySeaMap.Map('myMap');
                    map.initMap(params, function (result) {
                        _this.isLoading = true;
                        callbacks(result);
                    });
                    initConfig();
                });
            } else {
                _this.$element.onload = function () {
                    //以下操作必须在iframe加载完后才可进行  
                    map = new content.SkySeaMap.Map('myMap');
                    map.initMap(params, function (result) {
                        _this.isLoading = true;
                        callbacks(result);
                    });
                    initConfig();
                };
            }
        }

        function initConfig() {
            extend(_this, {
                'location': map.location,
                'updateAttr': map.updateAttr,
                'queryFeature': map.queryFeature,
                'queryNearFeature': map.queryNearFeature,
                'layerFilters': map.layerFilters,
                'checkedLayer': map.checkedLayer,
                'selectFeature': map.selectFeature,
                'toMapPoint': map.toMapPoint,
                'setRenderer': map.setRenderer,
                'delFeatures': map.delFeatures,
                'setSymbol': map.setSymbol,
                'geometryRelation': map.geometryRelation,
                'initTag': map.initTag,
                'tag': map.tag,
                'endTag': map.endTag,
                'transform': map.transform,
                'getLayerInfos': map.getLayerInfos,
                'clearScreen': map.clearScreen,
                'basemapToggle': map.basemapToggle,
                'layerCtrl': map.layerCtrl,
                'layerEdit': map.layerEdit,
                'select': map.select,
                'measure': map.measure,
                'zoomFull': map.zoomFull,
                'zoomIn': map.zoomIn,
                'zoomOut': map.zoomOut,
                'pan': map.pan,
                'on': map.on,
                'distance': map.distance,
                'layerConfig': map.layerConfig
            });
            SkySeaMap.RelationParameters = content.SkySeaMap.RelationParameters;
            SkySeaMap.TransformType = content.SkySeaMap.TransformType;;
            extend(window, {
                SKYSEAMAP_API_VERSION: content.SKYSEAMAP_API_VERSION
            });
        }
    }

    function extend(source, target) {
        for (var key in target) source[key] = target[key]
    }

    extend(window, {
        SkySeaMap: SkySeaMap
    });
})();
