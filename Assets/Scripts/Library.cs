using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class Library : MonoBehaviour, IPointerEnterHandler
{
    public GameObject card;
    public GameObject[] deck = new GameObject[17]; 
    //public cards[] cardsScriptD = new cards[17]; 
    public TMP_Text nombre;
    public TMP_Text numeroC;
    public TMP_Text ataque;
    public AudioSource move;
    public Image icon;

    //list of cards
    public List<GameObject> cards = new List<GameObject>();

    //public List <cards> cardsScript = new List<cards>(); 
   

    // Start is called before the first frame update
    void Start()
    {
        //load the cards from the resources folder

        for (int i = 0; i < 17; i++)
        {
           // deck[i] = Resources.Load<GameObject>("Prefabs/"+(i+1));

            //create a new card
           // cardsScriptD[i] = deck[i].GetComponent<cards>();

            GameObject newCard = Instantiate(card, transform.position, Quaternion.identity);
            
            //set the parent of the new card to the deck
            newCard.transform.SetParent(transform);

            //set parent of a object in the scene
            //newCard.transform.parent = GameObject.Find("Content").transform;

            //set the name of the new card
            newCard.name = "" + (i+1);

            //keep the size of the card the same
            newCard.transform.localScale = new Vector3(1, 1, 1);
            //change the source image of the card
            newCard.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cards/"+(i+1));

           // Debug.Log("Card name: "+deck[i].objectName);

            
        }

        //load the cards to the list
        for (int i = 0; i < 17; i++)
        {
            cards.Add(Resources.Load<GameObject>("Prefabs/"+(i+1)));
        }

        for (int i = 0; i < 17; i++)
        {
           // cardsScript.Add(deck[i].GetComponent<cards>());
        }

        //set false the image
        icon.enabled = false;
     

   

    }

    // Update is called once per frame
    void Update()
    {
       
    
        
        
    }

  

    public void OnPointerEnter(PointerEventData eventData)
    {

        icon.enabled = true;
        move.Play();
        string id = eventData.pointerEnter.name;
        numeroC.text = id;
        Debug.Log(id);
        int idC = int.Parse(id);
        for (int i = 0; i < 17; i++)
        {
            MaterialFusion materialFusion = cards[i].GetComponent<MaterialFusion>();
            if (idC == i+1)
            {
                nombre.text = cards[i].name;
                nombre.text = materialFusion.objectName;
                ataque.text = materialFusion.objectAtack.ToString();
                //nombre.text = cardsScriptD[i].cardName;
              //  nombre.text = cardsScript[i].cardName;
            }
        }
        icon.sprite = Resources.Load<Sprite>("Cards/"+id);

    }


   
}
