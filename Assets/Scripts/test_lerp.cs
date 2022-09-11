using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_lerp : MonoBehaviour
{
    public Collider2D area;
    Vector3 startPos;
    private float elapsedtime;
    private float duration = 5f;
    private Vector3 newPos;
    float x, y,p;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        elapsedtime += Time.deltaTime;
        p = elapsedtime / duration;
        Bounds bounds = area.bounds; //get bounds of the grid area

        Debug.Log(bounds.max.x);
        Debug.Log(bounds.min.x);
        // Pick a random position inside the bounds
        x = Random.Range(bounds.min.x, bounds.max.x);
        y = Random.Range(bounds.min.y, bounds.max.y);

        x = Mathf.Round(x);
        y = Mathf.Round(y);

        newPos = new Vector3(x, y, 0f);

        //transform.position = Vector3.Lerp(startPos, newPos, p);
  

    }
}
