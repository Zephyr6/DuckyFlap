using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class RestartMenu : MonoBehaviour {

	void Start () {
        HOTween.Init(true, true, true);
	}

    public void Show()
    {
        HOTween.To(transform, 1.5F, new TweenParms()
                        .Prop("position", Vector3.zero, false)
                        .Ease(EaseType.EaseOutBounce)
                        );
    }
}
