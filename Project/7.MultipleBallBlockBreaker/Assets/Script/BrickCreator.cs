using UnityEngine;
using System.Collections;

public class BrickCreator : MonoBehaviour 
{
	// actual level
	private int level;

	// brick model
	public GameObject brickModel;

	// brick's starting Y pos
	private const float brickYPos = 250f;

	// Use this for initialization
	void Start () 
	{
		level = 1;
		generateBrickLevel ();
	}

	public int getLevel()
	{
		return level;
	}

	public void nextLevel()
	{
		level++;
		MoveBricksDown ();
		generateBrickLevel ();
	}

	void generateBrickLevel()
	{

		int nbBrick = Random.Range (1, 8);
		bool[] array = new bool[8];

		for (int i = 0 ; i < nbBrick ; i++)
			array[i] = true;
		for (int i = nbBrick ; i < 8 ; i++)
			array[i] = false;

		bool[] finalArray = RandomizeArray(array);

		for (int i = 1 ; i <= 8 ; i++)
		{
			if (finalArray[i-1])
				makeABrick(i, level);
		}
	}

	void makeABrick(int xpos, int value)
	{
		// create the brick
		GameObject brick_obj = Instantiate (brickModel) as GameObject;
		Brick brick = brick_obj.GetComponent<Brick> ();

		// set the brick life
		brick.setLife (value);

		// set the position
		brick.setPos (convertPos (xpos), brickYPos);

		// set it to be a child
		brick.gameObject.transform.SetParent(this.transform);
	}

	float convertPos(int pos)
	{
		/*
		 * Brick X pos are in [1, 8] as integer.
		 * In the canvas frame they are -350, -250, -150, -50, 50, 150, 250, 350
		 */

		if (pos >= 1 && pos <= 8) 
			return pos * 100f - 450f;
		else
			return 0f;
	}

	static bool[] RandomizeArray(bool[] arr)
	{
		for (int i = arr.Length - 1; i > 0; i--) 
		{
			int r = Random.Range(0,i);
			bool tmp = arr[i];
			arr[i] = arr[r];
			arr[r] = tmp;
		}

		return arr;
	}

	void MoveBricksDown()
	{
		foreach (Transform child in this.transform) 
		{
			child.position += Vector3.down * 100f;
		}
	}
}
