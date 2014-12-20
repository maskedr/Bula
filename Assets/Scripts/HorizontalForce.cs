using UnityEngine;
using System.Collections;

public class HorizontalForce : MonoBehaviour {
		
	// Reference to camera
	private Camera 					    	mainCamera;
	// Storing the last mouse/touch position 
	private Vector3 						lastTouchPosition = Vector3.zero;
	private Vector3							curentTouchPosition = Vector3.zero;
	
	
	/// <summary>
	/// Editor displayed values
	/// </summary>
	// Used to set the maximum magnitude allowed
	public float 							maximumForceMagnitude = 40f;
	// For each step unit (for example if the object location has a distance of 1 then we have 10 steps to apply the force-> stepUnit * stepIncreaseForceMagnitude with maximum value define by maximumForceMagnitude)
	public float							stepUnit = 0.5f;
	// For each step (a script unit ) we increase the force by this value
	public float							stepIncreaseForceMagnitude = 5f;
	// Store at which step we are
	private float							curentForceStep = 0f;
	// Outpout the value in Editor
	public float							curentForceApplied = 0f;
	// Current distance between mouse/touch and object
	public float							distanceToTouch = 0f;
	// Current step value
	public float							activeForceStep = 0f;
	// Current gameObject velocity
	public float							curentVelocity = 0f;
	
	//-------------------------
	// Flag which will be used to trigger moving or stoping the movement
	//-------------------------
	// Flag for moving the object
	public bool					    		moveObjectFlag = false;
	// Direction were the force is applied
	public bool								forceAppliedLeft = false;
	public bool								forceAppliedRight = false;
	// FLags which will tell us in which direction the mouse/touch is moving
	public bool								touchIsMovingDown = false;
	public bool								touchIsMovingLeft = false;
	public bool								touchIsMovingUp = false;
	public bool								touchIsMovingRight = false;
	// Flags which keep track if the rays hit something
	public bool								rayCastCollisionDown = false;
	public bool								rayCastCollisionLeft = false;
	public bool								rayCastCollisionUp = false;
	public bool								rayCastCollisionRight = false;
	
	void Awake () {
		// Reference to the main camera
		mainCamera = Camera.main;        
	}
	
	// Use this for initialization
	void Start () {
	}
	
