using System;
using UnityEngine;

public class Cell
{
    public enum AttachmentTypes
    {
        CLEAR,
        BUTTON,
        TREE
    }

    public int GetFloorType => _floorType;
    public AttachmentTypes GetAttachmentType => _attachmentType;

    private CellView _view;
    private int _index;
    private int _row;
    private int _column;
    private Vector2 _offset;
    private Vector2 _cellSize;
    private int _floorType;
    private AttachmentTypes _attachmentType;
    private bool _status;
    private SceneSpawnObject _currentAttachment;

    public Cell(int index, int row, int column, Vector2 offset, Vector2 cellSize, int floorType, AttachmentTypes attachmentType, bool status)
    {
        _index = index;
        _row = row;
        _column = column;
        _offset = offset;
        _cellSize = cellSize;
        _floorType = floorType;
        _attachmentType = attachmentType;
        _status = status;
    }

    public void SetView(CellView cellView)
    {
        _view = cellView;
    }

    public Vector2 GetPosition()
    {
        return new Vector3(_row * _cellSize.x + _column * _cellSize.y + _offset.x, _column * _cellSize.y / 2f - _row * _cellSize.y / 2f + _offset.y, 1f);
    }

    public void ChangeStatus(bool status)
    {
        _status = status;
        _view.ChangeStatus(status);
    }


    public void SpawnAttachment(SceneSpawnObject sceneSpawnObject)
    {
        _attachmentType = AttachmentTypes.TREE;
        _currentAttachment = sceneSpawnObject;
        _view.SpawnAttachment(sceneSpawnObject);
    }

    internal Vector2Int GetIntPosition()
    {
        return new Vector2Int(_column, _row);
    }
}
