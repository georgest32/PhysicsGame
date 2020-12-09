using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPaddleAnime : MonoBehaviour
{
    //create an Animator variable for the animations to be triggered by code
    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); //Yanxi: initiate anime to the Animator\

    }

    // Update is called once per frame

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Input.GetKeyDown(KeyCode.J))
            {

                anim.Play("LeftPaddles_Both"); // Yanxi: when player long-presses 'J', triggers mermaid right-paddling animation


            }
            else
            {
                anim.Play("LeftPaddles_Right"); // Yanxi: when player long-presses 'F', triggers mermaid left-paddling animation
            }

          

        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {

                anim.Play("LeftPaddles_Both"); // Yanxi: when player long-presses 'J', triggers mermaid right-paddling animation


            }
            else
            {
                anim.Play("LeftPaddles_Left"); // Yanxi: when player long-presses 'J', triggers mermaid right-paddling animation
            }
            

        }
    }
}
