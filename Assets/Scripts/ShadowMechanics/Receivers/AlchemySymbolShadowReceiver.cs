using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ShadowMechanics.Receivers
{
    public class AlchemySymbolShadowReceiver : ShadowReceiver
    {
        Renderer symbolRenderer;

        void Awake()
        {
            symbolRenderer = GetComponent<Renderer>();
        }

        public override void OnEnterShadow()
        {
            Show();
        }

        public override void OnExitShadow()
        {
            Hide();
        }

        void Show()
        {
            symbolRenderer.enabled = true;

            //Shouldn't matter that we don't enable or disable functionality when out of shadows - the player should die by that point
        }

        void Hide()
        {
            symbolRenderer.enabled = false;
        }
    }
}