using UnityEngine;
using System.Collections;

public class Movable : MonoBehaviour {

	private Camera mainCamera;
	private bool	moveObject = false;

	void Awake () {
		// Reference to the main camera
		mainCamera = Camera.main;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (moveObject && Input.GetMouseButton(0)) {
			// Referrence to the mouse current mouse position
			Vector3 mousePosition = Input.mousePosition;
			// Output the current mouse position
			Debug.Log("Mouse Position: " + mousePosition);
			
			Vector3 tmpVector3 = new Vector3(mousePosition.x, mousePosition.y, mainCamera.transform.position.z);
			Debug.Log("Tmp Vector: " + tmpVector3);
			
			// Constructing a new Vector3 which will be used to set the position of the selected building
			Vector3 convertedVector3 = mainCamera.ScreenToWorldPoint(tmpVector3);
			// Output the new converted value
			Debug.Log("Converted mouse position: " + convertedVector3);
						
			// Moving the object to where the mouse is moving
			MoveTheObjectAtTheCoordinates(convertedVector3);
		}
		else if (Input.GetMouseButtonDown(0)) {
			Debug.Log("Pressed left click, casting ray.");
			CastRay();
		}
		else {
			moveObject = false;
		}

	}

	void CastRay() {

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);
		if (hit) {
			Debug.Log (hit.collider.gameObject.name);
			if (hit.collider == gameObject.collider2D) {
				Debug.Log ("The RayCast hitted my object");
				moveObject = true;
			}
		}
		else {
			Debug.Log ("The RayCast doesn't hitted an object");
			moveObject = false;
		}
	}

	void MoveTheObjectAtTheCoordinates (Vector3 newCoord) {

		Debug.Log ("MoveTheObjectAtTheCoordinates: " + newCoord);

		// Change the position of object
		transform.position = new Vector3 (newCoord.x, newCoord.y, transform.position.z);
	}
}
