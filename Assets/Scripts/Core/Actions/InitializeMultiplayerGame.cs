using System.Collections;
using System.Collections.Generic;


public class InitializeMultiplayerGame : GameInitializer
{
    readonly Installer _installer;
    readonly MapCreator _mapCreator;
    readonly MultiplayerConnector _multiplayerConector;
    readonly Menu _menu;
    readonly ScenarioController _scenarioController;

    string _skinName;

    public InitializeMultiplayerGame(Installer installer, MapCreator mapCreator, MultiplayerConnector multiplayerConnector, Menu menu, ScenarioController scenarioController)
    {
        _installer = installer;
        _mapCreator = mapCreator;
        _multiplayerConector = multiplayerConnector;
        _menu = menu;
        _scenarioController = scenarioController;
    }

    public void Start()
    {
        var _map = _mapCreator.Execute();

        _scenarioController.SetMap(_map);

        ServiceLocator.RegisterServices(_scenarioController);

        _multiplayerConector.Execute();

        _multiplayerConector.OnConnectToServer(() => {
            _menu.ShowButton();
        });

        _menu.SetUpButtonPlay((tuple) => {

            if (_multiplayerConector.IsConnected)
            {
                var _nickname = tuple.Item1;
                _skinName = tuple.Item2;

                _multiplayerConector.SetPlayerNickname(_nickname);
                _multiplayerConector.JoinRoom();
                _menu.HideLobby();
                _menu.ShowWaitingRoom(true);
            }
            else
            {
                _multiplayerConector.Execute();
            }
        });

        _multiplayerConector.OnJoinedRoom(() => {
            _menu.ShowWaitingRoom(!_multiplayerConector.HasCounterPlayer);

            var playerIndex = _multiplayerConector.PlayerCount - 1;
            var initialPosition = _mapCreator.GetPlayerStartPosition(playerIndex);
            var player = _multiplayerConector.InstanciatePlayer(initialPosition);
            player.Initialize(_map, _mapCreator.GetPlayerMappedStartPosition(playerIndex), playerIndex, _skinName, _installer);

        });

        _multiplayerConector.PlayerEnteredInARoom(() => {

            _menu.ShowWaitingRoom(!_multiplayerConector.HasCounterPlayer);

        });

        _installer.GenerateMap(_map);

    }
}
