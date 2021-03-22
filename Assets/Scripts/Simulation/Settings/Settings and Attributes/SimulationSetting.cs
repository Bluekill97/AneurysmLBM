using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HemeSimulation.Settings {
    /// <summary>
    /// Class to transport all information from the HemeLBSettings class to the UI
    /// without explicitly accessing and creating UI elements for every individual class 
    /// </summary>
    public class SimulationSetting {
        /// <summary>
        /// Name of the setting
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// List of all Attributes (Properties of this setting)
        /// </summary>
        public List<ISimulationAttribute> Attributes { get; private set; }

        /// <summary>
        /// List of all child settings
        /// </summary>
        public List<SimulationSetting> SubSettings { get; private set; }

        /// <summary>
        /// Holds the reference to the class where this setting is created from, 
        /// important to bring the updated infos back to the original class
        /// </summary>
        private ISimulationSetting OriginalSetting = default;

        public SimulationSetting(ISimulationSetting originalSettingclass,
            string name,
            List<ISimulationAttribute> attributes = null,
            List<SimulationSetting> subSettings = null) {

            Name = name;
            Attributes = attributes;
            SubSettings = subSettings;
            OriginalSetting = originalSettingclass;

            RegisterInAttributes();
        }

        public void RegisterAttributeChange(ISimulationAttribute updateAttr) {

            bool attributeFound = false;

            for (int i = 0; i < Attributes.Count; i++) {
                ISimulationAttribute compareAttr = Attributes[i];

                // If name or type doesn't fit -> skip
                if (updateAttr.GetAttributeType() != compareAttr.GetAttributeType() ||
                    updateAttr.GetName() != compareAttr.GetName())
                    continue;

                // Else update
                compareAttr = updateAttr;
                /*
                switch (updateAttr.GetAttributeType()) {
                    case AttributeType.Enum:
                        var enumAttr = (SimulationAttributeEnum)compareAttr;
                        var enumAttrNew = (SimulationAttributeEnum)updateAttr;

                        enumAttr.SetEnumValue(enumAttrNew.GetEnumValue());
                        break;

                    case AttributeType.Number:
                        var numbAttr = (SimulationAttributeNumber)compareAttr;
                        var numbAttrNew = (SimulationAttributeNumber)updateAttr;

                        numbAttr.SetNumber(numbAttrNew.GetNumber());
                        break;

                    case AttributeType.String:
                        var strAttr = (SimulationAttributeString)compareAttr;
                        var strAttrNew = (SimulationAttributeString)updateAttr;

                        strAttr.SetString(strAttrNew.GetStringValue());
                        break;

                    case AttributeType.Vector:
                        var vecAttr = (SimulationAttributeVector)compareAttr;
                        var vecAttrNew = (SimulationAttributeVector)updateAttr;

                        vecAttr.SetVector(vecAttrNew.GetVectorDouble());
                        break;
                } */

                attributeFound = true;
                break;
            }


            if (attributeFound)
                OriginalSetting.ApplySimulationSetting(this);
            else
                Debug.LogError("Couldn't match attribute " + updateAttr.GetName() +
                    " to setting " + this.Name);
        }

        public bool IsEqual(ISimulationSetting otherISet) {
            return this.IsEqual(otherISet.GetSimulationSetting());
        }

        public bool IsEqual(SimulationSetting other) {
            Debug.Log("<color=blue> HILFE NIMM MICH RAUS </color>");
            return false;

            if (other.Name != this.Name)
                return false;

            // Compare subsettings
            if (SubSettings != null && other.SubSettings != null)
                if (SubSettings.Count == other.SubSettings.Count)
                    for (int i = 0; i < SubSettings.Count; i++)
                        if (SubSettings[i].Name != other.SubSettings[i].Name)
                            return false;
                        else { }
                else
                    return false;

            // Compare Attributes
            else {
                if (Attributes != null && other.Attributes != null)
                    if (Attributes.Count == other.Attributes.Count)
                        for (int i = 0; i < Attributes.Count; i++)
                            if (Attributes[i].GetName() != other.Attributes[i].GetName())
                                return false;
                            else { }
                    else
                        return false;
            }

            // Only return true if there is no mismatch in the first order subsettings and attributes
            return true;
        }

        private void RegisterInAttributes() {
            if (Attributes == null)
                return;

            foreach (ISimulationAttribute attr in Attributes)
                attr.RegisterContainingSetting(this);
        }
    }
}