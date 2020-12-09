using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHCollider : MonoBehaviour
{
    public PlayerController playerController;
    private bool anyCandlesLit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerController.inlight = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            playerController.inlight = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject[] flames = GameObject.FindGameObjectsWithTag("CandleFlame");

            for (int i = 0; i < flames.Length; i++)
            {
                if (flames[i].GetComponent<Light>().enabled)
                {
                    anyCandlesLit = true;
                    return;
                }
                else
                {
                    anyCandlesLit = false;
                }
            }

            if (!anyCandlesLit)
            {
                playerController.inlight = false;
            }
        }
    }
}
