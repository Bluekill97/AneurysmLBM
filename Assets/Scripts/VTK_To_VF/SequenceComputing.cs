using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceComputing : MonoBehaviour {

    public VTKToVF VTKToVFInstance = default;

    [Tooltip("Path to superfolder of the VTK Data files ")]
    public string PathToSuperFolder = "";

    [Tooltip("Name of the Subfolders containing the VTK files")]
    public List<string> Subfolders = new List<string>();

    [Tooltip("Distance between adjacent gridpoints on any axis, in Millimeter (overwrites the value of the VTKToVF script)")]
    public float GridStepSize = default;

    public bool StartSequenceCompute = false;

    void Update() {
        if (StartSequenceCompute) {
            StartSequenceCompute = false;
            StartCoroutine(VTKToVFSequence());
        }
    }

    IEnumerator VTKToVFSequence() {
        VTKToVFInstance.GridStepSize = GridStepSize;

        int sequenceCounter = 1;
        foreach (string subfolderName in Subfolders) {
            yield return new WaitForEndOfFrame();

            //get complete path, add \ if necessary
            string completePath = PathToSuperFolder.EndsWith("\\") ?
               PathToSuperFolder + subfolderName :
               PathToSuperFolder + "\\" + subfolderName;

            Debug.Log("path: " + completePath);

            //set correct path
            VTKToVFInstance.PathToFolder = completePath;

            VTKToVFInstance.ConvertVTKToVectorfield = true;

            yield return new WaitForEndOfFrame();

            while (VTKToVFInstance.IsComputing)
                yield return new WaitForEndOfFrame();

            Debug.Log("<color=green> SEQUENCE " + sequenceCounter++ + " OF " + Subfolders.Count + " DONE</color>");
        }
    }
}
