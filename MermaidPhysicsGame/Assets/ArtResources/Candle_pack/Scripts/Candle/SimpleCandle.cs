using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCandle : MonoBehaviour 
{

	public GameObject candle;
	public Material canceledMaterial;
	public GameObject canceledFX;
	public Light fireLight;
	public float lightDimDuration = 1.0f;
	public float lightDimValue = 0.5f;
	public float meltingSpeed;
	public float meltedHeigth;

	public bool alight;


	// Important internal variables - Please don't change them blindly
	float lightDefaultIntensity; 
	float candleDefaultHeigth;
	Material candleDefaultMaterial;
	float meltAmount; 


	//=============================================================================================================================	
	// Initialize
	void Start () 
	{
		lightDefaultIntensity = fireLight.intensity;
		candleDefaultHeigth = candle.transform.localPosition.y;
		candleDefaultMaterial = candle.GetComponent<Renderer>().material;

		//yield WaitForEndOfFrame();
		SetFire(alight);

	}

	//----------------------------------------------------------------------------------------------------------------------------
	void Update () 
	{
		if (alight) 
		{
			float amplitude  = Mathf.Cos( Time.time / lightDimDuration * Mathf.PI ) * lightDimValue + (lightDefaultIntensity-lightDimValue);
			fireLight.intensity = amplitude;

			if (meltAmount < 0.99f) 
			{
				meltAmount += Time.deltaTime * meltingSpeed;
				candle.transform.localPosition = new Vector3 (
																	candle.transform.localPosition.x, 
																	-Mathf.Lerp(candleDefaultHeigth, meltedHeigth, meltAmount),
																	candle.transform.localPosition.z
																);
			}
			else
			{
				meltAmount = 0.99f;
				SetFire (false);
			}

		}

	}

	//----------------------------------------------------------------------------------------------------------------------------
	public void SetFire (bool enable) 
	{
		if (!enable) CancelIt (); 
		else 
			FireIt();
	}

	//----------------------------------------------------------------------------------------------------------------------------
	public void CancelIt () 
	{
		fireLight.gameObject.SetActive(false);
		candle.GetComponent<Renderer>().material = canceledMaterial;
		if (canceledFX) canceledFX.SetActive(true);

		alight = false;

	}

	//----------------------------------------------------------------------------------------------------------------------------
	public void FireIt () 
	{
		if (canceledFX) canceledFX.SetActive(false);
		fireLight.gameObject.SetActive(true);
		candle.GetComponent<Renderer>().material = candleDefaultMaterial;  
		alight = true;

	} 

	//----------------------------------------------------------------------------------------------------------------------------
	public float  GetMeltAmount ()
	{
		return meltAmount;

	}

	//----------------------------------------------------------------------------------------------------------------------------
	public void SetMeltAmount (float amount)
	{
		SetFire(true);

		meltAmount = amount;

		//yield WaitForEndOfFrame();
		candle.transform.localPosition = new Vector3 (
														candle.transform.localPosition.x, 
														-Mathf.Lerp(candleDefaultHeigth, meltedHeigth, meltAmount),
														candle.transform.localPosition.z
														);
	}

		

	//----------------------------------------------------------------------------------------------------------------------------
}
