using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    SerialPort porta = new SerialPort("COM3", 9600);

    private int port_val;

    // Start is called before the first frame update
    void Start()
    {

        Invoke("delay", 2);

    }

    // Update is called once per frame
    void Update()
    {
        if (porta.IsOpen)
        {
           
            try
            {
                port_val = porta.ReadByte();
                
                   

                    
                // quando qualquer butão é carregado muda de cena
                    if ((port_val < 6) && (port_val != 0))
                    {
                       
                       SceneManager.LoadScene("ArenaScene");
                    }
                
            }
            catch (System.Exception)
            {
            }
        }
    }

void delay()
    {
    porta.Open();
    porta.ReadTimeout = 50;

}

}
