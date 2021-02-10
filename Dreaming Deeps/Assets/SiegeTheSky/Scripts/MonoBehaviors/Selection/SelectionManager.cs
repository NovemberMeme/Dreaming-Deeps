using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SiegeTheSky
{
    public class SelectionManager : MonoBehaviour
    {
        #region Fields and Properties

        private IRayProvider _rayProvider;
        private ISelector _selector;
        private IClickable _clickResponse;
        private IDoubleClickable _doubleClickResponse;

        //[SerializeField] private ConnectedTowersList _connectedTowers;

        [Header("UI Logic: ")]
        [SerializeField] private GameObject _infoPanel;

        [Header("Power Grid Logic: ")]

        [SerializeField] private TextMeshProUGUI powerText;

        [Header("Drag Logic: ")]

        [SerializeField] private Vector3 lastMouseDownPosition;
        [SerializeField] private float doubleClickThreshold = 0.3f;
        [SerializeField] private bool isWithinDoubleClickThreshold = false;

        [SerializeField] private List<Draggable> draggedItems = new List<Draggable>();

        [Header("Selection Logic: ")]

        [SerializeField] private Transform _currentClickSelection;

        [Header("Crafting Logic: ")]

        [SerializeField] private List<GameObject> _currentCraftingMaterials = new List<GameObject>();

        //[SerializeField] private ThingRuntimeSet allWorldObjects;

        //[SerializeField] private List<RecipeType> allCraftRecipes = new List<RecipeType>();

        #endregion

        #region MonoBehavior Functions

        private void Awake()
        {
            //DelegateManager.connectedTowers = _connectedTowers;

            _rayProvider = GetComponent<IRayProvider>();
            _selector = GetComponent<ISelector>();
            _clickResponse = GetComponent<IClickable>();
            _doubleClickResponse = GetComponent<IDoubleClickable>();

            DelegateManager.infoPanel = _infoPanel;
        }

        private void Update()
        {
            UpdatePower();

            if (Input.GetMouseButtonDown(0))
            {
                OnLeftClick();

                if (_currentClickSelection != null)
                    OnDrag();
            }

            if (Input.GetMouseButton(0))
            {
                OnDrag();
            }

            if (Input.GetMouseButtonUp(0))
                OnLMB_Up();

            if (Input.GetMouseButtonDown(1))
                OnRightClick();

            //HighlightCraftingMaterials();

            _selector.Check(_rayProvider.CreateRay());
        }

        #endregion

        #region Power Grid UI

        private void UpdatePower()
        {
            powerText.text = "Power: " + DelegateManager.currentPower + " / " + DelegateManager.maxPower;
        }

        #endregion

        #region Selection Functions

        private void OnLeftClick()
        {
            if (DelegateManager.currentHoverSelection == null)
            {
                SetMarkerPosition();
            }
            else
            {
                if(DelegateManager.currentHoverSelection != null)
                {
                    _currentClickSelection = DelegateManager.currentHoverSelection;

                    lastMouseDownPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    lastMouseDownPosition.z = 0;

                    //DelegateManager.OnClickSelect(_currentClickSelection);
                }

                //SelectCraftingMaterial();
            }
        }

        private void OnDrag()
        {
            lastMouseDownPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            lastMouseDownPosition.z = 0;

            if (_currentClickSelection == null)
                return;

            Draggable selectionDraggable = _currentClickSelection.GetComponent<Draggable>();

            if (selectionDraggable != null)
            {
                selectionDraggable.BeginDrag(lastMouseDownPosition);

                if(!draggedItems.Contains(selectionDraggable))
                    draggedItems.Add(selectionDraggable);
            }
        }

        private void OnRightClick()
        {
            //DeselectCraftingMaterials();
        }

        private void SetMarkerPosition()
        {
            lastMouseDownPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            lastMouseDownPosition.z = 0;

            _clickResponse.Click(lastMouseDownPosition);
        }

        private void OnLMB_Up()
        {
            CheckDoubleClick();

            foreach (Draggable draggedItem in draggedItems)
            {
                if (draggedItem == null)
                    continue;

                if (!draggedItem.Drop())
                {
                    DelegateManager.OnClickSelect(_currentClickSelection);
                }
            }

            draggedItems.Clear();

            _currentClickSelection = null;
        }

        private void Drop()
        {

        }

        #endregion

        #region Crafting Code

        //private void SelectCraftingMaterial()
        //{
        //    if (_currentClickSelection.GetComponent<CraftingMaterial>() == null)
        //    {
        //        //DelegateManager.OnClickSelect(_currentClickSelection);
        //        return;
        //    }

        //    if (_currentCraftingMaterials.Count < 4)
        //    {
        //        for (int i = 0; i < _currentCraftingMaterials.Count; i++)
        //        {
        //            if (_currentClickSelection.gameObject == _currentCraftingMaterials[i])
        //                return;
        //        }

        //        _currentCraftingMaterials.Add(_currentClickSelection.gameObject);

        //        if (_currentCraftingMaterials.Count == 3)
        //        {
        //            AttemptCraft();
        //        }
        //        else
        //            HighlightCraftingMaterials();
        //    }
        //}

        //private void AttemptCraft()
        //{
        //    List<int> currentRecipe = new List<int>();

        //    for (int i = 0; i < _currentCraftingMaterials.Count; i++)
        //    {
        //        if (_currentCraftingMaterials[i].GetComponent<CraftingMaterial>() == null)
        //            return;

        //        currentRecipe.Add((int)_currentCraftingMaterials[i].GetComponent<CraftingMaterial>().MyCraftingMaterialType);
        //    }

        //    for (int i = 0; i < allCraftRecipes.Count; i++)
        //    {
        //        if(DoRecipesMatch(currentRecipe, allCraftRecipes[i].GetRecipe()))
        //        {
        //            Craft(allCraftRecipes[i]);
        //            return;
        //        }
        //    }

        //    _currentCraftingMaterials.Remove(_currentClickSelection.gameObject);

        //    Debug.Log("No recipe matched!");

        //    DeselectCraftingMaterials();

        //    for (int i = 0; i < currentRecipe.Count; i++)
        //    {
        //        Debug.Log(currentRecipe[i].ToString());
        //    }
        //}

        //private void Craft(RecipeType objectToCraft)
        //{
        //    // Marker

        //    GameObject craftedObject = Instantiate(objectToCraft.CraftedObject, DelegateManager.marker.transform.position, Camera.main.transform.rotation);

        //    Module newModule = craftedObject.GetComponent<Module>();

        //    if (newModule == null)
        //        return;

        //    newModule.RecipeType = objectToCraft;

        //    for (int i = _currentCraftingMaterials.Count - 1; i >= 0; i--)
        //    {
        //        Debug.Log(_currentCraftingMaterials[i].name);
        //        //Destroy(_currentCraftingMaterials[i]);
        //        _currentCraftingMaterials[i].SetActive(false);
        //    }

        //    DeselectCraftingMaterials();
        //}

        //private bool DoRecipesMatch(List<int> list1, List<int> list2)
        //{
        //    if (list1.Count != list2.Count)
        //        return false;

        //    list1.Sort();
        //    list2.Sort();

        //    for (int i = 0; i < list1.Count; i++)
        //    {
        //        if (list2[i] != list1[i])
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}

        //private void DeselectCraftingMaterials()
        //{
        //    UnHighlightCraftingMaterials();

        //    _currentCraftingMaterials.Clear();
        //}

        //private void HighlightCraftingMaterials()
        //{
        //    for (int i = 0; i < _currentCraftingMaterials.Count; i++)
        //    {
        //        DelegateManager.OnClickSelect(_currentCraftingMaterials[i].transform);
        //    }
        //}

        //private void UnHighlightCraftingMaterials()
        //{
        //    //for (int i = 0; i < _currentCraftingMaterials.Count; i++)
        //    //{
        //    //    DelegateManager.StopClickSelect(_currentCraftingMaterials[i].transform);
        //    //}

        //    DelegateManager.deselectAll();
        //}

        #endregion

        #region DoubleClick Code

        private void CheckDoubleClick()
        {
            if (DelegateManager.currentHoverSelection != null)
            {
                if (isWithinDoubleClickThreshold)
                {
                    _doubleClickResponse.DoubleClick(DelegateManager.currentHoverSelection.gameObject, lastMouseDownPosition);
                }

                StartCoroutine(_BeginDoubleClickThreshold());
            }
        }

        private IEnumerator _BeginDoubleClickThreshold()
        {
            isWithinDoubleClickThreshold = true;

            yield return new WaitForSeconds(doubleClickThreshold);

            isWithinDoubleClickThreshold = false;
        }

        #endregion
    }
}