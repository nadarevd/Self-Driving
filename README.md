# Self-Driving
Using Unity and C#, we implemented basic neural network concepts, removing the idea of back propogation and using genetic algorithms to give the cars the ability of reinforced learning. Cars will learn, over time, what the best path is based on cars with the best genes. 

# For Ms. Selvarajah
This github repository only has the code for the project, not the project itself. Since we do not know how to upload Unity projects (they are very large files, this one was 600Mb), we will just show you the code and some screenshots/videos. We hope you enjoy our work!

# Simulations (for those who do not want to use Unity)

**Figure 1.1: The Agent Model, an asset from the standard asset store in Unity**
![Agent Model](https://i.gyazo.com/64e093d86157af72663637ab08533abb.png)

**Figure 1.2: The Simulation**
![Image of Simulation](https://i.gyazo.com/0a4169d1ef3dd36e93d83674e5648251.png)

**Figure 1.3: In Action** <br/>
![gif](https://media.giphy.com/media/PhfJjvZ9c9mR3ztHkk/giphy.gif)

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
