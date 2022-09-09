using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [SerializeField] private float destroyTime;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 randomiseOffset;
    [SerializeField] private Color damageColour;
    
    private TextMeshPro damageText;

    private void Awake()
    {
        damageText = GetComponent<TextMeshPro>();
        transform.localPosition += offset;
        transform.localPosition += new Vector3(
            Random.Range(-randomiseOffset.x, randomiseOffset.x),
            Random.Range(-randomiseOffset.y, randomiseOffset.y),
            Random.Range(-randomiseOffset.z, randomiseOffset.z));
        //Destroy(gameObject, destroyTime); no need due to other script
    }

    public void Initialise(int damage)
    {
        damageText.text = damage.ToString();
    }
}