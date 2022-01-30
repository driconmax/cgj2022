using System;
using UnityEngine;

public interface MultiplayerConnector
{
    void Execute();
    void SetPlayerNickname(string nickname);
    void JoinRoom();
    bool IsConnected { get; }
    bool HasCounterPlayer { get; }
    void OnJoinedRoom(Action Then);
    void OnConnectToServer(Action Then);
    void PlayerEnteredInARoom(Action Then);
    CharacterController InstanciatePlayer(Vector2 initialPosition);
}