using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CellView : MonoBehaviour
{
    [SerializeField] List<Sprite> possibleSprites;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject buttonUp;

    private Cell _cell;

    private void Awake()
    {
        possibleSprites.Shuffle();        
        possibleSprites.First(sprite => spriteRenderer.sprite = sprite);
    }

    public void SetUp(Cell cell)
    {
        _cell = cell;
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