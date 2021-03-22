using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class BodyForces : ISimulationSetting {

        [XmlElement(ElementName = "gravitic", IsNullable = true)]
        public Gravitic gravitic;

        [XmlElement(ElementName = "magnetic", IsNullable = true)]
        public Magnetic magnetic;

        /// <summary>
        /// Information for the XML serializer to completly omit this element if its null
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializegravitic() {
            return gravitic != null;
        }

        /// <summary>
        /// Information for the XML serializer to completly omit this element if its null
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializemagnetic() {
            return magnetic != null;
        }

        public void ApplySimulationSetting(SimulationSetting applySetting) {
            if (applySetting.SubSettings[0] != null) {
                if (!ShouldSerializegravitic())
                    gravitic = new Gravitic();

                gravitic.ApplySimulationSetting(applySetting.SubSettings[0]);
            }

            if (applySetting.SubSettings[1] != null) {
                if (!ShouldSerializemagnetic())
                    magnetic = new Magnetic();

                magnetic.ApplySimulationSetting(applySetting.SubSettings[1]);
            }
        }

        public SimulationSetting GetSimulationSetting() {
            var list = new List<SimulationSetting>();

            if (ShouldSerializegravitic())
                list.Add(gravitic.GetSimulationSetting());

            if (ShouldSerializemagnetic())
                list.Add(magnetic.GetSimulationSetting());

            return new SimulationSetting(this, "Body Forces",
                null,
                list);
        }
    }
}