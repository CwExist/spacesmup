using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
	public float rotatonsPerSecond = 0.1f;
	public bool _________;
	public int levelShown = 0;

	void Update () {
		// read the current shield level from hero singleton
		// taking the floor ensures the shield jumps to the new x offset
		// rather than showing an offset between two shield icons
		int currLevel = Mathf.FloorToInt (_Hero.S.shieldLevel);
		//if this is different from levelShown...
		if (levelShown != currLevel) {
			levelShown = currLevel;
			Material mat = this.GetComponent<Renderer> ().material;
			//adjusts the texture offset to show different shield level
			//this adjusts the x offset of mat_shield to show the
			//proper shield level
			mat.mainTextureOffset = new Vector2 (0.2f * levelShown, 0);
		}
		// rotate the shield a bit every second
		// it will rotate a bit in the z direction every frame
		float rZ = (rotatonsPerSecond * Time.time * 360) % 360f;
		transform.rotation = Quaternion.Euler (0, 0, rZ);
	
	}
}
