using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

	public delegate void WeaponFireDelegate();

	public static int WeaponSwitch = 0; 
	public WeaponFireDelegate fireDelegate;

	static public Hero S;



	[Header("Set in Inspector")]
	public float speed=30;
	public float rollMult=-45;
	public float pitchMult = 30;
	public float projectileSpeed=40;
	public GameObject  projectilePrefab;

	[Header("Set Dynamically")]
	public float shieldLevel=1;


	void Awake(){
		if (S == null) {
			S = this;
		} else {
			Debug.LogError ("Hero.Awake() -Attempted to assign second Hero.S");
		}
//		fireDelegate += TempFire;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float xAxis = Input.GetAxis ("Horizontal");
		float yAxis = Input.GetAxis ("Vertical");

		Vector3 pos = transform.position;
		pos.x += xAxis * speed * Time.deltaTime;
		pos.y += yAxis * speed * Time.deltaTime;
		transform.position = pos;

		transform.rotation = Quaternion.Euler (yAxis * pitchMult, xAxis * rollMult, 0);

	//	if (Input.GetKeyDown(KeyCode.Space)){
		//	TempFire ();
	//}

		if (Input.GetAxis ("Jump") == 1 && fireDelegate != null) {
			fireDelegate ();
		}
		if (Input.GetKeyDown (KeyCode.B)) {
			WeaponSwitch = 1;
		} else if (Input.GetKeyDown (KeyCode.V)) {
			WeaponSwitch = 2;
		} else if (Input.GetKeyDown (KeyCode.Z)) {
			WeaponSwitch = 0;
		}
	
}
	/*void TempFire ()
	{
		GameObject projGO = Instantiate<GameObject> (projectilePrefab);
		projGO.transform.position = transform.position;
		Rigidbody rigidB = projGO.GetComponent<Rigidbody> ();
		//rigidB.velocity = Vector3.up * projectileSpeed; 

		Projectile proj = projGO.GetComponent<Projectile> ();
		proj.type = WeaponType.blaster;
		float tspeed = Main.GetWeaponDefinition (proj.type).velocity;
		rigidB.velocity = Vector3.up * tspeed;
	

	}*/ 	
}