using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace SiegeTheSky
{
    public class OnHoverDetails : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tokenName;

        // Update is called once per frame
        void Update()
        {
            if ( DelegateManager.hoveredObject == null)
                return;
            else
            {
                if (DelegateManager.infoPanel.activeSelf == false)
                {
                    tokenName.text = "";
                }
                else
                {
                    //Debug.Log("asdasdas");
                    tokenName.text = "Name: " + DelegateManager.hoveredObject.name;
                    //Debug.Log(DelegateManager.hoveredObject.name);
                }
            }
        }
    }
}
