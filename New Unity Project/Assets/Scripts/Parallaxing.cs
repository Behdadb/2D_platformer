using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

	private GameObject[] backs;
	public float smoothing = 10f;

	private Transform cam;
	private Vector3 previewsCamPosition;

	void Awake () {
		cam = Camera.main.transform;
	}

	// Use this for initialization
	void Start () {
		previewsCamPosition = cam.position;
	}
	
	// Update is called once per frame
	void Update () {
		backs = GameObject.FindGameObjectsWithTag ("Background");
		for (int i=0; i<backs.Length; i++) {
			Transform background = backs[i].transform;
			float parallax = (previewsCamPosition.x - cam.position.x) * background.position.z*-1;
			float backgroundTargetPosition = background.position.x + parallax;
			Vector3 newPos = new Vector3 (backgroundTargetPosition, background.position.y , background.position.z);
			background.position = Vector3.Lerp (background.position, newPos, smoothing * Time.deltaTime);
		}
		previewsCamPosition = cam.position;
	}
}
