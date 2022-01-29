using System;
using System.Collections;
using System.Collections.Generic;

public class CreateMap : MapCreator
{

    private readonly Mapper _mapper;

    public CreateMap(Mapper mapper)
    {
        _mapper = mapper;
    }

    public Map Execute()
    {
        var _map = new Map();

        for (int column = 0; column < _mapper.Colums; column++)
        {
            var _row = new List<Cell>();

            for (int row = 0; row < _mapper.Rows; row++)
            {
                _row.Add(new Cell());
            }

            _map.grid.Add(_row);
        }

        return _map;
    }
}

[Serializable]
public struct Mapper
{
    public int Rows;
    public int Colums;
    public (float,float) CellSize;
}

public class Map
{
    public List<List<Cell>> grid = new List<List<Cell>>();

    public Map()
    {

    }
} 

public struct Cell
{ 
    public int Index;
}

public interface MapCreator
{
    public Map Execute();
}
