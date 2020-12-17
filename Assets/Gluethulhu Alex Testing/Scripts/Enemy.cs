using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GLUETHULHU_TESTING {
    public class Enemy : MonoBehaviour {
        public Sprite badSpr;
        [Range(0, 90f)] public float tiltDeg;
        private SpriteRenderer myRenderer;
        private MinigameManager minigameManager;
        private Minigame minigame;
        // Start is called before the first frame update
        void Start() {
            myRenderer = GetComponent<SpriteRenderer>();
            minigameManager = FindObjectOfType<MinigameManager>();
            minigame = minigameManager.minigame;

            minigame.gameWin = false;

            transform.Rotate(Vector3.forward, tiltDeg);

            tiltDeg *= -2; // offsets so tilt applies properly
            StartCoroutine(tiltAnim());
        }

        // Update is called once per frame
        private IEnumerator tiltAnim() {
            while (isActiveAndEnabled) {
                yield return new WaitForSeconds(0.42f * 2);
                transform.Rotate(Vector3.forward, tiltDeg);
                tiltDeg = -tiltDeg;
            }
        }


        private void OnCollisionEnter2D(Collision2D collision) {
            minigame.gameWin = true;
            minigameManager.PlaySound("bonk");
            myRenderer.sprite = badSpr;
            myRenderer.flipY = true;
        }
    }
}
