using UnityEngine;
using System.Collections;

public class Tilt : MonoBehaviour {

    public float hopSpeed = 0F;

    public float topAngle = 90F;
    public float lowAngle = -90F;

    private Vector3 _origPos;
    private bool pressed = false;
    private Animator animator;
    private GameObject scoreKeeper;
    private bool hasExploded = false;

    private int score = 0;

    void Start()
    {
        _origPos = transform.position;
        animator = GetComponent<Animator>();
        scoreKeeper = GameObject.FindWithTag("Score");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            pressed = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Obstacle"))
        {
            // Fix our angle and stop moving
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.fixedAngle = true;
            rigidbody2D.isKinematic = true;

            // Explode ourselves
            animator.SetBool("Exploded", true);

            // Set spawner to stop spawning
            GameObject spawner = GameObject.FindGameObjectWithTag("Respawn");
            spawner.GetComponent<ObstacleSpawner>().IsSpawning = false;

            // Stop moving Obstacles
            GameObject[] Obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
            for (int i = 0; i < Obstacles.Length; i++)
            {
                ObstacleBehavior ob = Obstacles[i].GetComponent<ObstacleBehavior>();
                if(ob != null)
                    ob.speed = 0;
            }

            hasExploded = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag.Contains("Goal"))
        {
            score++;
            scoreKeeper.GetComponent<OutlinedText>().SetText(score.ToString());
        }
    }

	void FixedUpdate () {
	    // 90 to -90 z rotation depending on y velocity
        // y vel negative = -90 z rotation
        Vector3 moveDirection = rigidbody2D.velocity;
        if (moveDirection != Vector3.zero)
        {
            float angle = moveDirection.y * 20;

            angle = angle > topAngle ? topAngle : angle < lowAngle ? lowAngle : angle;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (angle < 20)
                animator.SetBool("IsFlapping", false);

            if(angle < -20)
                animator.SetBool("IsFalling", true);
            else
                animator.SetBool("IsFalling", false);
        }

        if (pressed && !hasExploded)
        {
            animator.SetBool("IsFlapping", true);
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, hopSpeed);
            pressed = false;
        }
	}
}
