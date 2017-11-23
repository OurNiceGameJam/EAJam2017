using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FUBAROnTouch : MonoBehaviour {
    
    private BoxCollider m_Collider = null;

    void Start()
    {
        m_Collider = GetComponent<BoxCollider>();
    }

    void OnCollisionEnter(Collision collider)
    {
        if (collider != null && collider.gameObject != null && collider.gameObject.GetComponent<PlayerDestroyAndRespawn>() != null)
            collider.gameObject.GetComponent<PlayerDestroyAndRespawn>().DestroyAndRespawn();
    }
}
