using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HemeSimulation.Settings;
using System;

namespace HemeSimulation.UI.Simulation.Settings {
    public class AttributeNumberController : MonoBehaviour {
        [SerializeField, Tooltip("Name of the setting")]
        Text settingName = default;

        [SerializeField, Tooltip("Number value of the setting")]
        InputField number = default;

        SimulationAttributeNumber attribute = default;

        Color parsableC = new Color(0f, 0f, 0f, 255f);
        Color unparsableC = new Color(210f, 0f, 0f, 255f);

        public void Register(SimulationAttributeNumber attr) {
            attribute = attr;

            settingName.text = attribute.GetName() + ":";
            number.text = Vector3Double.DoubleToString(attribute.GetNumber());

            // Color numbers black if parsable, red if not
            number.onValueChanged.AddListener(delegate { ValidateNumber(); });
        }


        private void ValidateNumber() {
            bool success = double.TryParse(number.text, out double result);

            if (!success)
                ColorNumbertext(false);
            else {
                attribute.SetNumber(result);
                ColorNumbertext(true);
            }
        }

        private void ColorNumbertext(bool parsable) {
            if (parsable)
                number.textComponent.color = parsableC;
            else
                number.textComponent.color = unparsableC;
        }
    }
}