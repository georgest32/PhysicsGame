using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject mainCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        GameObject.FindGameObjectWithTag("Menu").SetActive(false);
        mainCam.GetComponent<Orbital>().enabled = true;
    }
}
