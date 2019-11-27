using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork {
    public int hidden_layers = 3; //for simplicity
    public int size_hidden_layers = 5;
    public int num_of_outputs = 2;
    public int num_of_inputs = 5;
    public float max_value_init = 1.0f;

    private const float EULER = 2.71828f;
    private List<List<float>> neurons;
    private List<float[][]> weights;
    private int total_layers = 0;

    /* 
     * Initialize weights and array 
     */
    /*
     * A neural network is a dumb brain initially. We set up the network with randomized weights. 
     * It is supposed to know nothing at first. We will use feedforwarding and backtracking to
     * tweak these weights over time, aka make the model LEARN.
     */
    public NeuralNetwork() {
        total_layers = hidden_layers + 2; // +2 refers to input and output layers
        weights = new List<float[][]>(); //each 2D array will have a bunch of randomize values at first
        neurons = new List<List<float>>();

        //now we need to fill the neurons
        for (int i = 0 ; i < total_layers ; i++) {
            
            float[][]layer_weights;
            List<float> layer = new List<float>();
            int size_layer = get_size_layer(i); //first run, 5

            if (i != 1 + hidden_layers) { 
                layer_weights = new float[size_layer][];
                int next_size_layer = get_size_layer(i+1); //find beforehand. We want to assign weights between the previous layer and the next layer
                
                for (int j = 0 ; j < size_layer ; j++) {

                    layer_weights[j] = new float[next_size_layer];
                    for (int k = 0 ; k < next_size_layer ; k++) { // next layer weights
                        
                        layer_weights[j][k] = generate_random_value(); // 
                    }
                }
                weights.Add(layer_weights);
            }
            for (int j = 0 ; j < size_layer ; j++) {
                layer.Add(0);
            }
            neurons.Add(layer);
        } 
    }

    public NeuralNetwork(DNA dna) {
        //testing.
        List<float[][]> weights_of_DNA = dna.getDNA();





        total_layers = hidden_layers + 2; // +2 refers to input and output layers
        weights = new List<float[][]>(); //each 2D array will have a bunch of randomize values at first
        neurons = new List<List<float>>();

        //now we need to fill the neurons
        for (int i = 0 ; i < total_layers ; i++) {
            
            float[][]layer_weights;
            float[][] weights_of_DNA_layer;

            List<float> layer = new List<float>();
            int size_layer = get_size_layer(i); //first run, 5

            if (i != 1 + hidden_layers) { 

                layer_weights = new float[size_layer][];
                int next_size_layer = get_size_layer(i+1); //find beforehand. We want to assign weights between the previous layer and the next layer

                weights_of_DNA_layer = weights_of_DNA[i];
                for (int j = 0 ; j < size_layer ; j++) {

                    layer_weights[j] = new float[next_size_layer];
                    for (int k = 0 ; k < next_size_layer ; k++) { // next layer weights

                        layer_weights[j][k] = weights_of_DNA_layer[j][k]; // 
                    }
                }
                weights.Add(layer_weights);
            }
            for (int j = 0 ; j < size_layer ; j++) {
                layer.Add(0);
            }
            neurons.Add(layer);
        } 
    }

    //updating function. This is where we will feed the inputs through each layer, result is the data being send to output layer.
    public void feedForward(float [] inputs) {
        List<float> input_layer = neurons[0];
        for (int i = 0 ; i < inputs.Length; i++) {
            input_layer[i] = inputs[i];
        }

        for (int l = 0 ; l < neurons.Count -1; l++) {
            float[][] layer_weights = weights[l];
            int next_layer = l + 1;
            List<float> neurons_in_layer = neurons[l]; //these are the neurons in a specific layer
            List<float> neurons_in_next_layer = neurons[next_layer]; // these are the neurons in the next layer
            for (int j = 0; j < neurons_in_next_layer.Count ; j++) {
                float sum = 0;
                for (int k = 0 ; k < neurons_in_layer.Count ; k++) {
                    sum += layer_weights[k][j] * neurons_in_layer[k];
                }
                neurons_in_next_layer[j] = sigmoid(sum); // we need to turn it into a range [0,1] using sigmoid 
            }
        }
    }


    /*
    Some helper methods + getters and setters for the private variables of this script. sigmoid is a function in mathematics used to calculate a specific range [0,1] of a float value.
    method names explain the purpose well.
 */
    public int get_size_layer(int i) {
        int size_layer = 0;
        if (i == 0) { //if its the first layer, it means we're referring to the inputs
            size_layer = num_of_inputs;
        }
        else if (i == hidden_layers + 1) {
            size_layer = num_of_outputs; //if its the last layer, we're referring to the outputs
        }
        else {
            size_layer = size_hidden_layers;
        }
        return size_layer;
    }
   
    public List<float> get_outputs() {
        return neurons[neurons.Count - 1];
    }
    public float sigmoid(float x) {
        return 1/(float)(1 + Mathf.Pow(EULER, -x));
    }
    public float generate_random_value() {
        return Random.Range(-max_value_init, max_value_init);
    }

    public List<List<float>> get_neurons() {
        return neurons;
    }

    public List<float[][]> get_weights() {
        return weights;
    }
}
