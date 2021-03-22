using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HemeSimulation.Settings {

    /// <summary>
    /// Interface to hold all different types of Attributes
    /// </summary>
    public interface ISimulationAttribute {

        /// <summary>
        /// Name of the Attribute itself
        /// </summary>
        /// <returns></returns>
        string GetName();

        /// <summary>
        /// Type of the Attribute to cast it back to its original class
        /// </summary>
        /// <returns> Type of the Attribute </returns>
        AttributeType GetAttributeType();

        /// <summary>
        /// Get reference to the containing Setting to save changes
        /// </summary>
        /// <param name="containingSetting"> Setting containing this Attribute </param>
        void RegisterContainingSetting(SimulationSetting containingSetting);
    }

    public enum AttributeType {
        Enum,
        Number,
        String,
        Vector
    }
}