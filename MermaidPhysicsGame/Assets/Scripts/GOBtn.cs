using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GOBtn : MonoBehaviour
{
    Text text;

    private void Start()
    {
        text = GetComponentInChildren<Text>();
    }

    void OnMouseOver()
    {
        text.color = Color.red;
        Debug.Log("mouse is over btn");
    }

    void OnMouseExit()
    {
        text.color = new Color(93, 92, 92);
    }
}
