
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Player Movement
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
    }
    private void FixedUpdate() 
    {
        Vector2 position = transform.position;
        position.x = position.x + 5f * horizontal * Time.deltaTime;
        position.y = position.y + 5f * vertical * Time.deltaTime;
        
        rigidbody2d.MovePosition(position);

    }
}


