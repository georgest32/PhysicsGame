using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AboutTrigger : MonoBehaviour
{
    public GameObject about;
  public void triggerAbout()
    {
        about.SetActive(true);
    }
}
