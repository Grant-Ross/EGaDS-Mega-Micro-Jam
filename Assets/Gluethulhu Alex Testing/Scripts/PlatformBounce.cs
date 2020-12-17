using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
namespace GLUETHULHU_TESTING {
    public class PlatformBounce : MonoBehaviour {
        public int speed;
        private Rigidbody2D rb;
        int dir;
        // Start is called before the first frame update
        void Start() {
            rb = GetComponent<Rigidbody2D>();
            dir = (Random.Range(0, 2) > 0) ? 1 : -1;
            rb.velocity = transform.right * dir * speed;
            dir = -dir;
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.GetComponent<Projectile>() == null) {
                rb.velocity = transform.right * dir * speed;
                dir = -dir;
            } 
        }
    }
}