using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SaveSystemEditor : EditorWindow
{


    PlayerObject defaultPlayerData;
    PlayerObject usedPlayerData;

    [MenuItem("Window/Save And Data Reset")]
    static void Init()
    {
        SaveSystemEditor window = (SaveSystemEditor)EditorWindow.GetWindow(typeof(SaveSystemEditor));
        window.Show();
    }

    void OnGUI()
    {
        defaultPlayerData = EditorGUILayout.ObjectField("Default player data: ", defaultPlayerData, typeof(PlayerObject), true) as PlayerObject;
        usedPlayerData = EditorGUILayout.ObjectField("used player data: ", usedPlayerData, typeof(PlayerObject), true) as PlayerObject;
        
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Reset Save File"))
        {
            Debug.Log("Clicked Button");
        }

        if (GUILayout.Button("Reset PlayerData"))
        {
            ResetPlayer();
        }

        if (GUILayout.Button("Reset Perks"))
        {
            ResetPerks();
        }

        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Reset All Saveed Property"))
        {
            Debug.Log("Not implemented!");
        }
    }

    void ResetPlayer()
    {
        usedPlayerData.equipedGuns = new List<GunObject>(defaultPlayerData.equipedGuns);
        usedPlayerData.availableGuns = new List<GunObject>(defaultPlayerData.availableGuns);
        usedPlayerData.unlockedUltimates = new List<Abilit_Object>(defaultPlayerData.unlockedUltimates);

        usedPlayerData.ultimate = defaultPlayerData.ultimate;


        usedPlayerData.maxHealth    = defaultPlayerData.maxHealth;
        usedPlayerData.powerCores   = defaultPlayerData.powerCores;
        usedPlayerData.level        = defaultPlayerData.level;
        usedPlayerData.speed        = defaultPlayerData.speed;
        usedPlayerData.boostFillSpeed = defaultPlayerData.boostFillSpeed;
        usedPlayerData.boostDecSpeed  = defaultPlayerData.boostDecSpeed;
    }

    void ResetPerks()
    {
        FindPerks();
    }

    void FindPerks()
    {
        string[] guids;
        guids = AssetDatabase.FindAssets("t:Perk");
        foreach (string guid in guids)
        {
            Perk perk = (Perk)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(Perk));
            perk.Reset();
        }
        Debug.Log("Perks Reseted!");
    }
}
