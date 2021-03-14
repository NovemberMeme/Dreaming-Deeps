using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

namespace DreamingDeep
{
    public class WorldTileObject
    {
        protected Grid<WorldTileObject> _grid;
        protected int x;
        protected int y;
        public PlacedObject_WorldTile placedObject;

        public WorldTileObject(Grid<WorldTileObject> grid, int x, int y)
        {
            this._grid = grid;
            this.x = x;
            this.y = y;
            placedObject = null;
        }

        public override string ToString()
        {
            return x + ", " + y + "\n" + placedObject;
        }

        public void SetPlacedObject(PlacedObject_WorldTile placedObject)
        {
            this.placedObject = placedObject;
            _grid.TriggerGridObjectChanged(x, y);
        }

        public void ClearPlacedObject()
        {
            placedObject = null;
            _grid.TriggerGridObjectChanged(x, y);
        }

        public PlacedObject_WorldTile GetPlacedObject()
        {
            return placedObject;
        }

        public bool CanBuild()
        {
            return placedObject == null;
        }

        public bool CanAddTileAspect(TileAspect _aspectToAdd)
        {
            if (placedObject.MyAspects.Contains(_aspectToAdd))
            {
                if(_aspectToAdd.IsLoopPathAspect)
                    return false;
            }

            return true;
        }
    }

    public class LoopBuildingSystem : GridBuildingSystem2D
    {
        #region Fields and Properties

        public static GridBuildingSystem2D _Instance { get; private set; }

        private Pathfinding loopPathFinding;

        [Header("Database Settings: ")]

        [SerializeField] protected DataBaseSO DB;

        [Header("World Tile Settings: ")]

        [SerializeField] protected List<TileAspect> StartingAspects = new List<TileAspect>();

        [SerializeField] protected TileAspect LoopPathAspect;

        [SerializeField] protected Transform WorldTileParent;

        [Header("BuildingSystem Settings: ")]

        protected Grid<WorldTileObject> _grid;
        [SerializeField] protected PlacedWorldTileTypeSO placedWorldTileTypeSO;

        [Header("Grid Settings: ")]

        [SerializeField] protected float CellSize = 10;
        [SerializeField] protected Transform OriginPosition;

        #endregion

        #region Unity Callbacks

        protected override void Awake()
        {
            _Instance = this;

            InitializeGrid();
            InitializeWorldTiles();
        }

        protected override void Update()
        {
            CheckMouseInput();
        }

        #endregion

        #region Build System Code

        protected virtual void InitializeGrid()
        {
            _grid = new Grid<WorldTileObject>(DB.ShowDebug, DB.GridWidth, DB.GridHeight, CellSize, OriginPosition.position, (Grid<WorldTileObject> g, int x, int y) => new WorldTileObject(g, x, y));
            loopPathFinding = new Pathfinding(DB.GridWidth, DB.GridHeight, CellSize, OriginPosition.position, StartingAspects, LoopPathAspect);
        }

        protected virtual void InitializeWorldTiles()
        {
            for (int x = 0; x < DB.GridWidth; x++)
            {
                for (int y = 0; y < DB.GridHeight; y++)
                {
                    PlaceWorldTile(new Vector3(x * CellSize, y * CellSize, 0));
                }
            }

            //LoadWorldData();
        }

        protected virtual void CheckMouseInput()
        {
            if (Input.GetMouseButtonDown(0) && placedWorldTileTypeSO != null)
            {
                Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

                SetTileAspect(LoopPathAspect, mousePosition);

                //PlaceWorldTile(mousePosition);
            }

            if (Input.GetMouseButtonDown(1) && placedWorldTileTypeSO != null)
            {
                Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

                DeleteTileAspects(mousePosition);

                //PlaceWorldTile(mousePosition);
            }
        }

        public virtual void DeleteTileAspects(Vector3 _placePosition)
        {
            _grid.GetXY(_placePosition, out int x, out int z);

            if (x > DB.GridWidth || z > DB.GridHeight)
                return;

            Vector2Int _placedObjectOrigin = new Vector2Int(x, z);

            PlacedObject_WorldTile currentTileObject = _grid.GetGridObject(_placePosition).GetPlacedObject();

            currentTileObject.DeleteAspects();
        }

        public virtual void SetTileAspect(TileAspect _tileAspect, Vector3 _placePosition)
        {
            _grid.GetXY(_placePosition, out int x, out int z);

            if (x > DB.GridWidth || z > DB.GridHeight)
                return;

            Vector2Int _placedObjectOrigin = new Vector2Int(x, z);

            PlacedObject_WorldTile currentTileObject = _grid.GetGridObject(_placePosition).GetPlacedObject();

            List<TileAspect> newAspects = new List<TileAspect>();
            newAspects.Add(LoopPathAspect);

            if (!currentTileObject.MyAspects.Contains(LoopPathAspect))
                currentTileObject.SetTileAspects(newAspects);
        }


