using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA {
    private List<float[][]> dna;
    private float probability_of_mutation = 0.5f;
    private float max_mutation = 5f; //do we even need to use this?
    private float max_variation = 1f;


    public DNA(List<float[][]> weights ) { //test
        this.dna = weights;
    }

    public List<float[][]> getDNA() {
        return dna;
    }

    public DNA mutate() {
        List<float[][]> newDNA = new List<float[][]>();
        for (int i = 0 ; i < dna.Count ; i++) {
            float[][] layer_weights = dna[i];
            for (int j = 0 ; j < layer_weights.Length ; j++) {
                for (int k = 0 ; k < layer_weights[j].Length ; k++) {
                    float random = Random.Range(0f,1f);
                    if (random < probability_of_mutation) { 
                        layer_weights[j][k] = Random.Range(-max_variation,max_variation); //test
                    }
                }
            }
            newDNA.Add(layer_weights);
        }
        return new DNA(newDNA);
    }

    public DNA crossover(DNA other_parent) {
        List<float[][]> child = new List<float[][]>();
        for (int i = 0 ; i < dna.Count ; i++) {
            float[][] parent_layer = dna[i];
            float[][] other_parent_layer = other_parent.getDNA()[i];
            for (int j = 0 ; j < parent_layer.Length; j++) {
                for (int k = 0 ; k < parent_layer[j].Length; k++) {
                    float randomize = Random.Range(0f,1f);
                    if (randomize < 0.5f){
                        parent_layer[j][k] = other_parent_layer[j][k];
                    }
                    else { //edit: might not need this, check in the future.
                        parent_layer[j][k] = parent_layer[j][k];
                    }
                }
            }
            child.Add(parent_layer);
        }
        return new DNA(child);
    }
}
