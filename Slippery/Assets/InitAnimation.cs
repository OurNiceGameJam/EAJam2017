using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitAnimation : MonoBehaviour {

    public float speed = 1.0F;
    private float startTime;
    private float [] journeyLengths;
    public Transform targetPosition;
    public Transform body;
    public Transform[] childrenTransforms;
    // Use this for initialization
    void Start () { 

        startTime = Time.time;
        journeyLengths = new float[childrenTransforms.Length];

        for (int i = 0; i < childrenTransforms.Length; i++)
        {
            journeyLengths[i] = Vector3.Distance(targetPosition.position, childrenTransforms[i].position);
        }
       

        Debug.Log(childrenTransforms.Length);
    }
	
	// Update is called once per frame
	void Update () {

        float distCovered = (Time.time - startTime) * speed;

        for (int i = 0; i < journeyLengths.Length; i++)
        {
            float fracJourney = distCovered / journeyLengths[i];

            childrenTransforms[i].position = Vector3.Lerp(childrenTransforms[i].position, targetPosition.position, fracJourney);
        }

       

    }
}
