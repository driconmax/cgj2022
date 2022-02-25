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
    [SerializeField] GameObject gameHud;
    [SerializeField] Button next;
    [SerializeField] Button previous;
    [SerializeField] SkeletonGraphic animator;

    private int _index = 1;
    private Spine.Skin[] _skins;

    private void Awake()
    {
        ShowLobby();

        _skins = animator.Skeleton.Data.Skins.Items;
    }

    public void ShowLobby()
    {

        //play keyboard sound
        //nickname.onValueChanged.AddListener();
        play.onClick.AddListener(() =>
        {
            animator.SkeletonData.FindAnimation("menu_SELECTION");

        });

        next.onClick.AddListener(() =>
        {
            _index++;

            if (_index >= _skins.Length)
            {
                _index = 1;
            }

            animator.Skeleton.SetSkin(_skins[_index]);
            animator.Skeleton.SetToSetupPose();
            animator.Skeleton.SetSlotsToSetupPose();
        });

        previous.onClick.AddListener(() =>
        {
            _index--;

            if (_index < 1)
            {
                _index = _skins.Length - 1;
            }

            animator.Skeleton.SetSkin(_skins[_index]);
            animator.Skeleton.SetToSetupPose();
            animator.Skeleton.SetSlotsToSetupPose();
        });


        lobby.SetActive(true);
    }

    public void HideLobby()
    {
        lobby.SetActive(false);
    }

    public void SetUpButtonPlay(Action<(string, int)> EnterGame)
    {
        play.onClick.AddListener(() =>
        {
            EnterGame((nickname.text, _index));
        });
    }

    public void ShowWaitingRoom(bool activate)
    {
        waitingRoom.SetActive(activate);
    }

    public void ShowGameHud(bool activate)
    {
        gameHud.SetActive(activate);
    }

    public void ShowButton()
    {
        play.gameObject.SetActive(true);
    }
}
