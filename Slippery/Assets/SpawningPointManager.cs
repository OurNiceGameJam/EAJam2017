using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPointManager : MonoBehaviour {

    public GameObject[] spPoints;
    public GameObject[] powerupPrefabs;

    public float spawningTime;

    public int MaxGiftsOnfield = 2;

    [System.NonSerialized]
    public int GiftsOnField = 0;

	// Update is called once per frame
	void Start () {
        StartCoroutine(SpawnPowerups());
	}

    IEnumerator SpawnPowerups()
    {
        while (true)
        {
            if (GiftsOnField < MaxGiftsOnfield)
            {
                int pos = Random.Range(0, spPoints.Length);
                int type = Random.Range(1, (int)Powerup.PowerupType.Max);
                int prefab = Random.Range(0, powerupPrefabs.Length);

                GiftsOnField++;

                var p = (Instantiate(powerupPrefabs[prefab], spPoints[pos].transform.position, Quaternion.identity) as GameObject).GetComponent<Powerup>();

                p.powerupManager = this;
                p.TypeOfPowerup = (Powerup.PowerupType)type;
            }
            yield return new WaitForSecondsRealtime(spawningTime);
        }
    }
}
