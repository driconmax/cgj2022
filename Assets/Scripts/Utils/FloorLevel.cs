using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FloorLevel", menuName = "Scene/Floor Level")]
public class FloorLevel : ScriptableObject
{
    public string name;
    public List<FloorSprites> floorSprites;
}
