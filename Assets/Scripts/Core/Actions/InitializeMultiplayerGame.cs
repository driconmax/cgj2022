using System.Collections;
using System.Collections.Generic;


public class InitializeMultiplayerGame : GameInitializer
{
    readonly Installer _installer;
    readonly MultiplayerService _multiplayerService;
    readonly Menu _menu;
    readonly MapView _mapView;

    int _skinIndex;

    public InitializeMultiplayerGame(Installer installer, MultiplayerService multiplayerService, Menu menu, MapView mapView)
    {
        _installer = installer;
        _multiplayerService = multiplayerService;
        _mapView = mapView;
        _menu = menu;

    }

    public void Start()
    {
        _multiplayerService.Connect();

        _multiplayerService.OnConnectToServer(() => {
            _menu.ShowButton();
        });

        _menu.SetUpButtonPlay((tuple) => {

            if (_multiplayerService.IsConnected)
            {
                var _nickname = tuple.Item1;
                _skinIndex = tuple.Item2;

                _multiplayerService.SetPlayerNickname(_nickname);
                _multiplayerService.JoinRoom();
                _menu.HideLobby();
                _menu.ShowWaitingRoom(true);
            }
            else
            {
                _multiplayerService.Connect();
            }
        });

        _multiplayerService.OnJoinedRoom(() => {

            _mapView.Initialize();

            _menu.ShowWaitingRoom(!_multiplayerService.HasCounterPlayer);

            var playerIndex = _multiplayerService.PlayerCount - 1;
            var initialPosition = _mapView.Presenter.GetPlayerStartPosition(playerIndex);

            var player = _multiplayerService.InstanciatePlayer(initialPosition);
            player.Initialize(playerIndex, _skinIndex);

        });

        _multiplayerService.PlayerEnteredInARoom( playerId => {

            _menu.ShowWaitingRoom(!_multiplayerService.HasCounterPlayer);
            _menu.ShowGameHud(!_multiplayerService.HasCounterPlayer);

        });
    }
}
