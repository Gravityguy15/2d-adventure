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

    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);

    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Player Movement
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if(!Mathf.Approximately(move.x, move.y) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x , move.y);
            lookDirection.Normalize();
        }
         //Player Animations
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);


        if (isInvicible)
        {
           invincibleTimer -= Time.deltaTime;
           if (invincibleTimer < 0)
               isInvicible = false;    
        }

        if(Input.GetKeyDown (KeyCode.C))
        {
            Launch();     
        }
         if(Input.GetKeyDown (KeyCode.X))
         {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NpcScript charecter = hit.collider.GetComponent<NpcScript>();
                if (charecter != null)
                {
                    charecter.DisplayDialog();
                }

            }
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

        //Player Health
        public void ChangeHealth(int amount)
        {
            if (amount < 0)
            {
                animator.SetTrigger("Hit");
                if (isInvicible)
                    return;
            }
          if (isInvicible)
              return;

              isInvicible = true;
               invincibleTimer = timeInvicible;
    
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
            Debug.Log(currentHealth+ "/" + maxHealth);

        }
        //Projectile
        void Launch()
        {
            GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(lookDirection, 300);

            animator.SetTrigger("Launch");
        }
}


