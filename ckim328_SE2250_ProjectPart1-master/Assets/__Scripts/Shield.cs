using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	[Header("Set in Inspector")]
	public float rotationsPerSecond= 0.1f;

	[Header("Set Dynamically")]
	public int levelShown = 0;

	Material mat;

	// Use this for initialization
	void Start () {
		mat = GetComponent<Renderer> ().material;
	}
	
	// Update is called once per frame
	void Update () {
		//Reads the current shield level from the Hero
		int currLevel=Mathf.FloorToInt(Hero.S.shieldLevel);
		//If this is different from level shown
		if(levelShown!=currLevel){
			levelShown = currLevel;
			//adjust the texture offset to show the different shield level
			mat.mainTextureOffset=new Vector2(0.2f*levelShown, 0);
		}
		//rotate the shiled a bit each frame
		float rZ=-(rotationsPerSecond*Time.time*360)%360f;
		transform.rotation = Quaternion.Euler (0,0,rZ);
	}
}
