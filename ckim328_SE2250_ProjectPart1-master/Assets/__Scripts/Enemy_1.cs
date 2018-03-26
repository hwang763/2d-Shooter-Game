using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy {
	public int movetype;
	void Start(){
		movetype = Random.Range (0, 2);
	}
	public override void Move(){
		Vector3 tempPos = pos;

		switch (movetype) {
		case 0:
			tempPos.x -= speed * Time.deltaTime;
			break;
		case 1: 
			tempPos.x += speed * Time.deltaTime;
			break;
		}
		pos = tempPos;
		base.Move ();
	}

}
