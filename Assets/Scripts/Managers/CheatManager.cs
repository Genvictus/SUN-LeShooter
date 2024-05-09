using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightmare
{
    public class CheatManager : MonoBehaviour
    {
        private CheatDFA cheatDFA;
        private Cheat[] cheats;
        public AudioClip cheatAudioClip;
        AudioSource cheatAudio;

        void Start()
        {
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
                    cheatAudio.Play();
                    Debug.Log(cheatMessage);

                    cheatDFA.currentNode = cheatDFA.startNode;
                }
            }
        }
    }
}
