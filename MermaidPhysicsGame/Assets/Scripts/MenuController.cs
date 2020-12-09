using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject mainCam;
    public GameObject startCandle;
    public Transform player;
    Vector3 camStartPos;
    public Transform trans;
    public float CamMoveSpeed = 5f;
    private bool startingGame;
    private float camTime = 0;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        camStartPos = new Vector3(100f, -10.8f + 3.1f, -632 + -15.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (startingGame)
        {
            trans.LookAt(player.position);
            trans.position = Vector3.Lerp(trans.position, camStartPos, CamMoveSpeed * Time.deltaTime);

            if (Time.time - camTime > 3)
            {
                startingGame = false;
                mainCam.GetComponent<Orbital>().enabled = true;
                player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                startCandle.GetComponent<Light>().enabled = true;
                playerController.gameHasStarted = true;
            }
        }
    }

    public void Play()
    {
        GameObject.FindGameObjectWithTag("Menu").SetActive(false);
        camTime = Time.time;
        startingGame = true;
    }
}
