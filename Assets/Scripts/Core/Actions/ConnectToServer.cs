using System;
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

    public void Execute() => _multiplayer.Connect();

    public void SetPlayerNickname(string nickname) => _multiplayer.SetPlayerNickName(nickname);

    public void JoinRoom() => _multiplayer.JoinRoom();

    public bool IsConnected => _multiplayer.IsConnected;

    public int PlayerCount => _multiplayer.PlayerCount;

    public bool HasCounterPlayer => _multiplayer.HasCounterPlayer;

    public void OnJoinedRoom(Action Then) => _multiplayer.OnJoinedRoom(Then);

    public void OnConnectToServer(Action Then) => _multiplayer.OnConnectToServer(Then);

    public void PlayerEnteredInARoom(Action Then) => _multiplayer.PlayerEnteredInARoom(Then);

    public CharacterController InstanciatePlayer(Vector2 initialPosition) => _multiplayer.InstanciatePlayer(initialPosition);
}
