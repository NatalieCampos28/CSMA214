using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PopulationManager : MonoBehaviour
{
    public GameObject objPrefab;
    public int populationSize = 10;
    List<GameObject> population = new List<GameObject>();
    public static float elapsed = 0;
    int trialTime = 10;
    int generation = 1;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < populationSize; i++){
            Vector3 pos = new Vector3(Random.Range(-10, 10),
                                        Random.Range(-10, 10),
                                        Random.Range(-10, 10));
            GameObject gO = Instantiate(objPrefab, pos, Quaternion.identity, this.transform);
            gO.GetComponent<DNA>().r = Random.value;
            gO.GetComponent<DNA>().r = Random.value;
            gO.GetComponent<DNA>().r = Random.value;
            population.Add(gO);
        }
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > trialTime)
        {
            BreedNewPopulation();
            elapsed = 0;
        }
    }

    GameObject Breed (GameObject parent1, GameObject parent2)
    {
        Vector3 pos = new Vector3(Random.Range(-10, 10),
                                        Random.Range(-10, 10),
                                        Random.Range(-10, 10));
        GameObject offspring = Instantiate(objPrefab, pos, Quaternion.identity, this.transform);
        DNA dna1 = parent1.GetComponent<DNA>();
        DNA dna2 = parent2.GetComponent<DNA>();

        offspring.GetComponent<DNA>().r = Random.value < 0.5f ? dna1.r : dna2.r;
        offspring.GetComponent<DNA>().g = Random.value < 0.5f ? dna1.g : dna2.g;
        offspring.GetComponent<DNA>().b = Random.value < 0.5f ? dna1.b : dna2.b;

        return  offspring;

    }

    void BreedNewPopulation()
    {
        //create list to hold pop
        List<GameObject> newPopulation = new List<GameObject> ();
        //sort our list
        //List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<DNA>().timeToDie).ToList();
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<DNA>().timeToDie).ToList();
        //clear list
        population.Clear();
        //breed the fitness
        for (int i = (int)(sortedList.Count/2)-1; i < sortedList.Count; i++) 
        { 
            population.Add(Breed(sortedList[i], sortedList[i+1]));
            population.Add(Breed(sortedList[i+1], sortedList[i]));
        }
        //destroy list
        for(int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }
        //increment the generation
        generation++;
    }
}
