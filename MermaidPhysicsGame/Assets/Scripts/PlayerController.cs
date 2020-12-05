using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float strokeForce = 50000f;
    public float decayR = 1f;
    public float decayL = 1f;
    public float accR = 0f;
    public float accL = 0f;
    public int strokeTimeR = 0;
    public int strokeTimeL = 0;
    public Transform rightOar;
    public Transform leftOar;
    CameraLerp camLerp;

    // Start is called before the first frame update
    void Start()
    {
        camLerp = FindObjectOfType<CameraLerp>();
    }

    // Update is called once per frame
    void Update() 
    {
        if (Input.GetKey(KeyCode.F))
        {
            if (strokeTimeL > 10)
            {
                GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * strokeForce / decayL, leftOar.position);
                decayL *= 1.5f;
            }
            else
            {
                GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * strokeForce * decayL, leftOar.position);
                accL *= 5f;
            }

            strokeTimeL++;
        }
        if (Input.GetKey(KeyCode.J))
        {
            if(strokeTimeR > 10)
            {
                GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * strokeForce / decayR, rightOar.position);
                decayR *= 1.5f;
            }
            else
            {
                GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * strokeForce * decayR, rightOar.position);
                accR *= 5f;
            }

            strokeTimeR++;
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            decayL = 1f;
            strokeTimeL = 0;
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            decayR = 1f;
            strokeTimeR = 0;
        }
    }
}
