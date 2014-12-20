using UnityEngine;

public class FallingDetection : MonoBehaviour
{
	public float 			fallingThreshold = 0.1f;
	public float 			maxFallingThreshold = 1f;
	
	private float 			initialDistance = 0f;
	private RaycastHit 		hit;

	public float 			relDistance = 0f;

	public bool 			fellOffACliff = false;
	public bool 			basicFalling = false;
	public bool 			infiniteFall = false;

	
	void Start()
	{
		var dist = 0f;
		GetHitDistance(out dist);
		initialDistance = dist;
	}
	bool GetHitDistance(out float distance)
	{
		// 3d Ray downRay = new Ray(transform.position, -Vector3.up); // this is the downward ray
		distance = 0f;

		// Constructing the ray
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(transform.position.x, transform.position.y, gameObject.GetComponentInParent(typeof(Transform)).transform.position.z));
		// Casting Ray
		RaycastHit2D hit = Physics2D.Raycast (ray.origin, -Vector2.up, Mathf.Infinity);

		// 3d if (Physics.Raycast(downRay, out hit))
		if (hit)
		{
			distance = ( hit.point - (Vector2) transform.position ).magnitude;
			return true;
		}
		return false;
	}
	void Update()
	{
		// Resetting the flags
		fellOffACliff = false;
		basicFalling = false;
		infiniteFall = false;

		// Resseting the distance
		relDistance = 0f;

		var dist = 0f;
		if (GetHitDistance(out dist))
		{
			if (initialDistance < dist)
			{
				//Get relative distance
				relDistance = dist - initialDistance;
				
				//Are we actually falling?
				if (relDistance > fallingThreshold)
				{
					//How far are we falling
					if (relDistance > maxFallingThreshold) {
						// Debug.Log("Fell off a cliff");
						fellOffACliff = true;
						gameObject.rigidbody2D.gravityScale = 20;
					}
					else
					{
						// Debug.Log("basic falling!");
						basicFalling = true;
					}
				}
			}
		}
		else
		{
			//Debug.Log("Infinite Fall");
			infiniteFall = true;
		}
	}
}
