using UnityEngine;
using System.Collections;

public class MountainBehavior : MonoBehaviour {

    public float BaseSpeed = 1F;
	
	void FixedUpdate () {
        // move right
        transform.position = new Vector3(transform.position.x - (BaseSpeed / 60), transform.position.y, transform.position.z);

        // destroy if position x <-8
        if (transform.position.x < -15)
            Destroy(gameObject);  
	}
}
