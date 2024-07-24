using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ShadowMechanics.Receivers
{
    public class PlayerShadowReceiver : ShadowReceiver
    {
        public override void OnEnterShadow()
        {
            Debug.LogError("Why was the player not in shadow in the first place?!!");
        }

        public override void OnExitShadow()
        {
            Die();
        }

        void Die()
        {
            Debug.Log("Player died.");
        }
    }
}