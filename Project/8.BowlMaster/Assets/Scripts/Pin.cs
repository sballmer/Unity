using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour 
{
    public bool isActive { get; private set; }

    private Rigidbody myRigidbody;

	// Use this for initialization
	void Start () 
    {
        isActive = true;
        myRigidbody = GetComponent<Rigidbody>();
	}

    public void checkActive()
    {
        if (!isActive)
            return;

        Vector3 Up = Vector3.up;
        Vector3 pinDirection = Vector3.Normalize(transform.up);

        float angle = Mathf.Abs(Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(Up, pinDirection)));

        const float angleThresholdDeg = 25f;

        if (angle > angleThresholdDeg)
            isActive = false;
    }
        
    public void SetPinUp()
    {
        checkActive();

        if (isActive)
        {
            myRigidbody.useGravity = false;
            DontMove();

            const float upSpeed = 100f,
                        finalHeigh= 50f;
            myRigidbody.velocity = new Vector3(0f, upSpeed, 0f);

            Invoke("DontMove", finalHeigh / upSpeed);
        }
    }

    void DontMove()
    {
        myRigidbody.velocity = Vector3.zero;
        myRigidbody.angularVelocity = Vector3.zero;
    }

    public void ResetGravity()
    {
        myRigidbody.useGravity = true;
    }

    void OnTriggerExit(Collider collide)
    {
        if (collide.GetComponent<PinSetter> ())
            Destroy (gameObject); // isActive = false;
    }
}
