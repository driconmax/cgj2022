using System.Collections;
using System.Collections.Generic;


public class InitializeMultiplayerGame : GameInitializer
{
    readonly Installer _installer;
    readonly MapCreator _mapCreator;

    public InitializeMultiplayerGame(Installer installer, MapCreator mapCreator)
    {
        _installer = installer;
        _mapCreator = mapCreator;
    }

    public void Start()
    {
        var _map = _mapCreator.Execute();
        var player1_initialPosition = _mapCreator.GetStartPosition_Player1();
        var player2_initialPosition = _mapCreator.GetStartPosition_Player2();


        _installer.GenerateMap(_map);

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
