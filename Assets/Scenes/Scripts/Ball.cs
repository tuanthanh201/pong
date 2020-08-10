using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    [SerializeField]
    float speed = 6;
    float delay = 1.5f;
    float timer = 0;
    float boost;

    float radius = 5f;
    Vector2 direction;

    public Text right;
    public Text left;

    public int rightScore = 0;
    public int leftScore = 0;

    bool scoreLeft = false;
    bool scoreRight = false;

    public UnityEvent onHit;

    public Color color = Color.white;

    public Vector2 RandomVector(float min, float max)
    {
        var x = Random.Range(min, max);
        var y = Random.Range(min, max);
        return new Vector2(x, y).normalized; ;
    }

    void Start()
    {
        transform.position = transform.position * new Vector2(0, 0);
        //direction = Vector2.one.normalized;
        radius = transform.localScale.x / 2;
        timer = 0f;
        speed = 8f;
        direction = RandomVector(-5f, 5f);
        //color = Color.white;
    }

    void Update()
    {
        //transform.Translate(direction * speed * Time.deltaTime);

        timer += Time.deltaTime;
        if (timer > delay)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }

        boost += Time.deltaTime;
        if (boost > 10)
        {
            speed += 1;
            boost = 0;
        }

        #region Top and Bottom
        if (transform.position.y < GameManager.bottomLeft.y + radius && direction.y < 0)
        {
            direction.y = -direction.y;
        }

        if (transform.position.y > GameManager.topRight.y - radius && direction.y > 0)
        {
            direction.y = -direction.y;
        }
        #endregion

        #region Scoring
        if ((transform.position.x < (GameManager.bottomLeft.x + radius)) && (direction.x < 0))
        {
            Debug.Log("Right player wins");
            Start();

            scoreRight = true;
            UpdateScore();
        }


        if ((transform.position.x > (GameManager.topRight.x - radius)) && (direction.x > 0))
        {
            Debug.Log("Left player wins");
            Start();

            scoreLeft = true;
            UpdateScore();
        }

        scoreLeft = false;
        scoreRight = false;
        #endregion
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
         if (other.tag == "Paddle")
         {
            bool isRight = other.GetComponent<Paddle>().isRight;

            if(isRight == true && direction.x > 0)
            {
                direction.x = -direction.x;
                SoundManager.Play("Hit");
                onHit.Invoke();
            }

            if (isRight == false && direction.x < 0)
            {
                direction.x = -direction.x;
                SoundManager.Play("Hit");
                onHit.Invoke();
            }
        }
    }

    public void UpdateScore()
    {
        if(scoreRight)
        {
            rightScore++;
            right.text = rightScore.ToString();
        }

        if (scoreLeft)
        {
            leftScore++;
            left.text = leftScore.ToString();
        }
    }
}
