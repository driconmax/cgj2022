using System.Collections.Generic;
using UnityEngine;

public class MapView : MonoBehaviour
{
    [SerializeField] Mapper mapper;
    [SerializeField] private List<CellView> groundPrefabs;
    [SerializeField] List<SceneSpawnObject> sceneSpawnObjects;

    private MapPresenter _controller;
    private MapPresenter Initialize => new MapPresenter(this, new CreateMap(mapper), sceneSpawnObjects);

    private void Awake()
    {
        _controller = Initialize;
    }

    private void Start()
    {
        _controller.Present();
    }

    public CellView CreateGround(int type, Vector2 position)
    {
        var cellView = Instantiate(groundPrefabs[type], transform);

        cellView.transform.position = position;

        return cellView;
    }

    public SceneSpawnObject CreateSpawnObject(SceneSpawnObject spawnObject, Vector2 position)
    {
       return  Instantiate(spawnObject, position, Quaternion.identity);
    }
}