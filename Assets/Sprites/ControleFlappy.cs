using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleFlappy : MonoBehaviour
{
    public float vitesseX;
    public float vitesseY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("a") || Input.GetKey("right"))
        {
            transform.Translate(vitesseX, 0, 0);
        }
        if(Input.GetKey("d") || Input.GetKey("left")){
            transform.Translate(-vitesseX, 0, 0);
        }
        if(Input.GetKeyDown("w") || Input.GetKeyDown("up")){
            transform.Translate(0, vitesseY, 0);
        }
        
    }
}
