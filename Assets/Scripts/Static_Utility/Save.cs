using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Save : MonoBehaviour
{
    [SerializeField] bool DontLoad;
    [SerializeField] bool DontSave;

    string playerStateFile;
    string gunsAndPerksFile;
    [SerializeField] SaveObject saveObject;





    private void Awake()
    {
        InitSavepath();

        if (!DontSave)
        {
            SavePlayerState();
            SaveGunsAndPerks();
        }

        if (!DontLoad)
        {
            LoadPlayerState();
            LoadGunsAndPerks();        
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitSavepath()
    {
        playerStateFile = Application.persistentDataPath + "/SavePlayer.json";
        gunsAndPerksFile = Application.persistentDataPath + "/SaveObject.json";
    }

    bool CheckPlayerSaveFile()
    {
        return File.Exists(playerStateFile);
       
    }

    bool CheckObjectSaveFile()
    {
        return File.Exists(gunsAndPerksFile);
    }

    public void SavePlayerState()
    {
        if (CheckPlayerSaveFile())
        {
            string Json = JsonUtility.ToJson(Global.Instance.PlayerData);
            File.WriteAllText(playerStateFile, Json);
        }
        else
        {
            string Json = JsonUtility.ToJson(Global.Instance.PlayerData);
            File.WriteAllText(playerStateFile, Json);
        }
    }

    public void SaveGunsAndPerks()
    {
        if (CheckObjectSaveFile())
        {
            List<string> guns = new List<string>();
            List<string> perks = new List<string>();

            foreach (GunObject gun in saveObject.guns)
            {
                guns.Add(JsonUtility.ToJson(gun));
            }

            foreach (Perk perk in saveObject.perks)
            {
                perks.Add(JsonUtility.ToJson(perk));
            }

            SaveGunsAndPerks sGunsPerks = new SaveGunsAndPerks(guns, perks);
            string Json = JsonUtility.ToJson(sGunsPerks);
            File.WriteAllText(gunsAndPerksFile, Json);
        }
        else
        {
            string Json = JsonUtility.ToJson(saveObject);
            File.WriteAllText(gunsAndPerksFile, Json);
        }
    }


    void LoadPlayerState()
    {
        if (CheckPlayerSaveFile())
        {
            string Json = File.ReadAllText(playerStateFile);
            JsonUtility.FromJsonOverwrite(Json, Global.Instance.PlayerData);
        }
    }

    void LoadGunsAndPerks()
    {
        if (CheckObjectSaveFile())
        {
            string Json = File.ReadAllText(gunsAndPerksFile);
            SaveGunsAndPerks loaded = JsonUtility.FromJson<SaveGunsAndPerks>(Json);

            for (int i = 0; i < loaded.perks.Count; i++)
            {
                JsonUtility.FromJsonOverwrite(loaded.perks[i], saveObject.perks[i]);
            }

            for (int i = 0; i < loaded.guns.Count; i++)
            {
                JsonUtility.FromJsonOverwrite(loaded.guns[i], saveObject.guns[i]);
            }
        }
    }

}

[System.Serializable]
public class SaveGunsAndPerks
{
    public List<string> guns;
    public List<string> perks;

    public SaveGunsAndPerks(){}

    public SaveGunsAndPerks(List<string> _guns, List<string> _perks)
    {
        guns = new List<string>(_guns);
        perks = new List<string>(_perks);
    }
}