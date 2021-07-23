using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    public Game game;

    private void Start()
    {
        game = GameObject.Find("GamePlay").GetComponent<Game>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        game.RemoveLive();
    }
}
