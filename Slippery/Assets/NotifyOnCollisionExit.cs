using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyOnCollisionExit : MonoBehaviour {

    void OnTriggerExit(Collider collider)
    {

        Debug.Log("Collision Exited");
        if (collider != null && collider.gameObject != null && collider.gameObject.GetComponent<Rigidbody>() != null)
        {
            collider.gameObject.GetComponent<CubeCharacterController>().m_inputDisabled = true;
            collider.transform.Find("Arrow").gameObject.SetActive(false);
            collider.gameObject.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
            StartCoroutine(Respawn(collider.gameObject.GetComponent<PlayerDestroyAndRespawn>()));
        }
    }

    IEnumerator Respawn(PlayerDestroyAndRespawn pdar)
    {
        yield return new WaitForSecondsRealtime(1);
        pdar.DestroyAndRespawn();

        pdar.gameObject.GetComponent<Rigidbody>().constraints |= RigidbodyConstraints.FreezePositionY;

        pdar.transform.Find("Arrow").gameObject.SetActive(true);
    }
}
