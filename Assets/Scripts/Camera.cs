using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameratrans : MonoBehaviour
{
    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // allows us to move the camera/truck
    void Update()
    {
        {
            if (Input.GetKey(KeyCode.D) && transform.position.x < 17.25)
            {
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.A) && transform.position.x > 2.81f)
            {
                transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            }
        }
    }
    }

