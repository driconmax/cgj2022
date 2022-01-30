using UnityEngine;

public struct Cell
{ 
    public int Index;
    public int Row;
    public int Column;
    public Vector2 Offset;
    public Vector2 CellSize;
    public int Type;
    public bool Status;

    public Vector2 GetPosition()
    {
        return new Vector3(Row * CellSize.x + Column * CellSize.y + Offset.x, Column * CellSize.y / 2f - Row * CellSize.y / 2f + Offset.y, 1f);
    }
}
