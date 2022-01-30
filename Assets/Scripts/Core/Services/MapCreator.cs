using UnityEngine;

public interface MapCreator
{
    public Map Execute();
    public Vector2 GetPlayerStartPosition();
    public Vector2Int GetPlayerMappedStartPosition();
}
