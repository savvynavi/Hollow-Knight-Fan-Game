﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

	[SerializeField]
	GameObject prefab;
	[SerializeField]
	Transform parent;
	[SerializeField]
	int numInPool = 20;

	public GameObject Prefab {
		get { return prefab; }
		private set { }
	}

	List<GameObject> pooledObjects;

	private void Awake() {
		pooledObjects = new List<GameObject>();

		//instantiating all objects and setting them to inactive
		for(int i = 0; i < numInPool; i++) {
			GameObject obj = Instantiate(prefab, parent);
			obj.SetActive(false);
			pooledObjects.Add(obj);
		}
	}

	public GameObject GetPooledObject() {

		//loop over list, return first inactive obj in list to use
		for(int i = 0; i < pooledObjects.Count; i++) {
			if(!pooledObjects[i].activeInHierarchy) {
				return pooledObjects[i];
			}
		}

		//if all objects in use, instantiate new one and return that
		GameObject obj = Instantiate(prefab, parent);
		obj.SetActive(false);
		pooledObjects.Add(obj);
		return obj;
	}

	//turns all objects in the pool off and resets position
	public void ResetAll() {
		for(int i = 0; i < pooledObjects.Count; i++) {
			pooledObjects[i].SetActive(false);
			pooledObjects[i].transform.position = prefab.transform.position;
		}
	}
}
