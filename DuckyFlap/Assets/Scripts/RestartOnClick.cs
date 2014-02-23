using UnityEngine;
using System.Collections;

public class RestartOnClick : MonoBehaviour {

    void OnMouseUp()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
