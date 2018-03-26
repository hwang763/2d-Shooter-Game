using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {


	static public Hero S;
	//public float shieldLevel=1;
	private GameObject lastTriggerGo = null;


	[Header("Set in Inspector")]
	public float speed=30;
	public float rollMult=-45;
	public float pitchMult = 30;
	public float gameRestartDelay=2f;


	[Header("Set Dynamically")]
	[SerializeField]
	public float _shieldLevel=1;

	void Awake(){
		if (S == null) {
			S = this;
		} else {
			Debug.LogError ("Hero.Awake() -Attempted to assign second Hero.S");
		}
	
	}
	// Use this for initialization
	void Start () {
		print("Hey daddy");
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
	

	
	}

	void OnTriggerEnter(Collider other){
		Transform rootT = other.gameObject.transform.root;
		GameObject go = rootT.gameObject;
		//print ("Triggered: " + go.name);
		if(go==lastTriggerGo){
			return;
		}
		lastTriggerGo = go;
		if (go.tag == "Enemy") {
			shieldLevel--;
			Destroy (go);
		} else {
			print ("Triggered by non-Enemy: " + go.name);
		}
	}

	public float shieldLevel{
		get{ 
			return(_shieldLevel);
		}
		set{ 
			_shieldLevel = Mathf.Min (value, 4);
			//if the shield is going to be set to less than 0
			if(value<0){
				Destroy (this.gameObject);
				Main.S.DelayedRestart (gameRestartDelay);
			}
		}
	}
}
