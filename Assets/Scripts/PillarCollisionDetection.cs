using UnityEngine;
using System.Collections;

public class PillarCollisionDetection : MonoBehaviour {

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Pillar")
		{
			Destroy(col.gameObject);
		}
	}
}
