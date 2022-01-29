using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstaller : MonoBehaviour, Installer
{
    [SerializeField] Mapper mapper;
    [SerializeField] private List<GameObject> groundPrefabs;
    [SerializeField] private CharacterController player1;
    [SerializeField] private CharacterController player2;

    private GameInitializer _gameInitializer;
    private MapCreator _mapCreator;
    private Map _map;

    private InitializeMultiplayerGame _Initialize => new InitializeMultiplayerGame(this, _mapCreator);

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
        _map = map;

        for (var column = 0; column < map.grid.Count; column++) {

            for (var row = 0; row < map.grid[column].Count; row++) {
                var cell = map.grid[column][row];

                var cellObject = Instantiate(groundPrefabs[cell.Type], transform);

                cellObject.transform.localPosition = cell.GetPosition();
            }
        }
    }

    public void SetPlayerInitialPosition(PlayerData data, Vector2 position)
    {
        if (data.Player.Equals(PlayerType.Player1)){

           var _player1 = Instantiate(player1, position, Quaternion.identity);
            _player1.Initialize(_map, mapper.Player1_StartPosition);

        } else {

           var _player2 = Instantiate(player2, position, Quaternion.identity);
             _player2.Initialize(_map, mapper.Player2_StartPosition);

        }
    }
}
