using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Game.Runtime
{
    public class PlayerUIPanelManagement : MonoBehaviour
    {
        public TextMeshProUGUI attackPower;
        public TextMeshProUGUI jumpPower;

        public PlayerAttack attackSystem;
        public MovementController movementController;

        // Update is called once per frame
        void Update()
        {
            attackPower.text = "Power: " + attackSystem.attackPower.ToString();
            jumpPower.text = "Jump: " + movementController.jumpPower.ToString();
        }
    }
}
