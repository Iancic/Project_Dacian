using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.FilePathAttribute;
using static UnityEngine.UI.CanvasScaler;

public class SpawnerController : MonoBehaviour
{
    public GameObject Unit;
    public Unit Warrior;

    public void SpawnWarrior()
    {
        if (CurrencyController.Instance.coins >= 100)
        {

        CurrencyController.Instance.ExchangeCoins(-100);
        GameObject instance = Instantiate(Unit, new Vector3(1f, 2f, 0f), Quaternion.identity);

        UnitRTS prefabScript = instance.GetComponent<UnitRTS>();

        if (prefabScript != null)
        {
            prefabScript.unit = Warrior;
            prefabScript.LoadUnitData();
        }
        else
        {
            Debug.LogError("UnitRTS component not found on the instantiated object.");
        }

        }
    }
}
