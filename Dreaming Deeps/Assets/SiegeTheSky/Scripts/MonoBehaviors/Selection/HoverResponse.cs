using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiegeTheSky
{
    [RequireComponent(typeof(SpriteProperties))]
    public class HoverResponse : MonoBehaviour, IHoverable
    {
        private SpriteProperties spriteProperties;

        private bool isHovering;

        private void Awake()
        {
            isHovering = false;
        }
        private void Start()
        {
            spriteProperties = GetComponent<SpriteProperties>();

            DelegateManager.infoPanel.SetActive(false);
        }

        private void OnEnable()
        {
            DelegateManager.stopHover += StopHover;
        }

        private void OnDisable()
        {
            DelegateManager.stopHover -= StopHover;
        }

        public void OnHover()
        {
            spriteProperties.Highlighted = true;
            isHovering = true;
            DelegateManager.hoveredObject = gameObject;
            if (!IsInvoking("ShowHoverInfoPanel"))
                Invoke("ShowHoverInfoPanel", 0f);
        }

        public void StopHover()
        {
            spriteProperties.Highlighted = false;
            isHovering = false;
            DelegateManager.hoveredObject = null;
            if (!IsInvoking("HideHoverInfoPanel"))
                Invoke("HideHoverInfoPanel", 0f);
        }

        void ShowHoverInfoPanel()
        {
            if (isHovering)
                DelegateManager.infoPanel.SetActive(true);
        }

        void HideHoverInfoPanel()
        {
            if (!isHovering)
                DelegateManager.infoPanel.SetActive(false);
        }
    }
}