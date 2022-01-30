using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveThePlayer : PlayerMovementController
{
    private CharacterView _view;
    private Map _map;
    public Vector2Int _characaterPosition;
    CharacterRenderer _characterRenderer;


    public MoveThePlayer(CharacterView view, Map map, Vector2Int characterPositon, CharacterRenderer characterRenderer)
    {
        _view = view;
        _map = map;
        _characaterPosition = characterPositon;
        _characterRenderer = characterRenderer;
    }


    public void MoveCharacterRight()
    {
        if (IsValidPosition(_characaterPosition.x + 1, _characaterPosition.y))
        {
            MoveCharacterToPosition(_characaterPosition.x + 1, _characaterPosition.y);
        }
    }

    public void MoveCharacterLeft()
    {
        if (IsValidPosition(_characaterPosition.x - 1, _characaterPosition.y))
        {
            MoveCharacterToPosition(_characaterPosition.x - 1, _characaterPosition.y);
        }
    }

    public void MoveCharacterUp()
    {
        if (IsValidPosition(_characaterPosition.x, _characaterPosition.y + 1))
        {
            MoveCharacterToPosition(_characaterPosition.x, _characaterPosition.y + 1);
        }
    }

    public void MoveCharacterDown()
    {
        if (IsValidPosition(_characaterPosition.x, _characaterPosition.y - 1))
        {
            MoveCharacterToPosition(_characaterPosition.x, _characaterPosition.y - 1);
        }
    }

    private void MoveCharacterToPosition(int newX, int newY)
    {
        _characaterPosition = new Vector2Int(newX, newY);
        _characterRenderer.SetDirection(_characaterPosition);
        _view.MovePlayerToCell(_characaterPosition.x, _characaterPosition.y);
    }

    private bool IsValidPosition(int x, int y)
    {
        return PositionExistsInMap(x, y) && ThereIsNoTreeInPosition(x, y);
    }

    private bool ThereIsNoTreeInPosition(int x, int y)
    {
        return _map.grid[y][x].Type != 3;
    }

    private bool PositionExistsInMap(int x, int y)
    {
        return y >= 0 &&
               y < _map.grid.Count &&
               x >= 0 &&
               x < _map.grid[y].Count;
    }
}

public interface PlayerMovementController
{
    void MoveCharacterRight();
    void MoveCharacterLeft();
    void MoveCharacterUp();
    void MoveCharacterDown();
}
