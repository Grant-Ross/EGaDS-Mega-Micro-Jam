using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GLUETHULHU_TESTING {
    public class Projectile : MonoBehaviour {
        // Instance Variables
        [Tooltip("How fast the projectile will go")]
        public float speed;
        [Tooltip("How many times the projectile can bounce before it's destroyed")]
        public int allowedRicochets;
        [Tooltip("Actual number of times the projectile has ricocheted")]
        [SerializeField] private int ricochetCount;

        // Rigidbody of this GameObject
        private Rigidbody2D rb;
        private SpriteRenderer sr;
        private MinigameManager manager;

        // Start is called before the first frame update
        void Start() {
            rb = GetComponent<Rigidbody2D>();
            sr = GetComponent<SpriteRenderer>();
            manager = FindObjectOfType<MinigameManager>();
            rb.velocity += new Vector2(transform.up.x, transform.up.y) * speed;
            manager.PlaySound("boom");
        }

        // Called whenever the collider of this object contacts another
        private void OnCollisionEnter2D(Collision2D collision) {
            ricochetCount++;
            manager.PlaySound("hit");
            fadeoutSprite();
            if (ricochetCount >= allowedRicochets || collision.gameObject.name.Equals("Alex")) {
                Destroy(gameObject);
            }
        }

        private void fadeoutSprite() {
            float newAlpha = sr.color.a - (0.85f / allowedRicochets);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, newAlpha);
        }

        // Rotates the seed to face the direction it's moving
        private void HandleRotation() {
            // transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
    }
}
