using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

	public enum PowerupType
    {
        None,
        SpeedIncrease,
        SpeedDecrease,
        SizeIncrease,
        SizeDecrease,
        Max
    };

    public PowerupType TypeOfPowerup;

    void OnTriggerEnter(Collider collider)
    {
        collider.gameObject.GetComponent<CubeCharacterController>().SetPowerup(TypeOfPowerup);
        Destroy(gameObject);
    }
}
