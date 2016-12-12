using UnityEngine;
using System.Collections;

public class OtherStuff : MonoBehaviour {
	public int seite = 0;
	Animator anim;

	void Start () {
		anim = GetComponent<Animator>();
	}

	public void Update () {

		if (Input.GetButtonUp ("Cancel")) {
			Application.LoadLevel(0);
		}



		if (seite==0){
			if (Input.GetButtonDown ("Submit")) {
				anim.SetInteger("seite", 1);
				seite=1;
				Debug.Log("You Pushed me hard and often, Sir.");
			}
		}


		if (seite==1){
			if (Input.GetButtonDown ("Submit")) {
				anim.SetInteger("seite", 2);
				seite=2;
			}
		}


		if (seite==2){
			if (Input.GetButtonDown ("Submit")) {
				anim.SetInteger("seite", 3);
				seite=3;
			}
		}


		if (seite==3){
			if  (Input.GetButtonDown ("Submit")) {
				anim.SetInteger("seite", 4);
				seite=4;
			}
		}


		if (seite==4){
			if (Input.GetButtonDown ("Submit")){
				anim.SetInteger("seite", 0);
				seite=0;
			}
		}


	}
}





