using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private bool initialization = false; //it should not be initialized at run time.
    private DNA dna;
    private NeuralNetwork neural_network;
    private Vector3 initialPoint;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        neural_network = new NeuralNetwork();
        dna = new DNA(neural_network.get_weights());

        initialPoint = transform.position;
        initialization = true; 
    }

    public void Initialize() {

        neural_network = new NeuralNetwork();
        dna = new DNA(neural_network.get_weights());

        initialPoint = transform.position;
        initialization = true; 
    }

    public void Initialize(DNA dna) {
        neural_network = new NeuralNetwork(dna);
        this.dna = dna;

        initialPoint = transform.position;
        initialization = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if(initialization) {
            float[] inputs = GetComponent<Lasers>().getDistancesOfLasers();

            //we're going to feed the information into the neural network's input layer
            neural_network.feedForward(inputs);
            List<float> outputs = neural_network.get_outputs();
            GetComponent<Moving_Car>().updateMovement(outputs);
            distance = Vector3.Distance(transform.position, initialPoint);
        }
    }

    void OnTriggerEnter(Collider col) {
            Debug.Log("test");
            changeCamera();
        }   

    public void changeCamera() {
        AIController controller = GameObject.Find("car_controller").GetComponent<AIController>();
        List<GameObject> cars = controller.getCars();
        if (cars.Count == 2) {
            controller.winner = cars[0].GetComponent<Car>().getDNA();
            controller.runnerup = cars[1].GetComponent<Car>().getDNA();
        }
        if (cars.Count == 1 ) {
            if (!controller.winner.Equals(cars[0].GetComponent<Car>().getDNA())) {
                DNA inter = controller.runnerup;
                controller.runnerup = controller.winner;
                controller.winner = inter;
            }
            cars.Remove(gameObject);
            controller.newPopulation(true);
            Destroy(gameObject);

        } else {
            int rand = Random.Range(0, (int)cars.Count);
            if (cars[rand] == gameObject) {
                changeCamera();
            } else {
                if (gameObject == GameObject.Find("camera").GetComponent<CameraMovement>().getFollowing()) {
                    GameObject.Find("camera").GetComponent<CameraMovement>().Follow(cars[rand]);
                }
                cars.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }
    public DNA getDNA() {
        return dna;
    }
}
