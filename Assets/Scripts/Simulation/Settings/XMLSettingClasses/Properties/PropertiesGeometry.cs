using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class PropertiesGeometry : ISimulationSetting{

        [XmlAttribute("type")]
        public GeometryType Type = GeometryType.whole;

        public void ApplySimulationSetting(SimulationSetting applySetting) {
            Type = (GeometryType)((SimulationAttributeEnum)applySetting.Attributes[0]).GetEnumValue();
        }

        public SimulationSetting GetSimulationSetting() {
            var att1 = new SimulationAttributeEnum("Type", Type);

            return new SimulationSetting(this, "Geometry",
                new List<ISimulationAttribute>() { att1 });
        }
    }
}