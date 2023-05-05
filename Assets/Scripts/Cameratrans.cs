using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < 17.25)
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > 2.81f)
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
