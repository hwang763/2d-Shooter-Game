using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum WeaponType{
	none,
	blaster,
	spread,
	phaser,
	missle,
	laser,
	shield
}
[System.Serializable]
public class WeaponDefinition{
	public WeaponType type= WeaponType.none;
	public string letter;
	public Color color=Color.white;
	public GameObject projectilePrefab;
	public Color projectileColor=Color.white;
	public float damageOnHit = 0;
	public float delayBetweenShots= 0;
	public float velocity=20;
}

public class Main : MonoBehaviour
{
    static public Main S;
	static Dictionary<WeaponType,WeaponDefinition> WEAP_DICT;


    public GameObject[] prefabEnemies; //array of enemy prefabs
    public float enemiesSpwnedPerSecond = 0.5f; //#of enemies per second
    public float enemyDefaultPadding = 10f; //padding for position
	public WeaponDefinition[] weaponDefinitions;

    private BoundsCheck bndCheck;

    private void Awake()
    {
        S = this;
        bndCheck = GetComponent<BoundsCheck>();
        Invoke("SpawnEnemy", 1f / enemiesSpwnedPerSecond);

		WEAP_DICT= new Dictionary<WeaponType, WeaponDefinition>();
		foreach (WeaponDefinition def in weaponDefinitions) {
			WEAP_DICT [def.type] = def;
		}
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
        pos.y = bndCheck.camHeight+ enemyPadding;
        go.transform.position = pos;

        Invoke("SpawnEnemy", 1f / enemiesSpwnedPerSecond);
    }
	static public WeaponDefinition GetWeaponDefinition(WeaponType wt){
		//checks to make sure key exists in the dictionary
		// attempting to retrieve a key that doenst exists throws an error
		if (WEAP_DICT.ContainsKey(wt)) {
			return ( WEAP_DICT[wt]);
		}
			// this will return a new weapon def with  type  none
			// meaning it has failed to find the right  weapon def

			return(new WeaponDefinition());
		}


}
