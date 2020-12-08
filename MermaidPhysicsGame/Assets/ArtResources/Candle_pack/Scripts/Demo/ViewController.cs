using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour 
{
	
	public Transform lookAt;

	public float horizontalSpeed = 2.0f;
	public float verticalSpeed = 2.0f;
	public float scaleSpeed = 2.0f;

	public MeshRenderer[] renders;
	public Material[] materials;

	Vector3 initialScale;
	float s = -0.25f;



	void Start () 
	{
		initialScale = transform.localScale;
	}

	void Update ()
	{
		if (Input.GetMouseButton(0))
		{
			float h = horizontalSpeed * Input.GetAxis ("Mouse X");
			float v = verticalSpeed * Input.GetAxis ("Mouse Y");
			transform.Rotate (v, 0, -h);
		}

		s += scaleSpeed * Input.GetAxis ("Mouse ScrollWheel");
		s = Mathf.Clamp(s, -0.5f, 1.0f);
		transform.localScale = initialScale  + initialScale*s;

		Camera.main.transform.position = new Vector3(
														Camera.main.transform.position.x,
														Mathf.Lerp(Camera.main.transform.position.y, lookAt.position.y, 100*Time.deltaTime),
														Camera.main.transform.position.z
													);
		Camera.main.transform.LookAt(lookAt);

	}

	void OnGUI() 
	{
		GUI.Label (new Rect (10 ,Screen.height-75,100,50), "Choose skin:");

		for (int i = 0; i < materials.Length; i++)
			if (GUI.Button(new Rect(10 +i*105 ,Screen.height-55,100,50), materials[i].name )) 
				for (int j = 0; j < renders.Length; j++)  renders[j].material = materials[i];
	}
}
