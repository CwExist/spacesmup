using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	//variables
	public float speed = 10f;// speed in meters pers second
	public float fireRate = 0.3f; // seconds per shot (unuse for now)
	public float  health = 10f ;// enemy health
	public int score = 100; // points earned for destroying this

	public bool __________;
	public Bounds bounds;
	public Vector3 boundsCenterOffset; // distanct of bounds. center from position

	void Awake() {
		InvokeRepeating ("CheckOffScreen", 0f, 2f);
	}

	// Update is called once per frame
	void Update () {
		Move();   					// the function that, we, move the enemy?	
	}

	public virtual void Move() {
		 Vector3 tempPos = pos;
		tempPos.y -= speed * Time.deltaTime;
		pos = tempPos;

	}


public Vector3 pos {
		get{
			return(this.transform.position);
		}
		set {
			this.transform.position = value;
		}
	}
	void CheckOffScreen() {
		if(bounds.size == Vector3.zero){

			bounds = Utils.CombineBoundsOfChildren(this.gameObject);

			boundsCenterOffset = bounds.center - transform.position;
		}

		bounds.center = transform.position + boundsCenterOffset;
		Vector3 off = Utils.ScreenBoundsCheck (bounds, BoundsTest.offScreen);
		if (off != Vector3.zero){
			if (off.y < 0){
				Destroy (this.gameObject);
			}
		}
	}
}
