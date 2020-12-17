using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GLUETHULHU_TESTING {
    public class TextDisplay : MonoBehaviour
    {
        public Text UIText;
        public string startText;
        public string winText;
        private MinigameManager minigameManager;
        private Minigame minigame;
        
        private void Start()
        {
            UIText.text = startText;
            
            minigameManager = FindObjectOfType<MinigameManager>();
            minigame = minigameManager.minigame;

            minigame.gameWin = false;
        }

        private void Update() {
            if (minigame.gameWin) {
                UIText.text = winText;
            }
        }
    }
}
