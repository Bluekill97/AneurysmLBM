using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HemeSimulation.Settings {
    public interface ISimulationSetting {
        SimulationSetting GetSimulationSetting();

        void ApplySimulationSetting(SimulationSetting applySetting);
    }
}