using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] AudioClip[] ballSounds;
    AudioSource audioSource;

    Rigidbody2D myRB2D;
    [SerializeField] float randomFactor = 0.2f;

    private float xVel;
    private float yVel;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        yVel = 15f;
        myRB2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    public void RestartPosition(GameObject paddle)
    {
        float x = paddle.transform.position.x;
        float y = paddle.transform.position.y + GetComponent<CircleCollider2D>().radius + paddle.GetComponent<Renderer>().bounds.size.y/2;
        transform.position = new Vector2(x, y);
    }

    public void StartMoving()
    {
        xVel = Random.Range(-10, 10);
        myRB2D.velocity = new Vector2(xVel , yVel);
    }

    public void StopMoving()
    {
        myRB2D.velocity = new Vector2(0, 0);
    }

    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));

        myRB2D.velocity += velocityTweak;

        audioSource.PlayOneShot(ballSounds[Random.Range(0, ballSounds.Length)]);
    }

}
