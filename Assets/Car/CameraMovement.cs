using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject CarOne;
    public float rotation_speed = 0.2f;
    public float key_rotation_speed = 500f;
    private float rotation_of_camera = 0;
    public float timeAnimation = 1;

    public GameObject follow;
    public Vector3 initial_position;
    public float initial_time;

    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> cars = GameObject.Find("car_controller").GetComponent<AIController>().getCars();
        follow = cars[Random.Range(0,cars.Count - 1)];
        initial_position = transform.position;
    }

    // Update is called once per frame 
    void Update()
    {
        
        if (follow != null) {
            float time_passed = (Time.time - initial_time);
            float proportion = time_passed / timeAnimation;
            Vector3 current_position;
            if (proportion < 1) {
                current_position = Vector3.Lerp(initial_position, follow.transform.position, proportion);
            } else {
                current_position = follow.transform.position;
            }

            transform.position = new Vector3 ( current_position.x, current_position.y + 12f, current_position.z - 20f);
            transform.LookAt(current_position);
            transform.Translate(Vector3.right * Time.deltaTime * rotation_of_camera * 5);
        }
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
    }

    public void Follow(GameObject newFollow) {
        initial_position = follow.transform.position;
        initial_time = Time.time;
        follow = newFollow;
    }

    public void UnFollow() {
        follow = null;
    }

    public GameObject getFollowing() {
        return follow;
    }
}
