using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldSideSelector : MonoBehaviour
{
    public GameField gameField;
    private Transform selectedPosition;
    private bool isMonstersField = true; // Para controlar si está en el campo de monstruos o magias
    private int currentIndex = 0; // Índice actual de la posición seleccionada
    public Sprite indicatorSprite; // Sprite para indicar la posición seleccionada visualmente
    private GameObject indicatorObject; // Objeto para mostrar el indicador
    public FusionController fusionController; // Referencia al FusionController
    public GameController gameController;
    void Start()
    {
        // Empezar seleccionando la primera posición de monstruos
        if (gameField.monsterPositions.Length > 0)
        {
            selectedPosition = gameField.monsterPositions[0];
            Debug.Log("Monster position 1 selected.");
        }

        // Crear el objeto de indicador
        indicatorObject = new GameObject("PositionIndicator");
        indicatorObject.transform.position = selectedPosition.position;
        // Rotar el indicador 90 grados
        indicatorObject.transform.Rotate(Vector3.right, 90);
        // Mueve el indicador hacia arriba
        SpriteRenderer spriteRenderer = indicatorObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = indicatorSprite;
    }

    void Update()
    {
        if(gameController.isSelectingPosition)
        {
        // Manejar la entrada del teclado para moverse entre posiciones y cambiar de campo
        if (isMonstersField)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveLeft(gameField.monsterPositions);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveRight(gameField.monsterPositions);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                SwitchToSpellsField();
            }
        }
        else // Es el campo de magias
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveLeft(gameField.magicPositions);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveRight(gameField.magicPositions);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                SwitchToMonstersField();
            }
        }

        if (selectedPosition != null && indicatorObject != null)
        {
            indicatorObject.transform.position = selectedPosition.position;
            indicatorObject.transform.position += new Vector3(0, 0.6f, 0);
        }

        // Enviar la posición seleccionada al FusionController cuando se presiona Space
        if (Input.GetKeyDown(KeyCode.W)&& gameController.faseGame == 1)
        {
            fusionController.SetFusionPosition(selectedPosition.position);
            Debug.Log("Selected position: " + selectedPosition.name + " Position: " + selectedPosition.position);
            gameController.isFusionReady = true;
            gameController.faseGame = 2;
        }
        }
    }

    void MoveLeft(Transform[] positions)
    {
        if (positions.Length > 0)
        {
            currentIndex = (currentIndex - 1 + positions.Length) % positions.Length;
            selectedPosition = positions[currentIndex];
            Debug.Log("Position " + (currentIndex + 1) + " selected.");
        }
    }

    void MoveRight(Transform[] positions)
    {
        if (positions.Length > 0)
        {
            currentIndex = (currentIndex + 1) % positions.Length;
            selectedPosition = positions[currentIndex];
            Debug.Log("Position " + (currentIndex + 1) + " selected.");
        }
    }

    void SwitchToMonstersField()
    {
        isMonstersField = true;
        if (gameField.monsterPositions.Length > 0)
        {
            selectedPosition = gameField.monsterPositions[0];
            currentIndex = 0;
            Debug.Log("Switched to Monsters field. Monster position 1 selected.");
        }
    }

    void SwitchToSpellsField()
    {
        isMonstersField = false;
        if (gameField.magicPositions.Length > 0)
        {
            selectedPosition = gameField.magicPositions[0];
            currentIndex = 0;
            Debug.Log("Switched to Spells field. Magic position 1 selected.");
        }
    }

    public Transform GetSelectedPosition()
    {
        return selectedPosition;
    }
}
