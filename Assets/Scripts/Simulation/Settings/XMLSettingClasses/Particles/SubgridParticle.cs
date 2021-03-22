using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class SubgridParticle : ISimulationSetting {

        [XmlAttribute("units")]
        public SpaceUnits Units = SpaceUnits.lattice;

        [XmlAttribute("ParticleID")]
        public int ParticleID;

        [XmlAttribute("Radius")]
        public double Radius;

        public InitialPosition initialPosition;

        public void ApplySimulationSetting(SimulationSetting applySetting) {
            Units = (SpaceUnits)((SimulationAttributeEnum)applySetting.Attributes[0]).GetEnumValue();
            ParticleID = (int)((SimulationAttributeNumber)applySetting.Attributes[1]).GetNumber();
            Radius = ((SimulationAttributeNumber)applySetting.Attributes[2]).GetNumber();

            initialPosition.ApplySimulationSetting(applySetting.SubSettings[0]);
        }

        public SimulationSetting GetSimulationSetting() {
            var att1 = new SimulationAttributeEnum("Units", Units);
            var att2 = new SimulationAttributeNumber("Particle ID", ParticleID);
            var att3 = new SimulationAttributeNumber("Radius", Radius);

            return new SimulationSetting(this, "Subgrid particle",
                new List<ISimulationAttribute>() { att1, att2, att3 },
                new List<SimulationSetting>() { initialPosition.GetSimulationSetting()});
        }
    }
}