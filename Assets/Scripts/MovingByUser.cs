using UnityEngine;
using System.Collections;

public class MovingByUser : MonoBehaviour {

	// Reference to camera
	private Camera 					    	mainCamera;
	// Storing the last mouse/touch position 
	private Vector3 						lastTouchPosition = Vector3.zero;

	// Maximum jump on x or y axis
	public float							maximumSpeed = 0.5f;

	//-------------------------
	// Flag which will be used to trigger moving or stoping the movement
	//-------------------------
	[HideInInspector]
	// Flag for moving the object
	public bool					    		moveObjectFlag = false;
	// Flags which keep track if the rays hit something
	public bool								rayCastCollisionDown = false;
	public bool								rayCastCollisionLeft = false;
	public bool								rayCastCollisionUp = false;
	public bool								rayCastCollisionRight = false;
	// FLags which will tell us in which direction the mouse/touch is moving
	private bool							touchIsMovingDown = false;
	private bool							touchIsMovingLeft = false;
	private bool							touchIsMovingUp = false;
	private bool							touchIsMovingRight = false;

	void Awake () {
		// Reference to the main camera
		mainCamera = Camera.main;        
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
		// If moveObjectFlag is TRUE and no collision is detected we commit the movement
		//if (moveObjectFlag && !rayCastCollisionDown && !rayCastCollisionLeft && !rayCastCollisionUp && !rayCastCollisionRight) 
		if (moveObjectFlag) 
		{
			// Checking to see if lastTouchPosition is Vector3.zero -> no movement took place in the last frame
			if (lastTouchPosition == Vector3.zero)
				// Updating the last position with the curent transform.position
				lastTouchPosition = transform.position;

			// Updating the mouse/touch movement flags
			UpdateTouchDirectionFlags ();

			// Moving the object 
			MoveTheObject();

			//Debug.Log ("MOVING");
		}
		else
		{
		}
	}

