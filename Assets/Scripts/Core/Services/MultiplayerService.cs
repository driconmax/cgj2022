using Photon.Realtime;
using System;
using UnityEngine;

public interface MultiplayerService
{
    void Connect();
    void SetPlayerNickname(string nickname);
    bool JoinRoom();
    void OnJoinedRoom(Action Then);
    void OnConnectToServer(Action Then);
    void PlayerEnteredInARoom(Action<Player> Then);
    CharacterController InstanciatePlayer(Vector2 position);
    bool IsConnected { get; }
    bool HasCounterPlayer { get; }
    int PlayerCount { get; }
}
