using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class PhotonMultiplayerService : MonoBehaviourPunCallbacks, MultiplayerService
{
    private string roomName;
    private bool isConnecting;
    private Action OnJoinedRoomThen;
    private Action OnConnectToServerThen;
    private Action PlayerEnterInARoomThen;

    public bool IsConnected => PhotonNetworkAdapter.IsConnected;
    public bool HasCounterPlayer => PhotonNetworkAdapter.HasCounterPlayer();
    public int PlayerCount => PhotonNetworkAdapter.PlayerCount;

    public void Connect()
    {
        PhotonNetworkAdapter.AutomaticallySyncScene(false);
        isConnecting = PhotonNetworkAdapter.ConnectUsingSettings();
    }

    public void SetPlayerNickName(string nickname)
    {
        PhotonNetworkAdapter.SetPlayerNickName(nickname);
    }

    public bool JoinRoom()
    {
        var joinedRoom = PhotonNetworkAdapter.JoinRoom();
        return joinedRoom;
    }

    public void OnJoinedRoom(Action Then)
    {
        OnJoinedRoomThen += Then;
    }

    public void OnConnectToServer(Action Then)
    {
        OnConnectToServerThen += Then;
    }

    public void PlayerEnteredInARoom(Action Then)
    {
        PlayerEnterInARoomThen += Then;
    }

    public CharacterController InstanciatePlayer(Vector2 initialPosition)
    {
        return PhotonNetworkAdapter.Instantiate(CharacterController.CharacterPrefabName, initialPosition, Quaternion.identity).GetComponent<CharacterController>();
    }

    //Pun Fuctions

    public override void OnConnected()
    {
        Debug.Log("PHOTON: Connected()");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("PHOTON: OnConnectedToMaster()");

        if (isConnecting)
        {
            PhotonNetworkAdapter.JoinLobby();
            isConnecting = false;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {

    }

    public override void OnJoinedLobby()
    {
        OnConnectToServerThen();
        Debug.Log("PHOTON: OnJoinedLobby()");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("PHOTON: OnLeftRoom()");
    }

    public override void OnJoinedRoom()
    {
        Debug.LogWarning($"PHOTON: Joined room called {PhotonNetworkAdapter.CurrentRoom().Name} with {PhotonNetworkAdapter.CurrentRoom().PlayerCount} players ");

        PhotonNetworkAdapter.CurrentRoom().MaxPlayers = 2;
        OnJoinedRoomThen();
    }

    public override void OnCreatedRoom()
    {
        PhotonNetworkAdapter.CurrentRoom().MaxPlayers = 2;
        Debug.Log("PHOTON: OnCreatedRoom()");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
     
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        // view.SetStateText("Falló");
        Debug.Log(message);
        switch (returnCode)
        {
            case ErrorCode.JoinFailedFoundInactiveJoiner:
                PhotonNetworkAdapter.RejoinRoom("test");
                break;
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"Player enter with name {newPlayer.NickName}");
        PlayerEnterInARoomThen();
    }
}
