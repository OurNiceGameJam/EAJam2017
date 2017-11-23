using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestroyAndRespawn : MonoBehaviour {

    public GameManager m_GameManager;

    void Start()
    {
        if (m_GameManager == null)
        {
            Debug.LogError("GameManager is null!");
        }
    }

    public void DestroyAndRespawn()
    {
        //Notify the game manager that this cube fucked up
        //Debug.Log("Woops: " + gameObject);
        m_GameManager.SetNewRound(GetComponent<CubeCharacterController>().PlayerNumber);
    }
}
