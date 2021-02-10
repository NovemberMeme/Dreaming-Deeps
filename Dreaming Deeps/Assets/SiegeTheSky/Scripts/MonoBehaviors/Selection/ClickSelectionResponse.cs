using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiegeTheSky
{
    [RequireComponent(typeof(SpriteProperties))]
    public class ClickSelectionResponse : MonoBehaviour, IClickable
    {
        private SpriteProperties spriteProperties;

        private void Start()
        {
            spriteProperties = GetComponent<SpriteProperties>();
        }

        public void Click()
        {
            spriteProperties.Selected = true;
        }

        public void Click(Vector3 position)
        {

        }

        public void UnClickSelect()
        {
            spriteProperties.Selected = false;
        }
    }
}