using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManger : MonoBehaviour
{
    public string gameMode;

    public bool levelFinished = false;
    public GameObject points;
    private Text textbox;
    private int level;
    public GameObject canvas;
    public Text winFailMessageBox;
    public Text levelNum;
    public Text ScoreNeeded;
    public Button nextLevelButton;
    public Button tryAgainButton;
    public Button menuButton;
    private float lastSpawn;
    // Start is called before the first frame update
   
    //initialize the game mode
    void Start()
    {
        textbox = points.GetComponent<Text>();
        
        level = 1;
        canvas.SetActive(false);
        gameMode = StateNameController.menuGameMode;
        print(gameMode);
        switch (gameMode)
        {
            case "Normal": 
                initiateNormalMode(); 
                break;
            case "Endless":
                levelNum.enabled = false;
                ScoreNeeded.enabled = false;
                initiateNormalMode();
                print("we in endless case of start");
                break;
            case "Variant":
                initiateNormalMode();
                initiateNormalMode();
                break;

        }
        //initiateNormalMode();
        
        lastSpawn = Time.timeSinceLevelLoad;
    }

    // Updates the correct game mode
    void Update()
    {
        //allows to close game on escape
        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene("Menu");
        }

        switch (gameMode)
        {
            case "Normal":
                normalModeUpdate();
                break;
            case "Endless":
                endlessModeUpdate();
                break;
            case "Variant":
                normalModeUpdate();
                break;
        }
            
       
    }

    private void endlessModeUpdate()
    {
        if (levelFinished != true)
        {
            if (Time.timeSinceLevelLoad - lastSpawn >= 10)
            {
                print("spawning");
                level++;
                initiateNormalMode();
                lastSpawn = Time.timeSinceLevelLoad;
            }

            if (Convert.ToInt32(textbox.text) <= -20)
            {
                canvas.SetActive(true);
                winFailMessageBox.text = "You Lasted:" + Time.timeSinceLevelLoad;
                levelFinished = true;
                nextLevelButton.enabled = false;
                nextLevelButton.GetComponent<Image>().color = Color.gray;
            }
        }
          
        
    }

    private void normalModeUpdate()
    {
       
        //default it to true unless any sardines are infected
        levelFinished = true;
        //check for infected sardines left
        CheckForInfectedSardines();
        if (levelFinished == true)
        {
            canvas.SetActive(true);
            print("We in the canvas");
            if (CheckWin())
            {
                //show win screen
                winFailMessageBox.text = "You Win";
                nextLevelButton.enabled = true;
            }
            else
            {
                //show losss screen
                winFailMessageBox.text = "Game Over";
                nextLevelButton.enabled = false;
                nextLevelButton.GetComponent<Image>().color = Color.gray;
            }
        }
    }
    // this checks if there are any infected sardines left because that is the condition that is checked to end the level
    private void CheckForInfectedSardines()
    {
        GameObject[] sards = GameObject.FindGameObjectsWithTag("Sardine");
        foreach (GameObject s in sards)
        {
            if (s.GetComponent<Sardine>().isInfected)
            {
                levelFinished = false;
            }
        }
        //without this games ends as soon as you hover over last sardine because it changes its tag to TargetSardine
        if (GameObject.FindGameObjectsWithTag("TargetSardine").Length != 0)
        {
            if(GameObject.FindGameObjectWithTag("TargetSardine").GetComponent<Sardine>().isInfected )
            { 
                levelFinished = false; 
            }
        }
    }
    
   //destroys all sardines on the scene
    private static void DestroyAllSardines()
    {
        GameObject[] sards = GameObject.FindGameObjectsWithTag("Sardine");
        foreach (GameObject s in sards)
        {
                Destroy(s);
        }
    }
    //checks if the player has won at the end of the level
    private bool CheckWin()
    {
        switch (level)
        {
            case 1:
                if (Convert.ToInt32(textbox.text) >= 20) 
                    return true;
                break;
            case 2:
                if (Convert.ToInt32(textbox.text) >= 35) 
                    return true;
                break;
            case 3:
                if (Convert.ToInt32(textbox.text) >= 55)
                    return true;
                break;
            default:
                if (Convert.ToInt32(textbox.text) >= 80)
                    return true;
                break;
        }
        return false;
    }

    private void initiateNormalMode()
    {
        FindObjectOfType<SardineManger>().SpawnSardines(level, level, level, level, level, level, level, level);
    }


    private void initiateEndlessMode()
    {
        float currentTime = Time.time;
        float lastSpawn = Time.time;
        //loss condition
        while (Convert.ToInt32(textbox.text) > -20)
        {
            if (currentTime - lastSpawn >= 10)
            {
                FindObjectOfType<SardineManger>().SpawnSardines(1, 1, 1, 1, 1, 1, 1, 1);
                lastSpawn = Time.time;
            }
        }
    }
    
    public void nextLevelOnClick()
    {
        canvas.SetActive(false);
        level++;
        levelNum.text = level.ToString();
        levelFinished = false;
        DestroyAllSardines();
        Bath[] baths = FindObjectsOfType<Bath>();
        foreach (Bath b in baths) {

            b.clearbaths();
          
        }
           

        textbox.text = "0";
        switch (level)
        {
            case 1: ScoreNeeded.text = "20";break;
            case 2: ScoreNeeded.text = "35"; break;
            case 3: ScoreNeeded.text = "80"; break;
            default: ScoreNeeded.text = "160"; break;
        }
        initiateNormalMode();
        if(gameMode == "Variant")
        {
            initiateNormalMode();
        }
    }

    public void tryAgainOnClick()
    {
        canvas.SetActive(false);
        DestroyAllSardines();
        levelFinished = false;
        textbox.text = "0";
        initiateNormalMode();
        if (gameMode == "Variant")
        {
            initiateNormalMode();
        }
    }

    public void menuOnClick()
    {
        SceneManager.LoadScene("Menu");
    }

    //used to add points to the score if the level isn't over
    public void addPoints(int point)
    {
        if(!levelFinished)
        textbox.text = (Convert.ToInt32(textbox.text) + point).ToString();
    }
}
