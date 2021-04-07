using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VTKToVF : MonoBehaviour {
    [Tooltip("Path to CEL Data files containing the extensions .cel, .daten and .vrt \n" +
"STAR-CCM+ export options should be: Export Data: all Regions, Velocity (only vector \"Lab Reference Frame\"," +
" not magnitude or i, j, k; Export Type: Mesh and Solution Data")]
    public string PathToFolder = "";

    [Tooltip("Distance between adjacent gridpoints on any axis, in Millimeter")]
    public float GridStepSize = default;

    public bool ConvertVTKToVectorfield = default;

    [HideInInspector]
    public bool IsComputing = default;

    Dictionary<int, PCachePoint> points;

    private void Update() {
        if (ConvertVTKToVectorfield) {
            ConvertVTKToVectorfield = false;
            StartCoroutine(StartConvert());
        }
    }

    public IEnumerator StartConvert() {
        IsComputing = true;

        var startingTime = DateTime.Now;

        string filenames = FileNameFinder.GetName(PathToFolder, ".vtk");

        Debug.Log("<color=teal> Starting reading data " + filenames + "</color>");
        yield return new WaitForEndOfFrame();

        HemeVTKDataReader reader = new HemeVTKDataReader(PathToFolder, filenames);
        reader.ReadVTKData();

        Debug.Log("<color=teal> Reading complete, starting mapping to grid </color>");
        yield return new WaitForEndOfFrame();

        points = reader.points;

        var gridMapper = new PointcloudToGridMapper(points, GridStepSize);
        gridMapper.StartConvert();

        Debug.Log("<color=teal> Mapping complete, starting writing to vectorfield </color>");
        yield return new WaitForEndOfFrame();

        var writer = new VectorFieldWriter(PathToFolder, filenames);
        writer.WriteToVectorfield(gridMapper.directionGrid);
        //writer.WriteToVectorfield(gridMapper.info);

        Debug.Log("<color=teal> Writing to vectorfield complete </color>");

        var calcTime = (DateTime.Now - startingTime);
        Debug.Log("<color=teal> Everything done in " + (calcTime.Minutes * 60 + calcTime.Seconds) + " seconds. </color>");

        IsComputing = false;
    }
}
