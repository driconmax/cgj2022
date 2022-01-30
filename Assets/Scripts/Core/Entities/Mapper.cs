using System;
using UnityEngine;

[Serializable]
public struct Mapper
{
    public int Rows;
    public int Colums;
    public Vector2 CellSize;
    public Vector2 MapStartPosition;
    public Vector2Int Player1_StartPosition;
    public Vector2Int Player2_StartPosition;
}
