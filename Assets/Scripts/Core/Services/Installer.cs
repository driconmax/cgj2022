using System.Collections.Generic;
using UnityEngine;

public interface Installer
{
    void GenerateMap(Map map);
    Dictionary<Cell, CellView> CellToObject { get; }
}