using System.Collections;
using System.Collections.Generic;


public class InitializeMultiplayerGame : GameInitializer
{
    readonly Installer _installer;
    readonly MapCreator _mapCreator;
    readonly MultiplayerConnector _multiplayer;

    public InitializeMultiplayerGame(Installer installer, MapCreator mapCreator, MultiplayerConnector multiplayer)
    {
        _installer = installer;
        _mapCreator = mapCreator;
        _multiplayer = multiplayer;
    }

    public void Start()
    {
        var _map = _mapCreator.Execute();
        var player1_initialPosition = _mapCreator.GetStartPosition_Player1();
        var player2_initialPosition = _mapCreator.GetStartPosition_Player2();


        _installer.GenerateMap(_map);
        _multiplayer.Connect();

        //

        _installer.SetPlayerInitialPosition( 
            new PlayerData { 
                Player = PlayerType.Player1
            }, 
            player1_initialPosition
        );

        _installer.SetPlayerInitialPosition(
            new PlayerData {
                Player = PlayerType.Player2
            },
            player2_initialPosition
        );
    }
}
