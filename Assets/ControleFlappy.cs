using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ControleFlappy : MonoBehaviour
{
    //Déclaration des variables (publiques, privées et statiques)
    public float vitesseX; //vitesse de déplacement horizontale
    public float vitesseY; //  vitesse de déplacement verticale
    public Sprite flappyBlesse; //  image de Flappy blessé
    public Sprite flappyBlesseFerme; // image de Flappy blessé volant
    public Sprite flappyNormal; // image de Flappy normal
     public Sprite flappyNormalFerme; // image de Flappy normal volant
    public GameObject objetPieceOr; // objet pièce d'or
    public GameObject objetPackVie; // objet pack de vie
    public GameObject objetChampignon; // objet champignon
    public float deplacementAleatoireY ; // déplacement aléatoire en Y
    public AudioClip sonColonne; // son de la collision avec une colonne
    public AudioClip sonOr; // son de la collision avec une pièce d'or
    public AudioClip sonPack; // son de la collision avec un pack de vie
    public AudioClip sonChamp; // son de la collision avec un champignon
    public AudioClip SonFinPartie; // son de la fin de la partie
    AudioSource sourceAudio; // source audio
    public bool partieTerminee; // variable pour la fin de la partie
    public bool etatFlappyBlesse; // variable pour l'état de Flappy
    public TextMeshProUGUI textFinDuJeu; // texte du pointage

    public TextMeshProUGUI textPointage;
    float compteur = 0;



    // Appelé au début du jeu
    void Start()
    {
            sourceAudio = GetComponent<AudioSource>(); //va chercher le son
            partieTerminee = false; //initialise la variable partieTerminee à faux
            etatFlappyBlesse = false; //initialise la variable etatFlappyBlesse à faux    
            textFinDuJeu.text = "Flappy est mort...";
            textFinDuJeu.GetComponent<TextMeshProUGUI>().fontSize = 0; //va chercher le texte  
    }

     // Fonction qui gère les déplacements et le saut du personnage à l'aide des touches A, D et W.
    void Update()
    {
    
    if(partieTerminee == false)
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
            if(etatFlappyBlesse == false)
            {
                // On change l'image de Flappy
                GetComponent<SpriteRenderer>().sprite = flappyNormalFerme;
            }
            else
            {
                // On change l'image de Flappy
                GetComponent<SpriteRenderer>().sprite = flappyBlesseFerme;
            }

            vitesseY = 5;

        }
        else if(Input.GetKeyUp("w") || Input.GetKeyUp("up"))
        {
            if(etatFlappyBlesse == false)
            {
                // On change l'image de Flappy
                GetComponent<SpriteRenderer>().sprite = flappyNormal;
            }
            else
            {
                // On change l'image de Flappy
                GetComponent<SpriteRenderer>().sprite = flappyBlesse;
            }
        }
         // Si aucune touche n'est enfoncée, on récupère la vélocité y actuelle
        else
        {

            vitesseY = GetComponent<Rigidbody2D>().velocity.y;

        }
         // On ajuste la vélocité de Flappy
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, vitesseY);
    }
    else
    {
        
    }
    }


    // Fonction qui fait la gestion des collisions
    void OnCollisionEnter2D(Collision2D collisionTrue)
    {
        // Si Flappy touche une colonne
        if(collisionTrue.gameObject.name == "Colonne")
        {
            // joue le clip qui se trouve dans la variable sonCol
           sourceAudio.PlayOneShot(sonColonne, 1f);

            // On change l'état de Flappy à blessé
            if (collisionTrue.gameObject.name == "Colonne" && etatFlappyBlesse == false)
            {
                // On change l'image de Flappy
                GetComponent<SpriteRenderer>().sprite = flappyBlesse;
                etatFlappyBlesse = true;
            }
            else
            {
                // joue le clip qui se trouve dans la variable SonFinPartie
                sourceAudio.PlayOneShot(SonFinPartie, 1f);

                // On arrête la partie
                partieTerminee = true;
                textFinDuJeu.GetComponent<TextMeshProUGUI>().fontSize += 100;

                GetComponent<Rigidbody2D>().freezeRotation = false;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5);
                GetComponent<Collider2D>().enabled = false;


                Invoke("FinPartie", 3f); // On appelle la fonction FinPartie après 2 secondes (le temps que le son se joue

            }
            if (collisionTrue.gameObject.name == "Colonne")
            {
                compteur = compteur -5f;
                UpdatePointage();
            }

        }
        // Si Flappy touche une pièce d'or
        else if(collisionTrue.gameObject.name == "PieceOr")
        {
             // On désactive l'objet pièce d'or
            collisionTrue.gameObject.SetActive(false);
            Invoke("ActivePieceOr", 5f);

            // joue le clip qui se trouve dans la variable sonOr
           sourceAudio.PlayOneShot(sonOr, 1f);

            compteur = compteur + 5f;
            UpdatePointage();



        }
        // Si Flappy touche un pack de vie
        else if(collisionTrue.gameObject.name == "PackVie")
        {
             // On change l'image de Flappy
            GetComponent<SpriteRenderer>().sprite = flappyNormal;
            
             // On désactive l'objet pack de vie
            collisionTrue.gameObject.SetActive(false);
            Invoke("ActivePackVie", 5f);

            // joue le clip qui se trouve dans la variable sonPack
           sourceAudio.PlayOneShot(sonPack, 1f);

            // On change l'état de Flappy à normal
           etatFlappyBlesse = false;

            compteur = compteur + 5f;
            UpdatePointage();



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

             // joue le clip qui se trouve dans la variable sonChamp
            sourceAudio.PlayOneShot(sonChamp, 1f);

            compteur = compteur + 10f;
            UpdatePointage();
        }
        else if(collisionTrue.gameObject.name == "Decor")
        {
            compteur = compteur -5f;
            UpdatePointage();
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
    void FinPartie()
    {
      SceneManager.LoadScene("SceneFlappy4");
    }
    void UpdatePointage()
    {
        textPointage.text = "Pointage: " + compteur.ToString();
    }


}


