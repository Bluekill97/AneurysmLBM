using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class Gravitic : ISimulationSetting {

        [XmlAttribute("forceName")]
        public ForceNames ForceName = ForceNames.gravity;

        public BodyForcesField field;

        public void ApplySimulationSetting(SimulationSetting applySetting) {
            ForceName = (ForceNames)((SimulationAttributeEnum)applySetting.Attributes[0]).GetEnumValue();

            field.ApplySimulationSetting(applySetting.SubSettings[0]);
        }

        public SimulationSetting GetSimulationSetting() {
            var att1 = new SimulationAttributeEnum("Force name", ForceName);

            return new SimulationSetting(this, "Gravitic",
                new List<ISimulationAttribute>() { att1 },
                new List<SimulationSetting>() { field.GetSimulationSetting() }); ;
        }
    }
}