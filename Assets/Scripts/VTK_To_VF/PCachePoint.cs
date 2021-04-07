using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCachePoint {
    public int Type { get; private set; }
    public Vector3 Position { get; private set; }
    public Vector3 Direction { get; private set; }

    public PCachePoint(int _type, Vector3 _position, Vector3 _direction) {
        Type = _type;
        Position = _position;
        Direction = _direction;
    }

    public void SetPosition(Vector3 newPosition) {
        Position = newPosition;
    }

    public void SetDirection(Vector3 newDirection) {
        Direction = newDirection;
    }
}
