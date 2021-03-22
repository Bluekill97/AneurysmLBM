using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class DataFile : ISimulationSetting {

        [XmlAttribute("path")]
        public string Path = "";

        public void ApplySimulationSetting(SimulationSetting applySetting) {
            Path = ((SimulationAttributeString)applySetting.Attributes[0]).GetStringValue();
        }

        public SimulationSetting GetSimulationSetting() {
            var att1 = new SimulationAttributeString("Path", Path);

            return new SimulationSetting(this, "Data file",
                new List<ISimulationAttribute>() { att1 });
        }
    }
}