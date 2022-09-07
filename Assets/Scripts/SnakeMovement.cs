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

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI gameOverText;
    public float speed = 2f;
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
        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(CanMove)
        {                       
            if ((Input.GetKeyDown(KeyCode.W) | Input.GetKeyDown(KeyCode.UpArrow)) && direction != Vector2.down)  /* This nested loop gets player input and updates direction */
            {
                direction = Vector2.up;
            }
            else if ((Input.GetKeyDown(KeyCode.S) | Input.GetKeyDown(KeyCode.DownArrow)) && direction != Vector2.up)
            { 
                direction = Vector2.down;
            }
            else if ((Input.GetKeyDown(KeyCode.A) | Input.GetKeyDown(KeyCode.LeftArrow)) && direction != Vector2.right)
            {
                direction = Vector2.left;
            }
            else if ((Input.GetKeyDown(KeyCode.D) | Input.GetKeyDown(KeyCode.RightArrow)) && direction != Vector2.left)
            {
                direction = Vector2.right;
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
                (Mathf.Round(this.transform.position.x) + direction.x),
                (Mathf.Round(this.transform.position.y) + direction.y),
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
        SceneManager.LoadScene("GameScene");
    }

    private void OnTriggerEnter2D(Collider2D other) //When snake collides with the food it grows
    {
        if (other.tag == "Food")
        {
            score++;
            scoreText.text = score.ToString();
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
}
