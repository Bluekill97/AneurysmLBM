using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class BoundaryConditions : ISimulationSetting {

        public Lubrication lubrication;
        public Deletion deletion;
        public Spherical spherical;

        public void ApplySimulationSetting(SimulationSetting applySetting) {
            lubrication.ApplySimulationSetting(applySetting.SubSettings[0]);
            deletion.ApplySimulationSetting(applySetting.SubSettings[1]);
            spherical.ApplySimulationSetting(applySetting.SubSettings[2]);
        }

        public SimulationSetting GetSimulationSetting() {
            var list = new List<SimulationSetting>();
            list.Add(lubrication.GetSimulationSetting());
            list.Add(deletion.GetSimulationSetting());
            list.Add(spherical.GetSimulationSetting());

            return new SimulationSetting(this, "Boundary conditions",
                null,
                list);
        }
    }
}