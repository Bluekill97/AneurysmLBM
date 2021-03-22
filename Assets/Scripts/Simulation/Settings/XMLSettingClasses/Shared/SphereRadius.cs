using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class SphereRadius : ISimulationSetting {

        [XmlAttribute("units")]
        public SpaceUnits Units = SpaceUnits.lattice;

        [XmlAttribute("value")]
        public double Value;

        public void ApplySimulationSetting(SimulationSetting applySetting) {
            Units = (SpaceUnits)((SimulationAttributeEnum)applySetting.Attributes[0]).GetEnumValue();
            Value = ((SimulationAttributeNumber)applySetting.Attributes[1]).GetNumber();
        }

        public SimulationSetting GetSimulationSetting() {
            var att1 = new SimulationAttributeEnum("Units", Units);
            var att2 = new SimulationAttributeNumber("Value", Value);

            return new SimulationSetting(this, "Sphere radius",
                new List<ISimulationAttribute>() { att1, att2 });
        }
    }
}