using UnityEngine.UI;
using UnityEngine;

public class HudView : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private int _playerIndex;

    public int SetPlayerIndex(int value) => _playerIndex = value;

    public void OnNatureChange(int playerIndex, int comboLevel)
    {
        if (_playerIndex == playerIndex)
        {
            
        }
    }

}
