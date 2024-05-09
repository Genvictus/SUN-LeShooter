using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Nightmare
{
    public class CheatManager : MonoBehaviour
    {
        private CheatDFA cheatDFA;
        private Cheat[] cheats;
        public AudioClip cheatAudioClip;
        public GameObject cheatUI;
        public Canvas cheatCanvas;
        public Text cheatText;
        public float cheatDisplayTime = 2f;
        AudioSource cheatAudio;

        void Start()
        {
		    cheatCanvas = cheatUI.GetComponentInChildren<Canvas>();
		    cheatText = cheatCanvas.GetComponentInChildren<Text>();

            cheatAudio = gameObject.AddComponent<AudioSource>();
            cheatAudio.clip = cheatAudioClip;

            cheatDFA = new CheatDFA();
            cheats = new Cheat[] {
                new PlayerInvincibleCheat(),
                new OneHitKillCheat(),
                new InfiniteMoneyCheat(),
                new DoubleSpeedCheat(),
                new PetInvincibleCheat(),
                new KillPetCheat(),
                new GetAllOrbsCheat(),
                new SkipLevelCheat(),
            };

            foreach (Cheat cheat in cheats)
            {
                cheatDFA.AddCheat(cheat);
            }
        }

        void Update()
        {
            if (Input.anyKeyDown)
            {
                string inputKey = Input.inputString.ToLower();
                cheatDFA.Next(inputKey);
                if (cheatDFA.currentNode.isTerminal)
                {
                    Debug.Log("Cheat Activated: " + cheatDFA.currentNode.cheat.cheatName);
                    
                    string cheatMessage = cheatDFA.currentNode.cheat.ExecuteCheat();
                    Debug.Log(cheatMessage);
                    
                    cheatText.text = cheatMessage;
                    cheatCanvas.enabled = true;
                    cheatAudio.Play();

                    cheatDFA.currentNode = cheatDFA.startNode;

                    StartCoroutine(ReDisableCanvas());
                }
            }
        }

        private IEnumerator ReDisableCanvas() {
            yield return new WaitForSeconds(cheatDisplayTime);
           cheatCanvas.enabled = false;
        }
    }
}
