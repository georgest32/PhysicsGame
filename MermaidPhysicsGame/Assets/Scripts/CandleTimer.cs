using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleTimer : MonoBehaviour
{
    public float timeLimit;
    float timeElapsed = 0;
    public PlayerController player;
    public GameObject candles;
    private bool anyCandlesLit;

    void OnEnable()
    {
        player = FindObjectOfType<PlayerController>();
        candles = GameObject.FindGameObjectWithTag("CandleContainer");
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if(timeElapsed > timeLimit)
        {
            GetComponent<Light>().enabled = false;
            gameObject.SetActive(false);

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

            if (player.gameHasStarted && !anyCandlesLit)
            {
                player.inlight = false;
            }
        }
    }
}
