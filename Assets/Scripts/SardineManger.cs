using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SardineManger : MonoBehaviour
{
    public GameObject sard;
    //spawns sardines of the specified types
    public void SpawnSardines(int UVUIUM, int UVUIM, int VUIUM,int VUIM,int UVIUM, int UVIM,int VIM, int VIUM)
    {
        for (int i = 0; i < UVUIUM; i++)
        {
            GameObject s = Instantiate(sard, new Vector3(Random.Range(-4, 24), Random.Range(1, 2),0) , Quaternion.identity);
            s.GetComponent<Sardine>().setSardineVars(false,false,false);
        }
        for (int i = 0; i < UVUIM; i++)
        {
            GameObject s = Instantiate(sard, new Vector3(Random.Range(-4, 24), Random.Range(1, 2), 0), Quaternion.identity);
            s.GetComponent<Sardine>().setSardineVars(false, false, true);
        }
        for (int i = 0; i < VUIUM; i++)
        {
            GameObject s = Instantiate(sard, new Vector3(Random.Range(-4, 24), Random.Range(1, 2), 0), Quaternion.identity);
            s.GetComponent<Sardine>().setSardineVars(true, false, false);
        }
        for (int i = 0; i < VUIM; i++)
        {
            GameObject s = Instantiate(sard, new Vector3(Random.Range(-4, 24), Random.Range(1, 2), 0), Quaternion.identity);
            s.GetComponent<Sardine>().setSardineVars(true, false, true);
        }

        for (int i = 0; i < UVIUM; i++)
        {
            GameObject s = Instantiate(sard, new Vector3(Random.Range(-4, 24), Random.Range(1, 2), 0), Quaternion.identity);
            s.GetComponent<Sardine>().setSardineVars(false, true, false);
        }
        for (int i = 0; i < UVIM; i++)
        {
            GameObject s = Instantiate(sard, new Vector3(Random.Range(-4, 24), Random.Range(1, 2), 0), Quaternion.identity);
            s.GetComponent<Sardine>().setSardineVars(false, true, true);
        }
        for (int i = 0; i < VIM; i++)
        {
            GameObject s = Instantiate(sard, new Vector3(Random.Range(-4, 24), Random.Range(1, 2), 0), Quaternion.identity);
            s.GetComponent<Sardine>().setSardineVars(true, true, true);
        }
        for (int i = 0; i < VIUM; i++)
        {
            GameObject s = Instantiate(sard, new Vector3(Random.Range(-4, 24), Random.Range(1, 2), 0), Quaternion.identity);
            s.GetComponent<Sardine>().setSardineVars(true, true, false);
        }
    }
}
