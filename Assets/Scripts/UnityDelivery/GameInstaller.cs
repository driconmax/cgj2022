using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstaller : MonoBehaviour, Installer
{
    [SerializeField] Mapper mapper;
    [SerializeField] private List<GameObject> groundPrefabs;
    [SerializeField] private PhotonMultiplayerService photonServer;
    [SerializeField] private MenuView menu;

    private GameInitializer _gameInitializer;
    private MapCreator _mapCreator;
    private MultiplayerConnector _multiplayerConector;
    private Map _map;

    private InitializeMultiplayerGame _Initialize => new InitializeMultiplayerGame(this, _mapCreator, _multiplayerConector, menu);

    private void Awake()
    {
        _mapCreator = new CreateMap(mapper);
        _multiplayerConector = new ConnectToServer(photonServer);

        _gameInitializer = _Initialize;
    }

    private void Start()
    {
        _gameInitializer.Start();
    }

    //
    public void GenerateMap(Map map)
    {
        _map = map;

        for (var column = 0; column < map.grid.Count; column++) {

            for (var row = 0; row < map.grid[column].Count; row++) {
                var cell = map.grid[column][row];

                var cellObject = Instantiate(groundPrefabs[cell.Type], transform);

                cellObject.transform.localPosition = cell.GetPosition();
            }
        }
    }
}