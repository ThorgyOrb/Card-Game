using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class CardsInventory : MonoBehaviour
{
    public TMP_Text nombre;
    public TMP_Text numeroC;
    public TMP_Text ataque;
    public TMP_Text defensa;
    public GameObject card;
    public TMP_Text totalC;
    public Image icon;
    public GameObject[] deck = new GameObject[40];  
   // public cards[] cardsScriptD = new cards[40]; 
    public GameObject newCard;
    public int totalCards = 0;

    // Start is called before the first frame update
    void Start()
    {      
        System.IO.StreamReader file = new System.IO.StreamReader("Assets/User/Deck.txt");

       
        //create a deck of cards
        for (int i = 0; i < 40; i++)
        {
            string line = file.ReadLine();
            deck[i] = Resources.Load<GameObject>("Prefabs/"+line);
           
            //save the name of the card
            //deck[i] = GameObject.Find(line);
           // cardsScriptD[i] = deck[i].GetComponent<cards>();
            //create a new card
            newCard = Instantiate(card, transform.position, Quaternion.identity);
            //instantiate TMP nombre 
            TMP_Text nam = Instantiate(nombre, transform.position, Quaternion.identity);
            TMP_Text no = Instantiate(numeroC, transform.position, Quaternion.identity);
            TMP_Text atk = Instantiate(ataque, transform.position, Quaternion.identity);
            TMP_Text def = Instantiate(defensa, transform.position, Quaternion.identity);
            //instantiate the icon
            Image ic = Instantiate(icon, transform.position, Quaternion.identity);

            


            //set the parent of the new card to the deck
            newCard.transform.SetParent(transform);


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
            ic.transform.SetParent(newCard.transform);
            //change the position of the TMP
            nam.transform.localPosition = new Vector3(-300, -19, 0);
            no.transform.localPosition = new Vector3(-355,-20, 0);
            atk.transform.localPosition = new Vector3(330, -15, 0);
            def.transform.localPosition = new Vector3(330, -22, 0);
            ic.transform.localPosition = new Vector3(200, 0, 0);
            newCard.name = "" + line;
          /*  nam.text = cardsScriptD[i].cardName;
            no.text = ""+cardsScriptD[i].cardNumber;
            atk.text = ""+cardsScriptD[i].atack;
            def.text = ""+cardsScriptD[i].defence;*/
           // ic.sprite = Resources.Load<Sprite>("Icon/"+cardsScriptD[i].cardType);
            totalCards++;
           
        }

      
        
         file.Close();
    }

    // Update is called once per frame
    void Update()
    {
        //if press S save the deck
        if (Input.GetKeyDown(KeyCode.S))
        {
            saveDeck();
        }
        
        OnClick();

        totalC.text = "Deck " + totalCards;

    }
  
  //OnCLick
    public void OnClick()
    {
        if (EventSystem.current.IsPointerOverGameObject() )
        {
            //if the mouse is over the deck
            if (Input.GetMouseButtonDown(0))
            {
                
                //if the obhect has tag card
                if (EventSystem.current.currentSelectedGameObject.tag == "card")
                {
                    string name = EventSystem.current.currentSelectedGameObject.name;
                    Debug.Log(name);
                    //Destoy the object 
                    Destroy(GameObject.Find(name));
                    //remove that card from the deck
                    for (int i = 0; i < totalCards; i++)
                    {
                        if (deck[i].name == name)
                        {
                            //erase only one card
                            deck[i] = null;
                            totalCards--;
                            Debug.Log(totalCards);
                            //move the rest of the cards
                            for (int j = i; j < totalCards; j++)
                            {
                                deck[j] = deck[j + 1];
                                deck[j + 1] = null;
                                
                            }
                            
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
  
    public void saveDeck()
    {
        //save the deck
        if(totalCards == 40)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter("Assets/User/Deck.txt");
            for (int i = 0; i < totalCards; i++)
            {
                file.WriteLine(deck[i].name);
            }
            file.Close();
            Debug.Log("Deck saved");
            
        }else
        {
            Debug.Log("You need 40 cards");
        }
        
    }

    public void deleteDeck()
    {
        //delete child of Content0 
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

    }

    public void refresh()
    {   
        totalCards = 0;
        deleteDeck();
            for (int i = 0; i < 40; i++)
            {
                //save the name of the card
               // cardsScriptD[i] = deck[i].GetComponent<cards>();
                //create a new card
                newCard = Instantiate(card, transform.position, Quaternion.identity);
                //instantiate TMP nombre 
                TMP_Text nam = Instantiate(nombre, transform.position, Quaternion.identity);
                TMP_Text no = Instantiate(numeroC, transform.position, Quaternion.identity);
                TMP_Text atk = Instantiate(ataque, transform.position, Quaternion.identity);
                TMP_Text def = Instantiate(defensa, transform.position, Quaternion.identity);
                //instantiate the icon
                Image ic = Instantiate(icon, transform.position, Quaternion.identity);

                


                //set the parent of the new card to the deck
                newCard.transform.SetParent(transform);


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
                ic.transform.SetParent(newCard.transform);
                //change the position of the TMP
                nam.transform.localPosition = new Vector3(-300, -19, 0);
                no.transform.localPosition = new Vector3(-380,-20, 0);
                atk.transform.localPosition = new Vector3(330, -15, 0);
                def.transform.localPosition = new Vector3(330, -22, 0);
                ic.transform.localPosition = new Vector3(200, 0, 0);
                newCard.name = "" + deck[i].name;
              /*  nam.text = cardsScriptD[i].cardName;
                no.text = ""+cardsScriptD[i].cardNumber;
                atk.text = ""+cardsScriptD[i].atack;
                def.text = ""+cardsScriptD[i].defence;
                ic.sprite = Resources.Load<Sprite>("Icon/"+cardsScriptD[i].cardType);*/
                totalCards++;
            
            }

    }
}
    


