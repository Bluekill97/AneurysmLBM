using System.Xml.Serialization;

namespace HemeSimulation.Settings {

    /// <summary>
    /// Most of the enums only have one instance. Thats because i don't know any other alternatives,
    /// but wanted to already give the opportunity for further options to add later.
    /// </summary>

    public enum TimeUnits {
        //[XmlEnum("m")]
        //minutes,
        
        [XmlEnum("s")]
        s,

        //[XmlEnum("ms")]
        //ms
    }

    public enum SpaceUnits {
        [XmlEnum("lattice")]
        lattice,

        [XmlEnum("m")]
        m,

        [XmlEnum("dimensionless")]
        dimensionless
    }

    public enum PressureUnits {
        [XmlEnum("mmHg")]
        mmHg
    }

    public enum AngleUnit {
        [XmlEnum("rad")]
        rad
    }

    public enum TrigonometricFunctions {
        //[XmlEnum("sine")]
        //sine,

        [XmlEnum("cosine")]
        cosine
    }

    public enum GeometryType {
        [XmlEnum("whole")]
        whole,

        [XmlEnum("Ilet")]
        Ilet,

        [XmlEnum("Olet")]
        Olet,

        [XmlEnum("Wall")]
        Wall,

        [XmlEnum("Sphr")]
        Sphr
    }

    public enum FieldType {
        [XmlEnum("velocity")]
        velocity,

        [XmlEnum("pressure")]
        pressure
    }

    public enum ForceNames {
        [XmlEnum("gravity")]
        gravity,

        [XmlEnum("dipolar")]
        dipolar
    }
}