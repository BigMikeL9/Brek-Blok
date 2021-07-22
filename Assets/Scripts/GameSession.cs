using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int blockValue = 80;
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool autoPlay; /* We're making this a "[serialized Field]" because we don't want to be able to change its value in any other script. Just in the unity interface. 
                                       But we'd have to create a new public method now to tell the "paddle script" whether autoPlay is enabled or not; */


    private void Awake() // This whole things is called a "SINGLETON PATTERN", which means that there can be only one, if anything else (a copy of the same gameobject in other scenes) comes along, DESTROY IT. ONLY KEEP AND USE THE ORIGINAL ONE.
                         // "SINGLETON PATTERN" also applies to children that sits underneath the main "SINGLETON" gameobject. It keeps them protected and doesn't destroy them when we load to another scene.
                         // This method is called before the "Start()" method.
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length; // Gets the number of "GameStatus" classes/gameobjects there are in all the scenes.
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false); // This line of code is to FIX A BUG (REMEMBER TO ADD IT WHENEVER WE USE A "SINGLETON PATTERN"). The "Destroy()" method is at the very bottom of the execution order, so there is a fraction of a second where the "Destroy()" method doesn't get called forst (before all other methods) EVEN THO it is in the "AWAKE()" method, which causes all kinds of problems. SO inorder to fix this bug we set the "GameStatus" gameObject to "INACTIVE" AND we also "DESTROY" it. Just to make sure it doesn't cause any problems before it is destroyed in that fraction of a second. Thank you very much.
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {

        // score = 0;
        scoreText.text = score.ToString(); // Converts the "score" which is an int to a string, and store it in "scoreText. My way is way easier.
        scoreText.text = "Score: " + score;
    } 
    

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }


    public void UpdateScore()
    {
        score += blockValue;
        // scoreText.text = "Score: " + score; // My way has a bug so didn't use it.
        scoreText.text = score.ToString(); // Look at top comment for explanation.
        scoreText.text = "Score: " + score;
    }

    public void ResetGame()
    {
        Destroy(gameObject); // "gameObject" with a small "g", means that we are talking about this particular instance of gameObject, and NOT the CLASS gameObject which would be a capital "G" or "GameObject".
    }                        // I think this line of code destroys the gameStatus script (when called), and then the gameStatus script resets or comes back (A NEW ONE) (A NEW "DONT DESTROY ON LOAD COMES BACK, PROTECTING OUR GAMESTATUS) when the game starts again.


    public bool isAutoPlayEnabled() // This method is just to tell the paddle script whether "autoPlay" is on or not. We created this because "autoPlay" is not public (inorder to prevent its value fro mbeing changed in other scripts)
    {
        return autoPlay;
    }
}

        