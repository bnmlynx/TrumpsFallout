using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomMovement : MonoBehaviour {


	public Vector3 movement;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		float temp1 = 0f;
		float temp2 = Mathf.Cos (10f * Time.deltaTime);


		transform.Rotate (temp1, temp2, 0);



	}
}
