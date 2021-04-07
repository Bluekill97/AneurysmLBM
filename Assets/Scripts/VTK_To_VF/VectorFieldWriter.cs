using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VectorFieldWriter {
    private string _targetPath = "";
    private string _filename = "";
    private Vector3 _gridsize;
    private Vector3[,,] _direction;
    private float[,,] _info;

    private FileStream sr;


    public VectorFieldWriter(string targetPath, string filename) {
        _targetPath = targetPath;
        _filename = filename;
    }

    public void WriteToVectorfield(Vector3[,,] direction, bool squareGrid = false) {
        _direction = direction;

        int x, y, z;
        x = _direction.GetLength(0);
        y = _direction.GetLength(1);
        z = _direction.GetLength(2);
        _gridsize = new Vector3(x, y, z);

        if (squareGrid)
            SquareGridVEC();

        string fullPath = _targetPath + "/" + _filename + ".vf";
        Debug.Log(fullPath);

        sr = File.Create(fullPath);
        WriteHeaderVEC();
        WriteDataVEC();
        sr.Close();
    }

    public void WriteToVectorfield(float[,,] info, bool squareGrid = false) {
        _info = info;

        int x, y, z;
        x = _info.GetLength(0);
        y = _info.GetLength(1);
        z = _info.GetLength(2);
        _gridsize = new Vector3(x, y, z);

        if (squareGrid)
            SquareGridFLOAT();

        string fullPath = _targetPath + "/" + _filename + "_info.vf";

        sr = File.Create(fullPath);
        WriteHeaderFLOAT();
        WriteDataFLOAT();
        sr.Close();
    }

    #region vector
    private void SquareGridVEC() {
        //find highest dimension
        int highestDim = 0;
        if (_gridsize.x >= _gridsize.y && _gridsize.x >= _gridsize.z)
            highestDim = (int)_gridsize.x;
        if (_gridsize.y >= _gridsize.x && _gridsize.y >= _gridsize.z)
            highestDim = (int)_gridsize.y;
        if (_gridsize.z >= _gridsize.y && _gridsize.z >= _gridsize.x)
            highestDim = (int)_gridsize.z;

        //find next highest dimension count, that is a result of 2^n
        int maxDimCount = 0;
        for (int i = 0; i < 10; i++) {
            if (Math.Pow(2, i) > highestDim) {
                maxDimCount = (int)Math.Pow(2, i);
                break;
            }
        }

        //number of indices that have to be filled in each dimension
        int xFill, yFill, zFill;
        xFill = maxDimCount - (int)_gridsize.x;
        yFill = maxDimCount - (int)_gridsize.y;
        zFill = maxDimCount - (int)_gridsize.z;

        //index where the padding ends/ grids values get inserted
        int xFillStart = xFill / 2;
        int yFillStart = yFill / 2;
        int zFillStart = zFill / 2;

        //fill new grid with zero 
        Vector3[,,] newGrid = new Vector3[maxDimCount, maxDimCount, maxDimCount];
        for (int x = 0; x < maxDimCount; x++)
            for (int y = 0; y < maxDimCount; y++)
                for (int z = 0; z < maxDimCount; z++)
                    newGrid[x, y, z] = Vector3.zero;

        //insert old grid centered in the new one
        for (int x = 0; x < _gridsize.x; x++)
            for (int y = 0; y < _gridsize.y; y++)
                for (int z = 0; z < _gridsize.z; z++)
                    newGrid[x + xFillStart, y + yFillStart, z + zFillStart] = _direction[x, y, z];

        _direction = newGrid;
        _gridsize = new Vector3(maxDimCount, maxDimCount, maxDimCount);
    }

    private void WriteHeaderVEC() {
        Debug.Log("Writing VF-Header");

        //first line
        sr.Write(BitConverter.GetBytes('V'), 0, 1);
        sr.Write(BitConverter.GetBytes('F'), 0, 1);
        sr.Write(BitConverter.GetBytes('_'), 0, 1);
        sr.Write(BitConverter.GetBytes('V'), 0, 1);

        //x, y and z volume
        sr.Write(BitConverter.GetBytes((ushort)_gridsize.x), 0, 2);
        sr.Write(BitConverter.GetBytes((ushort)_gridsize.y), 0, 2);
        sr.Write(BitConverter.GetBytes((ushort)_gridsize.z), 0, 2);
    }

    private void WriteDataVEC() {
        Debug.Log("Writing data to file");

        // "Elements are packed as an array of struct, organized by X then Y then Z." -> loops order important 
        for (int z = 0; z < _gridsize.z; z++) {
            for (int y = 0; y < _gridsize.y; y++) {
                for (int x = 0; x < _gridsize.x; x++) {
                    sr.Write(BitConverter.GetBytes(_direction[x, y, z].x), 0, 4);
                    sr.Write(BitConverter.GetBytes(_direction[x, y, z].y), 0, 4);
                    sr.Write(BitConverter.GetBytes(_direction[x, y, z].z), 0, 4);
                }
            }
        }
    }
    #endregion

    #region float
    private void SquareGridFLOAT() {
        //find highest dimension
        int highestDim = 0;
        if (_gridsize.x >= _gridsize.y && _gridsize.x >= _gridsize.z)
            highestDim = (int)_gridsize.x;
        if (_gridsize.y >= _gridsize.x && _gridsize.y >= _gridsize.z)
            highestDim = (int)_gridsize.y;
        if (_gridsize.z >= _gridsize.y && _gridsize.z >= _gridsize.x)
            highestDim = (int)_gridsize.z;

        //find next highest dimension count, that is a result of 2^n
        int maxDimCount = 0;
        for (int i = 0; i < 10; i++) {
            if (Math.Pow(2, i) > highestDim) {
                maxDimCount = (int)Math.Pow(2, i);
                break;
            }
        }

        //number of indices that have to be filled in each dimension
        int xFill, yFill, zFill;
        xFill = maxDimCount - (int)_gridsize.x;
        yFill = maxDimCount - (int)_gridsize.y;
        zFill = maxDimCount - (int)_gridsize.z;

        //index where the padding ends/ grids values get inserted
        int xFillStart = xFill / 2;
        int yFillStart = yFill / 2;
        int zFillStart = zFill / 2;

        //fill new grid with zero 
        float[,,] newGrid = new float[maxDimCount, maxDimCount, maxDimCount];
        for (int x = 0; x < maxDimCount; x++)
            for (int y = 0; y < maxDimCount; y++)
                for (int z = 0; z < maxDimCount; z++)
                    newGrid[x, y, z] = 0;

        //insert old grid centered in the new one
        for (int x = 0; x < _gridsize.x; x++)
            for (int y = 0; y < _gridsize.y; y++)
                for (int z = 0; z < _gridsize.z; z++)
                    newGrid[x + xFillStart, y + yFillStart, z + zFillStart] = _info[x, y, z];

        _info = newGrid;
        _gridsize = new Vector3(maxDimCount, maxDimCount, maxDimCount);
    }

    private void WriteHeaderFLOAT() {
        Debug.Log("Writing VF-Header");

        //first line
        sr.Write(BitConverter.GetBytes('V'), 0, 1);
        sr.Write(BitConverter.GetBytes('F'), 0, 1);
        sr.Write(BitConverter.GetBytes('_'), 0, 1);
        sr.Write(BitConverter.GetBytes('F'), 0, 1);

        //x, y and z volume
        sr.Write(BitConverter.GetBytes((ushort)_gridsize.x), 0, 2);
        sr.Write(BitConverter.GetBytes((ushort)_gridsize.y), 0, 2);
        sr.Write(BitConverter.GetBytes((ushort)_gridsize.z), 0, 2);
    }

    private void WriteDataFLOAT() {
        Debug.Log("Writing data to file");

        // "Elements are packed as an array of struct, organized by X then Y then Z." -> loops order important 
        for (int z = 0; z < _gridsize.z; z++)
            for (int y = 0; y < _gridsize.y; y++)
                for (int x = 0; x < _gridsize.x; x++)
                    sr.Write(BitConverter.GetBytes(_info[x, y, z]), 0, 4);
    }
    #endregion
}
