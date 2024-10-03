using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;



public class Vertical_Plat : MonoBehaviour
{
    public static Vertical_Plat Instance { get; private set; }

    private PlatformEffector2D effector;
    public float WaitTime;

    public static bool boolean1;
    public static bool boolean2;

  

    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        boolean1 = false;
        boolean2 = false;
    }

    void Update()
    {
        //quando o boolean1 for true o player pode descer da platforma
        if (boolean1 == true)
        {
            Plat_Down();
        }

        //quando o boolean2 for true a platforma volta ao normal
        if (boolean2 == true)
        {
            Plat_reset();
        }
    }

    // função que deixa o player atravessar a platforma e descer
    public void Plat_Down()
    {

            if(WaitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                WaitTime = 0.4f;
            boolean1 = false;
        }
            else
            {
                WaitTime -= Time.deltaTime;
               
            }
       
    }

    //faz um reset a platforma
    public void Plat_reset()
    {
     
        effector.rotationalOffset = 0;
        boolean2 = false;
    }
}
