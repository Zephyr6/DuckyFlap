using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class TitleBounce : MonoBehaviour {

    public EaseType ease = EaseType.Linear;
    public float y_top = 2.9F;
    public float y_bottom = 2.6F;
    public float Duration = .6F;

	void Start () {
        HOTween.Init(true, true, true);

        Sequence s = new Sequence(new SequenceParms().Loops(-1));

        s.Append(HOTween.To(transform, Duration/2, new TweenParms()
                            .Prop("position", new Vector3(4.3F, y_top, 0F), false)
                            .Ease(ease)));

        s.Append(HOTween.To(transform, Duration/2, new TweenParms()
                            .Prop("position", new Vector3(4.3F, y_bottom, 0F), false)
                            .Ease(ease)));

        s.Play();
	}
	
	void Update () {
	
	}
}
