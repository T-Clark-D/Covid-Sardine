using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sardine : MonoBehaviour
{
    private bool isVaxed;
    private bool isMasked;
    public bool isInfected;

    private bool bathing;
    private GameObject bath;
    public GameObject Hook;

    public Sprite SardineUVUIUM;
    public Sprite SardineUVUIM;
    public Sprite SardineVUIUM;
    public Sprite SardineVUIM;
    public Sprite SardineUVIUM;
    public Sprite SardineUVIM;
    public Sprite SardineVIM;
    public Sprite SardineVIUM;

    public Sprite BathingSardineUVUIUM;
    public Sprite BathingSardineUVUIM;
    public Sprite BathingSardineVUIUM;
    public Sprite BathingSardineVUIM;
    public Sprite BathingSardineUVIUM;
    public Sprite BathingSardineUVIM;
    public Sprite BathingSardineVIM;
    public Sprite BathingSardineVIUM;

    float currentTime =  0.0f;
    float lastMove = 0.0f;
    float contamTickTime = 0.0f;
    public SpriteRenderer SR;
    private Rigidbody2D RB;

    public GameObject sniper;

    public GameObject points;
   
    private bool isTimeFrozen = false;
   
    private float powerActivateTime;
    private bool state = true;
    // Start is called before the first frame update
    void Start()
    {
        sniper = GameObject.FindGameObjectWithTag("Sniper");
        points = GameObject.FindGameObjectWithTag("Score");
       
        //entering sardines are not yet bathing
        bathing = false;
        bath = null;
        //initialise Sprite renderer & Rigid Body
        SR = GetComponent<SpriteRenderer>();
        RB = GetComponent<Rigidbody2D>();
      
        UpdateSprite();
    }

    public void setSardineVars(bool vax, bool mask, bool inf)
    {
        isVaxed = vax;
        isMasked = mask;
        isInfected = inf;
    }

    // Update is called once per frame
    void Update()
    {
        //checks to see if we are in variant mode and allows one usof power up per level
        if(FindObjectOfType<GameManger>().gameMode == "Variant")
        {
            if (Input.GetKeyDown("space"))
            {
                if (state)
                {
                    RB.velocity = Vector3.zero;
                    powerActivateTime = Time.realtimeSinceStartup;
                    isTimeFrozen = true;
                    state = false;
                    SR.color = Color.gray;
                }
            }
        }

        FlipLookDirection();
        ZLayering();
        if (bathing)
        {
            CheckContamination();
        }
        else if(!isTimeFrozen)
        {
            Movement();
        }
        else if (Time.realtimeSinceStartup - powerActivateTime >= 5)
        {
            isTimeFrozen = false;
            lastMove = Time.realtimeSinceStartup;
            SR.color = Color.white;
        }
    }
    //checks contamination of bath and contaminates the fish if its is not vaxed 
    private void CheckContamination()
    {
        if (bath.GetComponent<Bath>().isContaminated && !isVaxed)
        {
            if (isInfected == false)
            {
                FindObjectOfType<GameManger>().addPoints(-10);
                isInfected = true;
                UpdateSprite();
            }
        }
        if (bath.GetComponent<Bath>().contaminationLevel >=0.25f)
        {
            if (Time.realtimeSinceStartup - contamTickTime >= 5)
            {
                contamTickTime = Time.realtimeSinceStartup;
                FindObjectOfType<GameManger>().addPoints(-1);
            }
        }
    }
    //switches the direction it looks depending on the direction of the veolcity vector
    private void FlipLookDirection()
    {
        if (RB.velocity.x > 0)
        {
            SR.flipX = true;
        }
        else
        {
            SR.flipX = false;
        }
    }
    //controls random movement and intervals between the movements
    private void Movement()
    {
        currentTime = Time.realtimeSinceStartup;
        //print(currentTime);
        if(currentTime - lastMove >= 0.5)
        {
            Move();
            lastMove = Time.realtimeSinceStartup;
        }
    }

    private void Move()
    {
        Vector2 direction = (new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f))).normalized;
        RB.AddForce(direction, ForceMode2D.Impulse);
    }
    //check for bath collsions to let the m enter tha baths
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bath" && !bathing)
        {
            bath = collision.gameObject;

            if(!bath.GetComponent<Bath>().fullBath)
            { 
                bathing = true;
                //place in bath
                RB.velocity = Vector3.zero;
                RB.isKinematic = true;
                transform.position = bath.GetComponent<Bath>().GetPositionInBath(this.gameObject);
                UpdateSprite();
                GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.2530169f, -0.1230103f);
                GetComponent<CapsuleCollider2D>().size = new Vector2(5.560546f, 9.398066f);
                //chech for infection so that the bath can get contaminated
                if (isInfected == true)
                {
                    bath.GetComponent<Bath>().AddSickFish();
                }
                else
                {
                    bath.GetComponent<Bath>().AddFish();
                }

            }
        } 
    }
    //allows for us to see the sardines in the correct order, the closer ones overlapping the farther ones
    void ZLayering()
    {
        Vector3 newPosition = transform.position;
        newPosition.z = transform.position.y;
        transform.position = newPosition;

    }
    //is called when a sprite change is needed
    private void UpdateSprite()
    {
        switch (isVaxed, isInfected, isMasked, bathing)
        {
            case (false, false, false, false):
                SR.sprite = SardineUVUIUM;
                break;
            case (false, false, true, false):
                SR.sprite = SardineUVUIM;
                break;
            case (true, false, false, false):
                SR.sprite = SardineVUIUM;
                break;
            case (true, false, true, false):
                SR.sprite = SardineVUIM;
                break;
            case (false, true, false, false):
                SR.sprite = SardineUVIUM;
                break;
            case (false, true, true, false):
                SR.sprite = SardineUVIM;
                break;
            case (true, true, true, false):
                SR.sprite = SardineVIM;
                break;
            case (true, true, false, false):
                SR.sprite = SardineVIUM;
                break;

            case (false, false, false, true):
                SR.sprite = BathingSardineUVUIUM;
                break;
            case (false, false, true, true):
                SR.sprite = BathingSardineUVUIM;
                break;
            case (true, false, false, true):
                SR.sprite = BathingSardineVUIUM;
                break;
            case (true, false, true, true):
                SR.sprite = BathingSardineVUIM;
                break;
            case (false, true, false, true):
                SR.sprite = BathingSardineUVIUM;
                break;
            case (false, true, true, true):
                SR.sprite = BathingSardineUVIM;
                break;
            case (true, true, true, true):
                SR.sprite = BathingSardineVIM;
                break;
            case (true, true, false, true):
                SR.sprite = BathingSardineVIUM;
                break;
        }
    }
    //returns a random Boolean, can be used to randomise sardines
    private bool randomBoolean()
    {
        return (UnityEngine.Random.Range(0, 2) == 1);
    }

    //resets  the color and tag of the sardine when the player stops hovering
    private void OnMouseExit()
    {
        SR.color = Color.white;
        this.tag = "Sardine";
    }
    //changes the tag and highlight color of the sardine when the player hovers over it
    public void OnMouseOver()
    {
        if (sniper.GetComponent<Sniper>().Istargetable(isVaxed, isMasked, isInfected, true))
            SR.color = FindObjectOfType<Sniper>().col;
        this.tag = "TargetSardine";

    }


    //below are the implementation of the actions that can be  taken on the sardines


    public void Vaxinate()
    {
        if(isVaxed == false)
        {
            FindObjectOfType<SoundManager>().PlayVaxed();
            FindObjectOfType<GameManger>().addPoints(2);
            isVaxed = true;
            UpdateSprite();
            RB.AddForce((Vector2)(this.transform.position - sniper.transform.position).normalized * 5, ForceMode2D.Impulse);
        }
    }
    public void Mask()
    {
        if (isMasked == false)
        {
            FindObjectOfType<SoundManager>().PlayMasked();
            FindObjectOfType<GameManger>().addPoints(2);
            isMasked = true;
            UpdateSprite();
            RB.AddForce((Vector2)(this.transform.position - sniper.transform.position).normalized * 5, ForceMode2D.Impulse);
        }
    }
    public void Hookit()
    {
        if(isInfected == true) 
        {
            FindObjectOfType<SoundManager>().PlayHooked();
            FindObjectOfType<GameManger>().addPoints(5);
            GameObject hook = Instantiate(Hook, transform.position + new Vector3(-0.12f, 4f, 0), Quaternion.identity);
            this.transform.SetParent(hook.transform);
            if (!bathing)
            {
                RB.velocity = Vector2.zero;
                RB.isKinematic = true;
            }
            else
            {
                bath.GetComponent<Bath>().ExtractSickFish(this.gameObject);
                bathing = false;
                RB.isKinematic = true;
                UpdateSprite();
            }
        }
    } 
}
