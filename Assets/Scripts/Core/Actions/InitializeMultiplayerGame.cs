using System.Collections;
using System.Collections.Generic;


public class InitializeMultiplayerGame : GameInitializer
{
    readonly Installer _installer;
    readonly MultiplayerConnector _multiplayerConector;
    readonly Menu _menu;
    readonly MapPresenter _mapPresenter;

    string _skinName;

    public InitializeMultiplayerGame(Installer installer, MultiplayerConnector multiplayerConnector, Menu menu, MapPresenter mapPresenter)
    {
        _installer = installer;
        _multiplayerConector = multiplayerConnector;
        _mapPresenter = mapPresenter;
        _menu = menu;
    }

    public void Start()
    {
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
            var initialPosition = _mapPresenter.GetPlayerStartPosition(playerIndex);

            var player = _multiplayerConector.InstanciatePlayer(initialPosition);
            player.Initialize(_mapPresenter.GetPlayerMappedStartPosition(playerIndex), playerIndex, _skinName, _mapPresenter);

        });

        _multiplayerConector.PlayerEnteredInARoom(() => {

            _menu.ShowWaitingRoom(!_multiplayerConector.HasCounterPlayer);

        });
    }
}
