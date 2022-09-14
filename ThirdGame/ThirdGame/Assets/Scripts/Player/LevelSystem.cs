using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EG;
using TMPro;

public class LevelSystem : MonoBehaviour
{
    public Slider slider;
    public GameObject NinjaModel;
    public GameObject Skeloton;
    public GameObject Sword;
    public TextMeshProUGUI levelText;
    public int level;
    public float currentXp;
    public float requiredXp;
    [Header("Multiplier")]
    [Range(1f, 300f)]
    public float additionMultiplier = 300;
    [Range(2f, 4f)]
    public float powerMultiplier = 2;
    [Range(7f, 14f)]
    public float divisionMultiplier = 7;

    
    void Start()
    {
        currentXp = 0;
        slider.value = currentXp;

        requiredXp = CalculateRequiredXp();
        slider.maxValue = requiredXp;

        levelText.text = "Level " + level;
    }

    void Update()
    {
        UpdateXpUi();

        if(currentXp > requiredXp)
        {
            LevelUp();
        }
    }

    private void UpdateXpUi()
    {
        slider.value = currentXp;
        slider.maxValue = requiredXp;
    }
    
    public void GainExperienceFlateRate(float xpGained)
    {
        currentXp += xpGained;
    }

    public void GainExperienceScalable(float xpGained, int passedLevel)
    {
        if(passedLevel < level)
        {
            float multiplier = 1 + (level - passedLevel) * 0.1f;
            currentXp += xpGained * multiplier;
        }
        else
        {
            currentXp += xpGained;
        }
    }

    private void LevelUp()
    {
        level++;
        slider.value = 0;
        currentXp = Mathf.RoundToInt(currentXp - requiredXp);
        // boost playerStats
        NinjaModel.GetComponent<PlayerStats>().IncreaseMaxHealthLevel();
        //Skeloton.GetComponent<EnemyStats>().IncreaseMaxHealthLevel(); // causes bug due to destoying the GameO; how to increase All ENEMYS STATS?
        Sword.GetComponent<DamageCollider>().IncreaseDamage();
        requiredXp = CalculateRequiredXp();
        levelText.text = "Level " + level;
    }

    private int CalculateRequiredXp()
    {
        int solveForRequiredXp = 0;
        for (int levelCycle = 1; levelCycle <= level; levelCycle++)
        {
            solveForRequiredXp += (int)Mathf.Floor(levelCycle + additionMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));
        }
        slider.maxValue = requiredXp;
        return solveForRequiredXp / 4;
    }
}
