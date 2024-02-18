using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementObjets : MonoBehaviour
{
    public float vitesse;
    public float positionFin;
    public float positionDebut;
    public float deplacementAleatoire ;
  


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float valeurAleatoireY = Random.Range(-deplacementAleatoire, deplacementAleatoire); // valeur aléatoire pour le déplacement vertical

        if (transform.position.x < -20.4f)
        {
            transform.position  =  new Vector2 (positionDebut, valeurAleatoireY);	// positionnement vertical de l'objet
        }
        transform.Translate(vitesse, 0, 0);
    }
}