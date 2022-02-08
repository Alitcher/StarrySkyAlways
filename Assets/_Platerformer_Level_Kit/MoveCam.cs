using UnityEngine;
using System.Collections;

public class MoveCam : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float xAxisValue = Input.GetAxis("Horizontal");
		float yAxisValue = Input.GetAxis("Vertical");
		
		Camera.main.transform.Translate(new Vector3(xAxisValue/3, yAxisValue/3, 0.0f));
	}
}
