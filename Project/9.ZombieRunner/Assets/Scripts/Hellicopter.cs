using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hellicopter : MonoBehaviour 
{
    public AudioClip callSound;

    private bool called = false;
    private AudioSource audioSource;
    private Rigidbody rigidbody;

	// Use this for initialization
	void Start () 
    {
        audioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    void PlayHeliCopterCall()
    {
        audioSource.clip = callSound;
        audioSource.Play();
        print("playing");
    }

    public void Call() 
    {
        if (!called)
        {
            PlayHeliCopterCall();
            called = true;
            rigidbody.velocity = new Vector3(0f, 0f, 50f);
        }
    }
}
