using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    public List<GameObject> cars;
    public GameObject current_car;
    public int population = 10;
    public int generation = 0 ; //initially at zero
    [HideInInspector]
    public DNA winner;
    public DNA runnerup; 
    private int car_count = 0;
 
    // Start is called before the first frame update
    void Start()
    {
        newPopulation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<GameObject> getCars() {
        return cars;
    }

    public void newPopulation() {
        cars = new List<GameObject>();
        for (int i = 0 ; i < population ; i++) {
            GameObject car_object = (Instantiate(current_car));
            cars.Add(car_object);
            car_object.GetComponent<Car>().Initialize();
        }
        generation++;

    } 

    public void newPopulation(bool manipulation) {
        if(manipulation) {
            cars = new List<GameObject>();
            for (int i = 0 ; i < population ; i++) {
                DNA dna = winner.crossover(runnerup);
                DNA mutated = dna.mutate();
                GameObject car_object = Instantiate(current_car);
                cars.Add(car_object);
                car_object.GetComponent<Car>().Initialize(mutated);
            }
        }
        generation++;
        car_count = 0;
        GameObject.Find("camera").GetComponent<CameraMovement>().Follow(cars[0]);
    }

    public void restartGeneration() {
        cars.Clear();
        newPopulation();
    }
    
}
