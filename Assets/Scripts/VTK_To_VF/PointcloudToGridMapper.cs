using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PointcloudToGridMapper {
    /// <summary>
    /// Grid containing the directions for each grid point
    /// </summary>
    public Vector3[,,] directionGrid { get; private set; }

    /// <summary>
    /// Grid containing additional information for each grid point, like the point type (wall, inlet, outlet, ..)
    /// </summary>
    public float[,,] info { get; private set; }

    float _inputGridStepSize = -1;

    //pcache
    private List<PCachePoint> _points;
    //adjusted free points by including offset
    private List<PCachePoint> adjPoints;

    private Vector3 gridSize;
    private Vector3 offsetCorrection; //if the grid doesn't start at 0, 0, 0 -> vector to move all points, so that grid is aligned
    private float gridStepSize;

    //Inverted Lists: calculate neighbourhoods (lists)
    private List<PCachePoint>[,,] nei;

    /// <summary>
    /// Maps given points into a grid
    /// Should have a complexity of (numberOfGridPoints) * (numberOfMappedPoints)
    /// </summary>
    /// <param name="points"> List of PCachePoints that are mapped to the grid </param>
    /// <param name="inputGridStepSize"> Distance of adjacent gridpoints, entered by the user, in Millimeter </param>
    public PointcloudToGridMapper(Dictionary<int, PCachePoint> points, float inputGridStepSize) {
        _points = new List<PCachePoint>(points.Values);
        _inputGridStepSize = inputGridStepSize;
    }

    /// <summary>
    /// Maps given points into a grid
    /// Should have a complexity of (numberOfGridPoints) * (numberOfMappedPoints)
    /// Converts the given cells and vertices to a List of PCachePoints
    /// </summary>
    /// <param name="cells"> List of Cells consisting to the vertices </param>
    /// <param name="vertices"> List of vertices that belong to the Cells </param>
    /// <param name="inputGridStepSize"> Distance of adjacent gridpoints, entered by the user, in Millimeter </param>
    public PointcloudToGridMapper(Dictionary<int, Cell> cells, Dictionary<int, Vector3> vertices, float inputGridStepSize) {
        _inputGridStepSize = inputGridStepSize;

        //pcache point = middlepoint of cell
        _points = new List<PCachePoint>();

        foreach (KeyValuePair<int, Cell> kvp in cells) {
            Cell cell = kvp.Value;

            //calculate midpoint for each cell
            Vector3 cellMid = Vector3.zero;
            foreach (int vertexID in cell.VertexIDs)
                cellMid += vertices[vertexID];
            cellMid /= cell.VertexIDs.Count;

            //add point to list
            _points.Add(new PCachePoint(cell.CellType, cellMid, cell.Velocity));
        }
    }

    public Vector3 GetOffsetCorrection() {
        return offsetCorrection;
    }

    public void StartConvert() {
        CreateGrid();
        AlignPointsToCoordinateOrigin();
        CreateNeighborhoodListsForGridPoints();
        IntegratePointsIntoGrid();
    }

    private void CreateGrid() {
        //get min and max position values
        float maxX = float.MinValue;
        float maxY = float.MinValue;
        float maxZ = float.MinValue;
        float minX = float.MaxValue;
        float minY = float.MaxValue;
        float minZ = float.MaxValue;

        //iterate through all vertices -> find min/max values
        foreach (PCachePoint p in _points) {
            Vector3 vertex = p.Position;

            if (vertex.x > maxX)
                maxX = vertex.x;

            if (vertex.y > maxY)
                maxY = vertex.y;

            if (vertex.z > maxZ)
                maxZ = vertex.z;

            if (vertex.x < minX)
                minX = vertex.x;

            if (vertex.y < minY)
                minY = vertex.y;

            if (vertex.z < minZ)
                minZ = vertex.z;
        }



        //to align grid at point 0, 0, 0: calculate offset from origin
        float xOffset = 0 - minX;
        float yOffset = 0 - minY;
        float zOffset = 0 - minZ;
        offsetCorrection = new Vector3(xOffset, yOffset, zOffset);

        //conversion to millimeter
        gridStepSize = (_inputGridStepSize * Mathf.Pow(10, -3));

        //create grid, include offset to get the bounding boxes size
        int gridXSize = (int)Mathf.Ceil((maxX + xOffset) / gridStepSize);
        int gridYSize = (int)Mathf.Ceil((maxY + yOffset) / gridStepSize);
        int gridZSize = (int)Mathf.Ceil((maxZ + zOffset) / gridStepSize);

        gridSize = new Vector3(gridXSize - 1, gridYSize - 1, gridZSize - 1); // -1 because following array is 0 based
        directionGrid = new Vector3[gridXSize, gridYSize, gridZSize];
        info = new float[gridXSize, gridYSize, gridZSize];
    }

    private void AlignPointsToCoordinateOrigin() {

        //adjust free points by including offset
        adjPoints = new List<PCachePoint>();
        foreach (PCachePoint pd in _points)
            adjPoints.Add(new PCachePoint(pd.Type, pd.Position + offsetCorrection, pd.Direction));
    }

    private void CreateNeighborhoodListsForGridPoints() {
        /*
         * To integrate the points (point = "free" point calculated from the simulation results, not bound to grid) into the grid, 
         * we need to know which free points influence which gridpoints. Free points influence a gridpoint if they are near enough,
         * called the neighbourhood.
         * There are several ways to compute the neighbourhood: 
         * 
         * Naive approach: 
         * Iterate for each gridpoint g through all free points f
         * -> complexity = count(g) * count(f)
         * 
         * Inverted Lists approach:
         * The neighbourhood (near enough free points to a gridpoint) is saved as a List<Free Point> for each gridpoint
         * 1. Iterate through all free points
         * 2. Calculate which gridpoints are surrounding the current free point
         * 3. Save current free point in the neighbourhood-list of each gridpoint
         * 4. For grid calculation: neighbourhoods are saved and easily accessable
         * Advantage: complexity = count(g) + count (f)             (count(g) + count (f) << count(g) * count(f))
         * Disadvantage: more memory needed (each point in ~8 lists saved)
         */


        float neighborhoodRange = gridStepSize;


        //Inverted Lists: calculate neighbourhoods (lists)
        nei = new List<PCachePoint>[(int)gridSize.x, (int)gridSize.y, (int)gridSize.z];


        foreach (PCachePoint pcpoint in adjPoints) {

            /*
             * Find the grid points that surround the current point 
             * maxGridPoint: gridpoint (directly surrounding the current point) most distant from the grid origin
             * minGridPoint: gridpoint (directly surrounding the current point) nearest to the grid origin
             * Watch out: maxGridPoint and minGridPoint could contain indices that are out of the grids bounds
             */

            Vector3 minGridPoint = new Vector3(
                    getFloorGridPoint(pcpoint.Position.x),
                    getFloorGridPoint(pcpoint.Position.y),
                    getFloorGridPoint(pcpoint.Position.z));

            Vector3 maxGridPoint = new Vector3(
                getCeilGridPoint(pcpoint.Position.x, (int)gridSize.x),
                getCeilGridPoint(pcpoint.Position.y, (int)gridSize.y),
                getCeilGridPoint(pcpoint.Position.z, (int)gridSize.z));

            //Debug.Log("gridsize: " + (int)gridSize.x + " " + (int)gridSize.y + " " + (int)gridSize.z + ", point count: " + points.Count);
            //Debug.Log("point position: " + pcpoint.Position + " with step size: " + gridStepSize + " results in min neighbors " + min + " and max neighbors" + max);

            //get higher nearest gridpoint within neighborhood range (higher = most distant from the grid origin)
            float getCeilGridPoint(float value, int gridDimensionMax) {
                value += neighborhoodRange;
                value = Mathf.Floor(value / gridStepSize);
                return value > gridDimensionMax - 1 ? gridDimensionMax - 1 : value;
            }

            //get lower nearest gridpoint within neighborhood range (lower = nearest to the grid origin)
            float getFloorGridPoint(float value) {
                value -= neighborhoodRange;
                value = Mathf.Ceil(value / gridStepSize);
                return value < 0 ? 0 : value;
            }

            /*
             * Remember for all gridpoints surrounding the current point, that this point is in their neighbourhood
             * -> add free point to gridpoints neighborhood-list
             * 
             * All surrounding gridpoints: defined by minGridPoint and maxGridPoint
             */
            for (int x = (int)minGridPoint.x; x <= maxGridPoint.x; x++) {
                for (int y = (int)minGridPoint.y; y <= maxGridPoint.y; y++) {
                    for (int z = (int)minGridPoint.z; z <= maxGridPoint.z; z++) {
                        //Debug.Log("size: " + gridSize + " und koordinaten x: " + x + ", y: " + y + ", z: " + z);

                        if ((x >= gridSize.x) || (y >= gridSize.y) || (z >= gridSize.z) ||      //index positive out of grids bounds
                            x < 0 || y < 0 || z < 0) {                                      //index negative out of grids bounds
                            Debug.LogWarning("WHAT WHAT SOLLTE ABGEFANGEN SEIN");

                            continue;
                        }

                        if (nei[x, y, z] == null)
                            nei[x, y, z] = new List<PCachePoint>();

                        nei[x, y, z].Add(pcpoint);
                    }
                }
            }
        }
    }

    private void IntegratePointsIntoGrid() {
        /*
         * To calculate the direction vector on each gridpoint: take direction vectors of the neighbourhoods free points
         * Different approaches to combine the Vectors:
         * 1. Don't combine, just take nearest free point to the current gridpoint
         * 2. Treat all direction vectors equally, calculate mean direction vector
         * 3. Weight influence of each free point with the distance to the current gridpoint, less distance = high influence
         */

        //compute grid points
        for (int x = 0; x < gridSize.x; x++) {
            for (int y = 0; y < gridSize.y; y++) {
                for (int z = 0; z < gridSize.z; z++) {
                    Vector3 pointPos = new Vector3(x * gridStepSize, y * gridStepSize, z * gridStepSize);

                    if (nei[x, y, z] != null) {


                        //2. approach
                        Vector3 meanDirection = Vector3.zero;
                        foreach (PCachePoint currentNeighbor in nei[x, y, z])
                            meanDirection += currentNeighbor.Direction;

                        meanDirection /= nei[x, y, z].Count;

                        directionGrid[x, y, z] = meanDirection;
                        info[x, y, z] = 0;
                    }
                    else {
                        directionGrid[x, y, z] = Vector3.zero;
                        info[x, y, z] = 0;
                    }

                }
            }
        }
    }
}
