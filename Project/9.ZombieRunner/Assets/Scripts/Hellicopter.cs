using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hellicopter : MonoBehaviour 
{
    public AudioClip callSound;

    private bool called = false;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () 
    {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!called)
        {
            if (Input.GetButtonDown("CallHeli"))
            {
                PlayHeliCopterCall();
                called = true;
            }
        }
	}

    void PlayHeliCopterCall()
    {
        audioSource.clip = callSound;
        audioSource.Play();
        print("playing");
    }
}
