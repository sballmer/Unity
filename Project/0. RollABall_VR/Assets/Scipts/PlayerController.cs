using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    public float thrust;
    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // called at each frame but before any calculous made in rigidbody
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rigidbody.AddForce(new Vector3(moveHorizontal, 0f, moveVertical) * thrust);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<CubeControlle>())
            collider.gameObject.SetActive(false);
    }
}
