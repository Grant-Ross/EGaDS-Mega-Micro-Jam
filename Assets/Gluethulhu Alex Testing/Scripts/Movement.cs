using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GLUETHULHU_TESTING {
    public class Movement : MonoBehaviour {
        public float rotRange;
        public float rotSpeed;
        private Transform pivot;
        // Start is called before the first frame update
        void Start() {
            pivot = transform.GetChild(0);
        }

        // Update is called once per frame
        void Update() {
            float currentRotation = transform.eulerAngles.z;
            // converts angle from [0, 360] to [-180, 180]
            if (currentRotation > 180) { currentRotation -= 360; } 

            if (Input.GetAxisRaw("Horizontal") > 0 && currentRotation > -rotRange) {
                transform.RotateAround(pivot.position, transform.forward, -rotSpeed * Time.deltaTime * 60);
                print("rotating left");
            } else if (Input.GetAxisRaw("Horizontal") < 0 && currentRotation < rotRange) {
                transform.RotateAround(pivot.position, transform.forward, rotSpeed * Time.deltaTime * 60);
                print("rotating right");

            }
        }
    }
}
