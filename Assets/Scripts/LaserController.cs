using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;

public class LaserController : MonoBehaviour {

	public float mFireRate = .5f;
	public float mFireRange = 10000f;
	public float mHitForce = 100f;
	public int mLaserDamage = 100;
	public AudioClip shootSound;
	public AudioClip hitSound;
	public GameObject _sm;
	public ParticleSystem deathEffect;

	private LineRenderer mLaserLine;
	private bool mLazerLineEnabled;
	private WaitForSeconds mLaserDuration =  new WaitForSeconds(0.02f);
	private float mNextFire;
	private AudioSource source;
	private ScoreManager scoreManager;

	// Use this for initialization
	void Start ()
	{
		source = GetComponent<AudioSource>();
		mLaserLine = GetComponent<LineRenderer> ();
		scoreManager = _sm.GetComponent<ScoreManager> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButton ("Fire1") && Time.time > mNextFire) {
			Fire ();
		}
	}

	//function for firing lazer using raycast hit
	private void Fire() 
	{
		Transform cam = Camera.main.transform;

		//rate of fire
		mNextFire = Time.time + mFireRate;

		//the origin of the ray coming out of the camera
		Vector3 rayOrigin = cam.position;

		mLaserLine.SetPosition (0, transform.up * -5f);

		RaycastHit hit;

		//if statement to check if the raycast comes into contact with a collider
		if (Physics.Raycast (rayOrigin, cam.forward, out hit, mFireRange)) {

			//
			mLaserLine.SetPosition (1, hit.point);

			DonaldBehaviour behaviourScript = hit.collider.GetComponent<DonaldBehaviour> ();
			Debug.Log (hit.collider.transform.position);

			if (behaviourScript != null) {
				if (hit.rigidbody != null) {
					hit.rigidbody.AddForce (-hit.normal * mHitForce);

					//destroys game object as well as instantiating the explosion effect
					Destroy (Instantiate (deathEffect.gameObject, hit.collider.transform.position, Quaternion.FromToRotation (cam.forward, hit.collider.transform.localPosition)) as GameObject, deathEffect.startLifetime);
					behaviourScript.Hit (mLaserDamage);
					//decreases the level of radaiation
					scoreManager.decreaseCounter ();
					//play the hit sound effect
					source.PlayOneShot(hitSound);
						
				}
			} 


		} else {
			mLaserLine.SetPosition (1, cam.forward * mFireRange);
		}

		StartCoroutine (LaserFx ());
		source.PlayOneShot(shootSound, 1f);
	
	}

	//function to draw the line renderer aka the laser effect
	private IEnumerator LaserFx() 
	{
		mLaserLine.enabled = true;
		yield return mLaserDuration;
		mLaserLine.enabled = false;

	}
}
