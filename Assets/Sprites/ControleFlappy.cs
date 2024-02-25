using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleFlappy : MonoBehaviour
{
    //Déclaration des variables (publiques, privées et statiques)
    public float vitesseX; //vitesse de déplacement horizontale
    public float vitesseY; //  vitesse de déplacement verticale
    public Sprite flappyBlesse; //  image de Flappy blessé
    public Sprite flappyNormal; // image de Flappy normal
    public GameObject objetPieceOr; // objet pièce d'or
    public GameObject objetPackVie; // objet pack de vie
    public GameObject objetChampignon; // objet champignon
    public float deplacementAleatoireY ; // déplacement aléatoire en Y

    // Appelé au début du jeu
    void Start()
    {
        
    }

     // Fonction qui gère les déplacements et le saut du personnage à l'aide des touches A, D et W.
    void Update()
    {
         //On ajuste la variable vitesseX si la touche "a" ou "right" est appuyée
        if(Input.GetKey("a") || Input.GetKey("right"))
        {

            transform.Translate(vitesseX, 0, 0);

        }
         //On ajuste la variable vitesseX si la touche "d" ou "left" est appuyée
        if(Input.GetKey("d") || Input.GetKey("left"))
        {

            transform.Translate(-vitesseX, 0, 0);

        }
            //On ajuste la variable vitesseY si la touche "w" ou "up" est appuyée
        if(Input.GetKeyDown("w") || Input.GetKeyDown("up"))
        {

            vitesseY = 5;

        }
         // Si aucune touche n'est enfoncée, on récupère la vélocité y actuelle
        else
        {

            vitesseY = GetComponent<Rigidbody2D>().velocity.y;

        }
         // On ajuste la vélocité de Flappy
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, vitesseY);
    }



    // Fonction qui fait la gestion des collisions
    void OnCollisionEnter2D(Collision2D collisionTrue)
    {
        // Si Flappy touche une colonne
        if(collisionTrue.gameObject.name == "Colonne")
        {
            // On change l'image de Flappy
            GetComponent<SpriteRenderer>().sprite = flappyBlesse;

        }
        // Si Flappy touche une pièce d'or
        else if(collisionTrue.gameObject.name == "PieceOr")
        {
             // On désactive l'objet pièce d'or
            collisionTrue.gameObject.SetActive(false);
            Invoke("ActivePieceOr", 5f);

        }
        // Si Flappy touche un pack de vie
        else if(collisionTrue.gameObject.name == "PackVie")
        {
             // On change l'image de Flappy
            GetComponent<SpriteRenderer>().sprite = flappyNormal;
            
             // On désactive l'objet pack de vie
            collisionTrue.gameObject.SetActive(false);
            Invoke("ActivePackVie", 5f);

        }
        // Si Flappy touche un champignon
        else if(collisionTrue.gameObject.name == "Champignon")
        {
             //On change la taille de Flappy de manière à ce qu'il double de taille
            transform.localScale *= 2f;

             // On appelle la fonction ChampignonDiminueTaille après 5 secondes
            Invoke("ChampignonDiminueTaille", 5f);

             // On désactive l'objet champignon
            collisionTrue.gameObject.SetActive(false);
            Invoke("ActiveChampignon", 5f);

        }
    }



    // Fonction qui réduit la taille de Flappy après avoir touché un champignon 5 secondes plus tard
    void ChampignonDiminueTaille()
    {
        // On réduit la taille de Flappy de manière à ce qu'il redevienne normal
        transform.localScale /= 2f; 

    }


    // Fonction pour l'activation d'une pièce d'or à une position aléatoire
    void ActivePieceOr()
    {
        // On active l'objet pièce d'or
        objetPieceOr.SetActive(true);

        // On génère une valeur aléatoire pour le déplacement vertical
        float valeurAleatoireY = Random.Range(-deplacementAleatoireY, deplacementAleatoireY);
        objetPieceOr.transform.position = new Vector2(objetPieceOr.transform.position.x, valeurAleatoireY);

    }


    // Fonction pour l'activation d'un pack de vie à une position aléatoire
    void ActivePackVie()
    {
        // On active l'objet pack de vie
        objetPackVie.SetActive(true);

        // On génère une valeur aléatoire pour le déplacement vertical
        float valeurAleatoireY = Random.Range(-deplacementAleatoireY, deplacementAleatoireY);
        objetPackVie.transform.position = new Vector2(objetPackVie.transform.position.x, valeurAleatoireY);

    }


    // Fonction pour l'activation du champignon à une position aléatoire
    void ActiveChampignon()
    {
        // On active l'objet champignon
        objetChampignon.SetActive(true);

        // On génère une valeur aléatoire pour le déplacement vertical
        float valeurAleatoireY = Random.Range(-deplacementAleatoireY, deplacementAleatoireY);
        objetChampignon.transform.position = new Vector2(objetChampignon.transform.position.x, valeurAleatoireY);

    }
}