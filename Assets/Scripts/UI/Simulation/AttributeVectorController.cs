using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HemeSimulation.Settings;

namespace HemeSimulation.UI.Simulation.Settings {
    public class AttributeVectorController : MonoBehaviour {
        [SerializeField, Tooltip("Name of the setting")]
        Text settingName = default;

        [SerializeField, Tooltip("X value of the vector setting")]
        InputField x = default;

        [SerializeField, Tooltip("Y value of the vector setting")]
        InputField y = default;

        [SerializeField, Tooltip("Z value of the vector setting")]
        InputField z = default;

        SimulationAttributeVector attribute = default;

        Color parsableC = new Color(0f, 0f, 0f, 255f);
        Color unparsableC = new Color(210f, 0f, 0f, 255f);


        public void Register(SimulationAttributeVector attr) {
            attribute = attr;

            settingName.text = attribute.GetName() + ":";
            x.text = Vector3Double.DoubleToString(attribute.GetVectorDouble().X);
            y.text = Vector3Double.DoubleToString(attribute.GetVectorDouble().Y);
            z.text = Vector3Double.DoubleToString(attribute.GetVectorDouble().Z);

            // Color numbers black if parsable, red if not
            x.onValueChanged.AddListener(delegate { UpdateValueX(); });
            y.onValueChanged.AddListener(delegate { UpdateValueY(); });
            z.onValueChanged.AddListener(delegate { UpdateValueZ(); });
        }

        private void UpdateValueX() {
            bool success = double.TryParse(x.text, out double result);

            if (success) {
                // Set x value (set separate in case another number is not parsable)
                var vec = attribute.GetVectorDouble();
                vec.X = result;
                attribute.SetVector(vec);

                x.textComponent.color = parsableC;
            }
            else
                x.textComponent.color = unparsableC;
        }

        private void UpdateValueY() {
            bool success = double.TryParse(y.text, out double result);

            if (success) {
                // Set y value (set separate in case another number is not parsable)
                var vec = attribute.GetVectorDouble();
                vec.Y = result;
                attribute.SetVector(vec);

                y.textComponent.color = parsableC;
            }
            else
                y.textComponent.color = unparsableC;
        }

        private void UpdateValueZ() {
            bool success = double.TryParse(z.text, out double result);

            if (success) {
                // Set z value (set separate in case another number is not parsable)
                var vec = attribute.GetVectorDouble();
                vec.Z = result;
                attribute.SetVector(vec);

                z.textComponent.color = parsableC;
            }
            else
                z.textComponent.color = unparsableC;
        }
    }
}