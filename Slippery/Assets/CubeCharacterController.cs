using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CubeCharacterController : MonoBehaviour
{
    private Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    Rigidbody m_Rigidbody;

    public float MinimumVelocity = 1f;
    public float ThresholdImpulse = 1f;

    Vector3 m_lastDirection = Vector3.zero;

    // Use this for initialization
    void Start ()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // read inputs
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");

        // calculate move direction to pass to character
        if (m_Cam != null)
        {
            // calculate camera relative direction to move:
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = v * m_CamForward + h * m_Cam.right;

            if (m_Move.magnitude > Mathf.Epsilon)
                m_lastDirection = m_Move.normalized;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            m_Move = v * Vector3.forward + h * Vector3.right;

            if (m_Move.magnitude > Mathf.Epsilon)
                m_lastDirection = m_Move.normalized;
        }

        if (m_Rigidbody.velocity.magnitude < MinimumVelocity)
        {
            Debug.Log("MINIMUM ALERT");
            m_Rigidbody.AddForce(m_lastDirection * ThresholdImpulse, ForceMode.VelocityChange);
        }
        else
        {
            m_Rigidbody.AddForce(m_Move * 30, ForceMode.Acceleration);
        }

        Debug.Log("m_Move " + m_Move);
        Debug.Log("m_lastDirection " + m_lastDirection);
        Debug.Log("m_Rigidbody.velocity.magnitude " + m_Rigidbody.velocity.magnitude);
    }
}
