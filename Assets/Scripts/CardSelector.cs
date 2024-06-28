using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
public class CardSelector : MonoBehaviour
{
    public GameObject[] cards;
    public GameObject selector; 
    private int currentIndex = 0; 
    public List<GameObject> fusionObjects = new List<GameObject>(); 
    public int selectorIndex = 0; 
    private GameController gameController;
    bool isFirstUpdate = true;
    public bool isSelected = false;
    public Camera cameraWithAnimation;
    private Animator cameraAnimator;
    public DeckManager deckManager;

    void Start()
    {  
        float xPosition = -2.388f;
        float yPosition = 0.204f;
        float zPosition = -6.93f; 
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        deckManager = GameObject.Find("DeckManager").GetComponent<DeckManager>();

         if (deckManager != null)
    {
        for (int i = 0; i < deckManager.hand.Length; i++)
        {
            if (deckManager.hand[i] != null)
            {
                GameObject card = deckManager.hand[i];
                card.transform.position = new Vector3(xPosition, yPosition, zPosition);

                // Instanciar la carta y actualizar la referencia en `cards`
                GameObject instantiatedCard = Instantiate(card, card.transform.position, card.transform.rotation);
                cards[i] = instantiatedCard;
                xPosition += 1.18f;

               
            }
        }
    }

    if (cards.Length > 0)
    {
        MoveSelectorToCard(currentIndex); 
        SendCurrentCardData();         
    }

    if (cameraWithAnimation != null)
    {
        cameraAnimator = cameraWithAnimation.GetComponent<Animator>();
    }
    }

    void Update()
    {
        if(gameController.isSelectingCards && cards.Length == 5)
        {
            if (isFirstUpdate)
            {
                SendCurrentCardData(); 
                isFirstUpdate = false; 
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveRight();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
            SelectCards();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CardInFusionSender();
                Debug.Log("Cartas seleccionadas: ");
                foreach (var card in fusionObjects)
                {
                    Debug.Log(card.name);
                }
                // Llamada para notificar al FusionController
                FusionController fusionController = GameObject.Find("FusionController").GetComponent<FusionController>();
                fusionController.SetFusionObjects(fusionObjects.ToArray());

                if (cameraAnimator != null)
                {
                    cameraAnimator.SetBool("IsSelectedCards", true);
                }
                gameController.isSelectingCards = false;
                gameController.isSelectingPosition = true;
                gameController.faseGame = 1;
            }
            if (selector != null)
            {
                selector.transform.Rotate(Vector3.up, 100 * Time.deltaTime); 
            }
        }
    }

    void MoveRight()
    {
        if (currentIndex < cards.Length - 1)
        {
            currentIndex++;
            MoveSelectorToCard(currentIndex);
            SendCurrentCardData();
        }
    }

    void MoveLeft()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            MoveSelectorToCard(currentIndex);
            SendCurrentCardData();
        }
    }

    void MoveSelectorToCard(int index)
    {
        Vector3 cardPosition = cards[index].transform.position;
        Vector3 offset = new Vector3(-0.65f, 0.4f, 0); 
        selector.transform.position = cardPosition + offset;
    }

    void UpdateFusionIndicators()
    {
        selectorIndex = 0; 
        foreach (var card in cards)
        {
            if (fusionObjects.Contains(card))
            {
                ModifyTextMeshInCube(card, "" + (fusionObjects.IndexOf(card) + 1));
            }
            else
            {
                ModifyTextMeshInCube(card, "");
            }
        }
    }

    void SelectCards()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {     
            if (!fusionObjects.Contains(cards[currentIndex]))
            {
                fusionObjects.Add(cards[currentIndex]);
                UpdateFusionIndicators();
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (fusionObjects.Contains(cards[currentIndex]))
            {
                fusionObjects.Remove(cards[currentIndex]);
                UpdateFusionIndicators();
            }
        }
    }

    void ModifyTextMeshInCube(GameObject parent, string newText)
    {
        Transform cubeTransform = parent.transform.Find("Cube");
        if (cubeTransform != null)
        {
            cubeTransform.gameObject.SetActive(true);
            TextMeshPro textMeshPro = cubeTransform.GetComponentInChildren<TextMeshPro>(true); 
            if (textMeshPro != null)
            {
                textMeshPro.text = newText;
            }
            TextMeshProUGUI textMeshProUGUI = cubeTransform.GetComponentInChildren<TextMeshProUGUI>(true); 
            if (textMeshProUGUI != null)
            {
                textMeshProUGUI.text = newText;
            }
            if (string.IsNullOrEmpty(newText))
            {
                cubeTransform.gameObject.SetActive(false);
            }
        }
    }

    public GameObject[] CardInFusionSender()
    {
        isSelected = true;
        return fusionObjects.ToArray();
    }

    public string[] GetCurrentCardData()
    {
        GameObject currentCard = cards[currentIndex];
        if (cards.Length == 0) return new string[0];

        if (currentCard != null)
        {
            MaterialFusion materialFusion = currentCard.GetComponent<MaterialFusion>();
            if (materialFusion != null)
            {
                return new string[]
                {
                    "" + materialFusion.fusionID,
                    "" + materialFusion.objectName,
                    "" + materialFusion.objectAtack,
                    "" + materialFusion.objectDefence,
                    "" + materialFusion.guardianStar[0],
                    "" + materialFusion.guardianStar[1],
                };
            }
            else
            {
                Debug.LogWarning("MaterialFusion component not found on the current card.");
            }
        }
        else
        {
            Debug.LogWarning("Current card is null.");
        }

        return new string[0];
    }

    void SendCurrentCardData()
    {
        string[] currentCardData = GetCurrentCardData();
        if (gameController != null)
        {
            gameController.ReceiveCardData(currentCardData);
        }
        else
        {
            Debug.LogWarning("GameController not found.");
        }
    }
}
