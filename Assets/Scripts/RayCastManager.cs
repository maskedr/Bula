using UnityEngine;
using System.Collections;

public class RayCastManager : MonoBehaviour {

	// Used to store the location of player interaction
	private Vector3 						rayStartingLocation;
	// Flag for controlling the ray cast
	public bool								shouldCastRayFlag = false;
	// The results of the ray cast
	public bool								rayCastResult = false;
	// Reference to movingObjectFlag
	// Will be called by child
	public bool 							movementStatus;

	private int								frameCount = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	public virtual void Update () {
	
		// Checking to see if is a mouse click
		if (!movementStatus && Input.GetMouseButtonDown(0)) { 		// running on computer
			// Setting the startingPoint
			rayStartingLocation = Input.mousePosition;
			// Updating flag
			shouldCastRayFlag = true;

			// Debug.Log ("MOUSE Clicked");
		}
		else if (!movementStatus && Input.touchCount == 1) { 		// running on mobiles
			// Setting the startingPoint
			rayStartingLocation = Input.GetTouch(0).position;
			// Updating flag
			shouldCastRayFlag = true;

			// Debug.Log ("TOUCH screen");
		}
		else if (movementStatus && (Input.GetMouseButton(0) || Input.touchCount == 1))
		{
			// Setting the startingPoint
			rayStartingLocation = Input.mousePosition;
			// Updating flag
			shouldCastRayFlag = false;

			// Debug.Log ("----> NO CLICK detected but left MOUSE button is active");
		}
		else
		{
			// Updating flag
			shouldCastRayFlag = false;
			// Updating the flag from movingByUser script to disable the movement
			movementStatus = false;

			// Debug.Log ("----> NO CLICK detected and NO active MOUSE or TOUCH detected");
		}

		// Checking to see if we need to cast the Ray
		if (shouldCastRayFlag)
		{
			// Constructing the ray which will have the direction from camera to the plane
			Ray ray = Camera.main.ScreenPointToRay(rayStartingLocation);
			// Calling the casting of ray and save it
			rayCastResult = CastRay(ray);
		}

		// Debug.Log ("/**** Curent FRAME: " + frameCount + "****/");
		frameCount++;

	}

	// Casting Ray method
	bool CastRay (Ray ray) {
		
		// Casting (firing) the ray
		// If it return TRUE then the ray hitted an object and the object hitted is returned
		// If returns FALSE then no collidable object has been hit.
		RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);
		if (hit && hit.collider == gameObject.collider2D) {
			// Output the collider gameObject name
			//Debug.Log (hit.collider.gameObject.name);
			// Validation output
			//Debug.Log("The RayCast hitted my object");
			//Result
			return true; 
		}
		else {
			// Output the result
			//Debug.Log ("The RayCast doesn't hitted any object");
			//Result
			return false;
		}
		
	}
}
