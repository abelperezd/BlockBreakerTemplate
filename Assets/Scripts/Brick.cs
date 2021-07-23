using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject breakVFX;

    Level level;

    [SerializeField] int maxHits;
    [SerializeField] int timesHit;

    [SerializeField] Sprite[] hitSprites;

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "BreakableBlock")
            level.CountBlocks();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "BreakableBlock")
        {
            HitBlock();
        }
    }

    private void HitBlock()
    {
        timesHit++;
        if (timesHit >= maxHits)
            DestroyBlock();
        else
            ShowNextHitSprite();
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
    }

    private void DestroyBlock()
    {
        PlaySFX();
        PlayVFX();
        Destroy(gameObject);
        level.BlockDestroyed();
        FindObjectOfType<Game>().AddToScore();
    }

    private void PlaySFX()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void PlayVFX()
    {
        Destroy(Instantiate(breakVFX, transform.position, Quaternion.identity), 2f);
    }
}
