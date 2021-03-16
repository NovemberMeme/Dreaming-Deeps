using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DreamingDeep
{
    public class TileVisual : MonoBehaviour
    {
        // Database has all prefab references in a list
        // Whenever an Aspect/CardSO sets an Aspect, check if our current list of TileVisuals (in children) contain a TileVisual 
        //      with the index of the requested Aspect's TileVisual Prefab
        // If none we instantiate it, set its index, add it to our current list, and from here on we no longer setactive true/fals
        // Instead we just use enable = true/false for the sprite renderer/ other things

        public int PrefabIndex = 0;

        public int LoopPathIndex;
        public TextMeshProUGUI LoopPathIndexText;

        public SpriteRenderer RendererComponent;

        public virtual void SetActiveVisual(bool _isActive)
        {
            RendererComponent.enabled = _isActive;
            LoopPathIndexText.enabled = _isActive;
        }

        public virtual void SetLoopPath(bool _isActive, int _index)
        {
            LoopPathIndexText.enabled = _isActive;
            LoopPathIndex = _index;
            LoopPathIndexText.text = LoopPathIndex.ToString();
        }
    }
}