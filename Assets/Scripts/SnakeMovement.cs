using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SnakeMovement : MonoBehaviour
{
    private Vector2 direction = Vector2.right; //Snake will by default start moving to the right when the game starts
    public Transform segmentPrefab;
    List<Transform> segments;
    public bool CanMove = true;
    bool CanMoveLeft = true;
    bool CanMoveRight = true;
    bool CanMoveBack = true;
    bool CanMoveFront = true;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    private int score = 0;
    private Rigidbody2D rbd;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        gameOverText.enabled = false;
        CanMove = true;
        segments = new List<Transform>(); //Create new list every game
        segments.Add(this.transform);   //Init with head of snake 
    }

    // Update is called once per frame
    void Update()
    {
        if(CanMove)
        {                       
            if ((Input.GetKeyDown(KeyCode.W) | Input.GetKeyDown(KeyCode.UpArrow)) && CanMoveFront == true)                         /* This nested loop gets player input and updates direction */
            {
                direction = Vector2.up;
                CanMoveBack = false;
                CanMoveRight = true;
                CanMoveLeft = true;
            }
            else if ((Input.GetKeyDown(KeyCode.S) | Input.GetKeyDown(KeyCode.DownArrow)) && CanMoveBack == true)
            {
                direction = Vector2.down;
                CanMoveFront = false;
                CanMoveRight = true;
                CanMoveLeft = true;
            }
            else if ((Input.GetKeyDown(KeyCode.A) | Input.GetKeyDown(KeyCode.LeftArrow)) && CanMoveLeft == true)
            {
                direction = Vector2.left;
                CanMoveRight = false;
                CanMoveFront = true;
                CanMoveBack = true;
            }
            else if ((Input.GetKeyDown(KeyCode.D) | Input.GetKeyDown(KeyCode.RightArrow)) && CanMoveRight == true)
            {
                direction = Vector2.right;
                CanMoveLeft = false;
                CanMoveFront = true;
                CanMoveBack = true;
            }
        }
    }

    void FixedUpdate()
    {
        //If the player can move, continue its movement path, otherwise cease movement
        if (CanMove)
        {
            for (int i = segments.Count - 1; i > 0; i--)
            {
                segments[i].position = segments[i - 1].position;
            }

            this.transform.position = new Vector3(
                Mathf.Round(this.transform.position.x) + direction.x,
                Mathf.Round(this.transform.position.y) + direction.y,
                0.0f);
        }
    }
        

    void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    void GameOver()
    {
        for(int i = 1; i<segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }
        segments.Clear();
        segments.Add(this.transform);
        score = 0;
        scoreText.text = score.ToString();

        this.transform.position = Vector3.zero;
        gameOverText.enabled = false;
        CanMove = true;
    }

    private void OnTriggerEnter2D(Collider2D other) //When snake collides with the food it grows
    {
        if (other.tag == "Food")
        {
            score++;
            scoreText.text = score.ToString();
            Grow();
        }
        else if (other.tag == "Obstacle")
        {
            gameOverText.enabled = true;
            CanMove = false;
            Invoke("GameOver", 3f);
            
        }
    }
}
