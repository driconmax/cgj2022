using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MapCreator
{
    private readonly Mapper _mapper;
    private Map _map;

    public Vector2Int GetEnemyStartPosition => _mapper.EnemyStartPosition;
    public Vector2Int GetPlayerStartPosition => _mapper.PlayerStartPosition;

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
}
