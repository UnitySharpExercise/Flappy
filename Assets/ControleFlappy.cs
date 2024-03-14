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

     public GameObject elementGrille; // objet grille
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

    public TextMeshProUGUI textFinDuJeu; // texte en fin du jeu
    public TextMeshProUGUI textPointage; // texte du pointage 

    float compteur = 0; // compteur pour le pointage



    // Appelé au début du jeu
    void Start()
    {
        sourceAudio = GetComponent<AudioSource>(); //va chercher le son
        partieTerminee = false; //initialise la variable partieTerminee à faux
        etatFlappyBlesse = false; //initialise la variable etatFlappyBlesse à faux    
        textFinDuJeu.text = "Flappy est mort..."; //va chercher le texte fin du jeu et le modifie
        textFinDuJeu.GetComponent<TextMeshProUGUI>().fontSize = 0; //va chercher le texte  et modifie la taille
    }

     // Fonction qui gère les déplacements et le saut du personnage à l'aide des touches A, D et W.
    void Update()
    {
        //si la partie n'est pas terminée
        if(partieTerminee == false)
        {
            //On ajuste la variable vitesseX si la touche "a" ou "right" est appuyée
            if(Input.GetKey("a") || Input.GetKey("right"))
            {
                //déplacement de Flappy 
                transform.Translate(vitesseX, 0, 0);   

            }
            //On ajuste la variable vitesseX si la touche "d" ou "left" est appuyée
            if(Input.GetKey("d") || Input.GetKey("left"))
            {
                //déplacement de Flappy 
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
            //On ajuste la variable vitesseY si la touche "w" ou "up" est relâchée
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
                // On récupère la vélocité y actuelle
                vitesseY = GetComponent<Rigidbody2D>().velocity.y;

            }
            // On ajuste la vélocité de Flappy
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, vitesseY);
        }
        //si la partie est terminée on ne peut plus bouger Flappy
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
                // On change l'état de Flappy à blessé
                etatFlappyBlesse = true;

            }
            // Si ce n'est pas le cas
            else
            {

                // On joue le clip qui se trouve dans la variable SonFinPartie
                sourceAudio.PlayOneShot(SonFinPartie, 1f);
                // On arrête la partie
                partieTerminee = true;
                // On change le texte de fin de jeu
                textFinDuJeu.GetComponent<TextMeshProUGUI>().fontSize += 100;
                // On arrête la rotation de Flappy
                GetComponent<Rigidbody2D>().freezeRotation = false;
                // On change la vélocité de Flappy
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5);
                // On désactive le collider de Flappy
                GetComponent<Collider2D>().enabled = false;
                // On appelle la fonction FinPartie après 2 secondes que le temps que le son se joue
                Invoke("FinPartie", 3f); 

            }
            // si Flappy touche une colonne
            if (collisionTrue.gameObject.name == "Colonne")
            {

                // on enlève 5 points au compteur
                compteur = compteur -5f;
                // on appelle la fonction UpdatePointage
                UpdatePointage();

            }

        }
        // Si Flappy touche une pièce d'or
        else if(collisionTrue.gameObject.name == "PieceOr")
        {

             // On désactive l'objet pièce d'or
            collisionTrue.gameObject.SetActive(false);
            // On appelle la fonction ActivePieceOr après 5 secondes
            Invoke("ActivePieceOr", 5f);
            // joue le clip qui se trouve dans la variable sonOr
           sourceAudio.PlayOneShot(sonOr, 1f);
            // on ajoute 5 points au compteur
            compteur = compteur + 5f;
            // on appelle la fonction
            UpdatePointage();
            // on appelle la fonction ActivePieceOr après 5 secondes
            elementGrille.GetComponent<Animator>().enabled = true;  
    
        }
        // Si Flappy touche un pack de vie
        else if(collisionTrue.gameObject.name == "PackVie")
        {

             // On change l'image de Flappy
            GetComponent<SpriteRenderer>().sprite = flappyNormal;
             // On désactive l'objet pack de vie
            collisionTrue.gameObject.SetActive(false);
            // On appelle la fonction ActivePackVie après 5 secondes
            Invoke("ActivePackVie", 5f);
            // joue le clip qui se trouve dans la variable sonPack
           sourceAudio.PlayOneShot(sonPack, 1f);
            // On change l'état de Flappy à normal
           etatFlappyBlesse = false;
            // on ajoute 5 points au compteur
            compteur = compteur + 5f;
            // on appelle la fonction
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
            // On appelle la fonction ActiveChampignon après 5 secondes
            Invoke("ActiveChampignon", 5f);
             // joue le clip qui se trouve dans la variable sonChamp
            sourceAudio.PlayOneShot(sonChamp, 1f);
             // on ajoute 10 points au compteur
            compteur = compteur + 10f;
             // on appelle la fonction
            UpdatePointage();

        }
        // Si Flappy touche un décor
        else if(collisionTrue.gameObject.name == "Decor")
        {

            // On joue enleve 5 points au compteur
            compteur = compteur -5f;
            // on appelle la fonction
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

         // On appelle la fonction ActivePieceOr après 5 secondes
         elementGrille.GetComponent<Animator>().enabled = false;  

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

    // Fonction pour la fin de la partie
    void FinPartie()
    {

         // On change la scène pour recommencer le jeu
         SceneManager.LoadScene("SceneFlappy6");

    }

    // Fonction pour la mise à jour du pointage
    void UpdatePointage()
    {
         // On change le texte du pointage
         textPointage.text = "Pointage: " + compteur.ToString();
 
    }

}


