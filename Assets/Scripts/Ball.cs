using UnityEngine;

/* We dont need the generic namespaces in visual studio , thats why we deleted them. We never actually use them in unity. (It was causing an error with "Random.Range" 
because two namespaces uses the same class and we had to speciify which namespace was intended to be used, like "UnityEngine.Random.Range()". */

public class Ball : MonoBehaviour
{
    // Configuration Paramaters
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 20f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomVelocityFactor = 0.5f;

    // State
    private Vector2 ballToPaddlePos;
    private bool gameStarted = false;

    // Cashed component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;


    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>(); // Put in the start instead of having to write it down every time I need to reference it.
        myRigidBody2D = GetComponent<Rigidbody2D>();
        ballToPaddlePos = transform.position - paddle1.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted)
        {
            LockBalltoPaddle();
            LaunchBall();
        }
        
    }


    private void LockBalltoPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + ballToPaddlePos;
    }

    private void LaunchBall()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameStarted = true;
            myRigidBody2D.velocity = new Vector2(xPush, yPush);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float velocityTweakXY = Random.Range(0, randomVelocityFactor);
        Vector2 velocityTweak = new Vector2(velocityTweakXY, velocityTweakXY);
        if (gameStarted)
        {
            AudioClip clipIndex = ballSounds[Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clipIndex);
            myRigidBody2D.velocity += velocityTweak; /* This line of code just adds a tweak (a small push) to the velocity in the X and Y directions, to prevent 
                                                        boring ball loops as in the ball just going back and forth when cillin=ding with the side colliders. */
        }

    }
}
