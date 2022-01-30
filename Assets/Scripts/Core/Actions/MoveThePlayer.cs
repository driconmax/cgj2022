using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveThePlayer : PlayerMovementController
{
    private ICharacterView _view;
    private Map _map;
    public Vector2Int _characaterPosition;
    CharacterRenderer _characterRenderer;
    CheckPlayerCombo _checkPlayerCombo;
    Action<int> _OnPlayerMove;

    public MoveThePlayer(ICharacterView view, Map map, Vector2Int characterPositon, CharacterRenderer characterRenderer, List<PlayerCombo> playerCombos, Action<int> OnPlayerMove)
    {
        _view = view;
        _map = map;
        _OnPlayerMove += OnPlayerMove;
        _characaterPosition = characterPositon;
        _characterRenderer = characterRenderer;
        _checkPlayerCombo = new CheckPlayerCombo(playerCombos);
    }


    public void MoveCharacterRight(int playerIndex)
    {
        if (IsValidPosition(_characaterPosition.x + 1, _characaterPosition.y))
        {
            MoveCharacterToPosition(_characaterPosition.x + 1, _characaterPosition.y);

            _OnPlayerMove(_checkPlayerCombo.Check(Cardinal.East));
        }
    }

    public void MoveCharacterLeft(int playerIndex)
    {
        if (IsValidPosition(_characaterPosition.x - 1, _characaterPosition.y))
        {
            MoveCharacterToPosition(_characaterPosition.x - 1, _characaterPosition.y);
            _OnPlayerMove(_checkPlayerCombo.Check(Cardinal.West));
        }
    }

    public void MoveCharacterUp(int playerIndex)
    {
        if (IsValidPosition(_characaterPosition.x, _characaterPosition.y + 1) && IsValidPositionForPlayer(_characaterPosition.y + 1, playerIndex))
        {
            MoveCharacterToPosition(_characaterPosition.x, _characaterPosition.y + 1);
           _OnPlayerMove(_checkPlayerCombo.Check(Cardinal.North));
        }
    }

    public void MoveCharacterDown(int playerIndex)
    {
        if (IsValidPosition(_characaterPosition.x, _characaterPosition.y - 1) && IsValidPositionForPlayer(_characaterPosition.y - 1, playerIndex))
        {
            MoveCharacterToPosition(_characaterPosition.x, _characaterPosition.y - 1);
            _OnPlayerMove(_checkPlayerCombo.Check(Cardinal.South));
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

    private bool IsValidPositionForPlayer(int y, int playerIndex)
    {
        return (playerIndex == 0)? y < _map.grid.Count/2 : y >= _map.grid.Count/2;
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
    void MoveCharacterRight(int playerIndex);
    void MoveCharacterLeft(int playerIndex);
    void MoveCharacterUp(int playerIndex);
    void MoveCharacterDown(int playerIndex);
}
