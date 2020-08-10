using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    float speed = 7;
    float height;

    string input;

    public bool isRight;

    void Start()
    {
        height = transform.localScale.y;
        speed = 7f;
    }

    public void Init(bool isRightPaddle)
    {
        isRight = isRightPaddle;

        Vector2 pos = Vector2.zero;

        #region right paddle
        if (isRightPaddle)
        {
            pos = new Vector2(GameManager.topRight.x, 0);
            pos -= Vector2.right * transform.localScale.x;

            input = "PaddleRight";
        }
        #endregion

        #region left paddle
        else
        {
            pos = new Vector2(GameManager.bottomLeft.x, 0);
            pos += Vector2.right * transform.localScale.x;

            input = "PaddleLeft";
        }
        #endregion

        transform.position = pos;
        transform.name = input;
    }

    void Update()
    {
        float move = Input.GetAxis(input) * Time.deltaTime * speed;

        #region top and bottom
        if ((transform.position.y < GameManager.bottomLeft.y + height / 2) && (move < 0))
        {
            move = 0;
        }
        
        if ((transform.position.y > GameManager.topRight.y - height / 2) && (move > 0))
        {
            move = 0;
        }
        #endregion

        transform.Translate(move * Vector2.up);
    }
}
