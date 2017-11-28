using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    public SpawningPointManager powerupManager;

	public enum PowerupType
    {
        None,
        SizeIncrease,
        SizeDecrease,
        InstantBreak,
        //ReversedControl,
        ForcePush,
        Max
    };

    public PowerupType TypeOfPowerup;

    void OnTriggerEnter(Collider collider)
    {
        if (collider != null && collider.gameObject != null && collider.gameObject.GetComponent<CubeCharacterController>())
        collider.gameObject.GetComponent<CubeCharacterController>().SetPowerup(TypeOfPowerup);
        powerupManager.GiftsOnField--;
        Destroy(gameObject);
    }
}