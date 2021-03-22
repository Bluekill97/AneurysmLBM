using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class Deletion : ISimulationSetting {

        [XmlAttribute("appliesTo")]
        public GeometryType AppliesTo = GeometryType.Olet;

        [XmlAttribute("effectiveRange")]
        public double EffectiveRange;

        public void ApplySimulationSetting(SimulationSetting applySetting) {
            AppliesTo = (GeometryType)((SimulationAttributeEnum)applySetting.Attributes[0]).GetEnumValue();
            EffectiveRange = ((SimulationAttributeNumber)applySetting.Attributes[1]).GetNumber();
        }

        public SimulationSetting GetSimulationSetting() {
            var att1 = new SimulationAttributeEnum("AppliesTo", AppliesTo);
            var att2 = new SimulationAttributeNumber("Effective range", EffectiveRange);

            return new SimulationSetting(this, "Deletion",
                new List<ISimulationAttribute>() { att1, att2 });
        }
    }
}