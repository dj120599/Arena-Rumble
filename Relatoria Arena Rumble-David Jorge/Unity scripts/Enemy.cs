using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health = 5;
    private float speed = 2;
    private float jumpHeight = 10f;
    private float gravityScale = 1.5f;
    private Transform target;
    float moveDirection = 0;

    bool isGrounded = false;
    bool facingRight = true;
    
    LayerMask layerMask = ~(1 << 2 | 1 << 8);
    Collider2D mainCollider;
    Rigidbody2D r2d;
    Transform t;

 

void Start()
    {
        

        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        mainCollider = GetComponent<Collider2D>();
        r2d.freezeRotation = true;

        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        facingRight = t.localScale.x > 0;
        r2d.gravityScale = gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        //faz o enimigo seguir o player
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
       
        //se o player estiver acima do enimigo por 3 unidades,
        //o enimigo salta
        if ((target.position.y > transform.position.y + 3f) && isGrounded)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);

        }
        
    
        // estiver no chão, e o enimigo tiver velocidade
        if (isGrounded || r2d.velocity.x > 0.01f)
        {
            if(target.position.x - transform.position.x > 0)
            {
                moveDirection = -1;
            }
            else if(target.position.x - transform.position.x < 0)
            {
                moveDirection = 1;
            }
 
        }
        else
        {
            if (isGrounded || r2d.velocity.magnitude < 0.01f)
            {
                moveDirection = 0;
            }
        }

        // muda a direção em que o enimigo estiver virado
        if (moveDirection != 0)
        {
           

            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
        }
        
    }

    //função é chamada quando o enimigo leva dano
   public void TakeDamage(int damage)
    {
        Health -= damage;
        
        if(Health <= 0)
        {
            GameManager.Score += 1;
          Canvas.score += 5;
          Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        Bounds colliderBounds = mainCollider.bounds;
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, 0.1f, 0);

        // Check if player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheckPos, 0.23f, layerMask);
       

    }
}