	// Moving self to the designated coordinates
	void MoveTheObject () {
						
		// Referrence to the mouse current mouse position
		Vector3 mousePosition = Input.mousePosition;
		// Output the current mouse position
		//Debug.Log("Mouse Position: " + mousePosition);
		
		Vector3 tmpVector3 = new Vector3(mousePosition.x, mousePosition.y, mainCamera.transform.position.z);
		//Debug.Log("Tmp Vector: " + tmpVector3);
		
		// Constructing a new Vector3 which will be used to set the position of the selected object world points
		Vector3 convertedVector3 = mainCamera.ScreenToWorldPoint(tmpVector3);
		// Output the new converted value
		//Debug.Log("Converted mouse position: " + convertedVector3);

		// Vector3 for storing the coords which will be used to move the object
		Vector3 newObjectPos = transform.position;

		// Checking to see if the ray collided with any collider
		if (!rayCastCollisionDown && !rayCastCollisionLeft && !rayCastCollisionUp && !rayCastCollisionRight){

			// Checking the flag to see if the mouse/touch is moving down
			if (touchIsMovingDown && (transform.position.y - convertedVector3.y > 0))
			{
				if (Mathf.Abs(transform.position.y - convertedVector3.y) > maximumSpeed)
					newObjectPos = new Vector3(newObjectPos.x, newObjectPos.y - maximumSpeed, transform.position.z);
				else
					newObjectPos = new Vector3(newObjectPos.x, convertedVector3.y, transform.position.z);
			}
			// Checking the flag to see if the mouse/touch is moving left
			if (touchIsMovingLeft && (transform.position.x - convertedVector3.x > 0))
			{
				if (Mathf.Abs(transform.position.x - convertedVector3.x) > maximumSpeed)
					newObjectPos = new Vector3(newObjectPos.x - maximumSpeed, newObjectPos.y, transform.position.z);
				else
					newObjectPos = new Vector3(convertedVector3.x, newObjectPos.y, transform.position.z);
			}
			// Checking the flag to see if the mouse/touch is moving up
			if (touchIsMovingUp&& (transform.position.y - convertedVector3.y < 0))
			{
				if (Mathf.Abs(transform.position.y - convertedVector3.y) > maximumSpeed)
					newObjectPos = new Vector3(newObjectPos.x, newObjectPos.y + maximumSpeed, transform.position.z);
				else
					newObjectPos = new Vector3(newObjectPos.x, convertedVector3.y, transform.position.z);
			}
			// Checking the flag to see if the mouse/touch is moving right
			if (touchIsMovingRight && (transform.position.x - convertedVector3.x < 0))
			{
				if (Mathf.Abs(transform.position.y - convertedVector3.y) > maximumSpeed)
					newObjectPos = new Vector3(newObjectPos.x + maximumSpeed, newObjectPos.y, transform.position.z);	
				else
					newObjectPos = new Vector3(convertedVector3.x, newObjectPos.y, transform.position.z);	
			}

			// Updating the new object position
			// newObjectPos = new Vector3(convertedVector3.x, convertedVector3.y, transform.position.z); 
		}
		else 
		{
			// Checking down collision flag
			if (!rayCastCollisionDown)
			{
				// Checking the flag to see if the mouse/touch is moving down
				if (touchIsMovingDown && (transform.position.y - convertedVector3.y > 0))
				{
					if (Mathf.Abs(transform.position.y - convertedVector3.y) > maximumSpeed)
						newObjectPos = new Vector3(newObjectPos.x, newObjectPos.y - maximumSpeed, transform.position.z);
					else
						newObjectPos = new Vector3(newObjectPos.x, convertedVector3.y, transform.position.z);
				}
			}
			// Checking left collision flag
			if (!rayCastCollisionLeft) 
			{
				// Checking the flag to see if the mouse/touch is moving left
				if (touchIsMovingLeft && (transform.position.x - convertedVector3.x > 0))
				{
					if (Mathf.Abs(transform.position.x - convertedVector3.x) > maximumSpeed)
						newObjectPos = new Vector3(newObjectPos.x - maximumSpeed, newObjectPos.y, transform.position.z);
					else
						newObjectPos = new Vector3(convertedVector3.x, newObjectPos.y, transform.position.z);
				}
			}
			// Checking up collision flag
			if (!rayCastCollisionUp)
			{
				// Checking the flag to see if the mouse/touch is moving up
				if (touchIsMovingUp&& (transform.position.y - convertedVector3.y < 0))
				{
					if (Mathf.Abs(transform.position.y - convertedVector3.y) > maximumSpeed)
						newObjectPos = new Vector3(newObjectPos.x, newObjectPos.y + maximumSpeed, transform.position.z);
					else
						newObjectPos = new Vector3(newObjectPos.x, convertedVector3.y, transform.position.z);
				}
			}
			// Checking right collision flag
			if (!rayCastCollisionRight)
			{
				// Checking the flag to see if the mouse/touch is moving right
				if (touchIsMovingRight && (transform.position.x - convertedVector3.x < 0))
				{
					if (Mathf.Abs(transform.position.y - convertedVector3.y) > maximumSpeed)
						newObjectPos = new Vector3(newObjectPos.x + maximumSpeed, newObjectPos.y, transform.position.z);	
					else
						newObjectPos = new Vector3(convertedVector3.x, newObjectPos.y, transform.position.z);	
				}
			}
		}

		
		// Change the position of object
		transform.position = new Vector3(newObjectPos.x, newObjectPos.y, transform.position.z);

		// Storing the current mouse/touch position
		lastTouchPosition = convertedVector3;
	}

	// Updating the flags which keeps the track of mouse/touch movement direction
	private void UpdateTouchDirectionFlags ()
	{
		// Current mouse/touch position
		Vector3 curentTouchPosition = mainCamera.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, transform.position.z));

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

}
