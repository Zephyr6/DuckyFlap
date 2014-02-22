using UnityEngine;
using System.Collections;

public class OutlinedText : MonoBehaviour {

    public void SetText(string text)
    {
        guiText.text = text;

        foreach (GUIText g in GetComponentsInChildren<GUIText>())
        {
            g.text = text;
        }
    }
}
