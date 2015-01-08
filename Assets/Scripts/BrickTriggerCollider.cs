using UnityEngine;
using System.Collections;

public class BrickTriggerCollider : MonoBehaviour {

	// References
	private MovingByUser movingByUserScript;
	// Recerence to the parent 
	public GameObject parentObject;
	// For checking the states
	public bool triggerEnter = false, triggerExit = false;
	// Timer 
	public float timerDestroy = 1f;

	void Start() {
		// Reference to script
		movingByUserScript =  this.parentObject.GetComponent<MovingByUser>();
	}

	void OnTriggerEnter2D(Collider2D other) {
		//  Update the trigger bool
		triggerEnter = true;

		Debug.Log(other.collider2D.gameObject.name);

		if(other.gameObject.name == "lvl1_scene1_ground")
			Invoke("DestroingObject", timerDestroy);
		else if(other.gameObject.name == "Bula")
		{
			movingByUserScript.CharacterStepedOn();
		}
	}

	void OnTriggerExit2D(Collider2D other) {

		//  Update the trigger bool
		triggerEnter = false;
		triggerExit = true;

		// Check to see if the collider is Bula
		if (other.gameObject.name == "Bula")
		{
			parentObject.rigidbody2D.isKinematic = false;
			parentObject.rigidbody2D.mass = 50f;
			parentObject.rigidbody2D.gravityScale = 9.8f;
			parentObject.rigidbody2D.fixedAngle = false;
		}
	}

	void DestroingObject(){
		// We destroy the object
		DestroyObject(parentObject);
	}
}
