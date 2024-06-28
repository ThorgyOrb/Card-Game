using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class BuildDeck : MonoBehaviour
{   


    public TMP_Text nombre;
    public TMP_Text numeroC;
    public TMP_Text ataque;
    public TMP_Text defensa;
    public TMP_Text totalC;
    public Image icon;
    //get gameobject
    public GameObject card;
    public GameObject[] deck = new GameObject[17]; 
    //public cards[] cardsScriptD = new cards[17]; 

    public CardsInventory  inventory;
   // public Deck decka;
    public GameObject[] deck1 = new GameObject[40]; 

    public GameObject[] deck2 = new GameObject[40]; 

    public AudioSource err;
    public AudioSource ok;

     
    
    // Start is called before the first frame update
    void Start()
    {
       
        
        //create a deck of cards
        for (int i = 0; i < 17; i++)
        {
            TMP_Text nam = Instantiate(nombre, transform.position, Quaternion.identity);
            TMP_Text no = Instantiate(numeroC, transform.position, Quaternion.identity);
            TMP_Text atk = Instantiate(ataque, transform.position, Quaternion.identity);
            TMP_Text def = Instantiate(defensa, transform.position, Quaternion.identity);
            deck[i] = Resources.Load<GameObject>("Prefabs/"+(i+1));

            //create a new card
           // cardsScriptD[i] = deck[i].GetComponent<cards>();

            GameObject newCard = Instantiate(card, transform.position, Quaternion.identity);

            //set the parent of the new card to the deck
            newCard.transform.SetParent(transform);

            //set parent of a object in the scene
            //newCard.transform.parent = GameObject.Find("Content").transform;
            //keep the size of the card the same
            newCard.transform.localScale = new Vector3(1, 1, 1);
            //set text in TMP
            nam.transform.SetParent(newCard.transform);
            nam.transform.localScale = new Vector3(1, 1, 1);
            no.transform.SetParent(newCard.transform);
            no.transform.localScale = new Vector3(1, 1, 1);
            atk.transform.SetParent(newCard.transform);
            atk.transform.localScale = new Vector3(1, 1, 1);
            def.transform.SetParent(newCard.transform);
            def.transform.localScale = new Vector3(1, 1, 1);
  
            //change the position of the TMP
            nam.transform.localPosition = new Vector3(-300, -19, 0);
            no.transform.localPosition = new Vector3(-355,-20, 0);
            atk.transform.localPosition = new Vector3(330, -15, 0);
            def.transform.localPosition = new Vector3(330, -22, 0);
    
           
            //set the name of the new card
            newCard.name = "" + (i+1);

            //keep the size of the card the same
            newCard.transform.localScale = new Vector3(1, 1, 1);
            //save cardscript
            
           /* nam.text = cardsScriptD[i].cardName;
            no.text = ""+cardsScriptD[i].cardNumber;
            atk.text = ""+cardsScriptD[i].atack;
            def.text = ""+cardsScriptD[i].defence;*/
            //save the name of the card
           // namex[i] = newCard.GetComponent<nameOfCards>();
        }
       
       Debug.Log("total de cartas: "+inventory.totalCards);

        
  



        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 40; i++)
        {
            deck1[i] = inventory.deck[i];

        }
        OnClick();
        totalC.text = "Deck " + inventory.totalCards + "/40";

    }
  
    //Button click event accion kistener
    public void OnClick()
    {
        int actCard = inventory.totalCards;        //if the mouse is over the deck
        if (EventSystem.current.IsPointerOverGameObject() )
        {
            //if the mouse is over the deck
            if (Input.GetMouseButtonDown(0))
            {
                
                //if the obhect has tag card
                if (EventSystem.current.currentSelectedGameObject.tag == "card")
                {
                    string name = EventSystem.current.currentSelectedGameObject.name;
                //show the name of the object clicked
                    Debug.Log(name);
                    Debug.Log("total de cartas: "+inventory.totalCards);
                    for(int i = 0; i < 40; i++)
                    {
                         if(inventory.deck[i] == null)
                        {
                            //chcecj if there ara 3 cards of the same type
                            int count = 0;
                            for(int j = 0; j < 40; j++)
                            {
                                if(inventory.deck[j] != null)
                                {
                                    if(inventory.deck[j].name == name)
                                    {
                                        count++;
                                    }
                                }
                                
                            }
                            if(count < 3)
                            {
                                inventory.deck[i] = Resources.Load<GameObject>("Prefabs/"+name);
                                Debug.Log("carta: "+inventory.deck[i].name);
                                inventory.totalCards++;
                                ok.Play();
                                break;
                            }else
                            {
                                Debug.Log("No mas de 3 cartas del mismo tipo");
                                //play sound error
                                err.Play();
                            }
                         
                        }else
                        {
                            Debug.Log("no hay espacio");
                            
                            //err.Play();
                        }
                        if(inventory.totalCards == 40)
                        {
                            //Debug.Log("no hay espacio");
                            err.Play();
                            break;
                        }
                        
                    }

                }else
                {
                    Debug.Log("no card");
                    
                } 
            }
        }
    }



        
    
 
   
   
  
}
