using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TextController : MonoBehaviour {

	// State definition
	private enum State {
		corridor_0, corridor_1, corridor_2, corridor_3,
		stairs_0, stairs_1, stairs_2, 
		floor, closet_door, in_closet, courtyard, unknwon};

	// access definition
	private struct Access
	{
		// member
		public KeyCode _keycode;
		public string _direction;
		public State _to;

		// constructor
		public Access(KeyCode keycode, string theText, State to)
		{
			_keycode = keycode;
			_direction = theText;
			_to = to;
		}
	}

	// StateContent definition
	private struct StateContent
	{
		// member
		public string _text;
		public Access[] _possibility;

		// constructor
		public StateContent(string theText, Access[] possibilities)
		{
			_text = theText;
			_possibility = possibilities;
		}
	}

	// members
	public Text text;
	private State currentState;
	private Dictionary<State, StateContent> data;
	private bool changed;
	
	
	// Use this for initialization
	void Start () 
	{
		changed = true;
		text.text = "Hello world";
		currentState = State.corridor_0;
		this.data = new Dictionary<State, StateContent> ();

		addToData (State.corridor_0, "corridor 0", 		new Access(KeyCode.S, "Stairs", State.stairs_0),
		          								  		new Access(KeyCode.F, "floor", State.floor), 
		          								  		new Access(KeyCode.C, "closet door", State.closet_door));

		addToData (State.corridor_1, "corridor 1", 		new Access(KeyCode.S, "Stairs", State.stairs_1),
		          										new Access(KeyCode.P, "Pick", State.in_closet));

		addToData (State.corridor_2, "corridor 2",		new Access(KeyCode.B, "Back", State.in_closet),
		          										new Access(KeyCode.S, "Stairs", State.stairs_2));

		addToData (State.corridor_3, "corridor 3", 		new Access(KeyCode.U, "Undress", State.in_closet),
		          										new Access(KeyCode.S, "Stairs", State.courtyard));

		addToData (State.stairs_0, "stairs 0", 			new Access(KeyCode.R, "Return", State.corridor_0));

		addToData (State.stairs_1, "stairs 1", 			new Access(KeyCode.R, "Return", State.corridor_1));

		addToData (State.stairs_2, "stairs 2", 			new Access(KeyCode.R, "Return", State.corridor_2));

		addToData (State.floor, "floor", 				new Access (KeyCode.R, "Return", State.corridor_0),
		           										new Access (KeyCode.H, "Hairclip", State.corridor_1));

		addToData (State.closet_door, "closet door", 	new Access(KeyCode.R, "Return", State.corridor_0));

		addToData (State.in_closet, "in the closet", 	new Access(KeyCode.R, "Return", State.corridor_2),
		          										new Access(KeyCode.D, "Dress", State.corridor_3));

		addToData (State.courtyard, "courtyard, Well done you found the exit !");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (changed) 
		{
			string message = this.data [this.currentState]._text + "\n\nChoices:\n";

			for (int i = 0 ; i < this.data[this.currentState]._possibility.Length ; i++)
				message += "press " + data[this.currentState]._possibility[i]._keycode.ToString() + 
						   " to " + data[this.currentState]._possibility[i]._direction + "\n";
			 

			text.text = message;
			changed = false;
		}

		for (int i = 0 ; i < this.data[this.currentState]._possibility.Length ; i++) 
		{
			if (Input.GetKeyDown(this.data[this.currentState]._possibility[i]._keycode))
			{
			    this.currentState = data[this.currentState]._possibility[i]._to;
				changed = true;
			}
		}
	}

	void addToData(State state, string text)
	{
		// create data to add
		StateContent temp = new StateContent ();

		// add the text
		temp._text = text;

		// add the possibilities by taking care of the number of default variables
		temp._possibility = new Access [0];

		// adding data
		this.data.Add (state, temp);
	}

	void addToData(State state, string text, Access access_1)
	{
		// create data to add
		StateContent temp = new StateContent ();
		
		// add the text
		temp._text = text;
		
		// add the possibilities by taking care of the number of default variables
		temp._possibility = new Access [1];
		temp._possibility [0] = access_1;
		
		// adding data
		this.data.Add (state, temp);
	}

	void addToData(State state, string text, Access access_1, Access access_2)
	{
		// create data to add
		StateContent temp = new StateContent ();
		
		// add the text
		temp._text = text;
		
		// add the possibilities by taking care of the number of default variables
		temp._possibility = new Access [2];
		temp._possibility [0] = access_1;
		temp._possibility [1] = access_2;
		
		// adding data
		this.data.Add (state, temp);
	}

	void addToData(State state, string text, Access access_1, Access access_2, Access access_3)
	{
		// create data to add
		StateContent temp = new StateContent ();
		
		// add the text
		temp._text = text;
		
		// add the possibilities by taking care of the number of default variables
		temp._possibility = new Access [3];
		temp._possibility [0] = access_1;
		temp._possibility [1] = access_2;
		temp._possibility [2] = access_3;
		
		// adding data
		this.data.Add (state, temp);
	}
}
