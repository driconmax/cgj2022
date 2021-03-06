using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CellView : MonoBehaviour
{
    [SerializeField] FloorLevel floorLevel;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject buttonUp;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip popClip;
    [SerializeField] AudioClip downClip;
    [SerializeField] AudioClip upClip;
    [SerializeField] Transform button;
    [SerializeField] Transform attachment;

    private int floorIndex = 0;

    private GameObject _currentAttachment;
    private Dictionary<SceneSpawnObject, GameObject> _attachments;

    private Cell _cell;

    private void Awake()
    {
        _attachments = new Dictionary<SceneSpawnObject,GameObject>();
        spriteRenderer.sprite = floorLevel.floorSprites[0].sprites[0];
    }

    public void SetFloor(int index)
    {
        if (floorLevel.floorSprites.Count > index)
        {
            floorIndex = index;
            int rand = Random.Range(0, floorLevel.floorSprites[index].sprites.Count);
            spriteRenderer.sprite = floorLevel.floorSprites[index].sprites[0];
        }
    }

    public CellView ChangeStatus(bool status)
    {
        buttonUp.SetActive(status);
        audioSource.PlayOneShot(popClip);

        if (status)
        {
            audioSource.PlayOneShot(downClip);
        }
        else
        {
            audioSource.PlayOneShot(upClip);
        }

        return this;
    }
    public void SpawnAttachment(SceneSpawnObject sceneSpawnObject)
    {
        if (_attachments.ContainsKey(sceneSpawnObject))
        {
            _attachments[sceneSpawnObject].SetActive(true);
        }
        else
        {
            button.gameObject.SetActive(false);

            _currentAttachment?.SetActive(false);
            GameObject instance = Instantiate(sceneSpawnObject.gameObject, attachment);
            _attachments.Add(sceneSpawnObject, instance);
            _currentAttachment = instance;
        }
    }

    public void Damage()
    {
        if (_currentAttachment != null)
        {
            _currentAttachment.SetActive(false);
        }
        else
        {
            button.gameObject.SetActive(false);
        }
    }

}


public static class ListExtensions
{
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}