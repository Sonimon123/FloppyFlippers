using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    Rigidbody rb;
    Vector3 startPosition;
    int score = 0;
    bool gameOver = false;
    float gameplayTimer = 60;

    // Use this for initialization
    void Start () {
        // Declare Ball Starting Position and Rigid Body
        rb = gameObject.GetComponent<Rigidbody>();
        startPosition = transform.position;

    }

    // Update is called once per frame
    void Update() {

        // Gameplay Timer
        gameplayTimer = gameplayTimer - Time.deltaTime;
        if (gameplayTimer < 0)
        {
            gameOver = true;
            return;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Bell")
        {
            //If the thing that we collided with is the hoop, increment the score
            if (gameOver == false)
            {
                score++;
            }
        }
        else if (other.gameObject.name == "Floor")
        {
            //If the thing we collided with is the Floor, move back to the startPosition
            gameObject.transform.position = startPosition;
            rb.velocity = Vector3.zero;
        }
    }

    void OnGUI()
    {
        //Draw the score on the screen.
        Rect labelPosition = new Rect(0, 0, 400, 400);
        GUI.Label(labelPosition, "Score: " + score);

        if (gameplayTimer > 0)
        {
            Rect timerPosition = new Rect(0, 50, 400, 400);
            GUI.Label(timerPosition, "Time: " + gameplayTimer);
        }

        if (gameOver)
        {
            Rect gameOverPosition = new Rect(200, 200, 400, 400);
            if (score >= 2)
            {
                GUI.Label(gameOverPosition, "Game Over, You Lose.      You got " + score + " points.       To begin a new game, press N");
            }

            if (score < 2)
            {
                GUI.Label(gameOverPosition, "You Win!      You got " + score + " points.       To begin a new game, press N");
            }

            if (Input.GetKey(KeyCode.N))
            {
                gameOver = false;
                gameplayTimer = 60;
                score = 0;
                gameObject.transform.position = startPosition;
                rb.velocity = Vector3.zero;
            }
        }
    }

}
