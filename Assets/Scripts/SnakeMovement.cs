using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using TMPro;
using UnityEngine.SceneManagement;

public class SnakeMovement : MonoBehaviour
{
    private Animator animator;
    private Animator sheepAnimator;

    private Vector2 direction = Vector2.right; //Snake will by default start moving to the right when the game starts
    public Transform segmentPrefab;
    List<Transform> segments = new List<Transform>();
    public bool CanMove;
    public AudioClip[] DogSFXArray;
    public AudioSource DogSFX;
    public AudioClip[] deathSFXArray;
    public AudioSource deathSFX;
    private Vector2 input;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI gameOverText;

    public TextMeshProUGUI Den1;
    public TextMeshProUGUI Den2;
    public TextMeshProUGUI Den3;
    public TextMeshProUGUI Den4;

    private int score = 0;
    private Rigidbody2D rbd;

    public float speed = 20f;
    private float speedMultiplier = 1f;
    private float nextUpdate;

    private int DenM1, DenM2, DenM3, DenM4;




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
        InitDenMultiplier1();
        InitDenMultiplier2();
        InitDenMultiplier3();
        InitDenMultiplier4();
        Time.timeScale = 0.8f;
        animator = GetComponent<Animator>();
        DogSFX = GetComponent<AudioSource>();
        deathSFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Time : " + Time.timeScale);
        if (CanMove)
        {
            // Only allow turning up or down while moving in the x-axis
            if (direction.x != 0f)
            {
                
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    input = Vector2.up;
                    animator.SetFloat("YInput", 1f);
                    animator.SetFloat("XInput", 0f);
                    
                }
                else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    input = Vector2.down;
                    animator.SetFloat("YInput", -1f);
                    animator.SetFloat("XInput", 0f);
                }
            } 
            // Only allow turning left or right while moving in the y-axis
            else if (direction.y != 0f)
            {
                
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    input = Vector2.right;
                    animator.SetFloat("YInput", 0f);
                    animator.SetFloat("XInput", 1f);
                }
                else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    input = Vector2.left;
                    animator.SetFloat("YInput", 0f);
                    animator.SetFloat("XInput", -1f);
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
        if(CanMove)
        {
            for (int i = segments.Count - 1; i > 0; i--)
            {
                sheepAnimator = segments[i].GetComponent<Animator>();

                if(segments[i].position.y < segments[i - 1].position.y)
                {
                    //going up
                    sheepAnimator.SetFloat("YInput", 1f);
                    sheepAnimator.SetFloat("XInput", 0f);
                }
                if (segments[i].position.y > segments[i - 1].position.y)
                {
                    //going down
                    sheepAnimator.SetFloat("YInput", -1f);
                    sheepAnimator.SetFloat("XInput", 0f);
                }
                if (segments[i].position.x < segments[i - 1].position.x)
                {
                    //going right
                    sheepAnimator.SetFloat("YInput", 0f);
                    sheepAnimator.SetFloat("XInput", 1f);
                }
                if (segments[i].position.x > segments[i - 1].position.x)
                {
                    //going right
                    sheepAnimator.SetFloat("YInput", 0f);
                    sheepAnimator.SetFloat("XInput", -1f);
                }



                segments[i].position = segments[i - 1].position;
               
            }

            this.transform.position = new Vector3(
                (Mathf.Round(this.transform.position.x) + direction.x),
                (Mathf.Round(this.transform.position.y) + direction.y),
                0.0f);

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
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }
        segments.Clear();
        segments.Add(this.transform);
        this.transform.position = Vector3.zero;
        score = 0;
        scoreText.text = score.ToString();

        gameOverText.enabled = false;
        CanMove = true;
        Time.timeScale = 1;
        SceneManager.LoadScene("Art_GameScene");
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
            UpdateHighScore();
        }
        else if (other.tag == "Obstacle")
        {
            PlayDeathSFX();
            UpdateHighScore();
            gameOverText.enabled = true;
            CanMove = false;
            Invoke("GameOver", 3f);
        }
        else if(other.tag == "Den1")
        {
            if(segments.Count > 1)
            {
                int count = segments.Count - 1;

                for (int i = 1; i < segments.Count; i++)
                {
                    Destroy(segments[i].gameObject);
                }
                PlayDogSFX();
                segments.Clear();
                segments.Add(this.transform);
                score = score + (DenM1 * count);
                speedMultiplier = 1f;
                speed = 20f;
                Time.timeScale = 1;
                scoreText.text = score.ToString();
                InitDenMultiplier1();
            }
            UpdateHighScore();
        }
        else if (other.tag == "Den4")
        {
            if(segments.Count > 1)
            {
                int count = segments.Count - 1;

                for (int i = 1; i < segments.Count; i++)
                {
                    Destroy(segments[i].gameObject);
                }
                PlayDogSFX();
                segments.Clear();
                segments.Add(this.transform);
                score = score + (DenM4 * count);
                speedMultiplier = 1f;
                speed = 20f;
                Time.timeScale = 0.75f;
                scoreText.text = score.ToString();
                InitDenMultiplier4();
            }
            UpdateHighScore();
        }
        else if (other.tag == "Den2")
        {
            if(segments.Count > 1)
            {
                int count = segments.Count - 1;

                for (int i = 1; i < segments.Count; i++)
                {
                    Destroy(segments[i].gameObject);
                }
                PlayDogSFX();
                segments.Clear();
                segments.Add(this.transform);
                score = score + (DenM2 * count);
                speedMultiplier = 1f;
                speed = 20f;
                Time.timeScale = 1;
                scoreText.text = score.ToString();
                InitDenMultiplier2();
            }
            UpdateHighScore();
        }
        else if (other.tag == "Den3")
        {
            if(segments.Count > 1)
            {
                int count = segments.Count - 1;

                for (int i = 1; i < segments.Count; i++)
                {
                    Destroy(segments[i].gameObject);
                }
                PlayDogSFX();
                segments.Clear();
                segments.Add(this.transform);
                score = score + (DenM3 * count);
                speedMultiplier = 1f;
                speed = 20f;
                Time.timeScale = 1;
                scoreText.text = score.ToString();
                InitDenMultiplier3();
            }
            UpdateHighScore();
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

    private void UpdateHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = score.ToString();
        }
    }

    private void PlayDeathSFX()
    {
        deathSFX.clip = deathSFXArray[Random.Range(0, deathSFXArray.Length)];
        deathSFX.PlayOneShot(deathSFX.clip);
    }
    private void PlayDogSFX()
    {
        DogSFX.clip = DogSFXArray[Random.Range(0, DogSFXArray.Length)];
        DogSFX.PlayOneShot(DogSFX.clip);
    }

    void InitDenMultiplier1()
    {
        DenM1 = Random.Range(3, 7);
        Den1.text = "x" + DenM1;
    }

    void InitDenMultiplier2()
    {
        DenM2 = Random.Range(2, 6);
        Den2.text = "x" + DenM2;
    }

    void InitDenMultiplier3()
    {
        DenM3 = Random.Range(1, 5);
        Den3.text = "x" + DenM3;
    }

    void InitDenMultiplier4()
    {
        DenM4 = Random.Range(7, 11);
        Den4.text = "x" + DenM4;
    }
}
