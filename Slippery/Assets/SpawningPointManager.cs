using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPointManager : MonoBehaviour {

    public GameObject[] spPoints;
    public GameObject powerupPrefab;

    public float spawningTime;
	
	// Update is called once per frame
	void Start () {
        StartCoroutine(SpawnPowerups());
	}

    IEnumerator SpawnPowerups()
    {
        while (true)
        {
            int pos = Random.Range(0, spPoints.Length);
            int type = Random.Range(1, (int)Powerup.PowerupType.Max);

            var p = (Instantiate(powerupPrefab, spPoints[pos].transform.position, Quaternion.identity) as GameObject).GetComponent<Powerup>();

            p.TypeOfPowerup = (Powerup.PowerupType)type;

            yield return new WaitForSecondsRealtime(spawningTime);
        }
    }
}
