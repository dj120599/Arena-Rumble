using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform prefab;
    public Transform Boss;
    public Transform spawner1;
    public Transform spawner2;

    private int members;
    private int round;
    private float timer;
   
    private bool RounOn;
    private bool Bosson;

    void Start()
    {
        round = 1;
        timer = 8;
        members = 0;
        InvokeRepeating("RandomThing", 0,1.7f);
        RounOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        //se o o numerode enimigos criados for iguala ao numero da ronda vezes 4,
        //subir de ronda
        if (members == round * 4)
        {
            RounOn = false;
            //no final da ronda 5 e 10, criar um boss
            if(round == 5 || round == 10)
            {
                Invoke("BossSummon", 1.8f);
            }
          

            if (GameManager.Score == round * 4)
            {
               
                if (timer <= 0)
                {
                    round++;
                    Bosson = true;
                    RounOn = true;
                    timer = 8;
                    members = 0;
                    GameManager.Score = 0;
                }
                else
                {

                    timer -= Time.deltaTime;
                }

          
            }
        }


    }

    //função responsavel na criação de enimigos
    void RandomThing()
    {

        //se o numero de enimigos criados for menor ao numero da ronda vezes 4, criar enimigos
        if ((members <= round * 4) && (RounOn == true))
        {
            //se exitirem 8 enimigos no jogo, parar produção, ate que algum seja destruido
            if(members - GameManager.Score <= 7)
            {
                int randomTime2 = Random.Range(0, 2);

                if (randomTime2 == 1)
                {
                    Instantiate(prefab, spawner2.position, transform.rotation);
                    members++;
                }
                else
                {
                    Instantiate(prefab, spawner1.position, transform.rotation);
                    members++;
                }
            }
     
        }


    }

    //função que cria o boss enimigo
    void BossSummon()
    {
        if ((round == 5) && (Bosson == true))
        {
             GameManager.Score -= 1;
             Instantiate(Boss, spawner2.position, transform.rotation);
            Bosson = false;
        }

        if ((round == 10) && (Bosson == true))
        {
            GameManager.Score -= 1;
            Instantiate(Boss, spawner1.position, transform.rotation);
            Bosson = false;
        }
    }

}