	void FixedUpdate ()
	{
		
		// If moveObjectFlag is TRUE and no collision is detected we commit the movement
		//if (moveObjectFlag && !rayCastCollisionDown && !rayCastCollisionLeft && !rayCastCollisionUp && !rayCastCollisionRight) 
		if (moveObjectFlag) 
		{
			// Checking to see if lastTouchPosition is Vector3.zero -> no movement took place in the last frame
			if (lastTouchPosition == Vector3.zero)
				// Updating the last position with the curent transform.position
				lastTouchPosition = transform.position;
			
			// Updating the curent touch/mouse position
			UpdateCurentTouchPosition();
			
			// Updating the mouse/touch movement flags
			UpdateTouchDirectionFlags ();
			
			// Apply the force on object
			ApplyForce();
		}
		else
		{
		}
		
		// Updating curent Velocity
		curentVelocity = gameObject.rigidbody2D.velocity.x;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	/// <summary>
	/// Updates the curent touch position.
	/// </summary>
	private void UpdateCurentTouchPosition ()
	{
		curentTouchPosition = mainCamera.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
	}
	
	// Updating the flags which keeps the track of mouse/touch movement direction
	private void UpdateTouchDirectionFlags ()
	{
		// Resetting the flags 
		touchIsMovingDown = false;
		touchIsMovingLeft = false;
		touchIsMovingUp = false;
		touchIsMovingRight = false;
		
		float tmpValue;
		
		// Updating the flags which will keep track to the mouse movement
		if ((curentTouchPosition.y - lastTouchPosition.y < 0)) // Moving down 
		{
			tmpValue = curentTouchPosition.y - lastTouchPosition.y;
			//Debug.Log ("->>  Touch is moving DOWN: " + tmpValue);
			touchIsMovingDown = true;
		}
		
		if (curentTouchPosition.x - lastTouchPosition.x < 0) // Moving left 
		{
			tmpValue = curentTouchPosition.x - lastTouchPosition.x;
			//Debug.Log ("->>  Touch is moving LEFT: " + tmpValue);
			touchIsMovingLeft = true;
		}
		
		if (curentTouchPosition.y - lastTouchPosition.y > 0) // Moving up 
		{
			tmpValue = curentTouchPosition.y - lastTouchPosition.y;
			//Debug.Log ("->>  Touch is moving UP: " + tmpValue);
			touchIsMovingUp = true;
		}
		
		if (curentTouchPosition.x - lastTouchPosition.x > 0) // Moving right 
		{
			tmpValue = curentTouchPosition.x - lastTouchPosition.x;
			//Debug.Log ("->>  Touch is moving RIGHT: " + tmpValue);
			touchIsMovingRight = true;
		}
	}
	
	/// <summary>
	/// Depending on the mouse movement we roll the boulder in the designated direction
	/// Depending on the player movement (distance between user touch/click and position of the boulder) we increase or decrease the force applied
	/// </summary>
	void ApplyForce()
	{
		// Resetting the flags - force direction applied flags
		forceAppliedLeft = false;
		forceAppliedRight = false;
		
		// Updating the distance between object and touch/mouse
		distanceToTouch = transform.position.x - curentTouchPosition.x;
		// Default value is the distance fr
		float acceptedMagnitudeSteps = Mathf.Abs(distanceToTouch)/stepUnit;
		
		// Checking to see in which direction we will apply the force
		// Adding or substracting the force applied
		if (touchIsMovingLeft)
		{
			if (distanceToTouch < 0)
			{
				// Calculating how many steps we can apply to force magnitude
				// Because the movement is on left but the diff between object location and mouse/touch location is < 0 means the mouse/touch
				// is position on the right side of the object ... meaning the object has moved in the right direction so far
				// and for that we need to adjust the force magnitude
				// Checking if the force applied dont exit the maximum allowed
				if (acceptedMagnitudeSteps * stepIncreaseForceMagnitude > maximumForceMagnitude)
				{
					// Update the value with the maximum steps allowed
					acceptedMagnitudeSteps = maximumForceMagnitude/stepIncreaseForceMagnitude;
				}
				// Updating the force value applied to object
				gameObject.rigidbody2D.AddForce(Vector2.right * acceptedMagnitudeSteps * stepIncreaseForceMagnitude);
				// Updating the flag which will show the direction of the force applied
				forceAppliedRight = true;
			}
			else if (distanceToTouch > 0)
			{
				// The object is moving on left direction and the user is pushing further on left side
				// Checking if the force applied dont exit the maximum allowed
				if (acceptedMagnitudeSteps * stepIncreaseForceMagnitude > maximumForceMagnitude)
				{
					// Update the value with the maximum steps allowed
					acceptedMagnitudeSteps = maximumForceMagnitude/stepIncreaseForceMagnitude;
				}
				// Updating the force value applied to object
				gameObject.rigidbody2D.AddForce(-Vector2.right * acceptedMagnitudeSteps * stepIncreaseForceMagnitude);
				// Updating the flag which will show the direction of the force applied
				forceAppliedLeft = true;
			}
		}
		else if (touchIsMovingRight)
		{
			if (distanceToTouch < 0)
			{
				// The object is moving on right direction and the user is pushing further on right side
				// Checking if the force applied dont exit the maximum allowed
				if (acceptedMagnitudeSteps * stepIncreaseForceMagnitude > maximumForceMagnitude)
				{
					// Update the value with the maximum steps allowed
					acceptedMagnitudeSteps = maximumForceMagnitude/stepIncreaseForceMagnitude;
				}
				// Updating the force value applied to object
				gameObject.rigidbody2D.AddForce(Vector2.right * acceptedMagnitudeSteps * stepIncreaseForceMagnitude);
				// Updating the flag which will show the direction of the force applied
				forceAppliedRight = true;
			}
			else if (distanceToTouch > 0)
			{
				// Calculating how many steps we can apply to force magnitude				
				// Because the movement is on right but the diff between object location and mouse/touch location is < 0 means the mouse/touch
				// is position on the left side of the object ... meaning the object has moved in the left direction so far
				// and for that we need to adjust the force magnitude
				// Checking if the force applied dont exit the maximum allowed
				if (acceptedMagnitudeSteps * stepIncreaseForceMagnitude > maximumForceMagnitude)
				{
					// Update the value with the maximum steps allowed
					acceptedMagnitudeSteps = maximumForceMagnitude/stepIncreaseForceMagnitude;
				}
				// Updating the force value applied to object
				gameObject.rigidbody2D.AddForce(-Vector2.right * acceptedMagnitudeSteps * stepIncreaseForceMagnitude);
				// Updating the flag which will show the direction of the force applied
				forceAppliedLeft = true;
			}
		}
		else if (touchIsMovingDown)
		{
		}
		else if (touchIsMovingUp)
		{
		}
		
		// Updating the curentForceMagnitudeApplied in Editor
		curentForceApplied = acceptedMagnitudeSteps * stepIncreaseForceMagnitude;
		// Updating the activeStep value for editor
		activeForceStep = acceptedMagnitudeSteps;     
	}
}
