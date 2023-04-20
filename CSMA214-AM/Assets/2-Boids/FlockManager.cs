using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using System.Linq;

public class FlockManager : MonoBehaviour
{
    public GameObject boid;
    public Vector3 bounds = new Vector3(5, 5, 5);
    public int flocksize = 25;
    /*public GameObject[] allBoids;*/
    public List<GameObject> allBoids = new List<GameObject>();
    public Vector2 speed = new Vector2(1, 2);

    [Range(0.1f, 10.0f)]
    public float neighborDist = 5;

    [Range(1.0f, 5.0f)]
    public float comfortDist = 1.0f;

    [Range(0.5f, 10.0f)]
    public float rotationSpeed = 5;

    [Range(1.0f, 99.0f)]
    public float applyRulesFreq = 20;

  
    public Transform goal;
    public int mSpeed;

    public Transform enemy;

    //gene code
    public static float elapsed = 0;
    int trialTime = 10;
    int generation = 1;

    // Start is called before the first frame update
    void Start()
    {
        //allBoids = new GameObject[flocksize];
        for(int i = 0; i < flocksize; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-bounds.x, bounds.x),
                                         Random.Range(-bounds.y, bounds.y),
                                         Random.Range(-bounds.z, bounds.z));
            //allBoids[i] = (GameObject) Instantiate(boid, pos, Quaternion.identity, this.transform);
            //allBoids[i].GetComponent<Boid>().myManager = this;
            //allBoids[i].GetComponent<Boid>().speed = Random.Range(speed.x, speed.y);

            GameObject gO = Instantiate(boid, pos, Quaternion.identity, this.transform);
            gO.GetComponent<DNA>().r = Random.value;
            gO.GetComponent<DNA>().g = Random.value;
            gO.GetComponent<DNA>().b = Random.value;

            gO.GetComponent<Boid>().myManager = this;
            gO.GetComponent<Boid>().speed = Random.Range(speed.x, speed.y);
            allBoids.Add(gO);

            //allBoids[i].GetComponent<Boid>().applyRuleFreq = applyRulesFreq;
            //allBoids.Add((GameObject)Instantiate(boid, pos, Quaternion.identity, this.transform));
            //obj.transform.position = pos;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > trialTime)
        {
            BreedNewPopulation();
            Debug.Log("BreedingNewPop");
            elapsed = 0;
        }
        //if (goal != null)
        //{
        //    Vector3 direction = goal.position - this.transform.position;

        //    direction.z = 0.0f;
        //    if (direction != Vector3.zero)
        //        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
        //            Quaternion.FromToRotation(Vector3.right, direction),
        //            rotationSpeed * Time.deltaTime);

        //    //Move Towards Target
        //    this.transform.position += (goal.position - this.transform.position).normalized
        //        * mSpeed * Time.deltaTime;
        //}

    }

    //For genes
    GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Vector3 pos = new Vector3(Random.Range(-10, 10),
                                        Random.Range(-10, 10),
                                        Random.Range(-10, 10));
        GameObject offspring = Instantiate(boid, pos, Quaternion.identity, this.transform);
        DNA dna1 = parent1.GetComponent<DNA>();
        DNA dna2 = parent2.GetComponent<DNA>();

        offspring.GetComponent<DNA>().r = Random.value < 0.5f ? dna1.r : dna2.r;
        offspring.GetComponent<DNA>().g = Random.value < 0.5f ? dna1.g : dna2.g;
        offspring.GetComponent<DNA>().b = Random.value < 0.5f ? dna1.b : dna2.b;

        offspring.GetComponent<Boid>().myManager = this;
        offspring.GetComponent<Boid>().speed = Random.Range(speed.x, speed.y);

        return offspring;

    }

    void BreedNewPopulation()
    {
        //create list to hold pop
        List<GameObject> newPopulation = new List<GameObject>();
        //sort our list
        //List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<DNA>().timeToDie).ToList();
        List<GameObject> sortedList = allBoids.OrderBy(o => o.GetComponent<DNA>().timeToDie).ToList();

        Debug.Log(sortedList.Count);
        Debug.Log((int)(sortedList.Count / 2) - 1);
        //clear list
        allBoids.Clear();
        //breed the fitness
        for (int i = (int)(sortedList.Count / 2) - 1; i < sortedList.Count; i++)
        {
            Debug.Log(sortedList[i]);
            allBoids.Add(Breed(sortedList[i], sortedList[i + 1]));
            allBoids.Add(Breed(sortedList[i + 1], sortedList[i]));
        }
        //destroy list
        for (int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }
        //increment the generation
        generation++;
    }
}
