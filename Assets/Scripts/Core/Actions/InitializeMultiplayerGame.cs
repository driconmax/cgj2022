using System.Collections;
using System.Collections.Generic;


public class InitializeMultiplayerGame : GameInitializer
{
    readonly Installer _installer;
    readonly MultiplayerService _multiplayerService;
    readonly Menu _menu;
    readonly MapView _mapView;

    string _skinName;

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
                _skinName = tuple.Item2;

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

            _menu.ShowWaitingRoom(!_multiplayerService.HasCounterPlayer);

        });

        _multiplayerService.PlayerEnteredInARoom( player => {

            _mapView.Initialize();

            var playerIndex = _multiplayerService.PlayerCount - 1;
            var initialPosition = _mapView.Presenter.GetPlayerStartPosition(playerIndex);
            //characterController.Initialize(playerIndex, _skinName, _mapView.Presenter);
            //characterController.Initialize(player);
            ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();

            hashtable.Add("PlayerIndex", playerIndex);
            hashtable.Add("SkinName", _skinName);
            //hashtable.Add("PlayerIndex", playerIndex);

            player.SetCustomProperties(hashtable);

            var characterController = _multiplayerService.InstanciatePlayer(initialPosition);

            player.TagObject = characterController;

            characterController.SetPresenter(_mapView.Presenter);

            _menu.ShowWaitingRoom(!_multiplayerService.HasCounterPlayer);

        });
    }
}
