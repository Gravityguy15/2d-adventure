using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public int maxHealth = 5;
    public int health {get {return currentHealth; } }
    int currentHealth;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    public float speed = 5f;

    public float timeInvicible = 2.0f;
    bool isInvicible;
    float invincibleTimer;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Player Movement
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (isInvicible)
        {
           invincibleTimer -= Time.deltaTime;
           if (invincibleTimer < 0)
               isInvicible = false;
        }
        
    }
    private void FixedUpdate() 
    {
        //Player Speed
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        
        rigidbody2d.MovePosition(position);
    }


        public void ChangeHealth(int amount)
        {
          if (isInvicible)
              return;

              isInvicible = true;
               invincibleTimer = timeInvicible;
    
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            Debug.Log(currentHealth+ "/" + maxHealth);
        }
}


