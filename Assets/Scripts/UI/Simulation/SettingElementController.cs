using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HemeSimulation.Settings;

namespace HemeSimulation.UI.Simulation.Settings {

    /// <summary>
    /// Creates all UI Elements of a setting (one complete line), initializes them and creates all spacers for visualization, Attributes and SubSettings recursively
    /// </summary>
    public class SettingElementController : MonoBehaviour {
        //[SerializeField]
        //private GameObject SettingElementPrefab = default;
        private SimulationsSettingsManager SimSettMgr = default;

        [Tooltip("UI Prefab for the beginning of the line, indicating the hierarchy of the settings")]
        [SerializeField]
        private GameObject SpacerPrefab = default;

        [Tooltip("UI Prefab to display the settings name, first child must have the a UI.Text component")]
        [SerializeField]
        private GameObject NamePrefab = default;

        [Tooltip("UI Prefab to display and enter enum data, root has to have a AttributeEnumController")]
        [SerializeField]
        private GameObject AttributeEnumPrefab = default;

        [Tooltip("UI Prefab to display and enter a numerical value, root has to have a AttributeNumberController")]
        [SerializeField]
        private GameObject AttributeNumberPrefab = default;

        [Tooltip("UI Prefab to display and enter values of a vector, root has to have a AttributeVectorController")]
        [SerializeField]
        private GameObject AttributeVectorPrefab = default;

        [Tooltip("UI Prefab to display and enter a string, root has to have a AttributeStringController")]
        [SerializeField]
        private GameObject AttributeStringPrefab = default;

        /// <summary>
        /// Creates all UI elements for this setting and all subsettings recursively
        /// </summary>
        /// <param name="indentationDepth"> Distance of the setting to the root class </param>
        /// <param name="setting"> Setting to display </param>
        /// <param name="ssmgr"> SimulationsSettingsManager to get the SettingElementController Prefab </param>
        public void Initialize(int indentationDepth, SimulationSetting setting, SimulationsSettingsManager ssmgr) {
            SimSettMgr = ssmgr;
            gameObject.name = setting.Name;

            // Spawn spacer to indicate hierarchy
            for (int i = 0; i < indentationDepth; i++)
                Instantiate(SpacerPrefab, transform);

            // Spawn setting name
            var nameObj = Instantiate(NamePrefab, transform);
            SetName(setting.Name, nameObj);

            // Spawn Attributes and Subsettings (in HTML: Attributes and Elements) of current setting
            SpawnAttributes(setting.Attributes);
            SpawnSubSettings(indentationDepth, setting.SubSettings);
        }

        private void SetName(string name, GameObject nameObj) {
            Transform child = nameObj.transform.GetChild(0);
            UnityEngine.UI.Text text = child.GetComponent<UnityEngine.UI.Text>();

            text.text = name;
        }

        private void SpawnAttributes(List<ISimulationAttribute> attributes) {
            if (attributes == null)
                return;

            GameObject go;
            foreach (ISimulationAttribute attribute in attributes) {
                switch (attribute.GetAttributeType()) {
                    case AttributeType.Enum:
                        var enumAttr = (SimulationAttributeEnum)attribute;

                        go = Instantiate(AttributeEnumPrefab, transform);
                        go.GetComponent<AttributeEnumController>().Register(enumAttr);
                        break;

                    case AttributeType.Number:
                        var numbAttr = (SimulationAttributeNumber)attribute;

                        go = Instantiate(AttributeNumberPrefab, transform);
                        go.GetComponent<AttributeNumberController>().Register(numbAttr);
                        break;

                    case AttributeType.String:
                        var strAttr = (SimulationAttributeString)attribute;

                        go = Instantiate(AttributeStringPrefab, transform);
                        go.GetComponent<AttributeStringController>().Register(strAttr);
                        break;

                    case AttributeType.Vector:
                        var vecAttr = (SimulationAttributeVector)attribute;

                        go = Instantiate(AttributeVectorPrefab, transform);
                        go.GetComponent<AttributeVectorController>().Register(vecAttr);
                        break;
                }
            }
        }

        private void SpawnSubSettings(int currentIndentationDepth, List<SimulationSetting> subsettings) {
            if (subsettings == null)
                return;
            
            foreach (SimulationSetting sett in subsettings) {
                var subsett = Instantiate(SimSettMgr.GetSettingEntryPrefab(), transform.parent);
                subsett.GetComponent<SettingElementController>().Initialize(currentIndentationDepth + 1, sett, SimSettMgr);
            }
        }
    }
}