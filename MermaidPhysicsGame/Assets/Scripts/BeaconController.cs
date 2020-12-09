using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconController : MonoBehaviour
{
    Light pointLight;
    public Color color;
    public float intensity;
    public float range;
    public PlayerController player;
    public Transform candleContainer;
    public GameObject newCandlePrefab;

    // Start is called before the first frame update
    void Start()
    {
        pointLight = transform.GetComponentInChildren<Light>();
        pointLight.color = color;
        pointLight.intensity = intensity;
        pointLight.range = range;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player.candles++;

            GameObject newCandle = Instantiate(newCandlePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            Transform candleSpot = candleContainer.GetChild(player.candles);

            newCandle.transform.SetParent(candleSpot);
            newCandle.transform.localPosition = new Vector3(0, 0.35f, 0);
            newCandle.GetComponentInChildren<Light>().enabled = true;

            Destroy(this.gameObject);
        }
    }
}
