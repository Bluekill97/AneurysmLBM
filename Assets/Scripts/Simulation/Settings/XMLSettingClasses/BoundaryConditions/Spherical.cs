using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class Spherical : ISimulationSetting {

        [XmlAttribute("appliesTo")]
        public GeometryType AppliesTo = GeometryType.Sphr;

        public SphereRadius sphereRadius;
        public SphereCentre sphereCentre;

        public void ApplySimulationSetting(SimulationSetting applySetting) {
            AppliesTo = (GeometryType)((SimulationAttributeEnum)applySetting.Attributes[0]).GetEnumValue();

            sphereRadius.ApplySimulationSetting(applySetting.SubSettings[0]);
            sphereCentre.ApplySimulationSetting(applySetting.SubSettings[1]);
        }

        public SimulationSetting GetSimulationSetting() {
            var att1 = new SimulationAttributeEnum("AppliesTo", AppliesTo);

            return new SimulationSetting(this, "Spherical",
                new List<ISimulationAttribute>() { att1 },
                new List<SimulationSetting>() { sphereRadius.GetSimulationSetting(), sphereCentre.GetSimulationSetting() });
        }
    }
}