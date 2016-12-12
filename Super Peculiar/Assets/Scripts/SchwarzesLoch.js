#pragma strict
var spawn : Transform;
var spawn2 : Transform;

function OnTriggerEnter2D(other: Collider2D) {

 if (other.tag == "Sheep") { 
 	other.transform.position = spawn.position; 
 }
 
 if (other.tag == "Player") { 
 	other.transform.position = spawn2.position; 
 }
 
} 