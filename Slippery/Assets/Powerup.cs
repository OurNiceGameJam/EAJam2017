using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

	public enum PowerupType
    {
        None,
        SizeIncrease,
        SizeDecrease,
        InstantBreak,
        ReversedControl,
        ForcePush,
        Max
    };

    public PowerupType TypeOfPowerup;

    void OnTriggerEnter(Collider collider)
    {
        
        collider.gameObject.GetComponent<CubeCharacterController>().SetPowerup(TypeOfPowerup);
        Destroy(gameObject);
    }
}
