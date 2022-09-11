using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Food : MonoBehaviour
{
    public Collider2D gridArea1;
    public Collider2D gridArea2;
    public Collider2D gridArea3;
    public Collider2D gridArea4;
    public Collider2D gridArea5;
    public Collider2D gridArea6;
    public Collider2D gridArea7;

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
        int area = Random.Range(1, 8);
        switch(area)
        {
            case 1:
                Bounds bounds1 = gridArea1.bounds; //get bounds of the grid area
                                                 // Pick a random position inside the bounds
                float x1 = Random.Range(bounds1.min.x, bounds1.max.x);
                float y1 = Random.Range(bounds1.min.y, bounds1.max.y);

                // Round the values to ensure it aligns with the grid
                x1 = Mathf.Round(x1);
                y1 = Mathf.Round(y1);

                while (snake.Occupies(x1, y1))
                {
                    x1++;

                    if (x1 > bounds1.max.x)
                    {
                        x1 = bounds1.min.x;
                        y1++;

                        if (y1 > bounds1.max.y)
                        {
                            y1 = bounds1.min.y;
                        }
                    }
                }

                // Assign the final position
                transform.position = new Vector2(x1, y1);
                break;
            case 2:
                Bounds bounds2 = gridArea2.bounds; //get bounds of the grid area
                                                 // Pick a random position inside the bounds
                float x2 = Random.Range(bounds2.min.x, bounds2.max.x);
                float y2 = Random.Range(bounds2.min.y, bounds2.max.y);

                // Round the values to ensure it aligns with the grid
                x2 = Mathf.Round(x2);
                y2 = Mathf.Round(y2);

                while (snake.Occupies(x2, y2))
                {
                    x2++;

                    if (x2 > bounds2.max.x)
                    {
                        x2 = bounds2.min.x;
                        y2++;

                        if (y2 > bounds2.max.y)
                        {
                            y2 = bounds2.min.y;
                        }
                    }
                }

                // Assign the final position
                transform.position = new Vector2(x2, y2);
                break;
            case 3:
                Bounds bounds3 = gridArea3.bounds; //get bounds of the grid area
                                                  // Pick a random position inside the bounds
                float x3 = Random.Range(bounds3.min.x, bounds3.max.x);
                float y3 = Random.Range(bounds3.min.y, bounds3.max.y);

                // Round the values to ensure it aligns with the grid
                x3 = Mathf.Round(x3);
                y3 = Mathf.Round(y3);

                while (snake.Occupies(x3, y3))
                {
                    x3++;

                    if (x3 > bounds3.max.x)
                    {
                        x3 = bounds3.min.x;
                        y3++;

                        if (y3 > bounds3.max.y)
                        {
                            y3 = bounds3.min.y;
                        }
                    }
                }

                // Assign the final position
                transform.position = new Vector2(x3, y3);
                break;
            case 4:
                Bounds bounds4 = gridArea4.bounds; //get bounds of the grid area
                                                  // Pick a random position inside the bounds
                float x4 = Random.Range(bounds4.min.x, bounds4.max.x);
                float y4 = Random.Range(bounds4.min.y, bounds4.max.y);

                // Round the values to ensure it aligns with the grid
                x4 = Mathf.Round(x4);
                y4 = Mathf.Round(y4);

                while (snake.Occupies(x4, y4))
                {
                    x4++;

                    if (x4 > bounds4.max.x)
                    {
                        x4 = bounds4.min.x;
                        y4++;

                        if (y4 > bounds4.max.y)
                        {
                            y4 = bounds4.min.y;
                        }
                    }
                }

                // Assign the final position
                transform.position = new Vector2(x4, y4);
                break;
            case 5:
                Bounds bounds5 = gridArea5.bounds; //get bounds of the grid area
                                                  // Pick a random position inside the bounds
                float x5 = Random.Range(bounds5.min.x, bounds5.max.x);
                float y5 = Random.Range(bounds5.min.y, bounds5.max.y);

                // Round the values to ensure it aligns with the grid
                x5 = Mathf.Round(x5);
                y5 = Mathf.Round(y5);

                while (snake.Occupies(x5, y5))
                {
                    x5++;

                    if (x5 > bounds5.max.x)
                    {
                        x5 = bounds5.min.x;
                        y5++;

                        if (y5 > bounds5.max.y)
                        {
                            y5 = bounds5.min.y;
                        }
                    }
                }

                // Assign the final position
                transform.position = new Vector2(x5, y5);
                break;
            case 6:
                Bounds bounds6 = gridArea6.bounds; //get bounds of the grid area
                                                  // Pick a random position inside the bounds
                float x6 = Random.Range(bounds6.min.x, bounds6.max.x);
                float y6 = Random.Range(bounds6.min.y, bounds6.max.y);

                // Round the values to ensure it aligns with the grid
                x6 = Mathf.Round(x6);
                y6 = Mathf.Round(y6);

                while (snake.Occupies(x6, y6))
                {
                    x6++;

                    if (x6 > bounds6.max.x)
                    {
                        x6 = bounds6.min.x;
                        y6++;

                        if (y6 > bounds6.max.y)
                        {
                            y6 = bounds6.min.y;
                        }
                    }
                }

                // Assign the final position
                transform.position = new Vector2(x6, y6);
                break;
            case 7:
                Bounds bounds7 = gridArea7.bounds; //get bounds of the grid area
                                                  // Pick a random position inside the bounds
                float x7 = Random.Range(bounds7.min.x, bounds7.max.x);
                float y7 = Random.Range(bounds7.min.y, bounds7.max.y);

                // Round the values to ensure it aligns with the grid
                x7 = Mathf.Round(x7);
                y7 = Mathf.Round(y7);

                while (snake.Occupies(x7, y7))
                {
                    x7++;

                    if (x7 > bounds7.max.x)
                    {
                        x7 = bounds7.min.x;
                        y7++;

                        if (y7 > bounds7.max.y)
                        {
                            y7 = bounds7.min.y;
                        }
                    }
                }

                // Assign the final position
                transform.position = new Vector2(x7, y7);
                break;
            default:
                break;
        }

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
