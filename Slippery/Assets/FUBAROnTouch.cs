using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FUBAROnTouch : MonoBehaviour {
    
    private BoxCollider m_Collider = null;

    void Start()
    {
        m_Collider = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider collider)
    {

        collider.gameObject.GetComponent<PlayerDestroyAndRespawn>().DestroyAndRespawn();
    }
}
