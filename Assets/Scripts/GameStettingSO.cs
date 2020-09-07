using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SettingsFile", menuName = "SettingsFile")]
public class GameStettingSO : ScriptableObject
{
    public float masterVolume;
    public float musicVolume;
    public float fxVolume;
}
