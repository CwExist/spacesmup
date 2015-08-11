using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum BoundsTest{
	center, // is the center of thegame object?
	onScreen, // are the bounds entirely on screen?
	offScreen // are the bounds entirely off the screen?
}
public class Utils : MonoBehaviour {

	/* BOUNDS FUNCTIONS*/
	 
	public static Bounds BoundsUnion( Bounds b0, Bounds b1) {
		// if one of the bounds is Vector3.zero, ignore that one
		if (b0.size == Vector3.zero && b1.size != Vector3.zero) {
			return (b1);
		} else if (b0.size != Vector3.zero && b1.size == Vector3.zero) {
			return(b0);
		} else if (b0.size == Vector3.zero && b1.size == Vector3.zero) {
			return(b0);
		}
		//stretch b0 to include b1.min and b1 max
		b0.Encapsulate (b1.min);
		b0.Encapsulate (b1.max);
		return(b0);	
	}

	public static Bounds CombineBoundsOfChildren(GameObject go) {
		//create an empty bounds b
		Bounds b = new Bounds (Vector3.zero, Vector3.zero);
		// if a game object has a renderer component
		if (go.GetComponent<Renderer>()!= null) {
			//expand b to contain the renderer's bounds
			b = BoundsUnion (b, go.GetComponent<Renderer>().bounds);
	}
		if (go.GetComponent<Collider> () !=null) {
			b = BoundsUnion(b, go.GetComponent<Collider>().bounds);
	}
		foreach (Transform t in go.transform) {
			b = BoundsUnion(b,CombineBoundsOfChildren(t.gameObject));
		}
		return(b);
	}
		static public Bounds camBounds
	{
			get{ 
				if (_camBounds.size == Vector3.zero) {
					SetCameraBounds();
				}
				return(_camBounds);
			}
		}
		static private Bounds _camBounds;

		//public static void SetCameraBounds(Camera cam=null){
			//if(cam == null) {
				//	cam = Camera.main;
			//}
			// use the above commented stuff at home	
		public static void SetCameraBounds () {
			Camera cam;		// get rid of this at home
			cam = Camera.main; // get rid of this at home
			Vector3 topleft = new Vector3 (0,0,0);
			Vector3 bottomRight = new Vector3 (Screen.width,Screen.height,0);
			Vector3 boundTLN = cam.ScreenToWorldPoint (topleft);
			Vector3 boundBRF = cam.ScreenToWorldPoint (bottomRight);

			boundTLN.z += cam.nearClipPlane;
			boundBRF.z += cam.farClipPlane;
			Vector3 center = (boundTLN + boundBRF) /2f;
			_camBounds = new Bounds (center, Vector3.zero);
			_camBounds.Encapsulate (boundTLN);
			_camBounds.Encapsulate (boundBRF);

		}
		public static Vector3 ScreenBoundsCheck(Bounds bnd, BoundsTest test = BoundsTest.center){

			return(BoundsInBoundsCheck (camBounds, bnd, test));
	}

public static Vector3 BoundsInBoundsCheck( Bounds bigB, Bounds lilB, BoundsTest test = BoundsTest.onScreen){
		Vector3 pos = lilB.center;
		Vector3 off = Vector3.zero;
	switch (test) {

		case BoundsTest.center:
			if (bigB.Contains(pos)){
				return(Vector3.zero);
			}

			if(pos.x > bigB.max.x){
				off.x = pos.x - bigB.max.x;
			} else if (pos.x < bigB.min.x){
				off.x = pos.x - bigB.min.x;
			}

			if(pos.y > bigB.max.y){
				off.y = pos.y - bigB.max.y;
			} else if (pos.y < bigB.min.y){
				off.y = pos.y - bigB.min.y;
			}

			if(pos.z > bigB.max.z){
				off.z = pos.z - bigB.max.z;
			} else if (pos.z < bigB.min.z){
				off.z = pos.z - bigB.min.z;
			}
			return(off);
		}
		return(Vector3.zero);

	}
}
