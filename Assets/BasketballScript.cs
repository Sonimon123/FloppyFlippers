using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballScript : MonoBehaviour {
	//Declare all variables at the top of the class.
	Rigidbody rb;
	Vector3 startPosition;

	int score = 0;
	bool gameOver = false;
	float gameplayTimer = 20;

	//Shooting 
	float shootForce = 0;
	float maxShootForce = 1000;
	float shootForceChargingRate = 1000;

	GameObject chargeMeterObj;
	float chargeMeterMaxWidth = 3;

	// Use this for initialization
	void Start() {
		//Store the location so we can respawn at that location after shooting the ball
		startPosition = transform.position;

		//Get the RigidBody. We're gonna use this later to turn on the gravity, 
		//	and also to check whether or not we should move back and forth.
		rb = gameObject.GetComponent<Rigidbody>();

		//Get a reference to the "ChargeMeter" GameObject, we will resize this object in the resizeChargeMeter function.
		chargeMeterObj = GameObject.Find ("ChargeMeter");
	}
	
	// Update is called once per frame
	void Update () {
		//Make the gameplayTimer smaller, if it is less than 0, the game is over!
		gameplayTimer = gameplayTimer - Time.deltaTime;
		if (gameplayTimer < 0) {
			gameOver = true;
			return;
		}

		//As long as the player is holding space, make shootForce bigger
		//	Also, make sure that the ball isn't flying (we can check to see if gravity is on to figure this out)
		if (Input.GetKey (KeyCode.Space) && !rb.useGravity) {
			float newShootForce = shootForce + shootForceChargingRate * Time.deltaTime;
			if (newShootForce > maxShootForce || newShootForce < 0) {
				//If the meter got too full, or too empty, reverse the way that the shoot force changes
				shootForceChargingRate = shootForceChargingRate * -1;
			} else {
				//Set the shootForce if it didn't get too big or small
				shootForce = newShootForce;
			}
			resizeChargeMeter ();
		}

		//Launch the ball when space is released
		if (Input.GetKeyUp(KeyCode.Space)) {
			if (rb != null) {
				//Shoot the ball toward the hoop!
				rb.useGravity = true; 
				rb.AddForce(0, shootForce, shootForce);

				//Make it so shootForceChargingRate is positive
				shootForceChargingRate = Mathf.Abs (shootForceChargingRate);

				shootForce = 0;
				resizeChargeMeter ();
			}
		}

		
	}

	void resizeChargeMeter() {
		//Update the scale of the chargeMeterObj based on shootForce
		//1. Figure out how full the shootForce is
		float shootForcePercentageOfMax = shootForce / maxShootForce;
		//2. Scale the chargeMeterMaxWidth by that percentage
		float chargeMeterWidth = shootForcePercentageOfMax * chargeMeterMaxWidth;
		//3. Create a new Vector3 with that new width and apply it to the chargeMeter
		Vector3 newScale = new Vector3(chargeMeterWidth, chargeMeterObj.transform.localScale.y, chargeMeterObj.transform.localScale.z);
		chargeMeterObj.transform.localScale = newScale;

		//For fun, set the color of the chare meter to something random
		Renderer chargeMeterRenderer = chargeMeterObj.GetComponent<Renderer>();
		//Random.value returns a random number between 0 and 1
		float randomRed = Random.value;
		float randomGreen = Random.value;
		float randomBlue = Random.value;
		Color randomColor = new Color (randomRed, randomGreen, randomBlue);
		chargeMeterRenderer.material.color = randomColor;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "hoop") {
			//If the thing that we collided with is the hoop, increment the score
			score++;
		} else if (other.gameObject.name == "RespawnTrigger") {
			//If the thing we collided with is the RespawnTrigger, move back to the spartPosition
			//	and reset the rigidbody.
			rb.useGravity = false;
			rb.velocity = Vector3.zero;
			gameObject.transform.position = startPosition;
		}
	}

	void OnGUI() {
		//Draw the score on the screen.
		Rect labelPosition = new Rect (0, 0, 400, 400);
		GUI.Label (labelPosition, "SCORE: " + score);

		if (gameplayTimer > 0) {
			Rect timerPosition = new Rect (0, 50, 400, 400);
			GUI.Label (timerPosition, "TIME: " + gameplayTimer);
		}

		if (gameOver) {
			Rect gameOverPosition = new Rect (200, 200, 400, 400);
			GUI.Label (gameOverPosition, "GAME OVER");
		}
	}
}
