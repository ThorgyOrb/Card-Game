using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public bool isSelectingCards = true;
    public bool isSelectingPosition = false;
    public bool isFusionReady = false;
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI cardAtack;
    public TextMeshProUGUI cardDefence;
    //public TextMeshProUGUI selectedGuardianStar;
    public TextMeshProUGUI[] guardianStar = new TextMeshProUGUI[2];
    public int faseGame = 0;
    // Start is called before the first frame update
    void Start()
    {
      
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
    public void ReceiveCardData(string[] cardData)
    {
        Debug.Log("Datos de la carta seleccionada: ");
        foreach (string data in cardData)
        {
            cardName.text = cardData[1];
            cardAtack.text = cardData[2];
            cardDefence.text = cardData[3];
            guardianStar[0].text = cardData[4];
            guardianStar[1].text = cardData[5];
            Debug.Log(data);
        }
        // Aquí puedes actualizar la UI o manejar los datos según necesites
    }
}
