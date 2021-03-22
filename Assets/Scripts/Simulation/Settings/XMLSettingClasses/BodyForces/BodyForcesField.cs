using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class BodyForcesField : ISimulationSetting {

        [XmlAttribute("units")]
        public string Units = "m/s^2";

        [XmlAttribute("x")]
        public double X;

        [XmlAttribute("y")]
        public double Y;

        [XmlAttribute("z")]
        public double Z;

        public void ApplySimulationSetting(SimulationSetting applySetting) {
            Units = ((SimulationAttributeString)applySetting.Attributes[0]).GetStringValue();
            X = ((SimulationAttributeNumber)applySetting.Attributes[1]).GetNumber();
            Y = ((SimulationAttributeNumber)applySetting.Attributes[2]).GetNumber();
            Z = ((SimulationAttributeNumber)applySetting.Attributes[3]).GetNumber();
        }

        public SimulationSetting GetSimulationSetting() {
            var att1 = new SimulationAttributeString("Units", Units);
            var att2 = new SimulationAttributeNumber("Value", X);
            var att3 = new SimulationAttributeNumber("Value", Y);
            var att4 = new SimulationAttributeNumber("Value", Z);

            return new SimulationSetting(this, "Field",
                new List<ISimulationAttribute>() { att1, att2, att3, att4 });
        }
    }
}