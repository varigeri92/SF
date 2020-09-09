using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSurvival : MonoBehaviour
{
    public void StartSurvivalMode()
    {
        PresistentOptionsManager.Instance.survival = true;
        SelectedLevel.Instance.SetSurvivorlevel();
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
