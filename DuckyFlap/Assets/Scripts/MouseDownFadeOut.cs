using UnityEngine;
using System.Collections;

public class MouseDownFadeOut : MonoBehaviour {

    public float Duration = .5F;
    public ObstacleSpawner ObstacleSpawner;

    private float counter;
    private bool hasStarted = false;
    private SpriteRenderer[] renderers;

    void Start()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
        counter = Duration * 60;
    }



	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            if (ObstacleSpawner != null)
                ObstacleSpawner.IsSpawning = true;
        }

        if (hasStarted)
        {
            counter--;

            for (int i = 0; i < renderers.Length; i++)
            {
                float r = renderers[i].color.r;
                float g = renderers[i].color.g;
                float b = renderers[i].color.b;

                renderers[i].color = new Color(r, g, b, counter / (Duration * 60));

                if (renderers[i].color.a <= 0F)
                {
                    Destroy(gameObject);
                }
            }
        }
	}
}
