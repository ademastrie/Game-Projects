using UnityEngine;
using System.Collections;

public class FishSpawner : MonoBehaviour {

    public GameObject[] fishes;
    private int fishColor;
    public float wait, restart;
    
    // Use this for initialization
    void Start () {
        InvokeRepeating("SpawnFish", wait,restart);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SpawnFish() {
        fishColor = Random.Range(0, fishes.Length);
        Vector2 spawnPosition = new Vector2(15f, Random.Range(4f, -5f));
        Instantiate(fishes[fishColor] as GameObject, spawnPosition, Quaternion.identity);
    }
}

