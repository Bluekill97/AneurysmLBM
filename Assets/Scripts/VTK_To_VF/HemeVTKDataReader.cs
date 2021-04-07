using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;


public class HemeVTKDataReader {
    public Dictionary<int, PCachePoint> points { get; private set; }
    public Dictionary<int, float> pressure { get; private set; }

    private string _pathToFolder;
    private string _filenames;

    private StreamReader reader;

    //if we see that a read line belongs to another data structure -> store it here
    private string currentLine = default;



    private string removedLines = "Removed Lines:";

    public HemeVTKDataReader(string pathToFolder, string filenames) {
        _pathToFolder = pathToFolder;
        _filenames = filenames;
    }

    public void ReadVTKData() {
        points = new Dictionary<int, PCachePoint>();
        reader = new StreamReader(_pathToFolder + @"\" + _filenames + ".vtk");

        ReadPoints();
        ReadVertexIDs();


        //TODO: read scalar + vector information generic
        ReadPressure();
        ReadVelocity();

        reader.Close();
    }

    private void ReadPoints() {

        /*
         * File starts with Header
         * Point position information after line:
         * "POINTS x type" with x = pointcount and type = datatype (should be float)
         */
        bool dataStartDetected = false;
        while (!dataStartDetected) {
            currentLine = reader.ReadLine();

            if (currentLine.StartsWith("POINTS"))
                dataStartDetected = true;
            else
                removedLines += "\n" + currentLine;
        }

        //get number of points
        string[] info = currentLine.Split(' ');
        int pointCount = int.Parse(info[1], CultureInfo.InvariantCulture.NumberFormat);

        currentLine = reader.ReadLine();

        //read point data
        int pointID = 0;

        //store float values here, if enough floats (3) are present -> create a point from these floats
        //i did this in case a point starts in one line and ends in another (although i am not sure anymore if this occurs)
        List<float> storage = new List<float>();

        int linecounter = 1;

        while (pointID != pointCount) {
            if (currentLine.EndsWith(" "))
                currentLine = currentLine.Remove(currentLine.Length - 1); //else we get an empty array entry at the end in the next line 

            string[] parts = currentLine.Split(' ');

            foreach (string part in parts) {

                try {
                    //read next number
                    float f = float.Parse(part, CultureInfo.InvariantCulture);
                    storage.Add(f);
                }
                catch (System.Exception) {
                    Debug.LogError("<color=red>Couldn't parse line " + linecounter + " while reading points</color>");
                    Debug.LogError("the line is: >" + currentLine + "<");
                    return;
                }

                //enough floats? -> create new point
                if (storage.Count == 3)
                    StorageToPoint();
            }

            linecounter++;
            currentLine = reader.ReadLine();
        }

        void StorageToPoint() {

            points.Add(
                pointID,
                new PCachePoint(
                    0,  //unknown
                    new Vector3(storage[0], storage[1], storage[2]),
                    Vector3.zero)); //is added later

            storage.Clear();
            pointID++;
        }

        Debug.Log("ReadPoints: " + points.Count + " from " + pointCount);
    }

    private void ReadVertexIDs() {

        if (currentLine == null)
            return;

        // we don't need this information, so just skip it until pressure values come
        while (!currentLine.StartsWith("POINT_DATA"))
            currentLine = reader.ReadLine();
    }

    //TODO: rename to "read scalar data", change to reading multiple parameters
    private void ReadPressure() {
        // currently in line with "POINT_DATA" at the beginning, e.g. POINT_DATA 8884395

        // now in line with FIELD at the beginning, e.g. FIELD FieldData 2
        currentLine = reader.ReadLine();

        // now in line with value name at the beginning, e.g. pressure 1 8884395 double
        // (i think pressure = name, 1 = number values of one data point, 8884395 = number of data points, double = number type)
        currentLine = reader.ReadLine();

        //go into the first line with scalar values
        currentLine = reader.ReadLine();

        int currentPointID = 0;
        int maxPointID = points.Keys.Count;
        pressure = new Dictionary<int, float>();

        while (currentPointID != maxPointID) {

            if (currentLine.EndsWith(" "))
                currentLine = currentLine.Remove(currentLine.Length - 1); //else we get an empty array entry at the end in the next line 

            string[] parts = currentLine.Split(' ');

            foreach (string part in parts) {

                float f;
                if (float.TryParse(part.Split('.')[0], out f)) {

                }
                else {
                    Debug.Log("Failed parsing value >" + part + "<");
                    f = 0;
                }

                pressure[currentPointID] = f;
                currentPointID++;
            }

            currentLine = reader.ReadLine();
        }

        //TODO: read scalar info
    }

    private void ReadVelocity() {
        while (!currentLine.StartsWith("velocity"))
            currentLine = reader.ReadLine();

        //in line with "velocity" at the beginning, e.g. velocity 3 8884395 double

        //go into the first line with data
        currentLine = reader.ReadLine();

        //read point data
        int pointID = 0;

        //store float values here, if enough floats (3) are present -> create a point from these floats
        //i did this in case a point starts in one line and ends in another (although i am not sure anymore if this occurs)
        List<float> storage = new List<float>();
        int pointCount = points.Count;

        while (pointID < pointCount) {
            if (currentLine.EndsWith(" "))
                currentLine = currentLine.Remove(currentLine.Length - 1); //else we get an empty array entry at the end in the next line 

            string[] parts = currentLine.Split(' ');

            foreach (string part in parts) {

                try {
                    //read next number
                    float f = float.Parse(part, CultureInfo.InvariantCulture);
                    storage.Add(f);
                }
                catch (System.Exception) {
                    Debug.LogError("the line is: >" + currentLine + "<");
                    return;
                }

                //enough floats? -> create new point
                if (storage.Count == 3)
                    StorageToPointDirection();
            }

            currentLine = reader.ReadLine();
        }

        void StorageToPointDirection() {
            points[pointID].SetDirection(new Vector3(storage[0], storage[1], storage[2]));

            storage.Clear();
            pointID++;
        }
    }
}
