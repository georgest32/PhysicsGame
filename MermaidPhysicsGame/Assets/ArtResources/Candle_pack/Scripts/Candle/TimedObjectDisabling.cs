using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Utility/TimedDisabling")]
public class TimedObjectDisabling : MonoBehaviour 
{
	float timeOut = 1.0f;

	void OnEnable ()
	{
		Invoke ("DisableNow", timeOut);
	}

	void DisableNow ()
	{
		gameObject.SetActive(false);
	}
}
