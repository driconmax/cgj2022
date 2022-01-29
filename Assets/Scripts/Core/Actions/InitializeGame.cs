using System.Collections;
using System.Collections.Generic;


public class InitializeGame : GameInitializer
{
    Installer _installer;
    MapCreator _mapCreator;

    public InitializeGame(Installer installer, MapCreator mapCreator)
    {
        //Create grid
        _installer = installer;
        _mapCreator = mapCreator;
        //Position player
        //Start multiplayer
    }

    public void Start()
    {
        var _map = _mapCreator.Execute();
        _installer.GenerateMap(_map);
    }
}
