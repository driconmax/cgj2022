using UnityEngine;

public interface MapCreator
{
    public Map Execute();
    Vector2Int GetEnemyStartPosition { get; }
    Vector2Int GetPlayerStartPosition { get; }
}
