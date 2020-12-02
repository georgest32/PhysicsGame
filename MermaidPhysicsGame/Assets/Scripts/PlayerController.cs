using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.left * 20000);
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 5000);
            Debug.Log("paddle left");
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.right * 20000);
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 5000);
            Debug.Log("paddle right");
        }
    }
}
