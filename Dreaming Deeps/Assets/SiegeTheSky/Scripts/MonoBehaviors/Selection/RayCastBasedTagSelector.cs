using UnityEngine;

namespace SiegeTheSky
{
    public class RayCastBasedTagSelector : MonoBehaviour, ISelector
    {
        [Header("Highlight Logic")]

        [SerializeField] public string selectableTag = "Selectable";

        public void Check(Ray ray)
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Transform selection = hit.transform;

                if (selection.tag == selectableTag)
                {
                    DelegateManager.currentHoverSelection = selection;

                    DelegateManager.OnHover(selection);
                }
                else
                {
                    DelegateManager.currentHoverSelection = null;

                    DelegateManager.stopHover();
                }
            }
        }
    }
}