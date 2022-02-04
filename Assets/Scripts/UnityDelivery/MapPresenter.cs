using System.Collections.Generic;
using System.Linq;

public class MapPresenter
{
    private MapCreator _mapCreator;
    private MapView _view;

    private Map _map;
    public Map Map => _map;

    private List<SceneSpawnObject> _sceneSpawnObjects;
    private List<SpawnedSceneSpawnObject> _spawnedObjects = new List<SpawnedSceneSpawnObject>();

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

    public void SpawnObjectRandom(int value)
    {
        int x = UnityEngine.Random.Range(0, _map.grid.Count);
        int y = UnityEngine.Random.Range(0, _map.grid[x].Count);

        List<SceneSpawnObject> filteredSceneSpawnObjects = _sceneSpawnObjects.Where(obj => obj.value == value).ToList();

        int o = UnityEngine.Random.Range(0, filteredSceneSpawnObjects.Count);

        var position = _map.grid[x][y].GetPosition();

        var spawnObject = _view.CreateSpawnObject(filteredSceneSpawnObjects[o].gameObject, position);

        _spawnedObjects.Add(new SpawnedSceneSpawnObject
        {
            sceneSpawnObject = filteredSceneSpawnObjects[o],
            spawnObject = spawnObject,
            position = new UnityEngine.Vector2Int(x, y)
        });
    }

    struct SpawnedSceneSpawnObject
    {
        public SceneSpawnObject sceneSpawnObject;
        public UnityEngine.GameObject spawnObject;
        public UnityEngine.Vector2Int position;
    }
}
