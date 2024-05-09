using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightmare
{
    public class CheatManager : MonoBehaviour
    {
        private CheatDFA cheatDFA;
        private Pair<string, Action>[] cheatActions;

        void Start()
        {
            cheatDFA = new CheatDFA();
            cheatActions = new Pair<string, Action>[] {
                new("iwillprotectyou", PlayerInvincible),
                new("bigerwepong", OneHitKill),
                new("akutubuhpeluru", InfiniteMoney),
                new("fasterdadieh", DoubleSpeed),
                new("cumbehindme", PetInvincible),
                new("ambatu", KillPet),
                new("thankyousomuch", GetAllOrbs),
                new("skipkelas", SkipLevel),
            };

            foreach (Pair<string, Action> cheat in cheatActions)
            {
                cheatDFA.AddCheatCode(cheat.First, cheat.Second);
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
                    Debug.Log("Cheat Activated");

                    Action cheatAction = cheatDFA.currentNode.cheatAction;
                    cheatAction();

                    cheatDFA.currentNode = cheatDFA.startNode;
                }
            }
        }

        private void PlayerInvincible()
        {
            // TODO
            Debug.Log("PlayerInvincible Cheat");
        }

        private void OneHitKill()
        {
            // TODO
            Debug.Log("OneHitKill Cheat");
        }

        private void InfiniteMoney()
        {
            // TODO
            Debug.Log("InfiniteMoney Cheat");
        }

        private void DoubleSpeed()
        {
            // TODO
            Debug.Log("DoubleSpeed Cheat");
        }

        private void PetInvincible()
        {
            // TODO
            Debug.Log("PetInvincible Cheat");
        }

        private void KillPet()
        {
            // TODO
            Debug.Log("KillPet Cheat");
        }

        private void GetAllOrbs()
        {
            // TODO
            Debug.Log("GetAllOrbs Cheat");
        }

        private void SkipLevel()
        {
            // TODO
            Debug.Log("SkipLevel Cheat");
        }

    }
}
