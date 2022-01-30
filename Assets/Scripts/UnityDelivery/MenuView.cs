using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;

public class MenuView : MonoBehaviour, Menu
{
    [SerializeField] Button play;
    [SerializeField] TMP_InputField nickname;
    [SerializeField] GameObject lobby;
    [SerializeField] GameObject waitingRoom;

    public void ShowLobby()
    {
        //play keyboard sound
        //nickname.onValueChanged.AddListener();
        lobby.SetActive(true);
    }

    public void HideLobby()
    {
        lobby.SetActive(false);
    }

    public void SetUpButtonPlay(Action<string> EnterGame)
    {
        play.onClick.AddListener(() => EnterGame(nickname.text));
    }

    public void ShowWaitingRoom(bool activate)
    {
        waitingRoom.SetActive(activate);
    }

    public void ShowButton()
    {
        play.gameObject.SetActive(true);
    }
}
