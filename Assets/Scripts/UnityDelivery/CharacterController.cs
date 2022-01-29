using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour, CharacterView
{
    private PlayerMovementController _moveThePlayer;
    private Map _map;
    private Vector2Int _characterPosition;

    private MoveThePlayer _Initialize => new MoveThePlayer(this, _map, _characterPosition);

    public void Initialize(Map map, Vector2Int characterPosition)
    {
        _map = map;
        _characterPosition = characterPosition;
        _moveThePlayer = _Initialize;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            _moveThePlayer.MoveCharacterLeft();
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            _moveThePlayer.MoveCharacterRight();
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            _moveThePlayer.MoveCharacterUp();
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            _moveThePlayer.MoveCharacterDown();
    }

    public void MovePlayerToCell(int row, int column)
    {
        MovePlayerToCell(_map.grid[column][row]);
    }


    private void MovePlayerToCell(Cell gridCell)
    {
        transform.localPosition = gridCell.GetPosition();
    }
}


public interface CharacterView
{
    void MovePlayerToCell(int row, int column);
}