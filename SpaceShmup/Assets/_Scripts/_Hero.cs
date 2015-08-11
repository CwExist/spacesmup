using UnityEngine;
using System.Collections;

public class _Hero : MonoBehaviour {

	static public _Hero S;

	//movement control
	public float speed = 30f;
	public float rollMult = -45f;
	public float pitchMult = 30f;

	//ship status info
	public float shieldLevel = 1f;

	// separator
	public bool _________;
	public Bounds bounds;

	//awake is called as soon as launched
	void awake() {
		S = this; //set the singleton
		bounds = Utils.CombineBoundsOfChildren (this.gameObject);
	}

	// Update is called once per frame
	void Update () {
	//pull in info from input class
		float xAxis = Input.GetAxis ("Horizontal");
		float yAxis = Input.GetAxis ("Vertical");

		// change transform.position based on the axes
		Vector3 pos = transform.position;
		pos.x += xAxis * speed * Time.deltaTime;
		pos.y += yAxis * speed * Time.deltaTime;
		transform.position = pos;

		bounds.center = transform.position;

		Vector3 off = Utils.ScreenBoundsCheck (bounds, BoundsTest.onScreen);
		if (off != Vector3.zero){
			pos -= off;
			transform.position = pos;
		}

		// rotate the ship
		transform.rotation = Quaternion.Euler (yAxis * pitchMult, xAxis * rollMult, 0);

	}
}
