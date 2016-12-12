using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) 
	{

		// GreenChairs to Menu___
		if (other.tag == "production") {
			Application.LoadLevel (1);
		}

		// Intro to Spaceship_flight
		if (other.tag == "IntroEnd") {
			Application.LoadLevel (3);
		}

		// Saceship_flight to Sheep_Face
		if (other.tag == "Respawn") {
			Application.LoadLevel (4);
		}

		// Sheep_Face to Sheep_Run
		if (other.tag == "Faced") {
			Application.LoadLevel (5);
		}

		// Sheep_Run to Game!
		if (other.tag == "Goal") {
			Application.LoadLevel (6);
		}
	}
}
