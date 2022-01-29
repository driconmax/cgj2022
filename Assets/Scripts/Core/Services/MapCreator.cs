using UnityEngine;

public interface MapCreator
{
    public Map Execute();
    public Vector2 GetStartPosition_Player1();
    public Vector2 GetStartPosition_Player2();
}
