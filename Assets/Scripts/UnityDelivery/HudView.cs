using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class HudView : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject gameHud;

    private int _localPlayerIndex;
    private Dictionary<int, int> _playerScores = new Dictionary<int, int>();

    public int SetLocalPlayerIndex(int value) => _localPlayerIndex = value;

    public void OnNatureChange(int playerIndex, int comboLevel)
    {
        _playerScores[playerIndex] += comboLevel;

        UpdateUI();
    }

    private void UpdateUI()
    {
        int total = 0;
        foreach (int key in _playerScores.Keys)
        {
            total += _playerScores[key];
        }

        if(total > 0)
        {
            slider.value = (float)_playerScores[_localPlayerIndex] / (float)total;
        } else
        {
            slider.value = 0.5f;
        }

    }

    public void ToggleGameHud(bool state)
    {
        gameHud.SetActive(state);
    }
}
