# Self-Driving
Using Unity and C#, I implemented basic neural network concepts, removing the idea of back propogation and using genetic algorithms to give the cars the ability of reinforced learning. Cars will learn, over time, what the best path is based on cars with the best genes. \

# Simulations (for those who do not want to use Unity)
[place screenshots]

# Implementation
Cars are given randomly initialized DNA. In this simulation, the best cars are determined by how long they travel for (**fitness**). The two best cars' DNAs are crossed over and mutated

Cars are able to move and rotate over time. A set of lasers are given to each car, which act as the sensors of the car. The distances between the car and the walls are computed from each laser, which is ultimately used as the inputs of the neural network. 
```cpp
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
```

A special camera is implemented that keeps track of the best cars and focuses on them the whole time. 
```cpp
if (Input.GetKeyDown(KeyCode.LeftControl)) {
            List<GameObject> cars = GameObject.Find("car_controller").GetComponent<AIController>().getCars();
            int index = cars.IndexOf(follow); 
            if ( index == cars.Count - 1) {
                index = 0;
            } else {
                index += 1;
            }

            Follow(cars[index]);
        }
```
