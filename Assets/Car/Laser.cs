using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
    public Color laser_color = new Color(0,1,0,0.5f);
    public int distance_of_laser = 60;
    public int angle = 130;

    public float final_length = 0.02f;
    public float initial_length = 0.02f;

    private Vector3 position_of_laser;
    private LineRenderer line_renderer;
    private float distance = 0;
    void Start() {
        distance = distance_of_laser;
        position_of_laser = new Vector3(0,0,final_length);
        line_renderer = gameObject.AddComponent<LineRenderer>();
        line_renderer.material = new Material(Shader.Find("Particles/Additive"));
        line_renderer.startColor = laser_color;
        line_renderer.endColor = laser_color;
        line_renderer.startWidth = initial_length;
        line_renderer.endWidth = final_length;
        line_renderer.positionCount = 2;
    }

    void Update() {
        Vector3 finalPoint = transform.position + transform.forward * distance_of_laser;
        RaycastHit collisionPoint;
        if (Physics.Raycast(transform.position, transform.forward, out collisionPoint, distance_of_laser)) {
            line_renderer.SetPosition(0,transform.position);
            line_renderer.SetPosition(1,collisionPoint.point);
            distance = collisionPoint.distance;
        }
        else {
            line_renderer.SetPosition(0,transform.position);
            line_renderer.SetPosition(1,finalPoint);
            distance = distance_of_laser;
        }
    }
    public float getDistance() {
            return distance/distance_of_laser; 
        }
}