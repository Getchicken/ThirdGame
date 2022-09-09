using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArchivedCode : MonoBehaviour
{
    public class GameManager : MonoBehaviour
    {
        public GameObject damageTextPrefab, enemyInstance;
        public string textToDisplay;

    

        public void ShowDamageNumbers()
        {
            GameObject DamageTextInstance = Instantiate(damageTextPrefab, enemyInstance.transform);
            DamageTextInstance.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(textToDisplay);
        }
    }
}
