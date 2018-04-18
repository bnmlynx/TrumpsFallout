using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonaldBehaviour : MonoBehaviour {

	public float scaleMax = 1.5f;
	public float scaleMin = 0.1f;
	public float maxOrbitSpeed = 110f;
	public int donaldHealth = 100;
	public int scoreValue = 1; 
	public float growingSpeed = 20f;
	public AudioClip[] donaldSounds;
	public float speed = 5f;
	public Vector3 target;
	//public Transform playerPosition;

	private bool isAlive = true; 
	private float orbitSpeed;
	private Transform orbitAnchor;
	private Vector3 orbitDirection; 
	private Vector3 MaxScale;
	private bool isScaled = false;

	// Use this for initialization
	void Start () 
	{
		//DonaldSettings ();
		Vector3 target = new Vector3(0f, 0f, 0f);

	}
	
	// Update is called once per frame
	void Update () 
	{
		//RotateDonald ();
		float step = speed * Time.deltaTime;
		transform.position = transform.position = Vector3.MoveTowards(transform.position, target, step);

		if (!isScaled)
			ScaleDonald ();

		//if (transform.position.sqrMagnitude - playerPosition.sqrMagnitude > 2000f) {
		//}
	}
		

	public bool Hit(int hitDamage) 
	{
		donaldHealth -= hitDamage;

		if (donaldHealth >= 0 && isAlive) {
			ScoreManager.score += 1; 
			StartCoroutine (DestroyDonald ());
			return true;
		}

		return false;
	}

	private IEnumerator DestroyDonald() 
	{
		isAlive = false;
		GetComponent<Renderer> ().enabled = false;
		yield return new WaitForSeconds (0.5f);
		Destroy (gameObject);
	}

	private void RotateDonald() 
	{
		transform.RotateAround (orbitAnchor.position, orbitDirection, orbitSpeed * Time.deltaTime);
	}

	private void ScaleDonald()
	{
		if (transform.localScale != MaxScale) {
			transform.localScale = Vector3.Lerp (transform.localScale, MaxScale, Time.deltaTime * growingSpeed);
		
		} else {
			isScaled = true;
		}
	}



	private void DonaldSettings() 
	{
		orbitAnchor = Camera.main.transform;



		float x = Random.Range (-1f, 1f);
		float y = Random.Range (-1f, 1f);
		float z = Random.Range (-1f, 1f);
		orbitDirection = new Vector3 (x, y, z);

		orbitSpeed = Random.Range (30f, maxOrbitSpeed);
		float scale = Random.Range (scaleMin, scaleMax);

		MaxScale = new Vector3 (scale, scale, scale);

	}
}
