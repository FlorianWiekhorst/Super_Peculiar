using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {


		bool facingRight = true;							// For determining which way the player is currently facing.
		
		[SerializeField] float maxSpeed = 50f;				// The fastest the player can travel in the x axis.
		[SerializeField] float jumpForce = 3000f;			// Amount of force added when the player jumps.	
		
		[Range(0, 1)]
		
		[SerializeField] bool airControl = true;			// Whether or not a player can steer while jumping;
		[SerializeField] LayerMask whatIsGround;			// A mask determining what is ground to the character
		
		Transform groundCheck;								// A position marking where to check if the player is grounded.
		float groundedRadius = .2f;							// Radius of the overlap circle to determine if grounded
		bool grounded = false;								// Whether or not the player is grounded.
		bool canMove = true;								// To disable Player Movement in one Step.
		Animator anim;										// Reference to the player's animator component.
		
		//ladder climbing stuff
		bool isClimbing = false;							// Detect if the Player is climbing.
		bool ClimbAndMove = false;
		float climbSpeed = 11f;
		
		// GameMenu
		bool inGameMenu = false;						// sets GameMenu by Default to off.
		bool dead = false;	

		//Sound Array
		public AudioClip[] audioclip;						// Creates an Array to store my GameSounds
		
		[SerializeField] int lifes = 5;										//Number of Lifes
		[SerializeField] int notes = 0;						//show me number of notes collected
		
		
		
		void Awake()
		{
			// Setting up references.
			groundCheck = transform.Find("GroundCheck");
			anim = GetComponent<Animator>();
		}
		
		void FixedUpdate()
		{
			// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
			grounded = Physics2D.OverlapCircle (groundCheck.position, groundedRadius, whatIsGround);
			anim.SetBool ("Ground", grounded);
			
			// Set the vertical animation
			// When not climbing!!
			if (!isClimbing) 
			{
				anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
			}
			
			//The Death
			if (lifes < 0.5) 
			{
				GetComponent<Rigidbody2D>().velocity = (new Vector2 (0f, 0f));
				Debug.Log ("YOU ARE DEAD");
				canMove = false;
				Playsound(3);
				Invoke ("freeze", 7);
			}

		if (Input.GetButtonDown(buttonName:"Fire1"))
		    {
			Playsound(4);
			}
		}
		
		void freeze()
		{
			Time.timeScale = 0f;
		}
		
		//Enter a Trigger
		void OnTriggerEnter2D(Collider2D other) 
		{
			if (other.tag == "PickUP") 
			{
				Playsound(0);
				Destroy(other.gameObject);
				notes++;
			}
			if(other.gameObject.tag == "ladder")
			{
				isClimbing = true;
				anim.SetBool ("IsClimbing", true);
			}
			if (other.gameObject.tag == "Hurt") 
			{
				anim.SetBool ("Hurt", true);
				lifes--;
				Playsound(1);
				if(lifes>0)
				{
					GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 50f));
				}
				
			}
			if (other.gameObject.tag == "Goal") 
			{
				
				//Time.timeScale = 0f;
				//manager.CompleteLevel();
			}

			if (other.gameObject.tag == "Fall_OFF") 
			{
			Playsound (5);
			dead = true;
			canMove = false;
			Invoke ("freeze", 1);
			}
		}
		
		//Exit a Trigger     
		public void OnTriggerExit2D(Collider2D other)
		{
			if(other.gameObject.tag == "ladder")
			{
				isClimbing = false;
				GetComponent<Rigidbody2D>().gravityScale = 2.0F;
				anim.SetBool ("IsClimbing", false);
				
			}
			if (other.gameObject.tag == "Hurt") 
			{
				anim.SetBool ("Hurt", false);
			}
		}
		
		public void Move(float move, bool jump)
		{
			
			//Climbing controls
			if (isClimbing) {
				GetComponent<Rigidbody2D>().velocity = (new Vector2 (0f, 0f)); //on ladder no movement
				GetComponent<Rigidbody2D>().gravityScale = 0.0F;			// and no gravity
				anim.SetBool ("IsClimbing", true);
				anim.SetBool ("ClimbAndMove", false);
			} 
			if (isClimbing && Input.GetKey("w")) {	
				// Move the character UP
				GetComponent<Rigidbody2D>().velocity = (new Vector2 (0f, climbSpeed));
				GetComponent<Rigidbody2D>().gravityScale = 0.0F;
				anim.SetBool ("ClimbAndMove", true);
			}
			if (isClimbing && Input.GetKey("s")) {	
				// Move the character DOWN
				GetComponent<Rigidbody2D>().velocity = (new Vector2 (0f, -climbSpeed));
				GetComponent<Rigidbody2D>().gravityScale = 0.0F;
				anim.SetBool ("ClimbAndMove", true);
			}
			
			//only control the player if grounded or airControl is turned on
			if(grounded || airControl && canMove)
			{
				// The Speed animator parameter is set to the absolute value of the horizontal input.
				anim.SetFloat("Speed", Mathf.Abs(move));
				
				// Move the character
				GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
				
				if(move > 0 && !facingRight)
					Flip();
				
				else if(move < 0 && facingRight)
					Flip();
			}
			
			// THE JUMPING
			if (grounded && jump) {
				// Add a vertical force to the player.
				anim.SetBool("Ground", false);
				//GetComponent<AudioSource>().pitch = Random.Range(0.7f, 1.4f);
				//Playsound(4); //Jumpsound from Array inside Player
				GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
			}
		}
		
		void Flip ()
		{
			// Switch the way the player is labelled as facing.
			facingRight = !facingRight;
			
			// Multiply the player's x local scale by -1.
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
		
		//  SOUND
		void Playsound(int clip) // <3
		{
			GetComponent<AudioSource>().clip = audioclip [clip];
			GetComponent<AudioSource>().Play ();
		}
		
		
		void OnGUI()
		{
			GUILayout.Label( "Anzahl Noten = " + notes );
			GUILayout.Label( "Anzahl Leben = " + lifes );
			
			if (lifes < 0 || dead) 
			{
				GUILayout.Label("YOU ARE DEAD !!");
			}
		}
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
