using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class BodyForcesPosition : ISimulationSetting {

        [XmlAttribute("units")]
        public SpaceUnits Units = SpaceUnits.lattice;

        [XmlAttribute("x")]
        public double X;

        [XmlAttribute("y")]
        public double Y;

        [XmlAttribute("z")]
        public double Z;

        public void ApplySimulationSetting(SimulationSetting applySetting) {
            Units = (SpaceUnits)((SimulationAttributeEnum)applySetting.Attributes[0]).GetEnumValue();
            X = ((SimulationAttributeNumber)applySetting.Attributes[1]).GetNumber();
            Y = ((SimulationAttributeNumber)applySetting.Attributes[2]).GetNumber();
            Z = ((SimulationAttributeNumber)applySetting.Attributes[3]).GetNumber();
        }

        public SimulationSetting GetSimulationSetting() {
            var att1 = new SimulationAttributeEnum("Units", Units);
            var att2 = new SimulationAttributeNumber("Value", X);
            var att3 = new SimulationAttributeNumber("Value", Y);
            var att4 = new SimulationAttributeNumber("Value", Z);

            return new SimulationSetting(this, "Position",
                new List<ISimulationAttribute>() { att1, att2, att3, att4 });
        }
    }
}