using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public float speed_scroll_x = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        transform.Translate(x* speed_scroll_x* Time.deltaTime, 0, 0);

        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = Vector3.zero;
        }

    }
}
