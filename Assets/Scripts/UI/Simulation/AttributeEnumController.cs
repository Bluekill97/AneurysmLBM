using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HemeSimulation.Settings;
using System;

namespace HemeSimulation.UI.Simulation.Settings {
    public class AttributeEnumController : MonoBehaviour {
        [SerializeField, Tooltip("Name of the setting")]
        Text settingName = default;

        [SerializeField, Tooltip("Enum value of the setting")]
        Dropdown dd = default;

        SimulationAttributeEnum attribute = default;

        public void Register(SimulationAttributeEnum attr) {
            attribute = attr;
            PopulateDropdown();

            settingName.text = attribute.GetName() + ":";

            dd.value = attribute.GetEnumValue();

            dd.onValueChanged.AddListener(delegate { ValueChanged(); });
        }

        private void ValueChanged() {
            attribute.SetEnumValue(dd.value);
        }

        private void PopulateDropdown() {
            dd.ClearOptions();

            List<Dropdown.OptionData> ddos = new List<Dropdown.OptionData>();

            foreach (string opt in attribute.GetEnumChoices()) {
                var ddo = new Dropdown.OptionData();
                ddo.text = opt;
                ddos.Add(ddo);
            }

            dd.AddOptions(ddos);
        }
    }
}