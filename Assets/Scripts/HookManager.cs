using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookManager : MonoBehaviour
{
    public float speed = 3;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //hooks the fish and destroys itself and the fish when offscreen
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        if(transform.position.y >= 10)
        {
            Destroy(this.gameObject);
        }
    }
}
