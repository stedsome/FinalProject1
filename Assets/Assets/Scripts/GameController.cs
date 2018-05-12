using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Vector3 spawnPos;
    public GameObject Enemy;
    public GameObject Boss;
    public Transform parent;
    public float waveSpawnDist;
    private bool isSpawned = false;
    private int numWaves = 1;

    // Use this for initialization
    private void Update()
    {
        if (Global.GameState == Global.state.Play)
        {
            if (Global.lengthRun > waveSpawnDist * numWaves)
            {
                spawnWaves(numWaves++);
            }
        }

        if (Global.isBoss && (!isSpawned))
        {
            isSpawned = true;
            Boss.SetActive(true);
        }
        if(!Global.isBoss)
        {
            Boss.SetActive(false);
        }
        if (Global.lengthRun % 100 != 0)
        {
            isSpawned = false;
        }
    }

    // Update is called once per frame
    public void spawnWaves (int numWaves) {
        for (int i = 0; i < numWaves; i++)
        {
            Vector3 SpawnPositions = new Vector3(Random.Range(-spawnPos.x, spawnPos.x), spawnPos.y, spawnPos.z);
            Instantiate(Enemy, SpawnPositions, Quaternion.identity, parent);
        }
	}
}
