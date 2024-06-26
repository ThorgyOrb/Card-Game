using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaterialFusion : MonoBehaviour
{
    public int fusionID;
    public string objectTipe;
    public string objectName;
    public int objectDefence;
    public int objectAtack;
    private TextMeshPro attackText;
    private TextMeshPro defenseText;
    public string[] guardianStar = new string[2];
    public string selectedGuardianStar;

    void Start()
    {
        UpdateTextMeshValues();
        attackText = transform.Find("ATK").GetComponent<TextMeshPro>();
        defenseText = transform.Find("DEF").GetComponent<TextMeshPro>();
        objectDefence = int.Parse(defenseText.text);
        objectAtack = int.Parse(attackText.text);
        UpdateTextMeshValues();
     
    }

    void Update()
    {
        
    }
     private void UpdateTextMeshValues()
    {
        if (attackText != null)
            attackText.text = "" + objectAtack.ToString();

        if (defenseText != null)
            defenseText.text = "" + objectDefence.ToString();
    }
}
