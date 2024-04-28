using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float Speed;
    public Vector2 Direction;

    private Rigidbody2D Rigidbody2D;


    // Start is called before the first frame update
    void Start()
    {   
        Rigidbody2D = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D.velocity = Direction * Speed;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
        transform.localScale = new Vector3(direction.x, 1, 1);
    }
}
