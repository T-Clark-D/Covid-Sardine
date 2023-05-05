using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Sprite maskSprite;
    public Sprite VaxSprite;
    public Sprite HookSprite;
    public Sprite CleanSprite;


    private Sniper snip;
    private SpriteRenderer SR;
    void Start()
    {
        snip = FindObjectOfType<Sniper>();
        SR = GetComponent<SpriteRenderer>();
    }
    //switches to correct weapon/tool 
    void FixedUpdate()
    {
        switch (snip.gunMode)
        {
            case "Mask":
                SR.sprite = maskSprite;
                break;
            case "Vax":
                SR.sprite = VaxSprite;
                break;
            case "Hook":
                SR.sprite = HookSprite;
                break;
            case "Clean":
                SR.sprite = CleanSprite;
                break;
        }
    }
}
