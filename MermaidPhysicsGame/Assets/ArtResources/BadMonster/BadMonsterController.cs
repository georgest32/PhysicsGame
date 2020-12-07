using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadMonsterController : MonoBehaviour
{
    public ParticleSystem redMonster1;
    public ParticleSystem redMonster2;
    public ParticleSystem blackMonster1;
    public ParticleSystem blackMonster2;
    public ParticleSystem blackMonster3;
    public ParticleSystem blackMonster4;
    public ParticleSystem blackMonster5;
    public ParticleSystem blackMonster6;
    public ParticleSystem blackMonster7;
    public ParticleSystem blackMonster8;
    public ParticleSystem blackMonster9;
    public ParticleSystem blackMonster10;

    public GameObject redM1;
    public GameObject redM2;
    public GameObject blaM1;
    public GameObject blaM2;
    public GameObject blaM3;
    public GameObject blaM4;
    public GameObject blaM5;
    public GameObject blaM6;
    public GameObject blaM7;
    public GameObject blaM8;
    public GameObject blaM9;
    public GameObject blaM10;

    //scale up & down particle object
    public float scaleUpSpeed    = 0.15f;
    public float scaleDownSpeed  = 0.8f;

    public bool isInLightArea = true;

    //Audio
    public AudioSource BadMonsterAppear;
    public AudioSource BadMonsterHide;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (isInLightArea) 
        //{
        //    redMonster1.Stop();
        //    blackMonster1.Stop();
        //    blackMonster2.Stop();
        //}

        //enter the darkness, monster particle begin to scale up, until fill the screen. largest scale = 2
        if (!isInLightArea && redM1.transform.localScale.x <= 2)
        {
            redM1.transform.localScale += new Vector3(scaleUpSpeed * Time.deltaTime, scaleUpSpeed * Time.deltaTime, 0);
            redM2.transform.localScale += new Vector3(scaleUpSpeed * Time.deltaTime, scaleUpSpeed * Time.deltaTime, 0);
            blaM1.transform.localScale += new Vector3(scaleUpSpeed * Time.deltaTime, scaleUpSpeed * Time.deltaTime, 0);
            blaM2.transform.localScale += new Vector3(scaleUpSpeed * Time.deltaTime, scaleUpSpeed * Time.deltaTime, 0);
            blaM3.transform.localScale += new Vector3(scaleUpSpeed * Time.deltaTime, scaleUpSpeed * Time.deltaTime, 0);
            blaM4.transform.localScale += new Vector3(scaleUpSpeed * Time.deltaTime, scaleUpSpeed * Time.deltaTime, 0);
            blaM5.transform.localScale += new Vector3(scaleUpSpeed * Time.deltaTime, scaleUpSpeed * Time.deltaTime, 0);
            blaM6.transform.localScale += new Vector3(scaleUpSpeed * Time.deltaTime, scaleUpSpeed * Time.deltaTime, 0);
            blaM7.transform.localScale += new Vector3(scaleUpSpeed * Time.deltaTime, scaleUpSpeed * Time.deltaTime, 0);
            blaM8.transform.localScale += new Vector3(scaleUpSpeed * Time.deltaTime, scaleUpSpeed * Time.deltaTime, 0);
            blaM9.transform.localScale += new Vector3(scaleUpSpeed * Time.deltaTime, scaleUpSpeed * Time.deltaTime, 0);
            blaM10.transform.localScale += new Vector3(scaleUpSpeed * Time.deltaTime, scaleUpSpeed * Time.deltaTime, 0);
        }
        //enter the light, monster particle begin to scale down, smallest scale = 0.6
        if (isInLightArea && redM1.transform.localScale.x >= 0.6)
        {
            redM1.transform.localScale -= new Vector3(scaleDownSpeed * Time.deltaTime, scaleDownSpeed * Time.deltaTime, 0);
            redM2.transform.localScale -= new Vector3(scaleDownSpeed * Time.deltaTime, scaleDownSpeed * Time.deltaTime, 0);
            blaM1.transform.localScale -= new Vector3(scaleDownSpeed * Time.deltaTime, scaleDownSpeed * Time.deltaTime, 0);
            blaM2.transform.localScale -= new Vector3(scaleDownSpeed * Time.deltaTime, scaleDownSpeed * Time.deltaTime, 0);
            blaM3.transform.localScale -= new Vector3(scaleDownSpeed * Time.deltaTime, scaleDownSpeed * Time.deltaTime, 0);
            blaM4.transform.localScale -= new Vector3(scaleDownSpeed * Time.deltaTime, scaleDownSpeed * Time.deltaTime, 0);
            blaM5.transform.localScale -= new Vector3(scaleDownSpeed * Time.deltaTime, scaleDownSpeed * Time.deltaTime, 0);
            blaM6.transform.localScale -= new Vector3(scaleDownSpeed * Time.deltaTime, scaleDownSpeed * Time.deltaTime, 0);
            blaM7.transform.localScale -= new Vector3(scaleDownSpeed * Time.deltaTime, scaleDownSpeed * Time.deltaTime, 0);
            blaM8.transform.localScale -= new Vector3(scaleDownSpeed * Time.deltaTime, scaleDownSpeed * Time.deltaTime, 0);
            blaM9.transform.localScale -= new Vector3(scaleDownSpeed * Time.deltaTime, scaleDownSpeed * Time.deltaTime, 0);
            blaM10.transform.localScale -= new Vector3(scaleDownSpeed * Time.deltaTime, scaleDownSpeed * Time.deltaTime, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        isInLightArea = true;

        BadMonsterHide.Play();

        redMonster1.Stop();
        redMonster2.Stop();
        blackMonster1.Stop();
        blackMonster2.Stop();
        blackMonster3.Stop();
        blackMonster4.Stop();
        blackMonster5.Stop();
        blackMonster6.Stop();
        blackMonster7.Stop();
        blackMonster8.Stop();
        blackMonster9.Stop();
        blackMonster10.Stop();
        
        //if (other.tag.Equals("LightArea"))
        //{

        //}

        //if (!isInLightArea)
        //{
        //    isInLightArea = true;
        //}

        //redM1.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        //blaM1.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        //blaM2.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        //blaM3.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        //blaM4.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
    }

    void OnTriggerStay(Collider other)
    {
        isInLightArea = true;

        redMonster1.Stop();
        redMonster2.Stop();
        blackMonster1.Stop();
        blackMonster2.Stop();
        blackMonster3.Stop();
        blackMonster4.Stop();
        blackMonster5.Stop();
        blackMonster6.Stop();
        blackMonster7.Stop();
        blackMonster8.Stop();
        blackMonster9.Stop();
        blackMonster10.Stop();
        
    }

    void OnTriggerExit(Collider other)
    {
        isInLightArea = false;

        BadMonsterAppear.Play();

        redMonster1.Play();
        redMonster2.Play();
        blackMonster1.Play();
        blackMonster2.Play();
        blackMonster3.Play();
        blackMonster4.Play();
        blackMonster5.Play();
        blackMonster6.Play();
        blackMonster7.Play();
        blackMonster8.Play();
        blackMonster9.Play();
        blackMonster10.Play();
        
        

        //if (other.tag != "LightArea") 
        //{ 
        //    redM1.transform.localScale += new Vector3(.1f, .1f, .1f); 
        //}

    }
}
