using UnityEngine;
using System.Collections;

public class RayCastManagerBoulder : RayCastManager {

	// Reference to script
	private HorizontalForce					horizontalForceScript;
	
	void Start() {
		// Reference to script
		horizontalForceScript = gameObject.GetComponent<HorizontalForce>();
	}
	
	// Update is called once per frame
	public override void Update () 
	{
		// Before we start the process we need to pass the movement status value
		base.movementStatus = horizontalForceScript.moveObjectFlag;
		
		// Calling parent Update function
		base.Update ();
		
		// We pass the movement staus value back to script
		horizontalForceScript.moveObjectFlag = base.movementStatus;
		
		// We check to see if the ray has been casted
		// If YES we read and send the ray cast result
		if (base.shouldCastRayFlag) {
			// Passing the value
			horizontalForceScript.moveObjectFlag = base.rayCastResult;
		}
	}
}
