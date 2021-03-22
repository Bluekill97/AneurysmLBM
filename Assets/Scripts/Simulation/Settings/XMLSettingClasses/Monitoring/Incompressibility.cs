using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class Incompressibility : ISimulationSetting {
        public void ApplySimulationSetting(SimulationSetting applySetting) {
            
        }

        public SimulationSetting GetSimulationSetting() {
            return new SimulationSetting(this, "Incompressibility");
        }
    }
}