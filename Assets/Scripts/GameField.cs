using UnityEngine;

public class GameField : MonoBehaviour
{
    public Transform[] monsterPositions;
    public Transform[] magicPositions;

    void Awake()
    {
        // Asignar posiciones de monstruos
        Transform monsterPlayer = transform.Find("MonstersPlayer");
        if (monsterPlayer != null)
        {
            monsterPositions = new Transform[5];
            for (int i = 0; i < 5; i++)
            {
                monsterPositions[i] = monsterPlayer.Find((i + 1).ToString());
            }
        }
        else
        {
            Debug.LogError("No se encontró el objeto MonsterPlayer en el campo.");
        }

        // Asignar posiciones de magias
        Transform spellPlayer = transform.Find("SpellPlayer");
        if (spellPlayer != null)
        {
            magicPositions = new Transform[5];
            for (int i = 0; i < 5; i++)
            {
                magicPositions[i] = spellPlayer.Find((i + 1).ToString());
            }
        }
        else
        {
            Debug.LogError("No se encontró el objeto SpellPlayer en el campo.");
        }
    }
}
