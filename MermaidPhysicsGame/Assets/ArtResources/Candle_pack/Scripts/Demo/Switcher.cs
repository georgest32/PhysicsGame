using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher : MonoBehaviour {

	public SimpleCandle candle;

	// Use this for initialization
	void Start () 
		{
			if(!candle) candle = gameObject.GetComponent<SimpleCandle>();

		}

	void OnGUI() 
		{
			if (candle)
			if (candle.GetMeltAmount() <  0.99f)
			{
				if (GUI.Button(new Rect(10,10,100,50), "ON/OFF")) candle.SetFire(!candle.alight);
				GUI.Label (new Rect (10, 70, 100, 20), "Melting speed:");
				candle.meltingSpeed = GUI.HorizontalSlider (new Rect (10, 90, 100, 30), candle.meltingSpeed, 0.0f, 0.2f);
			}
			else
				if (GUI.Button(new Rect(10,10,100,50), "REPAIR")) candle.SetMeltAmount(0);

		}
}
