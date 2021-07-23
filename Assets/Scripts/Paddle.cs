using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float screenWidthUnits = 16.0f;
    private float min;
    private float max;
    private float posX;
    private float posY;

    private bool autoplayEnabled;

    void Start()
    {
        posY = transform.position.y;
        min = GetComponent<Renderer>().bounds.size.x / 2;
        max = screenWidthUnits - min;

        autoplayEnabled = FindObjectOfType<Game>().IsAutoplayEnabled();
    }

    // Update is called once per frame
    void Update()
    {
        posX = GetXPos();


        transform.position = new Vector2(Mathf.Clamp(posX, min, max), posY);
        
    }

    private float GetXPos()
    {
        if (autoplayEnabled)
        {
            return FindObjectOfType<Ball>().transform.position.x;
        }
        else 
            return Input.mousePosition.x / Screen.width * screenWidthUnits;
    }
}
