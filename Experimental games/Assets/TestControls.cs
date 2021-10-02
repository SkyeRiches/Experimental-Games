using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestControls : MonoBehaviour {

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            gameObject.transform.position += new Vector3(1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            gameObject.transform.position += new Vector3(-1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            gameObject.transform.position += new Vector3(0, 0, 1);
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            gameObject.transform.position += new Vector3(0, 0, -1);
        }
        if (Input.GetKeyDown(KeyCode.T)) {
            gameObject.transform.Rotate(0, 10, 0, Space.Self);

        }
    }
}
