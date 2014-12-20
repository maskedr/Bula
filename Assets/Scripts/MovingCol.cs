using UnityEngine;
using System.Collections;

public class MovingCol : MonoBehaviour {
	/*
    private Camera mainCamera;
    private CastingRayManager3 castingRayManager3;
    private bool moveObjectFlag = false;
    public float forceMagnitude = 15;

    // Use this for initialization
    void Start()
    {
        // Reference to scripts
        castingRayManager3 = gameObject.GetComponent<CastingRayManager3>();
    }

    // Update is called once per frame
    void Update()
    {

        if (moveObjectFlag && (Input.GetMouseButton(0) || Input.touchCount == 1))
        {
            // Apply the force for moving the object
            ApplyForce(gameObject.rigidbody2D);
        }
        else if (Input.GetMouseButton(0) || Input.touchCount == 1)
        {
            // Check if the player selected this->gameObject
            castingRayManager3.CheckIfSelectedByUser();
        }
    }

    // Method for updating the 
    public void ShouldMoveObject(bool newFlag)
    {
        // Updating the flag
        moveObjectFlag = newFlag;
    }

    void ApplyForce(Rigidbody2D body)
    {
        Debug.Log("ApplyForce");

        body.AddForce(transform.right * forceMagnitude);
    }
    */
}
