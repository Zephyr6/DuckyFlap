using UnityEngine;
using System.Collections;

public class BillboardScores : MonoBehaviour {

    public GUIText Score;
    public GUIText HighScore;

    public float xOffset = 0F;
    public float yOffset = 0F;

    public float xHSOffset = 0F;
    public float yHSOffset = 0F;

    private Camera mainCam;
    private GameObject player;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        int score = player.GetComponent<Tilt>().score;
        int hiScore = PlayerPrefs.GetInt("HighScore");
        Score.GetComponent<OutlinedText>().SetText(string.Format("SCORE:{0}", score));
        HighScore.GetComponent<OutlinedText>().SetText(string.Format("HI SCORE:{0}", hiScore));
    }
	
	void Update () 
    {
        Vector3 pos = mainCam.WorldToViewportPoint(transform.position);
        Score.transform.position = new Vector3(pos.x + xOffset, pos.y + yOffset, pos.z);
        HighScore.transform.position = new Vector3(pos.x + xHSOffset, pos.y + yHSOffset, pos.z);
	}
}
