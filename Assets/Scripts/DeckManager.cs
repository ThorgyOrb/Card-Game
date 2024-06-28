using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public GameObject[] deck = new GameObject[40];
    public GameObject[] hand = new GameObject[5];

    // Start is called before the first frame update
    void Start()
    {
        // Barajar el deck
        for (int i = 0; i < deck.Length - 1; i++)
        {
            int randomIndex = Random.Range(i, deck.Length);
            GameObject temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }

        // Asignar las primeras 5 cartas del deck a hand
        for (int i = 0; i < hand.Length; i++)
        {
            hand[i] = deck[i];
            deck[i] = null;
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            DrawCard();
        }
        
    }

    void DrawCard()
    {
        int emptyIndex = 0;

for (int i = 0; i < hand.Length; i++)
{
    if (hand[i] == null)
    {
        for (int j = i + 1; j < hand.Length; j++)
        {
            if (hand[j] != null)
            {
                hand[i] = hand[j];
                hand[j] = null;
                
                break;
            }
        }
        emptyIndex++;
    }
}


        if (emptyIndex > 0)
        {
            for (int i = 0; i < deck.Length; i++)
            {
                if (deck[i] != null)
                {
                    for (int j = 0; j < hand.Length; j++)
                    {
                        if (hand[j] == null)
                        {
                            hand[j] = deck[i];
                            deck[i] = null;
                        }
                    }                
                }
            }

        }


    }
}
