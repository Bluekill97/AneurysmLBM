using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class Condition : ISimulationSetting {

        [XmlAttribute("subtype")]
        public TrigonometricFunctions Subtype = TrigonometricFunctions.cosine;

        [XmlAttribute("type")]
        public FieldType Type = FieldType.pressure;

        public Amplitude amplitude;
        public Mean mean;
        public Phase phase;
        public Period period;

        public void ApplySimulationSetting(SimulationSetting applySetting) {
            Subtype = (TrigonometricFunctions)((SimulationAttributeEnum)applySetting.Attributes[0]).GetEnumValue();
            Type= (FieldType)((SimulationAttributeEnum)applySetting.Attributes[0]).GetEnumValue();

            amplitude.ApplySimulationSetting(applySetting.SubSettings[0]);
            mean.ApplySimulationSetting(applySetting.SubSettings[1]);
            phase.ApplySimulationSetting(applySetting.SubSettings[2]);
            period.ApplySimulationSetting(applySetting.SubSettings[3]);
        }

        public SimulationSetting GetSimulationSetting() {
            var att1 = new SimulationAttributeEnum("Subtype", Subtype);
            var att2 = new SimulationAttributeEnum("Type", Type);

            var list = new List<SimulationSetting>();
            list.Add(amplitude.GetSimulationSetting());
            list.Add(mean.GetSimulationSetting());
            list.Add(phase.GetSimulationSetting());
            list.Add(period.GetSimulationSetting());

            return new SimulationSetting(this, "Condition",
                new List<ISimulationAttribute>() { att1, att2 },
                list);
        }
    }
}