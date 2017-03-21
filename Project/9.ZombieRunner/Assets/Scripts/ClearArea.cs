using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearArea : MonoBehaviour 
{
    private float timeSinceLastTrigger = 0f;
    private bool needToFindAClearArea = false;
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetButtonDown("CallHeli"))
            needToFindAClearArea = true;

        timeSinceLastTrigger += Time.deltaTime;

        if (needToFindAClearArea && timeSinceLastTrigger > 1f)
        {
            SendMessageUpwards("OnFindClearArea");
        }
	}

    void OnTriggerStay()
    {
        timeSinceLastTrigger = 0f;
    }
}
