using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public List<GunData> allGunData = new List<GunData>();
    public List<GunData> playerGunData = new List<GunData>();

    [SerializeField] PlayerObject playerData;
    public PlayerObject PlayerData { get { return playerData; } }

    //SINGLETON
    private static Global instance;
    public static Global Instance
    {
        get { return instance; }
        set { instance = value; }
    }

   

    //SINGLETON
    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }

    private void Start()
    {
        playerGunData = new List<GunData>(allGunData);
    }
}

public enum UpgradeType { PlayerUpgrade, GunUpgrade, Ultimate, ItemUnlock }
public enum UpgradeProperty { Damage, FireRate, Health, Ammo, Speed, BoostDuration, BoostRefill, Unlock }
public enum UnlockableType { Gun, Ultimate }
public enum UltimateUpgrade { Charges, Duration, Damage, Unlock }
