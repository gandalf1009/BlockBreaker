using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    // config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    //[SerializeField] int maxHits;
    [SerializeField] Sprite[] hitSprites;

    // cashed reference
    GameStatus gameStatus;
    Level level;

    //state variables
    [SerializeField] int timesHit; 


    private void Start()
    {
        CountBreakableBlocks();

        gameStatus = FindObjectOfType<GameStatus>();

    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();

        if (tag == "Breakable")
        {
            level.CountBlocks();
        }

        else if (tag == "Unbreakable")
        {

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyBlock();
    }

    private void DestroyBlock()
    {

        if (tag == "Breakable")
        {
            int maxHits = hitSprites.Length + 1;
            timesHit++;
            if(timesHit >= maxHits)
            {
                PlayBlockDestroySFX();
                Destroy(gameObject);
                gameStatus.AddToScore();
                TriggerSparklesVFX();
            }
            else
            {
                ShowNextHitSprite();
            }

        } 
        else if (tag == "Unbreakable")
        {

        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.Log("Block sprite is missing from array!!!" + gameObject.name);
        }
    }

    private void PlayBlockDestroySFX()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        level.BlockDestroyed();
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
