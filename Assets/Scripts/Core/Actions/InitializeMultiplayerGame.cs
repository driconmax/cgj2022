using System.Collections;
using System.Collections.Generic;


public class InitializeMultiplayerGame : GameInitializer
{
    readonly Installer _installer;
    readonly MapCreator _mapCreator;
    readonly MultiplayerConnector _multiplayerConector;
    readonly Menu _menu;

    public InitializeMultiplayerGame(Installer installer, MapCreator mapCreator, MultiplayerConnector multiplayerConnector, Menu menu)
    {
        _installer = installer;
        _mapCreator = mapCreator;
        _multiplayerConector = multiplayerConnector;
        _menu = menu;
    }

    public void Start()
    {
        var _map = _mapCreator.Execute();

        _multiplayerConector.Execute();

        _multiplayerConector.OnConnectToServer(() => {
            _menu.ShowButton();
        });

        _menu.SetUpButtonPlay((nickname) => {

            if (_multiplayerConector.IsConnected)
            {
                _multiplayerConector.SetPlayerNickname(nickname);
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
            player.Initialize(_map, _mapCreator.GetPlayerMappedStartPosition(playerIndex), playerIndex, OnPlayerMove);

        });

        _multiplayerConector.PlayerEnteredInARoom(() => {

            _menu.ShowWaitingRoom(!_multiplayerConector.HasCounterPlayer);

        });

        _installer.GenerateMap(_map);

    }

    private void OnPlayerMove(int value)
    {
        UnityEngine.Debug.Log(value);
    }
}
