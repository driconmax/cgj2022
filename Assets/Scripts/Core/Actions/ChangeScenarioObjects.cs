using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChangeScenarioObjects : MonoBehaviour, ScenarioController
{
    public List<SceneSpawnObject> sceneSpawnObjects;

    private Map _map;
    private List<SpawnedSceneSpawnObject> _spawnedObjects = new List<SpawnedSceneSpawnObject>();

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

        Vector2 position = _map.grid[x][y].GetPosition();

        GameObject gameObject = Instantiate(filteredSceneSpawnObjects[o].gameObject, position, Quaternion.identity);

        _spawnedObjects.Add(new SpawnedSceneSpawnObject
        {
            sceneSpawnObject = filteredSceneSpawnObjects[o],
            gameObject = gameObject,
            position = new Vector2Int(x,y)
        });
    }

    struct SpawnedSceneSpawnObject
    {
        public SceneSpawnObject sceneSpawnObject;
        public GameObject gameObject;
        public Vector2Int position;
    }
}

public interface ScenarioController
{
    public void SetMap(Map map);
    public void SpawnObjectRandom(int value);
}