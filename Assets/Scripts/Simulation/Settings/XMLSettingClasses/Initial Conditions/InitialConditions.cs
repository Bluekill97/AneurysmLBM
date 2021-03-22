using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class InitialConditions : ISimulationSetting {

        public Pressure pressure;

        public void ApplySimulationSetting(SimulationSetting applySetting) {
            pressure.ApplySimulationSetting(applySetting.SubSettings[0]);
        }

        public SimulationSetting GetSimulationSetting() {
            var list = new List<SimulationSetting>();
            list.Add(pressure.GetSimulationSetting());

            return new SimulationSetting(this, "Initial conditions",
                null,
                list);
        }
    }
}