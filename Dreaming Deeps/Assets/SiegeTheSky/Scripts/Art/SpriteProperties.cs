using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiegeTheSky
{
    public class SpriteProperties : MonoBehaviour
    {
        [SerializeField] private int layerOrder;
        [SerializeField] private bool highlighted, selected, neutral;
        [SerializeField] private Color tokenColor, selectColor, highlightColor;
        [SerializeField]
        private GameObject maskSprite, colorFrameSprite, baseFrameSprite,
                                            selectFrameSprite, highlightFrameSprite, artSprite;

        public bool Highlighted { get => highlighted; set => highlighted = value; }
        public bool Selected { get => selected; set => selected = value; }
        public bool Neutral { get => neutral; set => neutral = value; }
        public int LayerOrder { get => layerOrder; set => layerOrder = value; }

        // Start is called before the first frame update
        void Start()
        {
            SetSpriteLayer();
            SetFrameColor();
        }

        private void OnEnable()
        {
            DelegateManager.deselectAll += UnSelect;
        }

        private void OnDisable()
        {
            DelegateManager.deselectAll -= UnSelect;
        }

        // Update is called once per frame
        void Update()
        {
            SetFrameColor();
            SetActiveSprite();
            SetSpriteLayer();
        }

        private void OnValidate()
        {
            SetFrameColor();
            SetSpriteLayer();
            SetActiveSprite();
        }

        public void UnSelect()
        {
            Selected = false;
        }

        private void SetSpriteLayer()
        {
            colorFrameSprite.GetComponent<SpriteRenderer>().sortingOrder = LayerOrder;
            baseFrameSprite.GetComponent<SpriteRenderer>().sortingOrder = LayerOrder;
            selectFrameSprite.GetComponent<SpriteRenderer>().sortingOrder = LayerOrder;
            highlightFrameSprite.GetComponent<SpriteRenderer>().sortingOrder = LayerOrder;
            artSprite.GetComponent<SpriteRenderer>().sortingOrder = LayerOrder;
            maskSprite.GetComponent<SpriteMask>().isCustomRangeActive = true;
            maskSprite.GetComponent<SpriteMask>().frontSortingOrder = LayerOrder;
            maskSprite.GetComponent<SpriteMask>().backSortingOrder = LayerOrder - 1;
        }

        private void SetFrameColor()
        {
            if (colorFrameSprite.GetComponent<SpriteRenderer>().color != tokenColor)
                colorFrameSprite.GetComponent<SpriteRenderer>().color = tokenColor;

            if (selectFrameSprite.GetComponent<SpriteRenderer>().color != selectColor)
                selectFrameSprite.GetComponent<SpriteRenderer>().color = selectColor;

            if (highlightFrameSprite.GetComponent<SpriteRenderer>().color != highlightColor)
                highlightFrameSprite.GetComponent<SpriteRenderer>().color = highlightColor;

        }

        private void SetActiveSprite()
        {

            selectFrameSprite.GetComponent<SpriteRenderer>().enabled = Selected;

            highlightFrameSprite.GetComponent<SpriteRenderer>().enabled = Highlighted;

            if (Selected || Highlighted)
                Neutral = false;
            else
                Neutral = true;

            baseFrameSprite.GetComponent<SpriteRenderer>().enabled = Neutral;
        }
    }
}