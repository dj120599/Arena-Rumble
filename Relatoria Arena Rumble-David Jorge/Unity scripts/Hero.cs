using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour
{
    public Animator anim;

    // Move player in 2D space
    public float maxSpeed = 3.4f;
    private float jumpHeight = 10f;
    public float gravityScale = 1.5f;
  
    public int Maxhealth = 50;
    public int Health;

    public HealthBar healthbar;

    bool facingRight = true;
    float moveDirection = 0;
    bool isGrounded = false;
    Vector3 cameraPos;
    Rigidbody2D r2d;
    Collider2D mainCollider;
    // Check every collider except Player and Ignore Raycast
    LayerMask layerMask = ~(1 << 2 | 1 << 9);
    Transform t;


    private float TimeBtwAtk;

    private float index;
    private float TimeSPL1;
    private float TimeSPL2;

    public bool spcl;
    private bool ending;
    private bool atk_anim;

    public Transform Atk_pos;
    public LayerMask EnemyMask;

    public float Atk_range;
    public int Dmg;
    public float TimeStrtAtk;

    SerialPort porta = new SerialPort("COM3", 9600);

    // Use this for initialization
    void Start()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<Collider2D>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;

        Health = Maxhealth;
        healthbar.SetMaxHealth(Maxhealth);

        ending = false;
        atk_anim = true;

        index = 2;

        porta.Open();
        porta.ReadTimeout = 1;
    }

    // Update is called once per frame
    void Update()
    {
 
        if (GameManager.rounds == index)
        {
            Health += 10;
            healthbar.SetHealth(Health);
            index++;
        }

            if (porta.IsOpen)
        {
            try
            {

                Canvas.button = ending;

                int ard_id = porta.ReadByte();
              
                if (ending == false)
                {

                    TimeBtwAtk -= Time.deltaTime;




                    // controla o movimento, se ard_id for 3, o player move-se para a esquerda,
                    //se ard_id for 4, o player move-se para a direita
                    if ((ard_id == 3 || ard_id == 4) && (isGrounded || r2d.velocity.x > 0.01f))
                    {
                        TimeBtwAtk -= Time.deltaTime;
                        moveDirection = ard_id == 3 ? -1 : 1;
                        anim.SetInteger("Value", 2);

                    }
                    else
                    {
                        if ((ard_id == 0) && (isGrounded || r2d.velocity.magnitude < 0.01f))
                        {
                            TimeBtwAtk -= Time.deltaTime;
                            moveDirection = 0;

                            anim.SetInteger("Value", 0);

                        }
                    }

                    // mudança de direção
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

                    // função que faz o jogador saltar
                    if ((ard_id == 1) && isGrounded)
                    {
                        TimeBtwAtk -= Time.deltaTime;
                        r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
                        Vertical_Plat.boolean2 = true;
                    }

                    //quando o ard_id for 5, o player executa o ataque
                    if (ard_id == 5)
                    {
                        
                        if(atk_anim == true)
                        {
                            anim.SetInteger("Value", 1);
                           
                        }
                        

                        if (TimeBtwAtk <= 0)
                        {
                           
                            atk_anim = true;
                            Collider2D[] DMG_Area = Physics2D.OverlapCircleAll(Atk_pos.position, Atk_range, EnemyMask);


                            foreach (Collider2D enemy in DMG_Area)
                            {

                                enemy.GetComponent<Enemy>().TakeDamage(Dmg);
                                TimeBtwAtk = TimeStrtAtk;
                            }

                        }else
                        {
                            TimeBtwAtk -= Time.deltaTime;
                            atk_anim = false;
                        }
                     
                    }


                    //o ataque especial é usado, quando o butão é carregado
                    if (ard_id > 5)
                    {

                        Canvas.boolean1 = true;
                        TimeBtwAtk -= Time.deltaTime;
                        Special(ard_id);
                    }

                    if (ard_id == 2)
                    {
                        Vertical_Plat.boolean1 = true;
                    }

                }
                else if (ending == true)
                { 
                    if((ard_id <= 5) && (ard_id != 0))
                    {
                        porta.Close();
                        SceneManager.LoadScene("StartMenu");
                    }
                        
                }
            }
            catch (System.Exception)
            {
            }
        }
        
            
    }

    //função que é chamada quando o player leva dano
    public void TakeDamage(int damage)
    {
        Health -= damage;
        healthbar.SetHealth(Health);

        if (Health <= 0)
        {
            Canvas.boolean4 = true;
            ending = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Atk_pos.position, Atk_range);
        


    }

    //chamado qaundo o butão especial é carregado
    void Special(int power)
    {

       
        if ((power < 65) && (power > 50))
        {
            
            TimeSPL1 = Time.time;
            spcl = true;

        }

        if ((power < 9  ) && (power >= 6))
        {
            Canvas.boolean1 = false;
            if (spcl == true)
            {
                
                Debug.Log("end");
                TimeSPL2 = Time.time;
                float count = TimeSPL2 - TimeSPL1;
                if (count != 0)
                {
                    if (count < 3)
                    {
                        TakeDamage(2);
                    }
                    else
                    {
                        
                        Vector3 spec = new Vector3(Atk_pos.position.x + 5, Atk_pos.position.y, Atk_pos.position.z);

                        Collider2D[] DMG_Area = Physics2D.OverlapCircleAll(spec, Atk_range * 10, EnemyMask);

                        
                        foreach (Collider2D enemy in DMG_Area)
                        {
                            Debug.Log(DMG_Area.Length);
                            enemy.GetComponent<Enemy>().TakeDamage(Dmg);
                            TimeBtwAtk = TimeStrtAtk;
                        }
                    }
                }
               
                spcl = false;
            }
        }


    }

    void FixedUpdate()
    {
        Bounds colliderBounds = mainCollider.bounds;
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, 0.1f, 0);
  
        isGrounded = Physics2D.OverlapCircle(groundCheckPos, 0.23f, layerMask);
        
  
        r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);

      
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, 0.23f, 0), isGrounded ? Color.green : Color.red);
    }
}