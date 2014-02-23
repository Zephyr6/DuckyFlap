using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {

	void OnMouseUp()
    {
        Application.LoadLevel(1);
    }
}
