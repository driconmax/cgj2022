using System.Collections;
using System.Collections.Generic;


public class InitializeMultiplayerGame : GameInitializer
{
    readonly Installer _installer;
    readonly MultiplayerConnector _multiplayerConector;
    readonly Menu _menu;
    readonly MapView _mapView;

    string _skinName;

    public InitializeMultiplayerGame(Installer installer, MultiplayerConnector multiplayerConnector, Menu menu, MapView mapView)
    {
        _installer = installer;
        _multiplayerConector = multiplayerConnector;
        _mapView = mapView;
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

            _mapView.Initialize();

            _menu.ShowWaitingRoom(!_multiplayerConector.HasCounterPlayer);

            var playerIndex = _multiplayerConector.PlayerCount - 1;
            var initialPosition = _mapView.Presenter.GetPlayerStartPosition(playerIndex);

            var player = _multiplayerConector.InstanciatePlayer(initialPosition);
            player.Initialize(playerIndex, _skinName, _mapView.Presenter);

        });

        _multiplayerConector.PlayerEnteredInARoom(() => {

            _menu.ShowWaitingRoom(!_multiplayerConector.HasCounterPlayer);

        });
    }
}
