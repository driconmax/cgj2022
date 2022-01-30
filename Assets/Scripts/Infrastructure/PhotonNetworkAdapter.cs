using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public static class PhotonNetworkAdapter 
{

    public static void SetPlayerNickName(string nick)
    {
        PhotonNetwork.NickName = nick;
    }

    public static bool IsConnected => PhotonNetwork.IsConnected;

    public static bool JoinLobby(TypedLobby typedLobby = null)
    {
        return PhotonNetwork.JoinLobby(typedLobby);
    }

    public static bool CreateRoom(string roomName, RoomOptions roomOptions = null, TypedLobby typedLobby = null, string[] expectedUsers = null)
    {
        return PhotonNetwork.CreateRoom(roomName, roomOptions, typedLobby, expectedUsers);
    }

    public static bool JoinRoom()
    {
        return PhotonNetwork.JoinRandomOrCreateRoom(null, 2);
    }

    public static bool ConnectUsingSettings()
    {
        return PhotonNetwork.ConnectUsingSettings();
    }

    public static bool AutomaticallySyncScene(bool value)
    {
        return PhotonNetwork.AutomaticallySyncScene = value;
    }

    public static void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    public static bool LeaveRoom(bool becomeInactive = true)
    {
        return InRoom() ? PhotonNetwork.LeaveRoom(becomeInactive) : false;
    }

    public static bool RejoinRoom(string roomName)
    {
        return PhotonNetwork.RejoinRoom(roomName);
    }

    public static bool InRoom()
    {
        return PhotonNetwork.InRoom;
    }

    public static bool HasCounterPlayer()
    {
        return PhotonNetwork.CurrentRoom == null ? false : PhotonNetwork.CurrentRoom.PlayerCount > 1;
    }

    public static Room CurrentRoom()
    {
        return PhotonNetwork.CurrentRoom;
    }

    public static bool GetRoomsList()
    {
        return PhotonNetwork.GetCustomRoomList(TypedLobby.Default, "*");
    }

    public static GameObject Instantiate(string name, Vector3 position, Quaternion rotation)
    {
        return PhotonNetwork.Instantiate(name, position, rotation);
    }

    public static bool IsMasterClient()
    {
        return PhotonNetwork.IsMasterClient;
    }

    public static  bool SetMasterClient(Player player)
    {
        return PhotonNetwork.SetMasterClient(player);
    }


}
