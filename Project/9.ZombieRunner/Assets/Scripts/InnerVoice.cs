using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerVoice : MonoBehaviour 
{
    public AudioClip whatHappened;

    private AudioSource audioSource;

	// Use this for initialization
	void Start () 
    {
        audioSource = GetComponent<AudioSource>();
	}
	
    public void PlayWhatHappened()
    {
        audioSource.clip = whatHappened;
        audioSource.Play();
    }

}
