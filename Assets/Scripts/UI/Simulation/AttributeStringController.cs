using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HemeSimulation.Settings;
using System;

namespace HemeSimulation.UI.Simulation.Settings {
    public class AttributeStringController : MonoBehaviour {
        [SerializeField, Tooltip("Name of the setting")]
        Text settingName = default;

        [SerializeField, Tooltip("Text value of the setting")]
        InputField text = default;

        SimulationAttributeString attribute = default;

        public void Register(SimulationAttributeString attr) {
            attribute = attr;

            settingName.text = attribute.GetName() + ":";
            text.text = attribute.GetStringValue();

            text.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        }

        private void ValueChangeCheck() {
            attribute.SetString(text.text);
        }
    }
}