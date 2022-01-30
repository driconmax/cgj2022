using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectToServer : MultiplayerConnector
{
    MultiplayerService _multiplayer;

    public ConnectToServer(MultiplayerService multiplayer)
    {
        _multiplayer = multiplayer;
    }

    public void Connect()
    {
        _multiplayer.Execute();
    }
}

public interface MultiplayerConnector
{
    void Connect();
}