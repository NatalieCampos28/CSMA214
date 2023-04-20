using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobScript : MonoBehaviour
{
    public float freq = 1.0f;
    public float amp = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Sine Function Value:" + (Mathf.Sin(freq*Time.time))*amp);

        Debug.Log("Cosine Function Value:" + (Mathf.Cos(freq*Time.time))*amp);

        float y_pos = Mathf.Sin(freq * Time.time) * amp;
        float x_pos = Mathf.Cos(freq * Time.time) * amp;
        Vector3 new_position = new Vector3(x_pos, y_pos, 0);


        //assign new position to transform position
        transform.position = new_position;

        //object position to origin
        Debug.DrawLine(transform.position, Vector3.zero);

        //object x position
        Debug.DrawLine(transform.position, new Vector3(transform.position.x, 0, 0));

        //object y position
        Debug.DrawLine(transform.position, new Vector3(0, transform.position.y, 0));
    }

}
