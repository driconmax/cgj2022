using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChangeScenarioObjects : MonoBehaviour, ScenarioController
{
    public List<SceneSpawnObject> sceneSpawnObjects;
    private Map _map;
    public void SetMap(Map map)
    {
        _map = map;
    }

    public void SpawnObjectRandom(int value)
    {
        int x = Random.Range(0, _map.grid.Count);
        int y = Random.Range(0, _map.grid[x].Count);

        List<SceneSpawnObject> filteredSceneSpawnObjects = sceneSpawnObjects.Where(obj => obj.value == value).ToList();

        int o = Random.Range(0, filteredSceneSpawnObjects.Count);


    }
}

public interface ScenarioController
{
    public void SetMap(Map map);
    public void SpawnObjectRandom(int value);
}