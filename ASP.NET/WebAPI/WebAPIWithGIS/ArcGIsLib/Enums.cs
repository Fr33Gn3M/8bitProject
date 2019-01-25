using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace ArcGIsLib
{
    [DataContract(Namespace = Namespaces.ArcGIsLib)]
    public enum ServiceType
    {
        [EnumMember()]
        Unknown = 0,
        [EnumMember()]
        MapServer,
        [EnumMember()]
        FeatureServer,
        [EnumMember()]
        GeocodeServer,
        [EnumMember()]
        GeoDataServer,
        [EnumMember()]
        GeometryServer,
        [EnumMember()]
        GPServer,
        [EnumMember()]
        GlobeServer,
        [EnumMember()]
        ImageServer,
        [EnumMember()]
        NAServer,
        [EnumMember()]
        MobileServer
    }

    [DataContract(Namespace = Namespaces.ArcGIsLib)]
    public enum HtmlPopupType
    {
        [EnumMember()]
        esriServerHTMLPopupTypeNone,
        [EnumMember()]
        esriServerHTMLPopupTypeAsURL,
        [EnumMember()]
        esriServerHTMLPopupTypeAsHTMLText
    }
    [DataContract(Namespace = Namespaces.ArcGIsLib)]
    public enum FontStyle
    {
        [EnumMember()]
        italic,
        [EnumMember()]
        normal,
        [EnumMember()]
        oblique
    }
    [DataContract(Namespace = Namespaces.ArcGIsLib)]
    public enum FontWeight
    {
        [EnumMember()]
        bold,
        [EnumMember()]
        bolder,
        [EnumMember()]
        lighter,
        [EnumMember()]
        normal
    }
    [DataContract(Namespace = Namespaces.ArcGIsLib)]
    public enum FontDecoration
    {
        [EnumMember()]
        none = 0,
        [EnumMember()]
        LineThrough,
        [EnumMember()]
        Underline
    }
    [DataContract(Namespace = Namespaces.ArcGIsLib)]
    public enum HorizontalAlignment
    {
        [EnumMember()]
        left,
        [EnumMember()]
        right,
        [EnumMember()]
        center,
        [EnumMember()]
        justify
    }
    [DataContract(Namespace = Namespaces.ArcGIsLib)]
    public enum LabelPlacement
    {
        [EnumMember()]
        esriServerPointLabelPlacementAboveCenter,
        [EnumMember()]
        esriServerPointLabelPlacementAboveLeft,
        [EnumMember()]
        esriServerPointLabelPlacementAboveRight,
        [EnumMember()]
        esriServerPointLabelPlacementBelowCenter,
        [EnumMember()]
        esriServerPointLabelPlacementBelowLeft,
        [EnumMember()]
        esriServerPointLabelPlacementBelowRight,
        [EnumMember()]
        esriServerPointLabelPlacementCenterCenter,
        [EnumMember()]
        esriServerPointLabelPlacementCenterLeft,
        [EnumMember()]
        esriServerPointLabelPlacementCenterRight,
        [EnumMember()]
        esriServerLinePlacementAboveAfter,
        [EnumMember()]
        esriServerLinePlacementAboveAlong,
        [EnumMember()]
        esriServerLinePlacementAboveBefore,
        [EnumMember()]
        esriServerLinePlacementAboveStart,
        [EnumMember()]
        esriServerLinePlacementAboveEnd,
        [EnumMember()]
        esriServerLinePlacementBelowAfter,
        [EnumMember()]
        esriServerLinePlacementBelowAlong,
        [EnumMember()]
        esriServerLinePlacementBelowBefore,
        [EnumMember()]
        esriServerLinePlacementBelowStart,
        [EnumMember()]
        esriServerLinePlacementBelowEnd,
        [EnumMember()]
        esriServerLinePlacementCenterAfter,
        [EnumMember()]
        esriServerLinePlacementCenterAlong,
        [EnumMember()]
        esriServerLinePlacementCenterBefore,
        [EnumMember()]
        esriServerLinePlacementCenterStart,
        [EnumMember()]
        esriServerLinePlacementCenterEnd,
        [EnumMember()]
        esriServerPolygonPlacementAlwaysHorizontal
    }

    [DataContract(Namespace = Namespaces.ArcGIsLib)]
    public enum SimpleFillSymbolStyle
    {
        [EnumMember()]
        esriSFSBackwardDiagonal,
        [EnumMember()]
        esriSFSSolid,
        [EnumMember()]
        esriSFSCross,
        [EnumMember()]
        esriSFSDiagonalCross,
        [EnumMember()]
        esriSFSForwardDiagonal,
        [EnumMember()]
        esriSFSHorizontal,
        [EnumMember()]
        esriSFSNull,
        [EnumMember()]
        esriSFSVertical
    }
    //Public Enum SimpleFillSymbolStyle
    //    esriSFSBackwardDiagonal
    //    esriSFSCross
    //    esriSFSDiagonalCross
    //    esriSFSForwardDiagonal
    //    esriSFSHorizontal
    //    esriSFSNull
    //    esriSFSSolid﻿'放在这里比较器中会多出个\0？？？
    //    esriSFSVertical
    //End Enum
    [DataContract(Namespace = Namespaces.ArcGIsLib)]
    public enum SimpleLineSymbolStyle
    {
        [EnumMember()]
        esriSLSDash,
        [EnumMember()]
        esriSLSDashDot,
        [EnumMember()]
        esriSLSDot,
        [EnumMember()]
        esriSLSNull,
        [EnumMember()]
        esriSLSSolid
    }
    [DataContract(Namespace = Namespaces.ArcGIsLib)]
    public enum SimpleMarkerSymbolStyle
    {
        [EnumMember()]
        esriSMSCircle,
        [EnumMember()]
        esriSMSCross,
        [EnumMember()]
        esriSMSDiamond,
        [EnumMember()]
        esriSMSSquare,
        [EnumMember()]
        esriSMSX
    }
    [DataContract(Namespace = Namespaces.ArcGIsLib)]
    public enum SymbolType
    {
        /// <summary>
        /// Color Symbol
        /// </summary>
        /// <remarks></remarks>
        [EnumMember()]
        Color,
        /// <summary>
        /// Simple Marker Symbol
        /// </summary>
        /// <remarks></remarks>
        [EnumMember()]
        esriSMS,
        /// <summary>
        /// Simple Line Symbol
        /// </summary>
        /// <remarks></remarks>
        [EnumMember()]
        esriSLS,
        /// <summary>
        /// Simple Fill Symbol
        /// </summary>
        /// <remarks></remarks>
        [EnumMember()]
        esriSFS,
        /// <summary>
        /// Picture Marker Symbol
        /// </summary>
        /// <remarks></remarks>
        [EnumMember()]
        esriPMS,
        /// <summary>
        /// Picture Fill Symbol
        /// </summary>
        /// <remarks></remarks>
        [EnumMember()]
        esriPFS,
        /// <summary>
        /// Text Symbol
        /// </summary>
        /// <remarks></remarks>
        esriTS
    }
    [DataContract(Namespace = Namespaces.ArcGIsLib)]
    public enum VerticalAlignment
    {
        [EnumMember()]
        baseline,
        [EnumMember()]
        top,
        [EnumMember()]
        middle,
        [EnumMember()]
        bottom
    }
    [DataContract(Namespace = Namespaces.ArcGIsLib)]
    public enum RendererType
    {
        [EnumMember()]
        simple,
        [EnumMember()]
        uniqueValue,
        [EnumMember()]
        classBreaks
    }
    [DataContract(Namespace = Namespaces.ArcGIsLib)]
    public enum EsriImageFormat
    {
        [EnumMember()]
        png,
        [EnumMember()]
        png8,
        [EnumMember()]
        png24,
        [EnumMember()]
        jpg,
        [EnumMember()]
        pdf,
        [EnumMember()]
        bmp,
        [EnumMember()]
        gif,
        [EnumMember()]
        svg,
        [EnumMember()]
        png32
    }
    [DataContract(Namespace = Namespaces.ArcGIsLib)]
    public enum LayersDisplayOption
    {
        [EnumMember()]
        show,
        [EnumMember()]
        hide,
        [EnumMember()]
        include,
        [EnumMember()]
        exclude
    }
    [DataContract(Namespace = Namespaces.ArcGIsLib)]
    public enum LayersOperationOption
    {
        [EnumMember()]
        top,
        [EnumMember()]
        visible,
        [EnumMember()]
        all
    }

    [DataContract(Namespace = Namespaces.ArcGIsLib)]
    public enum EsriOffsetHow
    {
        [EnumMember()]
        esriGeometryOffsetMitered,
        [EnumMember()]
        esriGeometryOffsetBevelled,
        [EnumMember()]
        esriGeometryOffsetRounded
    }
    [DataContract(Namespace = Namespaces.ArcGIsLib)]
    public enum EsriUnit
    {
        [EnumMember()]
        esriCentimeters,
        [EnumMember()]
        esriDecimalDegrees,
        [EnumMember()]
        esriDecimeters,
        [EnumMember()]
        esriFeet,
        [EnumMember()]
        esriInches,
        [EnumMember()]
        esriKilometers,
        [EnumMember()]
        esriMeters,
        [EnumMember()]
        esriMiles,
        [EnumMember()]
        esriMillimeters,
        [EnumMember()]
        esriNauticalMiles,
        [EnumMember()]
        esriPoints,
        [EnumMember()]
        esriUnknownUnits,
        [EnumMember()]
        esriYards
    }

    [DataContract(Namespace = Namespaces.ArcGIsLib)]
    public enum EsriSRUnitType
    {
        [EnumMember()]
        esriSRUnit_Meter = 9001,
        [EnumMember()]
        esriSRUnit_GermanMeter = 9031,
        [EnumMember()]
        esriSRUnit_Foot = 9002,
        [EnumMember()]
        esriSRUnit_SurveyFoot = 9003,
        [EnumMember()]
        esriSRUnit_ClarkeFoot = 9005,
        [EnumMember()]
        esriSRUnit_Fathom = 9014,
        [EnumMember()]
        esriSRUnit_NauticalMile = 9030,
        [EnumMember()]
        esriSRUnit_SurveyChain = 9033,
        [EnumMember()]
        esriSRUnit_SurveyLink = 9034,
        [EnumMember()]
        esriSRUnit_SurveyMile = 9035,
        [EnumMember()]
        esriSRUnit_Kilometer = 9036,
        [EnumMember()]
        esriSRUnit_ClarkeYard = 9037,
        [EnumMember()]
        esriSRUnit_ClarkeChain = 9038,
        [EnumMember()]
        esriSRUnit_ClarkeLink = 9039,
        [EnumMember()]
        esriSRUnit_SearsYard = 9040,
        [EnumMember()]
        esriSRUnit_SearsFoot = 9041,
        [EnumMember()]
        esriSRUnit_SearsChain = 9042,
        [EnumMember()]
        esriSRUnit_SearsLink = 9043,
        [EnumMember()]
        esriSRUnit_Benoit1895A_Yard = 9050,
        [EnumMember()]
        esriSRUnit_Benoit1895A_Foot = 9051,
        [EnumMember()]
        esriSRUnit_Benoit1895A_Chain = 9052,
        [EnumMember()]
        esriSRUnit_Benoit1895A_Link = 9053,
        [EnumMember()]
        esriSRUnit_Benoit1895B_Yard = 9060,
        [EnumMember()]
        esriSRUnit_Benoit1895B_Foot = 9061,
        [EnumMember()]
        esriSRUnit_Benoit1895B_Chain = 9062,
        [EnumMember()]
        esriSRUnit_Benoit1895B_Link = 9063,
        [EnumMember()]
        esriSRUnit_IndianFoot = 9080,
        [EnumMember()]
        esriSRUnit_Indian1937Foot = 9081,
        [EnumMember()]
        esriSRUnit_Indian1962Foot = 9082,
        [EnumMember()]
        esriSRUnit_Indian1975Foot = 9083,
        [EnumMember()]
        esriSRUnit_IndianYard = 9084,
        [EnumMember()]
        esriSRUnit_Indian1937Yard = 9085,
        [EnumMember()]
        esriSRUnit_Indian1962Yard = 9086,
        [EnumMember()]
        esriSRUnit_Indian1975Yard = 9087,
        [EnumMember()]
        esriSRUnit_Foot1865 = 9070,
        [EnumMember()]
        esriSRUnit_Radian = 9101,
        [EnumMember()]
        esriSRUnit_Degree = 9102,
        [EnumMember()]
        esriSRUnit_ArcMinute = 9103,
        [EnumMember()]
        esriSRUnit_ArcSecond = 9104,
        [EnumMember()]
        esriSRUnit_Grad = 9105,
        [EnumMember()]
        esriSRUnit_Gon = 9106,
        [EnumMember()]
        esriSRUnit_Microradian = 9109,
        [EnumMember()]
        esriSRUnit_ArcMinuteCentesimal = 9112,
        [EnumMember()]
        esriSRUnit_ArcSecondCentesimal = 9113,
        [EnumMember()]
        esriSRUnit_Mil6400 = 9114
    }
    [DataContract(Namespace = Namespaces.ArcGIsLib)]
    public enum EsriGeometryType
    {
        [EnumMember()]
        esriGeometryPoint,
        [EnumMember()]
        esriGeometryMultipoint,
        [EnumMember()]
        esriGeometryPolyline,
        [EnumMember()]
        esriGeometryPolygon,
        [EnumMember()]
        esriGeometryEnvelope
    }
    [DataContract(Namespace = Namespaces.ArcGIsLib)]
    public enum EsriSpatialRelationship
    {
        [EnumMember()]
        esriSpatialRelIntersects,
        [EnumMember()]
        esriSpatialRelContains,
        [EnumMember()]
        esriSpatialRelCrosses,
        [EnumMember()]
        esriSpatialRelEnvelopeIntersects,
        [EnumMember()]
        esriSpatialRelIndexIntersects,
        [EnumMember()]
        esriSpatialRelOverlaps,
        [EnumMember()]
        esriSpatialRelTouches,
        [EnumMember()]
        esriSpatialRelWithin,
        [EnumMember()]
        esriSpatialRelRelation
    }
    [DataContract(Namespace = Namespaces.ArcGIsLib)]
    public enum DrawingTool
    {
        [EnumMember()]
        esriFeatureEditToolPoint,
        [EnumMember()]
        esriFeatureEditToolLine,
        [EnumMember()]
        esriFeatureEditToolPolygon,
        [EnumMember()]
        esriFeatureEditToolAutoCompletePolygon,
        [EnumMember()]
        esriFeatureEditToolCircle,
        [EnumMember()]
        esriFeatureEditToolEllipse,
        [EnumMember()]
        esriFeatureEditToolRectangle,
        [EnumMember()]
        esriFeatureEditToolFreehand
    }

    [DataContract(Namespace = Namespaces.ArcGIsLib)]
    public enum FeatureLayerType
    {
        [EnumMember()]
        FeatureLayer,
        [EnumMember()]
        Table
    }

    [DataContract(Namespace = Namespaces.ArcGIsLib)]
    public enum MapLayerType
    {
        [EnumMember()]
        FeatureLayer,
        [EnumMember()]
        Table,
        [EnumMember()]
        GroupLayer,
        [EnumMember()]
        RasterLayer
    }
    [DataContract(Namespace = Namespaces.ArcGIsLib)]
    public enum EsriFieldType
    {
        [EnumMember()]
        esriFieldTypeSmallInteger,
        [EnumMember()]
        esriFieldTypeInteger,
        [EnumMember()]
        esriFieldTypeSingle,
        [EnumMember()]
        esriFieldTypeDouble,
        [EnumMember()]
        esriFieldTypeString,
        [EnumMember()]
        esriFieldTypeDate,
        [EnumMember()]
        esriFieldTypeOID,
        [EnumMember()]
        esriFieldTypeGeometry,
        [EnumMember()]
        esriFieldTypeBlob,
        [EnumMember()]
        esriFieldTypeRaster,
        [EnumMember()]
        esriFieldTypeGUID,
        [EnumMember()]
        esriFieldTypeGlobalID,
        [EnumMember()]
        esriFieldTypeXML
    }
    [DataContract(Namespace = Namespaces.ArcGIsLib)]
    public enum DomainType
    {
        [EnumMember()]
        Range,
        [EnumMember()]
        CodedValue
    }
    [DataContract(Namespace = Namespaces.ArcGIsLib)]
    public enum EsriTimeUnit
    {
        [EnumMember()]
        esriTimeUnitsUnknown = 0,
        [EnumMember()]
        esriTimeUnitsMilliseconds,
        [EnumMember()]
        esriTimeUnitsSeconds,
        [EnumMember()]
        esriTimeUnitsMinutes,
        [EnumMember()]
        esriTimeUnitsHours,
        [EnumMember()]
        esriTimeUnitsDays,
        [EnumMember()]
        esriTimeUnitsWeeks,
        [EnumMember()]
        esriTimeUnitsMonths,
        [EnumMember()]
        esriTimeUnitsYears,
        [EnumMember()]
        esriTimeUnitsDecades,
        [EnumMember()]
        esriTimeUnitsCenturies
    }
}