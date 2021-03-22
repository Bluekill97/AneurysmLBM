using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class InOutLetPosition : IXmlSerializable, ISimulationSetting {

        [XmlAttribute("units")]
        public SpaceUnits Units = SpaceUnits.m;

        [XmlAttribute("value")]
        public Vector3 Value = new Vector3();

        public XmlSchema GetSchema() {
            return (null);
        }

        public void ReadXml(XmlReader reader) {
            reader.MoveToContent();

            Units = (SpaceUnits)Enum.Parse(typeof(SpaceUnits), reader.GetAttribute("units"));

            var readVec = reader.GetAttribute("value");
            Value = Vector3Extensions.Vec3FromString(readVec);
        }

        public void WriteXml(XmlWriter writer) {
            writer.WriteAttributeString("units", Units.ToString()) ;

            string vec = Vector3Extensions.Vec3ToString(Value).Replace(" ", "");
            writer.WriteAttributeString("value", vec);
        }

        public void ApplySimulationSetting(SimulationSetting applySetting) {
            Units = (SpaceUnits)((SimulationAttributeEnum)applySetting.Attributes[0]).GetEnumValue();
            Value = ((SimulationAttributeVector)applySetting.Attributes[1]).GetVectorFloat();
        }

        public SimulationSetting GetSimulationSetting() {
            var att1 = new SimulationAttributeEnum("Units", Units);
            var att2 = new SimulationAttributeVector("Value", Value);

            return new SimulationSetting(this, "In/ Outlet Position",
                new List<ISimulationAttribute>() { att1, att2 });
        }
    }
}