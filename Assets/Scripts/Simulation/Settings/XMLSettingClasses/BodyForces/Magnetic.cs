using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class Magnetic : ISimulationSetting {

        [XmlAttribute("forceName")]
        public ForceNames ForceName = ForceNames.dipolar;

        public MagneticMoment magneticMoment;
        public BodyForcesPosition position;

        public void ApplySimulationSetting(SimulationSetting applySetting) {
            ForceName = (ForceNames)((SimulationAttributeEnum)applySetting.Attributes[0]).GetEnumValue();

            magneticMoment.ApplySimulationSetting(applySetting.SubSettings[0]);
            position.ApplySimulationSetting(applySetting.SubSettings[1]);
        }

        public SimulationSetting GetSimulationSetting() {
            var att1 = new SimulationAttributeEnum("Force name", ForceName);

            return new SimulationSetting(this, "Magnetic",
                new List<ISimulationAttribute>() { att1 },
                new List<SimulationSetting>() { magneticMoment.GetSimulationSetting(),
                    position.GetSimulationSetting()});
        }
    }
}