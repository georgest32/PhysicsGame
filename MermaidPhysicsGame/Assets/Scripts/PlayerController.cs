using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float turnTorque = 150000f;
    public float thrust = 50000f;
    public Transform rightOar;
    public Transform leftOar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //GetComponent<Rigidbody>().AddRelativeTorque(-transform.right * 150000);
            //GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 50000);
            GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * 150000, rightOar.position);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            //GetComponent<Rigidbody>().AddRelativeTorque(transform.right * 150000);
            //GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 50000);
            GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * 150000, leftOar.position);
        }
    }
}
