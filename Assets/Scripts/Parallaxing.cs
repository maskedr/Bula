using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

	public Transform[] parallaxElements; 	// For storing the elements we want to use when creating parallaxing effects. The main Layer (Default) will not be stored
	private float[] parallaxScales;			// The proportion of the camera movement to move the layers by
	public float smoothing = 1f;			// How smooth the parallax is going to be. Make sure to set this above 0 otherwise the parallaxing effect will not work

	private Transform cam;					// Reference to the main cameras transform
	private Vector3 previousCamPos;			// Store the position of the camera in the previous frame

	// Called before Start (). Call all the logic before Start () but after all the game objects are set up.
	// Great for references
	void Awake () {
		// Set up the camera reference
		cam = Camera.main.transform;
	}

	// Use this for initialization
	void Start () {
		// Store the previous frame camera position
		previousCamPos = cam.position;

		// Assigning corresponding parallaxScales
		parallaxScales = new float[parallaxElements.Length];
		for (int counter = 0 ; counter < parallaxScales.Length; counter++) {
			parallaxScales[counter] = parallaxElements[counter].position.z * -1;
		}
	}
	
	// Update is called once per frame
	void Update () {

		// For each background
		for (int counter = 0; counter < parallaxElements.Length; counter++) {

			// The parallax is the oposite of the camera movement because the previous frame multiplied by the scale
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[counter];

			// Set a target x position which is the current position plus the parallax
			float parallaxElementTargetPosX = parallaxElements[counter].position.x + parallax;

			// Create a target position which is the backgrounds current position with it's target x position
			Vector3 parallaxElementTargetPos = new Vector3(parallaxElementTargetPosX, parallaxElements[counter].position.y, parallaxElements[counter].position.z);

			// Fade between current position and the target position
			parallaxElements[counter].position = Vector3.Lerp (parallaxElements[counter].position, parallaxElementTargetPos, smoothing * Time.deltaTime);
		}

		// Set the previous cam pos to the camera's position of the end of the frame
		previousCamPos = cam.position;
	}
}
