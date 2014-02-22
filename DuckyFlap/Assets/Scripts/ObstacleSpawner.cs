using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour {

    public float SpawnDelay = 2.0F;
    public GameObject PrefabToSpawn;
    public bool IsSpawning = true;

    private float CurrentTime = 0F;
    private Random rand = new Random();

    private void ResetTimer()
    {
        CurrentTime = SpawnDelay * 60;
    }

	// Update is called once per frame
	void FixedUpdate () 
    {
        if (IsSpawning)
        {
            if (CurrentTime <= 0)
            {
                // Spawn thing
                Vector3 spawnPos = new Vector3(transform.position.x, Random.Range(-11.5F, -7F), transform.position.z);
                Instantiate(PrefabToSpawn, spawnPos, Quaternion.identity);

                ResetTimer();
            }
            else
            {
                CurrentTime--;
            }
        }
	}
}
