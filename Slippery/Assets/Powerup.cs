using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

	public enum PowerupType
    {
        SpeedIncrease,
        SpeedDecrease,
        SizeIncrease,
        SizeDecrease
    };

    public PowerupType TypeOfPowerup;

    void OnTriggerEnter(Collider collider)
    {
        collider.gameObject.GetComponent<CubeCharacterController>().UsePowerup(TypeOfPowerup);
        Destroy(gameObject);
    }
}
