using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;

namespace Nightmare
{
    public class CheatDFA
    {
        public class DFANode
        {
            public Dictionary<string, DFANode> transitions = new();
            public bool isTerminal = false;
            public string cheatCode = ""; // can be removed, just for debug
            public int cheatIndex = -1;

            public DFANode AddTransition(string input)
            {
                if (!transitions.ContainsKey(input))
                    transitions[input] = new DFANode();
                return transitions[input];
            }

        }

        private DFANode startNode;
        public DFANode currentNode;

        public CheatDFA()
        {
            startNode = new DFANode();
        }

        public void AddCheatCode(string cheatSequence, int cheatIndex)
        {
            DFANode node = startNode;
            foreach (char letter in cheatSequence)
            {
                string prevCheatCode = node.cheatCode;
                node = node.AddTransition(letter.ToString());
                node.cheatCode = prevCheatCode + letter;
            }
            node.isTerminal = true;
            node.cheatIndex = cheatIndex;
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
