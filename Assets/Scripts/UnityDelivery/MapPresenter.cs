﻿using System.Collections.Generic;
using System.Linq;

public class MapPresenter
{
    private MapCreator _mapCreator;
    private MapView _view;

    private Map _map;
    public Map Map => _map;

    private List<SceneSpawnObject> _sceneSpawnObjects;
    private List<SpawnedSceneSpawnObject> _spawnedObjects = new List<SpawnedSceneSpawnObject>();
    private Dictionary<int, List<Cell>> _spawnedObjectsLevels = new Dictionary<int, List<Cell>>();

    private Cell _lastCell;

    public MapPresenter(MapView view, MapCreator mapCreator, List<SceneSpawnObject> sceneSpawnObjects)
    {
        _view = view;
        _mapCreator = mapCreator;
        _sceneSpawnObjects = sceneSpawnObjects;

        ServiceLocator.RegisterServices<MapPresenter>(this);

        _map = _mapCreator.Execute();
    }

    public void Present()
    {
        GenerateMap(_map);
    }

    public void GenerateMap(Map map)
    {
        for (var column = 0; column < map.grid.Count; column++)
        {
            for (var row = 0; row < map.grid[column].Count; row++)
            {
                Cell cell = map.grid[column][row];

                var cellView = _view.CreateGround(cell.GetFloorType, cell.GetPosition());
                cell.SetView(cellView);
            }
        }
    }

    public UnityEngine.Vector2 GetPlayerStartPosition(int playerIndex)
    {
        UnityEngine.Vector2Int position = _mapCreator.GetPlayerStartPosition;
        
        if ((playerIndex % 2) == 1) 
        {
            position = _mapCreator.GetEnemyStartPosition;
        }
        return _map.grid[position.x][position.y].GetPosition();
    }

    public UnityEngine.Vector2Int GetPlayerMappedStartPosition(int playerIndex)
    {
        if ((playerIndex % 2) == 0)
        {
            return _mapCreator.GetPlayerStartPosition;
        }
        else
        {
            return _mapCreator.GetEnemyStartPosition;
        }
    }

    public void ToggleButton(Cell gridCell)
    {
        _lastCell?.ChangeStatus(true);
        gridCell.ChangeStatus(false);
        _lastCell = gridCell;
    }

    public void SpawnObjectRandom(int x, int y, int value)
    {

        List<SceneSpawnObject> filteredSceneSpawnObjects = _sceneSpawnObjects.Where(obj => obj.value == value).ToList();

        int o = UnityEngine.Random.Range(0, filteredSceneSpawnObjects.Count);

        Cell cell = _map.grid[x][y];

        if (value != 1)
        {
            _spawnedObjectsLevels[value - 1].Remove(cell);
        }

        if (!_spawnedObjectsLevels.ContainsKey(value))
        {
            _spawnedObjectsLevels.Add(value, new List<Cell>());
        }

        _spawnedObjectsLevels[value].Add(cell);

        cell.SpawnAttachment(filteredSceneSpawnObjects[o]);
    }

    public bool CheckValidSpawnCombo(int value)
    {
        if (value == 1 || (_spawnedObjectsLevels.ContainsKey(value - 1) && _spawnedObjectsLevels[value - 1].Count != 0)) return true;
        return false;
    }

    public UnityEngine.Vector2Int GetSpawnPosition(int value)
    {
        if(value == 1)
        {
            int x = UnityEngine.Random.Range(0, _map.grid.Count);
            int y = UnityEngine.Random.Range(0, _map.grid[x].Count);
            return new UnityEngine.Vector2Int(x, y);
        }

        int o = UnityEngine.Random.Range(0, _spawnedObjectsLevels[value - 1].Count);
        return _spawnedObjectsLevels[value - 1][o].GetIntPosition();
    }

    struct SpawnedSceneSpawnObject
    {
        public SceneSpawnObject sceneSpawnObject;
        public UnityEngine.GameObject spawnObject;
        public UnityEngine.Vector2Int position;
    }
}
