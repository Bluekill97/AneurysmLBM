using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class Monitoring : ISimulationSetting {

        // TODO: (if more options available) create more options, hide if necessary nullable 
        // classes like in BodyForces
        public Incompressibility incompressibility;

        public void ApplySimulationSetting(SimulationSetting applySetting) {
            incompressibility.ApplySimulationSetting(applySetting.SubSettings[0]);
        }

        public SimulationSetting GetSimulationSetting() {
            var list = new List<SimulationSetting>();
            list.Add(incompressibility.GetSimulationSetting());

            return new SimulationSetting(this, "Monitoring",
                null,
                list);
        }
    }
}