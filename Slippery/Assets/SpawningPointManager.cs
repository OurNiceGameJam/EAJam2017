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
            int pos = Random.Range(0, 10);
            int type = Random.Range(0, 4);

            var p = (Instantiate(powerupPrefab, spPoints[pos].transform.position, Quaternion.identity) as GameObject).GetComponent<Powerup>();

            switch (type)
            {
                case 0:
                    p.TypeOfPowerup = Powerup.PowerupType.SizeDecrease;
                    break;
                case 1:
                    p.TypeOfPowerup = Powerup.PowerupType.SizeIncrease;
                    break;
                case 2:
                    p.TypeOfPowerup = Powerup.PowerupType.SpeedDecrease;
                    break;
                case 3:
                    p.TypeOfPowerup = Powerup.PowerupType.SpeedIncrease;
                    break;
                default:
                    Debug.LogError("FUCKED UP!");
                    break;
            }

            yield return new WaitForSecondsRealtime(spawningTime);
        }
    }
}
