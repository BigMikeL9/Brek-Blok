using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //Configuration Paramters:
    [SerializeField] float screenWidth = 16f;
    [SerializeField] private float xRangeMin = 1f;
    [SerializeField]  private float xRangeMax = 15f;

    //Cached References
    GameSession gameSession;
    Ball ball;


    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Input.mousePosition.x / Screen.width * screenWidth); // Prints the mouse position on the screen.

        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y); // Creates a new Vector2 of the mouse position on the x axis and the paddle's y position.
        paddlePos.x = Mathf.Clamp(GetXPos(), xRangeMin, xRangeMax); // Creates boundaries to where the paddle can move
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if (gameSession.isAutoPlayEnabled())
        {
            float ballPosInUnits = ball.transform.position.x;
            return ballPosInUnits;
        }
        else
        {
            float mousePosInUnits = (Input.mousePosition.x / Screen.width * screenWidth); // Gets the position of the mouse on the screen, and puts it in a variable.
            return mousePosInUnits;
        }
    }
}
