using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCombo", menuName = "Player/Combo", order = 1)]
public class PlayerCombo : ScriptableObject
{
    public string name;
    public List<Cardinal> cardinals;
    public int value;
}
