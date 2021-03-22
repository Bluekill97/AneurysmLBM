using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class Inlet : ISimulationSetting {

        public Condition condition;
        public Normal normal;
        public InOutLetPosition position;

        public void ApplySimulationSetting(SimulationSetting applySetting) {
            condition.ApplySimulationSetting(applySetting.SubSettings[0]);
            normal.ApplySimulationSetting(applySetting.SubSettings[1]);
            position.ApplySimulationSetting(applySetting.SubSettings[2]);
        }

        public SimulationSetting GetSimulationSetting() {
            var list = new List<SimulationSetting>();
            list.Add(condition.GetSimulationSetting());
            list.Add(normal.GetSimulationSetting());
            list.Add(position.GetSimulationSetting());

            return new SimulationSetting(this, "In/ Outlet",
                null,
                list);
        }
    }
}