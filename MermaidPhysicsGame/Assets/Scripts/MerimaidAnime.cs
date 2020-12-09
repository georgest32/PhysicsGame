using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerimaidAnime : MonoBehaviour
{
    //create an Animator variable for the animations to be triggered by code
    public Animator animLeft;
    public Animator animRight;
    public Animator animBoth;

    // Start is called before the first frame update
    void Start()
    {
        animLeft = GetComponent<Animator>(); //Yanxi: initiate anime to the Animator\
        animRight = GetComponent<Animator>();
        animBoth = GetComponent<Animator>();
    }

    // Update is called once per frame

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Input.GetKeyDown(KeyCode.J))
            {

                animBoth.Play("Mermaid_Both"); // Yanxi: when player long-presses 'J', triggers mermaid right-paddling animation
                


            }
            else
            {
                animLeft.Play("Mermaid_Right"); // Yanxi: when player long-presses 'F', triggers mermaid left-paddling animation
                
            }


        }

        if (Input.GetKeyDown(KeyCode.J))
        {

            if (Input.GetKeyDown(KeyCode.F))
            {

                animBoth.Play("Mermaid_Both"); // Yanxi: when player long-presses 'J', triggers mermaid right-paddling animation
                


            }
            else
            {
                animRight.Play("Mermaid_Left"); // Yanxi: when player long-presses 'F', triggers mermaid left-paddling animation
                
            }

            



        }
    }
    
}

