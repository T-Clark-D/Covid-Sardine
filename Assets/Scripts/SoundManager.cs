using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
  
    public AudioSource Hooked;
    public AudioSource Masked;
    public AudioSource Vaxed;
    public AudioSource Cleaned;
    // Start is called before the first frame update
  
    public void PlayHooked()
    {
        Hooked.Play();
    }
    public void PlayMasked()
    {
        Masked.Play();
    }
    public void PlayVaxed()
    {
        Vaxed.Play();
    }
    public void PlayCleaned()
    {
        Cleaned.Play();
    }

}
