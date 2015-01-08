using UnityEngine;
using System.Collections;

public class BulaController : MonoBehaviour {

	// Will set the speed when walking
	public float walkSpeed = 1f;
	// Reference to the object Animator
	Animator anim;
	// Flag which will notify if a spike has been touch
	public bool spikesInteraction = false;
	// Reference to interact line cast end point
	public GameObject groundedEnd;

	// Use this for initialization
	void Start () {

		// Reference to the object Animator
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		RayCasting();
		Movement();
	}

	void RayCasting()
	{
		Debug.DrawLine(this.transform.position, groundedEnd.transform.position, Color.green);

		// Checking if Bula touched any deadly spikes
		spikesInteraction = Physics2D.Linecast(this.transform.position, groundedEnd.transform.position, 1 << LayerMask.NameToLayer("Spikes"));
	}

	void Movement(){

		// Checking if Bula touched the spikes
		// In this case he will die
		if (spikesInteraction)
		{
			Destroy(this.gameObject);
		}
		else
		{
			// Setting the velocity of Bula
			this.rigidbody2D.velocity = Vector2.right * walkSpeed;
			// Trigget the walk speed
			anim.SetTrigger("Bula_Walk");
		}
	}
}
