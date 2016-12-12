using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Platformer2DUserControl : MonoBehaviour 
{
	private PlayerMovement character;
	private bool jump;
	
	
	void Awake()
	{
		character = GetComponent<PlayerMovement>();
	}
	
	void Update ()
	{
		// Read the jump input in Update so button presses aren't missed.
		#if CROSS_PLATFORM_INPUT
		if (CrossPlatformInput.GetButtonDown("Jump")) jump = true;
		#else
		if (Input.GetButtonDown("Jump")) jump = true;
		#endif
		
	}
	
	void FixedUpdate()
	{
		
		#if CROSS_PLATFORM_INPUT
		float h = CrossPlatformInput.GetAxis("Horizontal");
		#else
		float h = Input.GetAxis("Horizontal");
		#endif
		
		// Pass all parameters to the character control script.
		character.Move( h, jump );
		
		// Reset the jump input once it has been used.
		jump = false;
	}
}
