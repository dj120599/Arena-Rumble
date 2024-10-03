using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static int Score;

    public static int rounds;

    void Awake()
    {
        if (Instance == null) 
        { 
            Instance = this; 
        } else if (Instance != this)
        { 
            Destroy(this); 
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Score = 0;
        rounds = 1;
    }

    void Update()
    {
        //quando o numero de enimigos mortos é igual a 4 vezes a ronda atual,
        //Canvas.boolean2 fica true e round aumenta por 1;
        if (Score == (rounds * 4))
        {
            
            rounds++;

            if (rounds == 11)
            {
                Canvas.boolean2 = true;
            }
          
        }
    }
}
