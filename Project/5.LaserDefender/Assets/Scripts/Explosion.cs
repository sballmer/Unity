using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Invoke ("end", getAnimationLength ());
	}
	
	void end()
	{
		Destroy (this.gameObject);
	}

	float getAnimationLength()
	{
		/*
		float length = 0f;
		Animator anim = GetComponent<Animator>();

		UnityEditorInternal.AnimatorController ac = anim.runtimeAnimatorController as UnityEditorInternal.AnimatorController;
		
		UnityEditorInternal.StateMachine sm = ac.GetLayer (0).stateMachine;
		
		for (int i = 0; i < sm.stateCount; i++) 
		{
			UnityEditorInternal.State state = sm.GetState (i);
			AnimationClip clip = state.GetMotion () as AnimationClip;
			if (clip != null) 
			{
				length = clip.length;
			}
		}
		return length;
		*/
		return 1.35f;
	}
}
