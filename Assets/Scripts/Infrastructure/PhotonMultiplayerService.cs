using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonMultiplayerService : MonoBehaviourPunCallbacks, MultiplayerService
{

    private string roomName;
    private bool isConnecting;

    public void Execute()
    {
        PhotonNetworkAdapter.AutomaticallySyncScene(true);
        isConnecting = PhotonNetworkAdapter.ConnectUsingSettings();
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
            PhotonNetworkAdapter.JoinLobby();
            isConnecting = false;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {

    }

    public override void OnJoinedLobby()
    {
        Debug.Log("PHOTON: OnJoinedLobby()");
    }


    public override void OnLeftRoom()
    {
        
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("PHOTON: OnJoinedRoom()");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("PHOTON: OnCreatedRoom()");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
     
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        
    }
}
