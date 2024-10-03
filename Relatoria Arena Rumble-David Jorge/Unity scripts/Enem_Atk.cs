using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enem_Atk : MonoBehaviour
{
    public Animator anim;

    private float TimeBtwAtk;
    public float TimeStrtAtk;

    public Transform Atk_pos;
    public LayerMask PlayerMask;
    public float Atk_range;
    private Vector2 vec;
    public int Dmg;


    void Start()
    {
   
        TimeBtwAtk = TimeStrtAtk;
        vec = new Vector2(Atk_range + .15f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        //quando TimeBtwAtk for menor que zero o enimigo ataqua
        if (TimeBtwAtk <= 0)
        {

            Collider2D DMG_Area = Physics2D.OverlapBox(Atk_pos.position, vec, 90, PlayerMask);
           
                if (DMG_Area != null)
                {
                //muda o boss e o enimigo para a animação de ataque
                anim.SetInteger("Boss_val", 1);
                anim.SetInteger("Enem_val", 1);

                TimeBtwAtk = TimeStrtAtk;
                    DMG_Area.GetComponent<Hero>().TakeDamage(Dmg);
                }
                else
                {
                //muda o boss e o enimigo para a animação de movimento
                anim.SetInteger("Boss_val", 0);
                anim.SetInteger("Enem_val", 0);
            }



        }
        else
        {
            TimeBtwAtk -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Atk_pos.position, new Vector3(Atk_range + .15f, 1, 1));
    }
}
