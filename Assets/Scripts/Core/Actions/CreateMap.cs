using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MapCreator
{

    private readonly Mapper _mapper;
    private Map _map;

    public CreateMap(Mapper mapper)
    {
        _mapper = mapper;
    }

    public Map Execute()
    {
        _map = new Map();
        int cellCount = 0;

        for (int column = 0; column < _mapper.Colums; column++)
        {

            var _row = new List<Cell>();

            for (int row = 0; row < _mapper.Rows; row++)
            {

                _row.Add(new Cell
                {
                    Index = cellCount,
                    Row = row,
                    Column = column,
                    Offset = _mapper.MapStartPosition,
                    CellSize = _mapper.CellSize,
                    Type = 0,
                    Status = false
                });

            }

            _map.grid.Add(_row);
        }

        return _map;
    }

    public Vector2 GetPlayerStartPosition(int playerIndex)
    {
        Vector2Int position = _mapper.PlayerStartPosition;
        if (playerIndex == 1)
        {
            position = _mapper.EnemyStartPosition;
        }
        return _map.grid[position.x][position.y].GetPosition();
    }

    public Vector2Int GetPlayerMappedStartPosition(int playerIndex)
    {
        if (playerIndex == 0)
        {
            return _mapper.PlayerStartPosition;
        } else {
            return _mapper.EnemyStartPosition;
        }
    }
}
