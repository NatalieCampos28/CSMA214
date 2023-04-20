using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Timers;

public class DNA : MonoBehaviour
{
    //change by gene called color
    public float r;
    public float g;
    public float b;

    //var of prefab
    Renderer rend;
    //BoxCollider collider;
    Collider collider;

    bool dead = false;
    public float timeToDie = 0;
    // Start is called before the first frame update

    private void OnMouseDown()
    {
        dead = true;
        //timeToDie = PopulationManager.elapsed;
        timeToDie = FlockManager.elapsed;
        rend.enabled = false;
        collider.enabled = false;
    }
    void Start()
    {
        rend = GetComponent<Renderer>();
        collider = GetComponent<Collider>();

        rend.material.color = new Color(r, g, b);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
