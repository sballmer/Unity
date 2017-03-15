using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinsGroup : MonoBehaviour 
{
    public GameObject pinPrefab;

	// Use this for initialization
	void Start () 
    {
        ResetPositionToAllPins ();
	}

    public void makeAllPinsUpIfHit()
    {
        foreach (Pin singlePin in GetComponentsInChildren<Pin>())
        {
            singlePin.SetPinUp();
        }
    }

    public void ResetGravityToAllPins()
    {
        foreach (Pin singlePin in GetComponentsInChildren<Pin>())
        {
            singlePin.ResetGravity();
        }
    }

    public void ResetPositionToAllPins()
    {
        print ("resetting pins");
        DestroyAllPins ();

        Vector3[] positions = getPinsStartingPosition ();

        foreach (Vector3 pos in positions)
        {
            GameObject pin = Instantiate (pinPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            pin.transform.SetParent (this.transform);
            pin.transform.position = pos;

            print ("added " + pin.transform.position);
        }
    }

    void DestroyAllPins()
    {
        foreach (Pin singlePin in GetComponentsInChildren<Pin>())
        {
            Destroy(singlePin.gameObject);
        }
    }

    void OnDrawGizmos() 
    {
        Vector3[] positions = getPinsStartingPosition ();
        Gizmos.color = Color.blue;

        foreach (Vector3 pos in positions)
        {
            Gizmos.DrawWireSphere(pos, 10f);
        }
    }

    Vector3[] getPinsStartingPosition()
    {
        float x_step = 15.24f;
        float z_step = 26.355f;
        float z_offset = 1829f;
        
        Vector3[] ret = new Vector3[10];
        ret[0] = new Vector3( 0f * x_step, 0f, 0f * z_step + z_offset);
        ret[1] = new Vector3(-1f * x_step, 0f, 1f * z_step + z_offset);
        ret[2] = new Vector3( 1f * x_step, 0f, 1f * z_step + z_offset);
        ret[3] = new Vector3(-2f * x_step, 0f, 2f * z_step + z_offset);
        ret[4] = new Vector3( 0f * x_step, 0f, 2f * z_step + z_offset);
        ret[5] = new Vector3( 2f * x_step, 0f, 2f * z_step + z_offset);
        ret[6] = new Vector3(-3f * x_step, 0f, 3f * z_step + z_offset);
        ret[7] = new Vector3(-1f * x_step, 0f, 3f * z_step + z_offset);
        ret[8] = new Vector3( 1f * x_step, 0f, 3f * z_step + z_offset);
        ret[9] = new Vector3( 3f * x_step, 0f, 3f * z_step + z_offset);

        return ret;
    }
}
