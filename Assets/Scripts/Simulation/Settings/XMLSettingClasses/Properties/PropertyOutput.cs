using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class PropertyOutput : ISimulationSetting {

        [XmlAttribute("file")]
        public string File = "whole.dat";

        [XmlAttribute("period")]
        public int Period;

        public PropertiesGeometry geometry;

        [XmlElement("field")]
        public List<PropertiesField> Fields { get; set; }

        public void ApplySimulationSetting(SimulationSetting applySetting) {
            File = ((SimulationAttributeString)applySetting.Attributes[0]).GetStringValue();
            Period = (int)((SimulationAttributeNumber)applySetting.Attributes[1]).GetNumber();

            for (int i = 0; i < applySetting.SubSettings.Count; i++) {

                // Add fields if necessary
                if (Fields.Count <= i)
                    Fields.Add(new PropertiesField());

                Fields[i].ApplySimulationSetting(applySetting.SubSettings[i]);
            }

            // Remove fields in case they got deleted
            int countDiff = Fields.Count - applySetting.SubSettings.Count;
            if (countDiff > 0)
                Fields.RemoveRange(applySetting.SubSettings.Count, countDiff);
        }

        public SimulationSetting GetSimulationSetting() {
            var att1 = new SimulationAttributeString("File", File);
            var att2 = new SimulationAttributeNumber("Period", Period);

            List<SimulationSetting> subSetts = new List<SimulationSetting>();
            foreach (var f in Fields)
                subSetts.Add(f.GetSimulationSetting());

            return new SimulationSetting(this, "Property output",
                new List<ISimulationAttribute>() { att1, att2 },
                subSetts);
        }
    }
}