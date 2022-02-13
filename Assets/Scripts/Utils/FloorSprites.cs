using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FloorSprites", menuName = "Scene/Floor Sprites")]
public class FloorSprites : ScriptableObject
{
    public int test;
    [SerializeField]
    public List<Sprite> sprites;

}
