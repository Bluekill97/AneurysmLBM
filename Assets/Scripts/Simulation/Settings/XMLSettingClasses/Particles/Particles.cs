using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class Particles : ISimulationSetting {

        [XmlElement("subgridParticle")]
        public List<SubgridParticle> SubgridParticles { get; set; }

        public SphereRadius sphereRadius;
        public SphereCentre sphereCentre;
        public EmissionCount emissionCount;
        public EmissionItrvl emissionItrvl;

        public void ApplySimulationSetting(SimulationSetting applySetting) {
            sphereRadius.ApplySimulationSetting(applySetting.SubSettings[0]);
            sphereCentre.ApplySimulationSetting(applySetting.SubSettings[1]);
            emissionCount.ApplySimulationSetting(applySetting.SubSettings[2]);
            emissionItrvl.ApplySimulationSetting(applySetting.SubSettings[3]);

            for (int i = 4; i < applySetting.SubSettings.Count; i++) {

                // Add fields if necessary
                if (SubgridParticles.Count <= i)
                    SubgridParticles.Add(new SubgridParticle());

                SubgridParticles[i].ApplySimulationSetting(applySetting.SubSettings[i]);
            }

            // Remove fields in case they got deleted
            int countDiff = SubgridParticles.Count - applySetting.SubSettings.Count;
            if (countDiff > 0)
                SubgridParticles.RemoveRange(applySetting.SubSettings.Count, countDiff);
        }

        public SimulationSetting GetSimulationSetting() {
            var list = new List<SimulationSetting>();
            list.Add(sphereRadius.GetSimulationSetting());
            list.Add(sphereCentre.GetSimulationSetting());
            list.Add(emissionCount.GetSimulationSetting());
            list.Add(emissionItrvl.GetSimulationSetting());

            foreach (SubgridParticle sp in SubgridParticles) 
                list.Add(sp.GetSimulationSetting());

            return new SimulationSetting(this, "Particles",
                null,
                list);
        }
    }
}
