using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class Pressure : ISimulationSetting {

        public Uniform uniform;

        public void ApplySimulationSetting(SimulationSetting applySetting) {
            uniform.ApplySimulationSetting(applySetting.SubSettings[0]);
        }

        public SimulationSetting GetSimulationSetting() {
            var list = new List<SimulationSetting>();
            list.Add(uniform.GetSimulationSetting());

            return new SimulationSetting(this, "Pressure",
                null,
                list);
        }
    }
}