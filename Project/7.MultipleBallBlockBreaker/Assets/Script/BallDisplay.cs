using UnityEngine;
using System.Collections;

public class BallDisplay : MonoBehaviour 
{
	public void setPos(float xpos)
	{
		this.transform.position = new Vector3 (xpos,
		                                      this.transform.position.y,
		                                      this.transform.position.z);
	}
}
