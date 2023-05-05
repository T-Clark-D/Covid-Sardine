using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour
{
   
    public static void normalModeOnClick(string gameMode)
    {
        StateNameController.menuGameMode = gameMode;
        SceneManager.LoadScene("main");
    }
  
}
