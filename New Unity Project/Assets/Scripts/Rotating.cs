using UnityEngine;
using System.Collections;

public class Rotating : MonoBehaviour {

	public GameObject parent;
	public int rotationOffset;

	// Use this for initialization
	void Start () {
		transform.position = parent.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = parent.transform.position;

		Vector3 difference = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
		difference.Normalize ();

		float rotZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (0f, 0f, rotZ + rotationOffset);
	}
}
