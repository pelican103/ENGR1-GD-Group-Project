using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillManager : MonoBehaviour
{

    //Base stats
    float speed = 6;
    int rollCD = 50;
    int rollDistance = 600;
    int rollSpeed = 12;
    int rollIframes = 6;
    int attackCD = 6;
    float attackDMG = 6;
    int attackLinger = 6;

    public static SkillManager Instance;
    
    public int totalSkillPoints = 3;
    public int skillPoints = 3;
    public TextMeshProUGUI skillPointText;

    public TextMeshProUGUI tooltipText;
    public string defaultTooltip = "Hover over a skill to see its effects.";

    // Dictionary of all skills: nodeID → data
    private Dictionary<string, SkillData> skillMap;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        InitializeSkillMap();
        ShowDefaultTooltip();
        UpdateSkillPointText();
    }

    private void InitializeSkillMap()
    {
        skillMap = new Dictionary<string, SkillData>()
        {
            {
    "Body 1", new SkillData(
        "Healthy I",
        "Increases your max HP.",
        SkillStatType.HP,
        1
    )
},
{
    "Body 2", new SkillData(
        "Healthy II",
        "Increases your max HP.",
        SkillStatType.HP,
        1
    )
},
{
    "Body 3", new SkillData(
        "Healthy III",
        "Increases your max HP.",
        SkillStatType.HP,
        1
    )
},
{
    "Body 4", new SkillData(
        "Healthy IV",
        "Increases your max HP.",
        SkillStatType.HP,
        1
    )
},
{
    "Body 5", new SkillData(
        "Fleet Feet I",
        "Increases your movement speed.",
        SkillStatType.SPD,
        1
    )
},
{
    "Body 6", new SkillData(
        "Fleet Feet II",
        "Increases your movement speed.",
        SkillStatType.SPD,
        1
    )
},
{
    "Body 7", new SkillData(
        "Fleet Feet III",
        "Increases your movement speed.",
        SkillStatType.SPD,
        1
    )
},
{
    "Body 8", new SkillData(
        "Fleet Feet IV",
        "Increases your movement speed.",
        SkillStatType.SPD,
        1
    )
},
{
    "Lamp 1", new SkillData(
        "Better Batteries I",
        "Increases your battery capacity.",
        SkillStatType.Battery,
        1
    )
},
{
    "Lamp 2", new SkillData(
        "Better Batteries II",
        "Increases your battery capacity.",
        SkillStatType.Battery,
        1
    )
},
{
    "Lamp 3", new SkillData(
        "Longer Light I",
        "Increases your flashlight distance.",
        SkillStatType.LightDistance,
        1
    )
},
{
    "Lamp 4", new SkillData(
        "Longer Light II",
        "Increases your flashlight distance.",
        SkillStatType.LightDistance,
        1
    )
},
{
    "Lamp 5", new SkillData(
        "Convalescent Circle I",
        "Increases your healing radius.",
        SkillStatType.Radius,
        1
    )
},
{
    "Lamp 6", new SkillData(
        "Convalescent Circle II",
        "Increases your healing radius.",
        SkillStatType.Radius,
        1
    )
},
{
    "Pen 1", new SkillData(
        "Hastier Handle I",
        "Increases attack speed.",
        SkillStatType.AtkSpeed,
        1
    )
},
{
    "Pen 2", new SkillData(
        "Hastier Handle II",
        "Increases attack speed.",
        SkillStatType.AtkSpeed,
        1
    )
},
{
    "Pen 3", new SkillData(
        "Sharper Stab I",
        "Increases attack damage.",
        SkillStatType.AtkDmg,
        1
    )
},
{
    "Pen 4", new SkillData(
        "Sharper Stab II",
        "Increases attack damage.",
        SkillStatType.AtkDmg,
        1
    )
},
{
    "Pen 5", new SkillData(
        "Scopic Spring I",
        "Increases attack range.",
        SkillStatType.AtkRange,
        1
    )
},
{
    "Shoe 1", new SkillData(
        "Longer Lunge I",
        "Increases roll distance.",
        SkillStatType.RollDistance,
        1
    )
},
{
    "Shoe 2", new SkillData(
        "Longer Lunge II",
        "Increases roll distance.",
        SkillStatType.RollDistance,
        1
    )
},
{
    "Shoe 3", new SkillData(
        "Rapid Recovery I",
        "Increases speed boost on roll.",
        SkillStatType.SpdBoost,
        1
    )
},
{
    "Shoe 4", new SkillData(
        "Rapid Recovery II",
        "Increases speed boost on roll.",
        SkillStatType.SpdBoost,
        1
    )
},
{
    "Shoe 5", new SkillData(
        "Rapid Recovery III",
        "Increases speed boost on roll.",
        SkillStatType.SpdBoost,
        1
    )
},
{
    "Shoe 6", new SkillData(
        "Stronger Stamina I",
        "Reduces roll cooldown.",
        SkillStatType.RollCD,
        -1
    )
},
{
    "Shoe 7", new SkillData(
        "Stronger Stamina II",
        "Reduces roll cooldown.",
        SkillStatType.RollCD,
        -1
    )
},
{
    "Shoe 8", new SkillData(
        "Stronger Stamina III",
        "Reduces roll cooldown.",
        SkillStatType.RollCD,
        -1
    )
},
{
    "Shoe 9", new SkillData(
        "Elevated Evasion I",
        "Increases roll invulnerability duration.",
        SkillStatType.Invulnerability,
        1
    )
},
{
    "Shoe 10", new SkillData(
        "Elevated Evasion II",
        "Increases roll invulnerability duration.",
        SkillStatType.Invulnerability,
        1
    )
}
        };
    }

    public void ShowNodeTooltip(string nodeID)
    {
        if (!skillMap.TryGetValue(nodeID, out SkillData data))
        {
            tooltipText.text = "Unknown Skill.";
            return;
        }

        tooltipText.text =
            $"{data.nodeName}\n" +
            $"{data.description}\n" +
            $"{data.statType} ";
        tooltipText.text += (data.amount > 0) ? "+" : "-";
        tooltipText.text += $"{Math.Abs(data.amount)}";


    }

    public void ShowDefaultTooltip()
    {
        tooltipText.text = defaultTooltip;
    }

    public void RefreshPlayerStats()
    {
        Player p = Player.Instance;
        p.speed           =   speed;
        p.rollCD          =   rollCD;
        p.rollDistance    =   rollDistance;
        p.rollSpeed       =   rollSpeed;
        p.rollIframes     =   rollIframes;
        p.attackCD        =   attackCD;
        p.attackDMG       =   attackDMG;
        p.attackLinger    =   attackLinger;
        foreach (KeyValuePair<string, SkillData> pair in skillMap)
        {
            if (pair.Value.enabled)
            {
                switch (pair.Value.statType)
                {
                    case SkillStatType.HP:
                        break;
                    case SkillStatType.SPD:
                        p.speed++;
                        break;
                    case SkillStatType.Battery:
                        break;
                    case SkillStatType.LightDistance:
                        break;
                    case SkillStatType.Radius:
                        break;
                    case SkillStatType.AtkSpeed:
                        p.attackCD--;
                        break;
                    case SkillStatType.AtkDmg:
                        p.attackDMG++;
                        break;
                    case SkillStatType.AtkRange:
                        p.attackLinger++;
                        break;
                    case SkillStatType.RollDistance:
                        p.rollDistance++;
                        break;
                    case SkillStatType.SpdBoost:
                        p.rollSpeed++;
                        break;
                    case SkillStatType.RollCD:
                        p.rollCD--;
                        break;
                    case SkillStatType.Invulnerability:
                        p.rollIframes++;
                        break;
                }
            }
        }

    }

    //    SKILL POINT SPENDING
    public bool CanAffordSkill()
    {
        return skillPoints > 0;
    }

    public void SpendSkillPoint(string nodeID)
    {
        if (skillMap[nodeID].enabled) return;
        skillMap[nodeID].enabled = true;
        skillPoints--;
        UpdateSkillPointText();
    }

    public void RefundSkillPoint(string nodeID)
    {
        if (!skillMap[nodeID].enabled) return;
        skillMap[nodeID].enabled = false;
        skillPoints++;
        UpdateSkillPointText();
    }

    private void UpdateSkillPointText()
    {
        if (skillPointText != null)
            skillPointText.text = "Skill Points: " + skillPoints;
    }
}

// Skill Data Container
[System.Serializable]
public class SkillData
{
    public string nodeName;
    public string description;
    public SkillStatType statType;
    public int amount;
    public bool enabled;

    public SkillData(string nodeName, string description, SkillStatType statType, int amount)
    {
        this.nodeName = nodeName;
        this.description = description;
        this.statType = statType;
        this.amount = amount;
        this.enabled = false;
    }
}


public enum SkillStatType
{
    // Body
    HP,
    SPD,

    // Lamp
    Battery,
    LightDistance,
    Radius,

    // Pen
    AtkSpeed,
    AtkDmg,
    AtkRange,

    // Shoe
    RollDistance,
    SpdBoost,
    RollCD,
    Invulnerability
}
