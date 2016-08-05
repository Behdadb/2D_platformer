using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

	public int offsetX = 2;
	public bool hasRightBuddy = false;
	public bool hasLeftBuddy = false;
	public bool reverseScale = false;

	private float spriteWidth = 0f;
	private Camera cam;
	private Transform myTransform;
	
	void Awake () {
		cam = Camera.main;
		myTransform = transform;
	}

	// Use this for initialization
	void Start () {
		SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
		spriteWidth = sRenderer.sprite.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
		if ( !hasLeftBuddy || !hasRightBuddy ) {
			float camHorizentalExtent = cam.orthographicSize * Screen.width/Screen.height;
			float rightEdge = (myTransform.position.x + spriteWidth/2) - camHorizentalExtent;
			float leftEdge = (myTransform.position.x - spriteWidth/2) + camHorizentalExtent;

			if(cam.transform.position.x >= rightEdge - offsetX && !hasRightBuddy){
				MakeNewBuddy(1);
				hasRightBuddy = true;
			}
			else if(cam.transform.position.x <= leftEdge + offsetX && !hasLeftBuddy){
				MakeNewBuddy(-1);
				hasLeftBuddy = true;
			}

		}
	}

	void MakeNewBuddy (int direction) {
		Vector3 newPos = new Vector3 (myTransform.position.x + spriteWidth*direction , myTransform.position.y , myTransform.position.z);
		Transform newBuddy = (Transform)Instantiate (myTransform, newPos, myTransform.rotation);

		if (reverseScale) {
			newBuddy.localScale = new Vector3(newBuddy.localScale.x*-1,newBuddy.localScale.y, newBuddy.localScale.z);
		}

		newBuddy.parent = myTransform.parent;

		if (direction > 0) {
			newBuddy.GetComponent<Tiling> ().hasLeftBuddy = true;
		} else {
			newBuddy.GetComponent<Tiling>().hasRightBuddy = true;
		}
	}
}
