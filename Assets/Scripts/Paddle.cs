using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // configuration parameters;

    [SerializeField] float screenWidthInUnits = 16f; // screen width
    [SerializeField] float minPaddlePosX = 1f;  // left paddle border
    [SerializeField] float maxPaddlePosX = 15f; // right paddle border

    // cashed references
    GameStatus theGameSession;
    Ball theBall;

    // Start is called before the first frame update
    void Start()
    {
        theGameSession = FindObjectOfType<GameStatus>();
        theBall = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.mousePosition.x / Screen.width * screenWidthInUnits);
        Vector2 paddlePos = new Vector2(GetXPos(), transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), minPaddlePosX, maxPaddlePosX);
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if(theGameSession.IsAutoPlayEnabled())
        {
            return theBall.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits; // mouse position
        }
    }
}
