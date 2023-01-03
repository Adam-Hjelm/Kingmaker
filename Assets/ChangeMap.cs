using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMap : MonoBehaviour
{

    public Sprite[] sprites;
    public GameObject[] wallPrefabs;

    private int randomPrefab;
    private GameObject currentWalls;

    // Start is called before the first frame update



    public void RandomizeMap()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        randomPrefab = Random.Range(0, wallPrefabs.Length);
        currentWalls = Instantiate(wallPrefabs[randomPrefab]);
    }

    public void ClearMap()
    {
        if (currentWalls != null)
        {
            Destroy(currentWalls);
        }
    }
}
