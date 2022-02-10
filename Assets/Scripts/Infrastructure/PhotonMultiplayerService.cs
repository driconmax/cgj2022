using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class PhotonMultiplayerService : MonoBehaviourPunCallbacks, MultiplayerService
{
    private const string CHARACTER_PREFAB_NAME = "Character";
    private string roomName;
    private bool isConnecting;
    private Action OnJoinedRoomThen;
    private Action OnConnectToServerThen;
    private Action<Player> PlayerEnterInARoomThen;

    public bool IsConnected => PhotonNetwork.IsConnected;
    public bool HasCounterPlayer => PhotonNetwork.CurrentRoom == null ? false : PhotonNetwork.CurrentRoom.PlayerCount > 1;
    public int PlayerCount => PhotonNetwork.CurrentRoom.PlayerCount;

    public void Connect()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        isConnecting = PhotonNetwork.ConnectUsingSettings();
    }

    public void SetPlayerNickname(string nickname)
    {
        PhotonNetwork.NickName = nickname;
    }

    public CharacterController InstanciatePlayer(Vector2 position)
    {
        return PhotonNetwork.Instantiate(CHARACTER_PREFAB_NAME, position, Quaternion.identity).GetComponent<CharacterController>();
    }

    public bool JoinRoom()
    {
        var joinedRoom = PhotonNetwork.JoinRandomOrCreateRoom(null, 2);
        return joinedRoom;
    }

    public override void OnConnected()
    {
        Debug.Log("PHOTON: Connected()");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("PHOTON: OnConnectedToMaster()");

        if (isConnecting)
        {
            PhotonNetwork.JoinLobby();
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
        Debug.LogWarning($"PHOTON: Joined room called {PhotonNetwork.CurrentRoom.Name} with {PhotonNetwork.CurrentRoom.PlayerCount} players ");
        roomName = PhotonNetwork.CurrentRoom.Name;
        PhotonNetwork.CurrentRoom.MaxPlayers = 2;
        OnJoinedRoomThen();
    }

    public override void OnCreatedRoom()
    {
        PhotonNetwork.CurrentRoom.MaxPlayers = 2;
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
                PhotonNetwork.RejoinRoom(roomName);
                break;
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"Player enter with name {newPlayer.NickName}");
        PlayerEnterInARoomThen(newPlayer);
    }


    public void OnJoinedRoom(Action Then) => OnJoinedRoomThen += Then;

    public void OnConnectToServer(Action Then) => OnConnectToServerThen += Then;

    public void PlayerEnteredInARoom(Action<Player> Then) => PlayerEnterInARoomThen += Then;
}
