using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("01_Story");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("02_Story");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("03_Story");
    }

    public void LoadLevel4()
    {
        SceneManager.LoadScene("Level1");
    }

}
