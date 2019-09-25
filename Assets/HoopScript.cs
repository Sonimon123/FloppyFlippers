using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopScript : MonoBehaviour {

	float theta = 0;
	float moveSpeed = 1.5f;
	float amplitude = 10f;
	float initialX;

	// Use this for initialization
	void Start () {
		//Store the initial x position, so we can offset based on timing
		initialX = gameObject.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		//Move the hoop left and right
		theta = theta + moveSpeed * Time.deltaTime;
		float offset = Mathf.Sin (theta) * amplitude;
		Vector3 newPosition = new Vector3 (initialX + offset, gameObject.transform.position.y, gameObject.transform.position.z);
		gameObject.transform.position = newPosition;
	}
}
