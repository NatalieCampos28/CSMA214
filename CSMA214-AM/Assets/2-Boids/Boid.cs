using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public FlockManager myManager;
    public float speed = 0.5f;

    public Transform goal;
    public Transform enemy;
    // Start is called before the first frame update
    //3d model https://sketchfab.com/3d-models/animated-dragon-three-motion-loops-eca98cf6cd084c1596cecf716e110c29
    bool returning = false;
    public float applyRuleFreq = 20f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ApplyRules();

        Bounds bounds = new Bounds(myManager.transform.position, myManager.bounds * 2);
        //Bounds bounds = new Bounds(myManager.transform.position, Vector3.one * 5 * 2);

        if (!bounds.Contains(transform.position))
        {
            returning = true;
        }
        else
        {
            returning = false;
        }

        if (returning)
        {
            Vector3 direction = myManager.transform.position-transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),myManager.rotationSpeed * Time.deltaTime);
        }//if fleeing
        else
        {
            if (Random.Range(0, 100) < myManager.applyRulesFreq)
            {
                ApplyRules();
            }
        }


        //transforming the pos
       transform.Translate(Vector3.forward * Time.deltaTime*speed);
       
        
        //transform.Translate(Mathf.Cos(Time.time), Mathf.Sin(Time.time), Time.deltaTime*speed);
    }
    public void ApplyRules()
    {
        //GameObject[] neighbors;
        //neighbors = myManager.allBoids;
        List<GameObject> neighbors = new List<GameObject>();
        neighbors = myManager.allBoids;

        Vector3 vector_center = Vector3.zero;
        Vector3 vector_avoid = Vector3.zero;
        float neighborhood_speed = 0.01f;
        float neighborhood_dist;
        int neighborhood_size = 0;

        foreach(GameObject boid in neighbors)
        {
            if(boid != this.gameObject)
            {
                neighborhood_dist = Vector3.Distance(boid.transform.position, enemy.transform.position);

                //add randomness
                if(neighborhood_dist <= myManager.neighborDist * Random.value)
                {
                    vector_center += boid.transform.position;
                    neighborhood_size++;

                    if(neighborhood_dist < myManager.comfortDist*Random.value)
                    {
                        vector_avoid = vector_avoid + (enemy.transform.position - boid.transform.position);
                    }
                    Boid other_boid = boid.GetComponent<Boid>();

                    neighborhood_speed = neighborhood_speed +other_boid.speed;
                }
            }
        }

        if(neighborhood_size > 0)
        {
            //define goal
            Vector3 goal = myManager.GetComponent<FlockManager>().goal.position;

            vector_center = (vector_center/neighborhood_size);

            speed = neighborhood_speed/neighborhood_size;

            //Vector3 direction = (vector_center + vector_avoid) - goal.transform.position;

            //sub to goal
            Vector3 direction = this.goal.position - this.transform.position;

            if (direction != Vector3.zero)
            {
             
               transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), myManager.rotationSpeed * Time.deltaTime);

                //Move Towards Target
                this.transform.position += (this.goal.position - this.transform.position).normalized
                    * speed * Time.deltaTime;
            }
           
        }
    }
}
