using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using System;

namespace Nightmare
{
    public class CheatDFA
    {
        public class DFANode
        {
            public Dictionary<string, DFANode> transitions = new();
            public bool isTerminal = false;
            public string cheatCode = ""; // can be removed, just for debug
            public Cheat cheat = null;

            public DFANode AddTransition(string input)
            {
                if (!transitions.ContainsKey(input))
                    transitions[input] = new DFANode();
                return transitions[input];
            }

        }

        public DFANode startNode;
        public DFANode currentNode;

        public CheatDFA()
        {
            startNode = new DFANode();
            currentNode = startNode;
        }

        public void AddCheat(Cheat cheat)
        {
            DFANode node = startNode;
            foreach (char letter in cheat.cheatCode)
            {
                string prevCheatCode = node.cheatCode;
                node = node.AddTransition(letter.ToString());
                node.cheatCode = prevCheatCode + letter;
            }
            node.isTerminal = true;
            node.cheat = cheat;
        }

        public void Next(string input)
        {
            if (currentNode.transitions.ContainsKey(input))
            {
                currentNode = currentNode.transitions[input];
            }
            else
            {
                currentNode = startNode;
            }
        }

    }
}
