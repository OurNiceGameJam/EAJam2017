using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class CubeCharacterController : MonoBehaviour
{
    public Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    Rigidbody m_Rigidbody;

    public Transform m_arrow;
    public float MinimumVelocity = 1f;
    public float ThresholdImpulse = 1f;

    #region Powerups 
    float m_SpeedBoostFactor = 1f;
    public Vector3 m_originalScale;
    public float m_originalMass;
    public float leftTriggerThreshold = 0.5f;

    public Text PowerupName;
    #endregion

    public float BoostCooldown;
    bool canUseBoost = true;

    Powerup.PowerupType m_CurrentPowerup = Powerup.PowerupType.None;

    public int PlayerNumber = 1;
    
    // Use this for initialization
    void Start()
    {
        SetPowerup(Powerup.PowerupType.None);
        m_Rigidbody = GetComponent<Rigidbody>();
        m_originalScale = transform.localScale;
        m_originalMass = m_Rigidbody.mass;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // read inputs
        float h = Input.GetAxis("Horizontal" + PlayerNumber.ToString());
        float v = Input.GetAxis("Vertical" + PlayerNumber.ToString());

        float rt = Input.GetAxis("Boost" + PlayerNumber.ToString());
        float aButton = Input.GetAxis("Powerup" + PlayerNumber.ToString());
        
        Vector3 u = v * Vector3.forward;
        Vector3 l = h * Vector3.right;

        // Debug.Log(h);
        // calculate move direction to pass to character
        if (m_Cam != null)
        {
            // calculate camera relative direction to move:
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = v * m_CamForward + h * m_Cam.right;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            m_Move = v * Vector3.forward + h * Vector3.right;
        }
        
        m_Rigidbody.AddForce(m_Move * 15 + rt * transform.forward * 300, ForceMode.Acceleration);
        
        Vector3 pos = transform.position;
        pos.y = 0;
        transform.position = pos;

        transform.LookAt(transform.position + (u + l) * 10);

        if (aButton >= leftTriggerThreshold)
            UsePowerup(m_CurrentPowerup);
    }

    public void SetPowerup(Powerup.PowerupType powerup)
    {
        m_CurrentPowerup = powerup;

        switch (m_CurrentPowerup)
        {
            case Powerup.PowerupType.None:
                PowerupName.text = "No Powerup";
                break;
            case Powerup.PowerupType.SizeDecrease:
                PowerupName.text = "Size Decrease";
                break;
            case Powerup.PowerupType.SizeIncrease:
                PowerupName.text = "Size Increase";
                break;
            case Powerup.PowerupType.SpeedDecrease:
                PowerupName.text = "Speed Decrease";
                break;
            case Powerup.PowerupType.SpeedIncrease:
                PowerupName.text = "Speed Increase";
                break;
        }

    }

    public void UsePowerup(Powerup.PowerupType typeOfPowerup)
    {
        //We should use coroutines in order to remove the efect after a while 
        switch (typeOfPowerup)
        {
            case Powerup.PowerupType.None:
                break;
            case Powerup.PowerupType.SizeDecrease:
                StartCoroutine(HalfSize());
                break;
            case Powerup.PowerupType.SizeIncrease:
                StartCoroutine(DoubleSize());
                break;
            case Powerup.PowerupType.SpeedDecrease:
                StartCoroutine(HalfSpeed());
                break;
            case Powerup.PowerupType.SpeedIncrease:
                StartCoroutine(DoubleSpeed());
                break;
            default:
                Debug.LogError("PowerType: " + typeOfPowerup + " not handled");
                break;
        }

        SetPowerup(Powerup.PowerupType.None);
    }

    public void ResetPowerupEffects()
    {
        StopAllCoroutines();

        m_SpeedBoostFactor = 1f;
        transform.localScale = m_originalScale;
        m_Rigidbody.mass = m_originalMass;
    }

    IEnumerator DoubleSize()
    {
        transform.localScale *= 2;
        m_Rigidbody.mass *= 4;
        yield return new WaitForSecondsRealtime(5);
        m_Rigidbody.mass /= 4;
        transform.localScale /= 2;
    }
 
    IEnumerator HalfSize()
    {
        transform.localScale /= 2;
        m_Rigidbody.mass /= 2;
        yield return new WaitForSecondsRealtime(5);
        m_Rigidbody.mass *= 2;
        transform.localScale *= 2;
    }

    IEnumerator DoubleSpeed()
    {
        m_SpeedBoostFactor = 2f;
        yield return new WaitForSecondsRealtime(5);
        m_SpeedBoostFactor = 1f;
    }

    IEnumerator HalfSpeed()
    {
        m_SpeedBoostFactor = 0.5f;
        yield return new WaitForSecondsRealtime(5);
        m_SpeedBoostFactor = 1f;
    }
}