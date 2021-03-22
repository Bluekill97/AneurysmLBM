using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class PropertiesField : ISimulationSetting{

        [XmlAttribute("type")]
        public FieldType Type = FieldType.velocity;

        public void ApplySimulationSetting(SimulationSetting applySetting) {
            Type = (FieldType)((SimulationAttributeEnum)applySetting.Attributes[0]).GetEnumValue();
        }

        public SimulationSetting GetSimulationSetting() {
            var att1 = new SimulationAttributeEnum("Units", Type);

            return new SimulationSetting(this, "Field",
                new List<ISimulationAttribute>() { att1 });
        }
    }
}