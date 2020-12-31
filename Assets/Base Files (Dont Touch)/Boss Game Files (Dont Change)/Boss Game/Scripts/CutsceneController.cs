using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeNice
{
    public class CutsceneController : StageController
    {
        public static StageController instance;
        public string music;
        public float stageLength;
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            instance = this;
            StartCoroutine(EndStage(stageLength));
            var cutseceneMusic = GameObject.Find(music).GetComponent<AudioSource>();
            if(cutseceneMusic != null)
            {
                cutseceneMusic.Play();
            }
        }
        private IEnumerator EndStage(float delay)
        {
            yield return new WaitForSeconds(stageLength);
            gameWon.Invoke();
        }
    }
}
