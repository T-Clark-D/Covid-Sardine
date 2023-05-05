using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    public Camera mainCamera;
    Vector2 mousePos = new Vector2(0, 0);
    public string gunMode;
    private SpriteRenderer SR;
    public Color col;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        SR = GetComponent<SpriteRenderer>();
        col = Color.cyan;
        gunMode = "Mask";
        
    }

    // Update is called once per frame
    void Update()
    {
        
        WeaponSwitch();
        
        FireWeapon();
    }
    //performs the actions on the gameobjects that are targeted on mouse click
    private void FireWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            if (GameObject.FindGameObjectsWithTag("TargetSardine").Length != 0 || GameObject.FindGameObjectsWithTag("TargetBath").Length != 0) { 
        {
            switch (gunMode)
            {
                case "Mask":
                    GameObject.FindWithTag("TargetSardine").GetComponent<Sardine>().Mask();
                    break;
                case "Vax":
                    GameObject.FindWithTag("TargetSardine").GetComponent<Sardine>().Vaxinate();
                    break;
                case "Hook":
                    GameObject.FindWithTag("TargetSardine").GetComponent<Sardine>().Hookit();
                    break;
                case "Clean":
                    GameObject.FindWithTag("TargetBath").GetComponent<Bath>().Clean();
                    break;
                        default:print("y we here?"); break;
            }
                    anim.SetTrigger("JiggleTrigger");
        }
    }
    }
    //swithces teh weapon curently equipped
    private void WeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gunMode = "Mask";
            col = Color.cyan;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gunMode = "Vax";
            col = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            col = Color.magenta;
            gunMode = "Clean";
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            col = Color.yellow;
            gunMode = "Hook";
        }
        SR.color = col;
    }

    //checks if the bool values of the sradine make it targettable with the tool being used
    public bool Istargetable(bool vaxed, bool masked, bool infected,bool isSardine)
    {

        switch (gunMode)
        {
            case "Mask":
                if (masked == false)
                    return true;
                break;
            case "Vax":
                if (vaxed == false)
                    return true;
                break;
            case "Hook":
               if(infected == true)
                    return true;
                break;
            case "Clean":
                if (isSardine == false)
                    return true;
                break;
            default:
                return false;
        }
        return false;
    }
}
