using UnityEngine;
using System.Collections;

public class ObstacleBehavior : MonoBehaviour
{

    public float speed = 10F;

    void Start()
    {
    }

    void FixedUpdate()
    {
        // move right
        transform.position = new Vector3(transform.position.x - (speed / 60), transform.position.y, transform.position.z);

        // destroy if position x <-8
        if (transform.position.x < -8)
            Destroy(gameObject);  
    }
}
