using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{

    // Config parameters
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparkleVFX;
    [SerializeField] Sprite[] hitSprite;

    // Cached references
    Level levelScript;
    GameSession gameStatusScript;

    // State variables
    [SerializeField] int timesHit; // The amount of times the block was hit by the ball. ONLY serialized for debug purposes if there is any in the future.



    private void Start()
    {
        levelScript = FindObjectOfType<Level>();
        gameStatusScript = FindObjectOfType<GameSession>();

        CountBreakableBlocks();

    }
    
   
    private void CountBreakableBlocks()
    {
        if (tag == "Breakable")
        {
            // levelScript = GetComponent<Level>(); // CANNOT USE THIS BECAUSE WE WANT TO COUNT THE NUMBER OF BLOCKS (USING FindObjectOfType<...>()), AND NOT JUST REFERENCE THE LEVEL GAMEOBJECT.
            levelScript.CountBlocks();
        }
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable") // If there is one equal sign "=", it means that we are ASSIGNING tag to BE EQUAL TO "Breakable"; whereas if there is TWO eual signs "==" then it means that we are saying that if tag IS equal to "breakable" then execute the code in between the curly brackets.
        {
            HandleHits();
        }
    }


    private void HandleHits() // Take cares of when the "BREAKABLE" block is hit.
    {
        timesHit++; // Adds one whenever the block is hit(collided) by the ball.
        int maxHits = hitSprite.Length + 1; // Means that "maxHits" equals the number of our "hitSprite" array plus 1.
        if (timesHit >= maxHits)
        {
            BlockDestroy();
        }
        else
        {
            ShowNextHitSprite();
        }
    }


    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1; // "spriteIndex" has to be equal to 0 inorder to show/display the first sprite in the "hitSprite" array; which is why we are subtracting "timesHit" by 1 because everytime the ball hits the block, it adds 1 to "timesHit" (so it basically starts with 1).
        if (hitSprite[spriteIndex] != null) // this line of code (along with "else) says that if the "spriteIndex" is not null (in other words, of it VALID) then continue on with the code in between the "if" statement; otherwise print an error message to the console, just to avoid a bug that randomly popups up.
        {                                   // We could be fine without this line code (the if/else) but it is just to prevent future bigger bugs.
            GetComponent<SpriteRenderer>().sprite = hitSprite[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite id missing from array. Block: " + gameObject.name);
        }
        
    }


    public void BlockDestroy()
    {
        Destroy(gameObject);
        PlayBlockBreakSFX();
        TriggerSparkesVFX();
        levelScript.BlockDestroyed();
        gameStatusScript.UpdateScore();
    }


    private void PlayBlockBreakSFX()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position); /* We're not using "GetComponent<AudioSource>()" because this function CREATES an 
                                                                        audio source (doesnt use the one attached to the block) but automatically disposes
                                                                        of it once the clip has finished playing */
    }
    

    private void TriggerSparkesVFX()
    {
        GameObject sparkleVFX = Instantiate(blockSparkleVFX, transform.position, transform.rotation);
        Destroy(sparkleVFX, 1f);
    }

}
