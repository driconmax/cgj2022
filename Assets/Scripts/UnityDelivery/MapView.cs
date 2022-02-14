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

    [Range(0, 10f)]
    public float speed = 1f;

    private float offset = 0;

    public void Initialize()
    {
        _presenter = Present;
        _presenter.Present();

        gameObject.SetActive(true);
    }

    private void LateUpdate()
    {

        for (var column = 0; column < _presenter.Map.grid.Count; column++)
        {
            for (var row = 0; row < _presenter.Map.grid[column].Count; row++)
            {
                Cell cell = _presenter.Map.grid[column][row];

                int sample = (int)((Noise.Sum(Noise.methods[1][1], cell.GetPosition(), 0.2f, 1, 2f, 0.5f, offset) * 0.5f + 0.5f) * 4f);
                cell.GetView.SetFloor(sample);
            }
        }

        offset += speed * Time.deltaTime;
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