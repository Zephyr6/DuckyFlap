using UnityEngine;
using System.Collections;

public class Tilt : MonoBehaviour {

    public float hopSpeed = 0F;

    public float topAngle = 90F;
    public float lowAngle = -90F;

    public AudioClip pickup;
    public AudioClip explode;
    public AudioClip wing;

    public bool IsStarted = false;

    private Vector3 _origPos;
    private bool pressed = false;
    private Animator animator;
    private GameObject scoreKeeper;
    private bool hasExploded = false;
    private Flash flasher;
    private float gravityScale = 0F;

    public int score = 0;
    public int hiScore = 0;

    void Start()
    {
        _origPos = transform.position;
        animator = GetComponent<Animator>();
        scoreKeeper = GameObject.FindWithTag("Score");
        GameObject f = GameObject.FindWithTag("Flash");
        if (f != null)
            flasher = f.GetComponent<Flash>();

        // Start as flapping
        animator.SetBool("IsFlapping", true);

        // Set Gravity Scale
        gravityScale = rigidbody2D.gravityScale;
        rigidbody2D.gravityScale = 0F;

        // Get High score
        hiScore = PlayerPrefs.GetInt("HighScore");
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
            Boom();
        }
    }

    void Boom()
    {
        // Fix our angle and stop moving
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.fixedAngle = true;
        rigidbody2D.isKinematic = true;

        // Explode ourselves
        animator.SetBool("Exploded", true);

        // Set spawner to stop spawning
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Respawn");
        for (int i = 0; i < spawners.Length; i++)
        {
            ObstacleSpawner os = spawners[i].GetComponent<ObstacleSpawner>();
            if (os != null)
                os.IsSpawning = false;

            MountainSpawner ms = spawners[i].GetComponent<MountainSpawner>();
            if (ms != null)
                ms.IsSpawning = false;
        }

        // Stop moving Obstacles
        GameObject[] Obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        for (int i = 0; i < Obstacles.Length; i++)
        {
            ObstacleBehavior ob = Obstacles[i].GetComponent<ObstacleBehavior>();
            if (ob != null)
                ob.speed = 0;
        }

        // Set ourselves as exploded
        hasExploded = true;

        // Stop clouds
        GameObject particles = GameObject.FindGameObjectWithTag("Particles");
        if (particles != null)
        {
            particles.GetComponent<ParticleSystem>().Pause();
        }

        // Stop mountains
        GameObject[] Mountains = GameObject.FindGameObjectsWithTag("Mountains");
        for (int i = 0; i < Mountains.Length; i++)
        {
            MountainBehavior ob = Mountains[i].GetComponent<MountainBehavior>();
            if (ob != null)
                ob.BaseSpeed = 0;
        }

        // Stop Music
        GameObject music = GameObject.FindGameObjectWithTag("Music");
        if (music != null)
        {
            music.GetComponent<AudioSource>().Stop();
            Destroy(music);
        }

        // Flash the screen
        flasher.StartFlash();

        // Play Explosion Sound
        audio.clip = explode;
        audio.Play();

        // Show menu
        GameObject.FindGameObjectWithTag("RestartMenu").GetComponent<RestartMenu>().Show();
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag.Contains("Goal"))
        {
            score++;
            audio.clip = pickup;
            audio.Play();
            Destroy(collider.gameObject);
            scoreKeeper.GetComponent<OutlinedText>().SetText(score.ToString());

            if (score > hiScore)
                PlayerPrefs.SetInt("HighScore", score);
        }
    }

	void FixedUpdate () {

        if (IsStarted)
        {
            DoMovement();
        }

        if (pressed && !hasExploded)
        {
            if (!IsStarted)
            {
                IsStarted = true;
                rigidbody2D.gravityScale = gravityScale;
            }

            animator.SetBool("IsFlapping", true);
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, hopSpeed);
            pressed = false;
            audio.clip = wing;
            audio.Play();
        }
	}

    private void DoMovement()
    {
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

            if (angle < -20)
                animator.SetBool("IsFalling", true);
            else
                animator.SetBool("IsFalling", false);
        }

        if (transform.position.y > 6F && !hasExploded)
            Boom();
    }
}
