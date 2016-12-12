#pragma strict



var resett_me : Transform;



function OnTriggerEnter2D(other: Collider2D) {

 	if (other.tag == "Sheep") { 
 		other.transform.position = resett_me.position; 
 		
 	}
} 