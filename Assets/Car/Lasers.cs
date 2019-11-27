using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour {

    public Color laser_color_rgb = new Color(0,255,0,0.5f);
    public int distance = 60;
    public static int number_of_lasers = 5;
    public int angle = 130;
    public float height = 0;

    private int count;
    private GameObject[] laser_group;
    void Start() {
        count = angle / (number_of_lasers-1);
        laser_group = new GameObject[number_of_lasers];   

        for (int i = 0 ; i < number_of_lasers ; i++) {
            float currentAngle = count * i - angle/2; 
            GameObject o = new GameObject();
            Laser laser= o.AddComponent<Laser>();
            laser.final_length = 0.02f;
            laser.laser_color = laser_color_rgb;
            laser.distance_of_laser = distance;
            laser.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            laser_group[i] = o;
            laser_group[i].transform.Rotate(new Vector3(0, currentAngle, 0));
            o.transform.SetParent(transform);
        }
    }


    public float[] getDistancesOfLasers() {
        float [] lasers = new float[laser_group.Length];
        for (int i = 0 ; i < laser_group.Length ; i++)
            lasers[i] = laser_group[i].GetComponent<Laser>().getDistance();
        return lasers;
    }


        void Update() {
        foreach(GameObject laser in laser_group) {
            laser.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
        }
    }
}