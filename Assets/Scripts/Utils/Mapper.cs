using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Mapper", menuName = "Scene/Mapper")]
public class Mapper : ScriptableObject
{
    public int Rows;
    public int Colums;
    public Vector2 CellSize;
    public Vector2 MapStartPosition;
    public Vector2Int PlayerStartPosition;
    public Vector2Int EnemyStartPosition;
}
