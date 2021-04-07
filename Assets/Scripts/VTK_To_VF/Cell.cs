using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cell {
    public int ID { get; private set; }
    public int CellType { get; private set; }
    public List<int> VertexIDs { get; private set; }
    public Vector3 Velocity { get; private set; }

    public Cell(int _id, int _cellType, List<int> _position) {
        ID = _id;
        VertexIDs = _position;
    }

    public Cell(int _id, int _cellType, List<int> _position, Vector3 _direction) {
        ID = _id;
        CellType = _cellType;
        VertexIDs = _position;
        Velocity = _direction;
    }

    public void SetCellType(int type) {
        CellType = type;
    }

    public void SetVelocity(Vector3 newDirection) {
        Velocity = newDirection;
    }
}
