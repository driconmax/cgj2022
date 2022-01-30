using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Object", menuName = "Scene/Spawn Object", order = 1)]
public class SceneSpawnObject : ScriptableObject
{ 
    public GameObject gameObject;
    public int value;
}
