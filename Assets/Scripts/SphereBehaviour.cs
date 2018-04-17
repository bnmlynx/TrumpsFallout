using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBehaviour : MonoBehaviour {

	public float mScaleMax = 4f;
	public float mScaleMin = 1f;
	public float mOrbitMaxSpeed = 30f;
	public int mCubeHealth = 100;
	public int scoreValue = 1;
	private bool mIsAlive = true;
	private float mOrbitSpeed;
	private Transform mOrbitAnchor;
	private Vector3 mOrbitDirection;
	private Vector3 mCubeMaxScale;
	public float mGrowingSpeed = 10f;
	private bool mIsCubeScaled = false;


	// Use this for initialization
	void Start () {
		CubeSettings ();
	}


	void Update() {
		RotateCube ();

		if (!mIsCubeScaled)
			ScaleObj ();
	}

	public bool Hit(int hitDamage) {
		mCubeHealth -= hitDamage;
		if (mCubeHealth >= 0 && mIsAlive) {
			ScoreManager.time += 2.0f;
			StartCoroutine (DestroyCube ());
			return true;
		}
		return false;
	}

	private IEnumerator DestroyCube() {
		mIsAlive = false;
		GetComponent<Renderer> ().enabled = false;
		yield return new WaitForSeconds (0.5f);
		Destroy(gameObject);
	}

	private void RotateCube() {
		transform.RotateAround (mOrbitAnchor.position, mOrbitDirection, mOrbitSpeed * Time.deltaTime);
		transform.Rotate (mOrbitDirection * 30 * Time.deltaTime);
	}

	private void ScaleObj () {
		if (transform.localScale != mCubeMaxScale) {
			transform.localScale = Vector3.Lerp (transform.localScale, mCubeMaxScale, Time.deltaTime * mGrowingSpeed);
		} else {
			mIsCubeScaled = true;
		}
	}

	private void CubeSettings() 
	{
		mOrbitAnchor = Camera.main.transform;

		float x = Random.Range (-1f, 1f);
		float y = Random.Range (-1f, 1f);
		float z = Random.Range (-1f, 1f);
		mOrbitDirection = new Vector3 (x, y, z);

		mOrbitSpeed = Random.Range (5f, mOrbitMaxSpeed);

		float scale = Random.Range (mScaleMin, mScaleMax);
		mCubeMaxScale = new Vector3 (scale, scale, scale);

		transform.localScale = Vector3.zero;
	}
}
