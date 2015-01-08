using UnityEngine;
using System.Collections;

public class DirectionRaycasting2DBrick : DirectionRaycasting2DCollider {

	// References
	private MovingByUser movingByUserScript;

	public override void Start() {
		// Calling parent Start function
		base.Start();

		// Reference to script
		movingByUserScript = gameObject.GetComponent<MovingByUser>();
	}

	// Update is called once per frame
	public override void Update () {

		if(this.gameObject != null)
		{
			// Calling parent Update function
			base.Update ();
			
			movingByUserScript.rayCastCollisionDown = collisionDown;
			movingByUserScript.rayCastCollisionLeft = collisionLeft;
			movingByUserScript.rayCastCollisionUp = collisionUp;
			movingByUserScript.rayCastCollisionRight = collisionRight;
		}
	}
}
