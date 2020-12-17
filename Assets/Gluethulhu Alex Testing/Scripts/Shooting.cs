using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GLUETHULHU_TESTING {
    public class Shooting : MonoBehaviour {

        [Tooltip("The object that the projectile will come from. Should be attached to player")]
        public Transform firePoint;
        [Tooltip("The actual projectile we want to spawn when we shoot")]
        public GameObject shotPrefab;
        // [Tooltip("Amount of time in seconds before the player shoots. Tweak to match animation")]
        // public float animTime = 0.3f;
        [Tooltip("Amount of time in seconds before the player can shoot again")]
        public float cooldownTime = 1.5f;
        // Controls shooting animation
        private Animator anim;
        // Prevents the player from shooting twice
        private bool hasFired;

        // Start is called before the first frame update
        void Start() {
            // anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update() {

            // if (anim.GetCurrentAnimatorStateInfo(0).IsName("Spitting")) {
                // Stops the character's shoot animation from triggering twice in a row
                //anim.SetBool("shooting", false);
            // }
            if (Input.GetButtonDown("Space") && !hasFired) {
                //anim.SetBool("shooting", true);
                StartCoroutine(Fire());
            }

        }
        // Creates our projectile
        private IEnumerator Fire() {
            hasFired = true;
            Instantiate(shotPrefab, firePoint.position, firePoint.rotation);
            yield return new WaitForSeconds(cooldownTime);
        }
    }
}