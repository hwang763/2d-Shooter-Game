﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static public Main S;


    public GameObject[] prefabEnemies; //array of enemy prefabs
    public float enemiesSpwnedPerSecond = 0.5f; //#of enemies per second
    public float enemyDefaultPadding = 1.5f; //padding for position

    private BoundsCheck bndCheck;

    private void Awake()
    {
        S = this;
        bndCheck = GetComponent<BoundsCheck>();
        Invoke("SpawnEnemy", 1f / enemiesSpwnedPerSecond);
    }

    public void SpawnEnemy()
    {
        //pick an enemy to instantiate
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

        //positioning the enemy
        float enemyPadding = enemyDefaultPadding;
        if (go.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }

        //setting the pos
        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyPadding;
        float xMax = bndCheck.camWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight*2.5f + enemyPadding;
        go.transform.position = pos;

        Invoke("SpawnEnemy", 1f / enemiesSpwnedPerSecond);
    }

	public void DelayedRestart(float delay){
	
		Invoke ("Restart", delay);
	}
	public void Restart(){
	
		SceneManager.LoadScene ("_Scene_0");
	}

}
