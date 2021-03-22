using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class SimulationAttributeNumber : ISimulationAttribute {
        string Name;
        double Number;
        private SimulationSetting ContainingSetting;

        public SimulationAttributeNumber(string name, double number) {
            Name = name;
            Number = number;
        }

        public void RegisterContainingSetting(SimulationSetting containingSetting) {
            ContainingSetting = containingSetting;
        }

        public AttributeType GetAttributeType() {
            return AttributeType.Number;
        }

        public string GetName() {
            return Name;
        }

        public double GetNumber() {
            return Number;
        }

        public void SetNumber(double newNum) {
            Number = newNum;
            ContainingSetting.RegisterAttributeChange(this);
        }
    }
}