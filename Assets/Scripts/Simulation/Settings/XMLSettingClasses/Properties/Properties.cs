using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class Properties : ISimulationSetting {

        public PropertyOutput propertyoutput;

        public void ApplySimulationSetting(SimulationSetting applySetting) {
            propertyoutput.ApplySimulationSetting(applySetting.SubSettings[0]);
        }

        public SimulationSetting GetSimulationSetting() {

            return new SimulationSetting(this, "Properties",
                null,
                new List<SimulationSetting>() { propertyoutput.GetSimulationSetting() });
        }
    }
}