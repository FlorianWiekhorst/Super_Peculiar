using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {


	public void Stuff () {
		
		Application.LoadLevel(8);
	}

	
	public void Play () {

		Application.LoadLevel(2);
	}

	public void Quit () {
		
		Application.Quit();
	}


}

