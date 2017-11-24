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
    bool m_ReversedControl = false;
    float m_SpeedBoostFactor = 1f;
    public Vector3 m_originalScale;
    public float m_originalMass;
    public float leftTriggerThreshold = 0.5f;
    public float forcePushPower = 20f;

    public Text PowerupName;
    #endregion

    public GameObject otherPlayer;

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

        if (m_ReversedControl)
        {
            h *= -1f;
            v *= -1f;
        }

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
        
        m_Rigidbody.AddForce(m_Move * 15 + rt * transform.forward * 300 * m_SpeedBoostFactor, ForceMode.Acceleration);
        
        Vector3 pos = transform.position;
        pos.y = 0;
        transform.position = pos;

        transform.LookAt(transform.position + (u + l) * 10);

        if (aButton >= leftTriggerThreshold)
            UsePowerup(Powerup.PowerupType.ForcePush);
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
            case Powerup.PowerupType.ForcePush:
                PowerupName.text = "Force Push";
                break;
            case Powerup.PowerupType.InstantBreak:
                PowerupName.text = "Instant Break";
                break;
            case Powerup.PowerupType.ReversedControl:
                UsePowerup(Powerup.PowerupType.ReversedControl);
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
            case Powerup.PowerupType.InstantBreak:
                InstantBreak();
                break;
            case Powerup.PowerupType.ReversedControl:
                StartCoroutine(ReversedControl());
                break;
            case Powerup.PowerupType.ForcePush:
                ForcePush();
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
        m_ReversedControl = false;
    }

    void ForcePush()
    {
        Vector3 oppositeDirectionVector = otherPlayer.transform.position - transform.position;
        otherPlayer.GetComponent<Rigidbody>().AddForce(oppositeDirectionVector.normalized * forcePushPower, ForceMode.Impulse);
    }

    void InstantBreak()
    {
        m_Rigidbody.velocity = Vector3.zero;
    }

    IEnumerator ReversedControl()
    {
        m_ReversedControl = true;
        yield return new WaitForSecondsRealtime(5);
        m_ReversedControl = false;
    }

    IEnumerator DoubleSize()
    {
        transform.localScale *= 2;
        m_Rigidbody.mass *= 4;
        m_SpeedBoostFactor *= 0.8f;
        yield return new WaitForSecondsRealtime(5);
        m_Rigidbody.mass /= 4;
        transform.localScale /= 2;
        m_SpeedBoostFactor /= 0.8f;
    }
 
    IEnumerator HalfSize()
    {
        transform.localScale /= 2;
        m_Rigidbody.mass /= 2;
        m_SpeedBoostFactor *= 1.2f;
        yield return new WaitForSecondsRealtime(5);
        m_Rigidbody.mass *= 2;
        transform.localScale *= 2;
        m_SpeedBoostFactor /= 1.2f;
    }
}