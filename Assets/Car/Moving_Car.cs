using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Car : MonoBehaviour
{

    private float rotation_of_car_y = 0;
    public float speed_of_car = 0.2f;
    private float rotation_of_car_z;
    private float acceleration;
    private float increase_acceleration = 5;

    public float speed_of_rotation = 0.5f;

    public bool activate_acceleration = true;
    // Start is called before the first frame update
    void Start()
    {
        rotation_of_car_z = speed_of_car;
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.deltaTime;
        transform.position += transform.forward * (rotation_of_car_z * time + 0.5f * acceleration * time * time);
        transform.Rotate(new Vector3(0, rotation_of_car_y, 0));
    }


    public float getAcceleration() {
        return acceleration;
    }


    /*revise - figure out why out of range */
    public void updateMovement(List<float> outputs_rotation) {

        // rotation_of_car_y = 5f;
        if (outputs_rotation[0] * 2 > 1f) {
            rotation_of_car_y = (outputs_rotation[0]*2 -1) * speed_of_rotation * Time.deltaTime;
        }
        else {
            rotation_of_car_y = -(outputs_rotation[0]*2) * Time.deltaTime;
        }
        if (outputs_rotation[1] * 2 > 1f) {
            acceleration = (outputs_rotation[1]*2 - 1) * increase_acceleration;
        }
        else {
            acceleration = -outputs_rotation[1] * 2 * increase_acceleration;
        }
    }
}
