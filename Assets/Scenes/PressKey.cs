using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressKey : MonoBehaviour
{
    public float step = 500;
    public float moveX = 0;
    public float moveY = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveY = step * Time.deltaTime;
        }
        if (Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveY = -step * Time.deltaTime;
        }
        if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveX = -step * Time.deltaTime;
        }
        if (Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveX = step * Time.deltaTime;
        }
    }
}
