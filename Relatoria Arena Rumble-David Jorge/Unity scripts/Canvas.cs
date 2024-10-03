using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{
    public static Canvas Instance { get; private set; }

    public static int score;

    public Text scr_text;
    public Text spc_text;
    public Text end_text;

    private float TimeBtwAtk;
    public float TimeStrtAtk;
    public static bool button;

    public static bool boolean1;
    public static bool boolean2;
    public static bool boolean3;
    public static bool boolean4;

    public int prev_round;
    public float fade = 2;
    // Start is called before the first frame update
    void Start()
    {
        boolean1 = false;
        boolean2 = false;
        boolean3 = true;
        boolean4 = false;
        TimeBtwAtk = TimeStrtAtk;
        score = 0;
        prev_round = GameManager.rounds;
    }

    // Update is called once per frame
    void Update()
    {
        //escrever o texto score
        scr_text.text = "Score: " + score;
        
        if (TimeBtwAtk <= 0)
        {
           
            score += 5;

            TimeBtwAtk = TimeStrtAtk;
        }
        else
        {
            TimeBtwAtk -= Time.deltaTime;
        }


        //se o ataque especial for ativado alterar texto
        if (boolean1 == true)
        {
            spc_text.text = "The special is charging";
        }
        else
        {
            spc_text.text = " ";
        }

        //quando o jogo chega ao fim escrever este texto
        if (boolean2 == true)
        {
            end_text.text = "You survived the arena!!";
        }

        //quando o jogadoe morre escreve este texto
        if (boolean4 == true)
        {
            spc_text.text = "You have been killed!!";
            end_text.text = "Press any Button to go back to Menu";
            
            GameManager.Score = 0;
            GameManager.rounds = 1;
            
        }

        //quando  a ronda muda altera a boolean3 para true
        if ((prev_round != GameManager.rounds) &&(boolean4 == false))
        {
            boolean3 = true;
            prev_round = GameManager.rounds;
        }

        //quando boolean3 é true, escreve o texto
        if (boolean3 == true)
        {
            end_text.text = "Round " + GameManager.rounds + " is starting";

            if (fade <= 0)
            {
               
                end_text.text = " ";
                fade = 2;
                boolean3 = false;
            }
            else
            {
                fade -= Time.deltaTime;
            }
        }
    }
}
