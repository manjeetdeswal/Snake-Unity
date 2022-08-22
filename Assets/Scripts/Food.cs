using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    public BoxCollider2D grid;



    private void RandomPos()
    {
        Bounds bounds = grid.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);

        float y = Random.Range(bounds.min.y, bounds.max.y);

        transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0f);

    }

    void Start()
    {
        RandomPos();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if(collision.tag == "Player")
        {
            RandomPos();
        }

    }

   
}
