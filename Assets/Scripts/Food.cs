using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Food : MonoBehaviour
{
    public Collider2D gridArea;
    public Transform Player;
    public float minDistance = 3f;
    private float randomX;
    private float randomY;
    private int Tracker;
    private float distance;

    private SnakeMovement snake;


    private void Awake()
    {
        snake = FindObjectOfType<SnakeMovement>();
    }

    public void RandomizePosition()
    {
        Bounds bounds = gridArea.bounds; //get bounds of the grid area
        // Pick a random position inside the bounds
        float x = Random.Range(bounds.min.x, bounds.max.x); 
        float y = Random.Range(bounds.min.y, bounds.max.y); 

        // Round the values to ensure it aligns with the grid
        x = Mathf.Round(x); 
        y = Mathf.Round(y);

        while (snake.Occupies(x, y))
        {
            x++;

            if (x > bounds.max.x)
            {
                x = bounds.min.x;
                y++;

                if (y > bounds.max.y)
                {
                    y = bounds.min.y;
                }
            }
        }

        // Assign the final position
        transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D other) //When snake collides with the food
    {
        if(other.tag == "Player")
        {
            RandomizePosition();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        RandomizePosition();
    }

    void Update()
    {
    }
}
