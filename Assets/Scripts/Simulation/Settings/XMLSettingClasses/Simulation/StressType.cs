using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class StressType : ISimulationSetting {

        [XmlAttribute("value")]
        public int Value = 1;

        public void ApplySimulationSetting(SimulationSetting applySetting) {
            Value = (int)((SimulationAttributeNumber)applySetting.Attributes[0]).GetNumber();
        }

        public SimulationSetting GetSimulationSetting() {
            var att = new SimulationAttributeNumber("Value", Value);

            return new SimulationSetting(this, "Stress type",
                new List<ISimulationAttribute>() { att });
        }
    }
}