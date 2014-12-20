using UnityEngine;
using System.Collections;

public class DirectionRaycasting2DBoulder : DirectionRaycasting2DCollider {

	// References
	private RollingBoulder			 rollingBoulderScript;
	
	public override void Start() {
		// Calling parent Start function
		base.Start();
		
		// Reference to script
		rollingBoulderScript = gameObject.GetComponent<RollingBoulder>();
	}
	
	// Update is called once per frame
	public override void Update () {
		
		// Calling parent Update function
		base.Update ();
		
		rollingBoulderScript.rayCastCollisionDown = collisionDown;
		rollingBoulderScript.rayCastCollisionLeft = collisionLeft;
		rollingBoulderScript.rayCastCollisionUp = collisionUp;
		rollingBoulderScript.rayCastCollisionRight = collisionRight;
	}
}
