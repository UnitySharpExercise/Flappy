using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleFlappy : MonoBehaviour
{
    public float vitesseX;
    public float vitesseY;
    public Sprite flappyBlesse; 
    public Sprite flappyNormal;
    public GameObject objetPieceOr;
    public GameObject objetPackVie;
    public GameObject objetChampignon;
    public float deplacementAleatoireY ;


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
            vitesseY = 5;
        }
        else
        {
            vitesseY = GetComponent<Rigidbody2D>().velocity.y;
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, vitesseY);
    }


          
    void OnCollisionEnter2D(Collision2D collisionTrue)
    {
        //si l'objet touché est ………. alors on ………..
        if(collisionTrue.gameObject.name == "Colonne")
        {
            GetComponent<SpriteRenderer>().sprite = flappyBlesse;
        }
        else if(collisionTrue.gameObject.name == "PieceOr")
        {
            collisionTrue.gameObject.SetActive(false);
            Invoke("ActivePieceOr", 5f);
        }
        //si l'objet touché est ………. alors on ………..   
        else if(collisionTrue.gameObject.name == "PackVie")
        {
            GetComponent<SpriteRenderer>().sprite = flappyNormal;
            collisionTrue.gameObject.SetActive(false);
            Invoke("ActivePackVie", 5f);

        }
        else if(collisionTrue.gameObject.name == "Champignon")
        {
            transform.localScale *= 2f;
            Invoke("ChampignonDiminueTaille", 5f);
            collisionTrue.gameObject.SetActive(false);
            Invoke("ActiveChampignon", 5f);

        }
    }
            void ChampignonDiminueTaille()
            {
                transform.localScale /= 2f; 
            }
            void ActivePieceOr()
            {
                objetPieceOr.SetActive(true);
                float valeurAleatoireY = Random.Range(-deplacementAleatoireY, deplacementAleatoireY);
                objetPieceOr.transform.position = new Vector2(objetPieceOr.transform.position.x, valeurAleatoireY);
            }
            void ActivePackVie()
            {
                objetPackVie.SetActive(true);
                 float valeurAleatoireY = Random.Range(-deplacementAleatoireY, deplacementAleatoireY);
                objetPackVie.transform.position = new Vector2(objetPackVie.transform.position.x, valeurAleatoireY);


            }
            void ActiveChampignon()
            {
                objetChampignon.SetActive(true);
                float valeurAleatoireY = Random.Range(-deplacementAleatoireY, deplacementAleatoireY);
                objetChampignon.transform.position = new Vector2(objetChampignon.transform.position.x, valeurAleatoireY);
            }
}