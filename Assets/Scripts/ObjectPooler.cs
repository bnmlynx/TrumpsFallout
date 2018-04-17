using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

	public static ObjectPooler mSharedInstance;
	public List<GameObject> mPooledObjects;
	public GameObject mObjectToPool;
	public int mAmountToPool;
	public bool mWillGrow = true;

	void Awake()
	{
		mSharedInstance = this;
	}

	// Use this for initialization
	void Start () {

		for (int i = 0; i < mAmountToPool; i++) {
			GameObject obj = (GameObject)Instantiate (mObjectToPool);
			obj.SetActive (false);
			mPooledObjects.Add (obj);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject GetPooledObject() 
	{
		for (int i = 0; i < mPooledObjects.Count; i++) {
			if (!mPooledObjects[i].activeInHierarchy) {
				return mPooledObjects [i];
			}
		}

		if (mWillGrow)
		{
			GameObject obj = (GameObject)Instantiate (mObjectToPool);
			mPooledObjects.Add (obj);
			return obj;
		}

		return null;
	}
}
