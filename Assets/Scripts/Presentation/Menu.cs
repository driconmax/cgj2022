using System;

public interface Menu
{
    void ShowLobby();
    void HideLobby();
    void ShowButton();
    void ShowWaitingRoom(bool activate);
    void SetUpButtonPlay(Action<(string, int)> EnterGame);
}
