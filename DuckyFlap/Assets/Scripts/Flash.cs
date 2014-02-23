using UnityEngine;
using System.Collections;


public class Flash : MonoBehaviour 
{
    public float Duration = .5F;

    private float counter;
    private GUITexture texture;

    void Start()
    {
        texture = GetComponent<GUITexture>();
    }

    public void StartFlash()
    {
        texture.color = new Color(1, 1, 1, 0.8F);
        counter = Duration * 60;
    }

	void FixedUpdate () 
    {
        if (counter > 0)
        {
            counter--;
            texture.color = new Color(1, 1, 1, (counter / (Duration * 60)) * .5F);
        }
	}
}
