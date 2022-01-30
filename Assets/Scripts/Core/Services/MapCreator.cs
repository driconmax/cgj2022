using UnityEngine;

public interface MapCreator
{
    public Map Execute();
    public Vector2 GetPlayerStartPosition(int playerIndex);
    public Vector2Int GetPlayerMappedStartPosition(int playerIndex);
}
