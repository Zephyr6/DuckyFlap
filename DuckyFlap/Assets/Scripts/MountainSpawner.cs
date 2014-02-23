using UnityEngine;
using System.Collections.Generic;


public class MountainSpawner : MonoBehaviour {

    public float SpawnDelay = 2.0F;
    public float SpawnDelayRange = 1F;
    public List<GameObject> PrefabToSpawn;
    public bool IsSpawning = true;
    public float RangeBottom = 0F;
    public float RangeTop = 0F;

    private float CurrentTime = 0F;
    private int counter;
    private int offset = 1;

    void Start()
    {
        counter = PrefabToSpawn[0].renderer.sortingOrder;
    }

    private void ResetTimer()
    {
        CurrentTime = SpawnDelay * 60;
        CurrentTime += Random.Range(0, SpawnDelayRange) * 60;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsSpawning)
        {
            if (CurrentTime <= 0)
            {
                // Spawn thing
                Vector3 spawnPos = new Vector3(transform.position.x, Random.Range(RangeBottom, RangeTop), transform.position.z);
                Object ob = Instantiate(PrefabToSpawn[Random.Range(0, PrefabToSpawn.Count)], spawnPos, Quaternion.identity);

                if (offset == 0)
                    offset = 1;
                else if (offset == 1)
                    offset = -1;
                else if (offset == -1)
                    offset = 0;

                ((GameObject)ob).renderer.sortingOrder = counter + offset;

                ResetTimer();
            }
            else
            {
                CurrentTime--;
            }
        }
    }
}
