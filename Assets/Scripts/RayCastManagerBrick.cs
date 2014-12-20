using UnityEngine;
using System.Collections;

public class RayCastManagerBrick : RayCastManager {

	// Reference to MovingByUser script
	private MovingByUser					movingByUserScript;

	void Start() {
		// Reference to script
		movingByUserScript = gameObject.GetComponent<MovingByUser>();
	}

	// Update is called once per frame
	public override void Update () 
	{
		// Before we start the process we need to pass the movement status value
		base.movementStatus = movingByUserScript.moveObjectFlag;

		// Calling parent Update function
		base.Update ();

		// We pass the movement staus value back to script
		movingByUserScript.moveObjectFlag = base.movementStatus;

		// We check to see if the ray has been casted
		// If YES we read and send the ray cast result
		if (base.shouldCastRayFlag) {
			// Passing the value
			movingByUserScript.moveObjectFlag = base.rayCastResult;
		}
	}
}
