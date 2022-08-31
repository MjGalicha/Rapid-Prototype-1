using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Food : MonoBehaviour
{
    public Collider2D gridArea;



    public void RandomizePosition()
    {
        Bounds bounds = gridArea.bounds; //get bounds of the grid area

        // Pick a random position inside the bounds
        float x = Random.Range(bounds.min.x, bounds.max.x); 
        float y = Random.Range(bounds.min.y, bounds.max.y); 

        // Round the values to ensure it aligns with the grid
        x = Mathf.Round(x); 
        y = Mathf.Round(y);

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
