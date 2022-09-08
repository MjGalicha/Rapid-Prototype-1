using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using TMPro;
using UnityEngine.SceneManagement;

public class SnakeMovement : MonoBehaviour
{
    private Vector2 direction = Vector2.right; //Snake will by default start moving to the right when the game starts
    public Transform segmentPrefab;
    List<Transform> segments;
    public bool CanMove;
    private Vector2 input;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI gameOverText;
    private int score = 0;
    private Rigidbody2D rbd;

    public float speed = 20f;
    private float speedMultiplier = 1f;
    private float nextUpdate;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        gameOverText.enabled = false;
        CanMove = true;
        segments = new List<Transform>(); //Create new list every game
        segments.Add(this.transform);   //Init with head of snake 
        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        rbd = GetComponent<Rigidbody2D>();
        speedMultiplier = 1f;
        //speed = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Time : " + Time.timeScale);
        if(CanMove)
        {
            // Only allow turning up or down while moving in the x-axis
            if (direction.x != 0f)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    input = Vector2.up;
                }
                else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    input = Vector2.down;
                }
            }
            // Only allow turning left or right while moving in the y-axis
            else if (direction.y != 0f)
            {
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    input = Vector2.right;
                }
                else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    input = Vector2.left;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (input != Vector2.zero)
        {
            direction = input;
        }

        if (Time.time < nextUpdate)
        {
            return;
        }
        //If the player can move, continue its movement path, otherwise cease movement
        if (CanMove)
        {
            for (int i = segments.Count - 1; i > 0; i--)
            {
                segments[i].position = segments[i - 1].position;
            }

            this.transform.position = new Vector3(
                (Mathf.Round(this.transform.position.x) + direction.x),
                (Mathf.Round(this.transform.position.y) + direction.y),
                0.0f);
            
            //Debug.Log(Time.time);
            nextUpdate = Time.time + (1f / (speed * speedMultiplier));
 
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
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }

    private void OnTriggerEnter2D(Collider2D other) //When snake collides with the food it grows
    {
        if (other.tag == "Food")
        {
            score++;
            scoreText.text = score.ToString();
            speed = speed + 5f;
            Time.timeScale = Time.timeScale + 0.05f;
            Grow();
            if(score > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", score);
                highScoreText.text = score.ToString();
            }
        }
        else if (other.tag == "Obstacle")
        {
            gameOverText.enabled = true;
            CanMove = false;
            Invoke("GameOver", 3f);
            
        }
    }

    public bool Occupies(float x, float y)
    {
        foreach (Transform segment in segments)
        {
            if (segment.position.x == x && segment.position.y == y)
            {
                return true;
            }
        }

        return false;
    }
}
