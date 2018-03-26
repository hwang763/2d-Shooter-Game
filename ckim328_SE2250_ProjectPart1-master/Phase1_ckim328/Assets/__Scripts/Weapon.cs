﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour {
	static public Transform PROJECTILE_ANCHOR;

	[Header ("Set Dynamically")] [SerializeField]
	private WeaponType _type = WeaponType.none;
	public WeaponDefinition def;
	public GameObject collar;
	public float lastShotTime;
	private Renderer collarRend;
		

	// Use this for initialization
	void Start () {
		collar = transform.Find ("Collar").gameObject;
		collarRend = collar.GetComponent<Renderer>();

		SetType (_type);


		if (PROJECTILE_ANCHOR == null) {
			GameObject go = new GameObject ("_ProjectileAnchor");
			PROJECTILE_ANCHOR = go.transform;
		}

		GameObject rootGO = transform.root.gameObject;
		if (rootGO.GetComponent<Hero> () != null) {
			rootGO.GetComponent<Hero> ().fireDelegate += Fire;
		
		}
	}
	public WeaponType type {
		get { return (_type); }
		set { SetType (value); }
	}

	public void SetType(WeaponType wt){
		_type=wt;
		if (_type==WeaponType.none){
			this.gameObject.SetActive(false);
			return;
		} else{
			this.gameObject.SetActive (true);
		}
		def = Main.GetWeaponDefinition (_type);
		collarRend.material.color = def.color;
		lastShotTime = 0;
	}

	public void Fire (){
		if (!gameObject.activeInHierarchy)
			return;
		if (Time.time - lastShotTime < def.delayBetweenShots) {
			return;
		}

		Projectile p;
		Vector3 vel = Vector3.up * def.velocity;
		if (transform.up.y < 0) {
			vel.y = -vel.y;
		}
		switch (type) {
		case  WeaponType.blaster:
			p = MakeProjectile ();
			p.rigid.velocity = vel;
			break;
		
		case WeaponType.spread:
			p = MakeProjectile ();//makes the middle projectile
			p.rigid.velocity = vel;
			p = MakeProjectile ();//makes right projectile
			p.transform.rotation = Quaternion.AngleAxis (30, Vector3.forward);
			p.rigid.velocity = p.transform.rotation * vel;
			p = MakeProjectile ();//makes left projectile
			p.transform.rotation = Quaternion.AngleAxis (30, Vector3.back);
			p.rigid.velocity = p.transform.rotation * vel;
			break;
		}
	}


	public Projectile MakeProjectile(){
		GameObject go = Instantiate<GameObject>(def.projectilePrefab);
		if (transform.parent.gameObject.tag=="Hero"){
			go.tag="ProjectileHero";
			go.layer=LayerMask.NameToLayer("ProjectileHero");
		} else {
		//	go.tag= "ProjectileEnemy" ;
		go.layer= LayerMask.NameToLayer("ProjectileEnemy");
		}
		go.transform.position=collar.transform.position;
		go.transform.SetParent(PROJECTILE_ANCHOR, true);
		Projectile p = go.GetComponent<Projectile>();
		p.type=type;
		lastShotTime= Time.time;
		return (p);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			SetType (WeaponType.blaster);
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			SetType (WeaponType.spread);
		}
	}
}
