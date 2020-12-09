using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    public GameObject menu;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //menu.SetActive(true);
            SceneManager.LoadScene("Level1");
        }
    }
}
