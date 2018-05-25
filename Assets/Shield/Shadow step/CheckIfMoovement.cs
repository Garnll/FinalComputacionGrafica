using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfMoovement : MonoBehaviour {

	class MyChildren
	{
		public Transform childTranform;
		public Vector3 staticTransform;
	}

	MyChildren[] children;


	public delegate void Factor();
	public static event Factor OnSomethingMoved;

	// Use this for initialization
	void Start () {
		Transform[] child = transform.GetComponentsInChildren<Transform> ();

		children = new MyChildren[child.Length];

		for (int i = 0; i < children.Length; i++) {
			children [i] = new MyChildren ();

			children [i].childTranform = child [i];

			children [i].staticTransform = children [i].childTranform.position;
		}

	}
	
	// Update is called once per frame
	void LateUpdate () {
		for (int i = 0; i < children.Length; i++) {
			if (children [i].childTranform.position != children [i].staticTransform)
			{
				OnSomethingMoved();
				children [i].staticTransform = children [i].childTranform.position;
				break;
			}
		}
	}
}
