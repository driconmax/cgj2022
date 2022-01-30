using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;
using Spine.Unity;

public class MenuView : MonoBehaviour, Menu
{
    [SerializeField] Button play;
    [SerializeField] TMP_InputField nickname;
    [SerializeField] GameObject lobby;
    [SerializeField] GameObject waitingRoom;
    [SerializeField] Button next;
    [SerializeField] Button previous;
    [SerializeField] SkeletonGraphic animator;

    public int index = 0;

    private void Awake()
    {
        ShowLobby();
    }

    public void ShowLobby()
    {
        var charactersCount = animator.Skeleton.Data.Skins.Count;

        //play keyboard sound
        //nickname.onValueChanged.AddListener();
        play.onClick.AddListener(() => animator.SkeletonData.FindAnimation("menu_SELECTION"));
       
        next.onClick.AddListener(() =>
        {
            index++;

            if (index >= charactersCount)
            {
                index = 0;
            }

            animator.Skeleton.SetSkin(animator.Skeleton.Data.Skins.Items[index]);
        });

        previous.onClick.AddListener(() =>
        {
            index--;

            if (index < 0)
            {
                index = charactersCount - 1;
            }

            animator.Skeleton.SetSkin(animator.Skeleton.Data.Skins.Items[index]);
        });


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
