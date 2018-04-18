using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class SpawnScript : MonoBehaviour {

	public GameObject donaldTrump;
	public int mTotalTrumps = 10;
	public float mTimeToSpawn = 1f;
	public GameObject g;
	public Transform mTarget;

	private ScoreManager manager;
	private GameObject[] trumps;
	private bool mPositionSet;

	void Start()
	{
		StartCoroutine (SpawnLoop ());
		trumps = new GameObject[mTotalTrumps];
		manager = g.GetComponent<ScoreManager> ();
	}

	private IEnumerator SpawnLoop() 
	{
		StartCoroutine (ChangePosition ());

		yield return new WaitForSeconds (0.2f);

		int i = 0;
		while (i <= (mTotalTrumps - 1)) {
			trumps [i] = SpawnElement ();
			i++;
			yield return new WaitForSeconds (Random.Range (mTimeToSpawn, mTimeToSpawn * 3));
		}
	}

	private GameObject SpawnElement()
	{
		GameObject donald = Instantiate (donaldTrump, (Random.insideUnitSphere * 8) + transform.position, transform.rotation) as GameObject;
		float scale = Random.Range (1f, 3f);
		donald.transform.localScale = new Vector3 (scale, scale, scale);
		donald.transform.LookAt(mTarget);
		//manager.increaseCounter ();
		return donald;
	}

	private IEnumerator ChangePosition() 
	{
		yield return new WaitForSeconds (0.2f);

		if (!mPositionSet) {
			if (VuforiaBehaviour.Instance.enabled) {
				SetPosition ();
			}
		}
	}
		
	private void SetPosition() 
	{
		Transform cam = Camera.main.transform;
		transform.position = cam.forward * 50;
	}
}
