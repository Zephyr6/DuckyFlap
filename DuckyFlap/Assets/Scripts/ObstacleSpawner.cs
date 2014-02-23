using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour {

    public float SpawnDelay = 2.0F;
    public GameObject PrefabToSpawn;
    public bool IsSpawning = true;
    public float RangeBottom = -11.5F;
    public float RangeTop = -7F;

    private float CurrentTime = 0F;

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
                Vector3 spawnPos = new Vector3(transform.position.x, Random.Range(RangeBottom, RangeTop), transform.position.z);
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
