using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentController : MonoBehaviour
{
    public Vector3 currentDirection;
    public int currentSpeed;
    Transform underBoat;

    private void Start()
    {
        underBoat = GameObject.Find("Under Boat").transform;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(currentDirection.x, 0, currentDirection.z) * currentSpeed, underBoat.position);
        }
    }
}
