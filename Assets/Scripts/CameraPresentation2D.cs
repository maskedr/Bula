using UnityEngine;
using System.Collections;

public class CameraPresentation2D : MonoBehaviour {

	public float cameraSpeed = 0.19f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		// Move 1 unit left 
		transform.position = new Vector3 (transform.position.x + cameraSpeed * Time.deltaTime, transform.position.y, transform.position.z);

	}
}