        public virtual void PlaceWorldTile(Vector3 _placePosition)
        {
            _grid.GetXY(_placePosition, out int x, out int z);

            Vector2Int _placedObjectOrigin = new Vector2Int(x, z);

            List<Vector2Int> gridPositionList = placedWorldTileTypeSO.GetGridPositionList(_placedObjectOrigin, dir);
            bool canBuild = true;
            foreach (Vector2Int gridPosition in gridPositionList)
            {
                if (!_grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild())
                {
                    canBuild = false;
                    break;
                }
            }

            if (canBuild)
            {
                Vector2Int rotationOffset = placedWorldTileTypeSO.GetRotationOffset(dir);
                Vector3 placedObjectWorldPosition = _grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, rotationOffset.y) * _grid.GetCellSize();

                PlacedObject_WorldTile placedObject = PlacedObject_WorldTile.Create(WorldTileParent, StartingAspects, placedObjectWorldPosition, _placedObjectOrigin, dir, placedWorldTileTypeSO);
                placedObject.transform.rotation = Quaternion.Euler(0, 0, -placedWorldTileTypeSO.GetRotationAngle(dir));

                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    _grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
                }

                //OnObjectPlaced?.Invoke(this, EventArgs.Empty);

                //DeselectObjectType();
            }
            else
            {
                // Cannot build here
                UtilsClass.CreateWorldTextPopup("Cannot Build Here!", _placePosition);
            }
        }

        public override Vector3 GetMouseWorldSnappedPosition()
        {
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
            _grid.GetXY(mousePosition, out int x, out int y);

            if (placedWorldTileTypeSO != null)
            {
                Vector2Int rotationOffset = placedWorldTileTypeSO.GetRotationOffset(dir);
                Vector3 placedObjectWorldPosition = _grid.GetWorldPosition(x, y) + new Vector3(rotationOffset.x, rotationOffset.y) * _grid.GetCellSize();
                return placedObjectWorldPosition;
            }
            else
            {
                return mousePosition;
            }
        }

        public override PlacedObjectTypeSO GetPlacedObjectTypeSO()
        {
            return placedWorldTileTypeSO;
        }

        #endregion

        #region Random Loop Builder

        public virtual void GenerateRandomLoopPath()
        {
            Vector2Int randomLoopSize = GenerateRandomLoopSize();
            Vector2Int maxLoopPlacementRange = new Vector2Int(DB.GridWidth - randomLoopSize.x - 1, DB.GridHeight - randomLoopSize.y - 1);

            List<Vector2Int> randomLoopPathNodes = new List<Vector2Int>();

            int randomAmount = UnityEngine.Random.Range(3, 6);

            for (int i = 0; i < randomAmount; i++)
            {
                randomLoopPathNodes.Add(GetRandomPathNode(maxLoopPlacementRange));
            }

            List<Vector2Int> allLoopPathNodes = new List<Vector2Int>();

            int j = 0;
            while(j < randomAmount + 1)
            {
                if (j < randomAmount - 1)
                {
                    AddLoopPathNodes(Pathfinding.Instance.FindPath(randomLoopPathNodes[j], randomLoopPathNodes[j + 1]), allLoopPathNodes);
                }
                else
                {
                    // Check if it can still pathfind to goal even if goal cant be reached cuz it itself has looppathaspect
                    AddLoopPathNodes(Pathfinding.Instance.FindPath(randomLoopPathNodes[j], randomLoopPathNodes[0]), allLoopPathNodes);
                }
            }
        }

        protected virtual List<Vector2Int> AddLoopPathNodes(List<PathNode> _pathNodes, List<Vector2Int> _allPathNodes)
        {
            for (int i = 0; i < _pathNodes.Count; i++)
            {
                _allPathNodes.Add(new Vector2Int(_pathNodes[i].x, _pathNodes[i].y));
            }

            return _allPathNodes;
        }

        protected virtual Vector2Int GetRandomPathNode(Vector2Int _maxLoopPlacementRange)
        {
            while (true)
            {
                Vector2Int randomPathNode =
                    new Vector2Int(UnityEngine.Random.Range(1, _maxLoopPlacementRange.x), UnityEngine.Random.Range(1, _maxLoopPlacementRange.y));
                if(!Pathfinding.Instance.IsAdjacentToOtherLoopPaths(Pathfinding.Instance.GetNode(randomPathNode.x, randomPathNode.y)))
                {
                    return randomPathNode;
                }
            }
        }

        public virtual Vector2Int GenerateRandomLoopSize()
        {
            int loopHeight = UnityEngine.Random.Range(DB.MinRandomLoopHeight, DB.MaxRandomLoopHeight);
            int loopWidth = UnityEngine.Random.Range(DB.MinRandomLoopWidth, DB.MaxRandomLoopWidth);
            return new Vector2Int(loopWidth, loopHeight);
        }

        public virtual void GetRandomPathDirection(Vector2Int _currentPos)
        {

        }

        #endregion

        #region Save/Load

        public virtual void SaveWorldData()
        {
            List<WorldTileData> _newWorldSaveData = new List<WorldTileData>();

            for (int x = 0; x < DB.GridWidth; x++)
            {
                for (int y = 0; y < DB.GridHeight; y++)
                {
                    _newWorldSaveData.Add(
                        new WorldTileData(_grid.GetGridObject(new Vector3(x * CellSize, y * CellSize)).GetPlacedObject().MyAspects));
                }
            }

            DB.SaveWorldData(_newWorldSaveData);
        }

        public virtual void LoadWorldData()
        {
            if (DB.WorldTileDatas == null)
                return;

            int i = 0;

            for (int x = 0; x < DB.GridWidth; x++)
            {
                for (int y = 0; y < DB.GridHeight; y++)
                {
                    _grid.GetGridObject(new Vector3(x * CellSize, y * CellSize)).GetPlacedObject().
                        SetTileAspects(DB.WorldTileDatas[i].SavedAspects);
                    i++;
                }
            }
        }

        #endregion
    }
}