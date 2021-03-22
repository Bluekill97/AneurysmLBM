using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class Simulation : ISimulationSetting{

        //[XmlAttribute("step_length")]
        public StepLength step_length;

        //[XmlAttribute("steps")]
        public Steps steps;

        //[XmlAttribute("stresstype")]
        public StressType stresstype;

        //[XmlAttribute("voxel_size")]
        public VoxelSize voxel_size;

        //[XmlAttribute("origin")]
        public Origin origin;

        public void ApplySimulationSetting(SimulationSetting applySetting) {
            step_length.ApplySimulationSetting(applySetting.SubSettings[0]);
            steps.ApplySimulationSetting(applySetting.SubSettings[1]);
            stresstype.ApplySimulationSetting(applySetting.SubSettings[2]);
            voxel_size.ApplySimulationSetting(applySetting.SubSettings[3]);
            origin.ApplySimulationSetting(applySetting.SubSettings[4]);
        }

        public SimulationSetting GetSimulationSetting() {

            return new SimulationSetting(this, "Simulation",
                null,
                new List<SimulationSetting>() { step_length.GetSimulationSetting(),
                steps.GetSimulationSetting(),
                stresstype.GetSimulationSetting(),
                voxel_size.GetSimulationSetting(),
                origin.GetSimulationSetting()});
        }
    }
}