using UnityEngine;

public interface Installer
{
    void GenerateMap(Map map);
    void SetPlayerInitialPosition(PlayerData data, Vector2 position);
}