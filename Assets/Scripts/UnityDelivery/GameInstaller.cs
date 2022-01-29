using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstaller : MonoBehaviour, Installer
{
    [SerializeField] Mapper mapper;
    [SerializeField] private List<GameObject> groundPrefabs;

    private GameInitializer _gameInitializer;
    private MapCreator _mapCreator;

    private InitializeGame _Initialize => new InitializeGame(this, _mapCreator);

    private void Awake()
    {
        _mapCreator = new CreateMap(mapper); 
        
        _gameInitializer = _Initialize;
    }

    private void Start()
    {
        _gameInitializer.Start();
    }

    public void GenerateMap(Map map)
    {
        for (var column = 0; column < map.grid.Count; column++)
        {
            for (var row = 0; row < map.grid[column].Count; row++)
            {
                var cellType = map.grid[column][row];

                var cell = Instantiate(groundPrefabs[0], transform);

                cell.transform.localPosition = new Vector3(row * mapper.CellSize.x + column * mapper.CellSize.y, column * mapper.CellSize.y/2f - row * mapper.CellSize.y/2f, 1f);

            }
        }
    }
}

public interface Installer
{
    void GenerateMap(Map map);
}