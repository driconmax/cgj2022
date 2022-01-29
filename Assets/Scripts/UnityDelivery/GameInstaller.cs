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
        for (var row = 0; row < map.grid.Count; row++)
        {
            for (var column = 0; column < map.grid[row].Count; column++)
            {
                var cellType = map.grid[row][column];

                var cell = Instantiate(groundPrefabs[0], transform);
                cell.transform.localPosition = new Vector3(column * 1f, row * 1f, 1f);

            }
        }
    }
}

public interface Installer
{
    void GenerateMap(Map map);
}