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

    }

    public class LoopBuildingSystem : GridBuildingSystem2D
    {
        public static GridBuildingSystem2D _Instance { get; private set; }

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

        protected virtual void InitializeGrid()
        {
            _grid = new Grid<WorldTileObject>(DB.ShowDebug, DB.GridWidth, DB.GridHeight, CellSize, OriginPosition.position, (Grid<WorldTileObject> g, int x, int y) => new WorldTileObject(g, x, y));
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
    }
}