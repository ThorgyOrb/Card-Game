using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class FusionController : MonoBehaviour
{
    public GameObject[] fusionObjects;
    public GameObject[] fusionResultObjects;
    public ScriptableObject[] fusionObjectsScriptable;
    public GameObject FusionList;

    private CompatibilityList compatibilityList;
    private bool fusionObjectsReady = false;
    private Vector3 fusionPosition;

    public GameController gameController;

    void Start()
    {
        LoadCompatibilities();
    }

    void Update()
    {
        if(gameController.isFusionReady && fusionObjectsReady && Input.GetKeyDown(KeyCode.Space) && gameController.faseGame == 2)
        //if (fusionObjectsReady && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("...");
            Debug.Log("Cartas seleccionadas Para la fusion: ");
            foreach (GameObject fusionObject in fusionObjects)
            {
                MaterialFusion materialFusion = fusionObject.GetComponent<MaterialFusion>();
                Debug.Log("ID: " + materialFusion.fusionID + " - Name: " + materialFusion.objectName);
            }
            InvokeObject(fusionObjects);
            fusionObjectsReady = false; 
        }
    }

    public void SetFusionObjects(GameObject[] selectedObjects)
    {
        fusionObjects = selectedObjects;
        fusionObjectsReady = true;
    }  
    public void SetFusionPosition(Vector3 position)
    {
        fusionPosition = position; 
    }


    public GameObject SendFusionResult()
    {
        LoadCompatibilities();
        GameObject result = null;
        return result;
    }

    void InvokeObject(GameObject[] objects)
{
    int idResult = GetFusionResultID(objects);
    if (idResult != 0)
    {
        string prefabPath = "Prefabs/" + idResult;
        GameObject fusionPrefab = Resources.Load<GameObject>(prefabPath);
        if (fusionPrefab != null)
        {
            // Usar la posición de fusión correcta al instanciar el objeto
            GameObject fusionResult = Instantiate(fusionPrefab, fusionPosition, Quaternion.identity);
            fusionResult.transform.Rotate(Vector3.right, 90);
            fusionResult.transform.position += new Vector3(0, 0.6f, 0);
            MaterialFusion fusionMaterial = fusionResult.GetComponent<MaterialFusion>();
            if (fusionMaterial != null)
            {
                fusionMaterial.fusionID = idResult;
                fusionMaterial.objectName = "Resultado de Fusión";
            }
        }
        else
        {
            Debug.LogError("No se encontró el prefab de fusión con ID: " + idResult + " en la carpeta Resources.");
        }
    }
    else
    {
        if (objects.Length > 0)
        {
            GameObject nonFusionObject = objects[objects.Length - 1]; 
            nonFusionObject.transform.position = fusionPosition;
            nonFusionObject.transform.Rotate(Vector3.right, 90);
            nonFusionObject.transform.position += new Vector3(0, 0.6f, 0);
            nonFusionObject.transform.Find("Cube").gameObject.SetActive(false);
            Debug.Log("No se produjo fusión. La carta ha sido movida a la posición seleccionada.");
        }
    }
}


    public GameObject SpawnFusionResult(int fusionID)
    {
        GameObject fusionResult = null;

        string prefabPath = "FusionResults/" + fusionID;

        Debug.Log("Prefab Path: " + prefabPath);
        GameObject fusionPrefab = Resources.Load<GameObject>(prefabPath);

        if (fusionPrefab != null)
        {
            fusionResult = Instantiate(fusionPrefab);
            MaterialFusion fusionMaterial = fusionResult.GetComponent<MaterialFusion>();

            if (fusionMaterial != null)
            {
                fusionMaterial.fusionID = fusionID;
                fusionMaterial.objectName = "Resultado de Fusión";
            }
        }
        else
        {
            Debug.LogError("No se encontró el prefab de fusión con ID: " + fusionID + " en la carpeta Resources.");
        }

        return fusionResult;
    }

    int GetFusionResultID(GameObject[] objects)
    {
        int idResult = 0;
        bool isFusion = false;
        for (int i = 0; i < objects.Length - 1; i++)
        {
            MaterialFusion material1 = null;
            MaterialFusion material2 = null;
            if (isFusion)
            {
                material1 = SpawnFusionResult(idResult).GetComponent<MaterialFusion>();
                Debug.Log("Material 1: " + material1.objectName);
                Debug.Log("Material 1 ID: " + material1.fusionID);
                material2 = objects[i + 1].GetComponent<MaterialFusion>();
            }
            else
            {
                material1 = objects[i].GetComponent<MaterialFusion>();
                material2 = objects[i + 1].GetComponent<MaterialFusion>();
            }
            if (material1 == null || material2 == null)
            {
                Debug.LogError("Uno de los objetos no tiene el componente MaterialFusion.");
                return -1;
            }

            var compatibility = compatibilityList.compatibilities.FirstOrDefault(c =>
                (c.id1 == material1.fusionID && c.id2 == material2.fusionID) ||
                (c.id1 == material2.fusionID && c.id2 == material1.fusionID));

            if (compatibility != null)
            {
                idResult = compatibility.resultID;
                Debug.Log("obj1: " + material1.objectName + " - obj2: " + material2.objectName + " - Result: " + compatibility.resultID);
                Debug.Log("Fusion:" + compatibility.resultID);
                Debug.Log("Objeto predominate: " + idResult);
                Destroy(material1.gameObject);
                Destroy(material2.gameObject);
                isFusion = true;
            }
            else
            {
                idResult = material2.fusionID;
                Debug.Log("obj1: " + material1.objectName + " - obj2: " + material2.objectName);
                Debug.Log("No hay fusion");
                Debug.Log("Objeto predominate: " + idResult);
                Destroy(material1.gameObject);
                isFusion = false;
                if (i == objects.Length - 2)
                {
                    idResult = 0;
                }
            }
        }
        Debug.Log("ID Result final: " + idResult);
        return idResult;
    }

    bool AreObjectsCompatible(GameObject[] objects)
    {
        if (objects.Length < 2)
        {
            return false;
        }

        for (int i = 0; i < objects.Length - 1; i++)
        {
            MaterialFusion materialFusion = objects[i].GetComponent<MaterialFusion>();
            MaterialFusion materialFusion2 = objects[i + 1].GetComponent<MaterialFusion>();
            if (!IsCompatible(objects[i], objects[i + 1]))
            {
                Debug.Log(materialFusion.objectName + " y " + materialFusion2.objectName + " no son compatibles");
            }
            else
            {
                Debug.Log(materialFusion.objectName + " y " + materialFusion2.objectName + " son compatibles");
            }
        }
        return true;
    }

    bool IsCompatible(GameObject obj1, GameObject obj2)
    {
        MaterialFusion material1 = obj1.GetComponent<MaterialFusion>();
        MaterialFusion material2 = obj2.GetComponent<MaterialFusion>();
        Debug.Log("Material 1: " + material1.objectName + " - Material 2: " + material2.objectName);
        Debug.Log("Material 1 ID: " + material1.fusionID + " - Material 2 ID: " + material2.fusionID);
        return compatibilityList.compatibilities.Any(c =>
            (c.id1 == material1.fusionID && c.id2 == material2.fusionID) ||
            (c.id1 == material2.fusionID && c.id2 == material1.fusionID));
    }

    void LoadCompatibilities()
    {
        TextAsset jsonText = Resources.Load<TextAsset>("compatibilities");
        if (jsonText != null)
        {
            compatibilityList = JsonUtility.FromJson<CompatibilityList>(jsonText.text);
        }
        else
        {
            Debug.LogError("No se pudo cargar el archivo de compatibilidades.");
        }
    }
}
