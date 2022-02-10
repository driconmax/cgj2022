using System.Collections.Generic;
using UnityEngine;

public class MapView : MonoBehaviour
{
    [SerializeField] Mapper mapper;
    [SerializeField] private List<CellView> groundPrefabs;
    [SerializeField] List<SceneSpawnObject> sceneSpawnObjects;
    [SerializeField] Transform groundParent;
    [SerializeField] Transform spawnObjectParent;

    private MapPresenter _presenter;
    private MapPresenter Present => new MapPresenter(this, new CreateMap(mapper), sceneSpawnObjects);

    public MapPresenter Presenter => _presenter;

    public void Initialize()
    {
        _presenter = Present;
        _presenter.Present();

        gameObject.SetActive(true);
    }

    public CellView CreateGround(int type, Vector2 position)
    {
        var cellView = Instantiate(groundPrefabs[type], groundParent);

        cellView.transform.position = position;

        return cellView;
    }

    public GameObject CreateSpawnObject(GameObject spawnObject, Vector2 position)
    {
       return Instantiate(spawnObject, position, Quaternion.identity, spawnObjectParent);
    }
}