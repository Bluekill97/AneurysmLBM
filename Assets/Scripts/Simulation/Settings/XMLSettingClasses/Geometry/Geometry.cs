using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class Geometry : ISimulationSetting{

        public DataFile datafile;

        public void ApplySimulationSetting(SimulationSetting applySetting) {
            datafile.ApplySimulationSetting(applySetting.SubSettings[0]);
        }

        public SimulationSetting GetSimulationSetting() {

            return new SimulationSetting(this, "Geometry",
                null,
                new List<SimulationSetting>() { datafile.GetSimulationSetting()});
        }
    }
}