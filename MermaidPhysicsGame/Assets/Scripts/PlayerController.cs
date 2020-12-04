using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rowForce = 150000f;
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
        if (Input.GetKeyDown(KeyCode.F))
        {
            //GetComponent<Rigidbody>().AddRelativeTorque(-transform.right * 150000);
            //GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 50000);
            GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * 150000f, rightOar.position);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            //GetComponent<Rigidbody>().AddRelativeTorque(transform.right * 150000);
            //GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 50000);
            GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * 150000f, leftOar.position);
        }
    }
}
