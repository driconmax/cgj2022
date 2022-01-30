using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MultiplayerService
{
    void Connect();
    void SetPlayerNickName(string nickname);
    bool JoinRoom();
    void OnJoinedRoom(Action Then);
    void OnConnectToServer(Action Then);
    void PlayerEnteredInARoom(Action Then);
    CharacterController InstanciatePlayer(Vector2 initialPosition);
    bool IsConnected { get; }
    bool HasCounterPlayer { get; }
    int PlayerCount { get; }
}
