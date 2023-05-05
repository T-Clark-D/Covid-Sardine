using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bath : MonoBehaviour
{

    public float contaminationLevel;
    public float contaminationBaseRate;
    //number of contaminated sardines in bath (multiplies the contaminatinon rate)
    public float contaminationModifier;
    public bool isContaminated;
    private float contaminationRate;
    private SpriteRenderer SR;

    public Sprite zeroContam;
    public Sprite quarterContam;
    public Sprite halfContam;
    public Sprite threeQuarterContam;
    public Sprite Contam;

    public GameObject placeHolder;

    public int maxFish;
    public int fishInBath = 0;

    public bool fullBath = false;
    private bool foundSpace = false;

    public GameObject points;


    public GameObject sniper;
    public GameObject[] sardineArray;
    // Start is called before the first frame update
    void Start()
    {
        
        points = GameObject.FindGameObjectWithTag("Score");

        //baths start clean and with a base contamination rate
        maxFish = 7;
        contaminationLevel = 0;
        contaminationBaseRate = 0.001f;
        contaminationModifier = 0;
        contaminationRate = 0;
        isContaminated = false;
        SR = GetComponent<SpriteRenderer>();
        for (int i = 0; i < sardineArray.Length; i++)
            sardineArray[i] = Instantiate(placeHolder);
    }
     //after each level we reset the baths
    public void clearbaths()
    {
        for (int i = 0; i < sardineArray.Length; i++)
            sardineArray[i] = Instantiate(placeHolder);
    }


    private void FixedUpdate()
    {
        if (fishInBath == maxFish)
        {
            fullBath = true;
        }
        else
        {
            fullBath = false;
        }
        if (contaminationLevel < 1)
        {
            contaminationLevel += contaminationRate;
        }
        else
        {
            contaminationLevel = 1;
            isContaminated = true;
        }
        if (contaminationLevel != 0)
        {
            UpdateSprite();
        }
    }
    //updates the sprite of the bath depending on contamination level
    private void UpdateSprite()
    {
        if (contaminationLevel == 0)
        {
            SR.sprite = zeroContam;
        }
        else if (contaminationLevel >= 0.25 && contaminationLevel < 0.5)
        {
            SR.sprite = quarterContam;
        }
        else if (contaminationLevel >= 0.5 && contaminationLevel < 0.75)
        {
            SR.sprite = halfContam;
        }
        else if (contaminationLevel >= 0.75 && contaminationLevel < 1)
        {
            SR.sprite = threeQuarterContam;
        }
        else if (contaminationLevel >= 1)
        {
            SR.sprite = Contam;
        }
    }

    //add a fish that increases contaminationrate
    public void AddSickFish()
    {
        contaminationModifier++;
        fishInBath++;

        contaminationRate = contaminationBaseRate * contaminationModifier;
    }

    //add a fish that doesn't increases contaminationrate
    public void AddFish()
    {
        fishInBath++;
    }

    //remove a contaminated fish and therfore decrese contamiantion rate
    public void ExtractSickFish(GameObject sard)
    {
        for (int i = 0; i < sardineArray.Length; i++)
            print(sardineArray[i]);

        fishInBath--;
        contaminationModifier--;
        contaminationRate = contaminationBaseRate * contaminationModifier;
        for (int i = 0; i < sardineArray.Length; i++)
        {
            if (sardineArray[i] == sard)
            {
                sardineArray[i] = Instantiate(placeHolder);
            }
        }

        for (int i = 0; i < sardineArray.Length; i++)
            print(sardineArray[i]);
    }
    //remove fish without decreasing contamination rate
    public void extractFish()
    {
        fishInBath--;
    }
    //returns an available position in the bath
    public Vector2 GetPositionInBath(GameObject sard)
    {
        if (!sard.Equals(null))
        {
            int rand = 0;
            while (!foundSpace)
            {
                rand = UnityEngine.Random.Range(0, sardineArray.Length);
                if (sardineArray[rand].Equals(null) || sardineArray[rand].name == "placeHolder" || sardineArray[rand].name == "placeHolder(Clone)")
                {
                    sardineArray[rand] = sard;
                    foundSpace = true;
                }
            }
            foundSpace = false;
            switch (rand)
            {
                case 0:
                    return transform.position + new Vector3(-1.1f, 0.3f, 0);
                case 1:
                    return transform.position + new Vector3(-0.6f, 0.5f, 0);
                case 2:
                    return transform.position + new Vector3(0, 0.6f, 0);
                case 3:
                    return transform.position + new Vector3(0.6f, 0.5f, 0);
                case 4:
                    return transform.position + new Vector3(1.1f, 0.3f, 0);
                case 5:
                    return transform.position + new Vector3(0.5f, 0.2f, 0);
                case 6:
                    return transform.position + new Vector3(-0.5f, 0.2f, 0);

            }
        }

        return transform.position + Vector3.right * 10;
    }

    //allows player to target the bath if using the right tool for the job
    private void OnMouseOver()
    {
        if (contaminationLevel > 0)
        {
            if (sniper.GetComponent<Sniper>().Istargetable(true, true, false, false))
            {
                SR.color = FindObjectOfType<Sniper>().col;
                this.tag = "TargetBath";
            }

        }
    }
    //untargets the bath whenmouse stops hovering over it
    private void OnMouseExit()
    {
        print("im out");
        SR.color = Color.white;
        this.tag = "Bath";
    }
    //allows the player to clean the bath whenusing the right tool
    public void Clean()
    {
        contaminationLevel = 0;
        FindObjectOfType<SoundManager>().PlayCleaned();
        if (contaminationLevel >= 0.25f)
        {
            FindObjectOfType<GameManger>().addPoints(2);
            //contaminationLevel = 0;
            UpdateSprite();
        }
        UpdateSprite();
    }
}
