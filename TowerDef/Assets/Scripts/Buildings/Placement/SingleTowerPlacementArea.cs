
using UnityEngine;

namespace TowerDefense.Buildings.Placement
{
    /// <summary>
    /// An area suitable for placing a single tower
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class SingleTowerPlacementArea : MonoBehaviour, IPlacementArea
    {
        /// <summary>
        /// Visualisation prefab to instantiate
        /// </summary>
        public PlacementTile placementTilePrefab;

        /// <summary>
        /// Visualisation prefab to instantiate on mobile platforms
        /// </summary>
        public PlacementTile placementTilePrefabMobile;

        /// <summary>
        /// <see cref="PlacementTile"/> we've spawned on our spot
        /// </summary>
        PlacementTile m_SpawnedTile;

        /// <summary>
        /// If the area is occupied
        /// </summary>
        bool m_IsOccupied;

        /// <summary>
        /// Set up visualisation tile
        /// </summary>
        protected void Awake()
        {
            PlacementTile tileToUse;
#if UNITY_STANDALONE
            tileToUse = placementTilePrefab;
#else
			tileToUse = placementTilePrefabMobile;
#endif

            if (tileToUse != null)
            {
                m_SpawnedTile = Instantiate(tileToUse);
                m_SpawnedTile.transform.SetParent(transform);
                m_SpawnedTile.transform.localPosition = new Vector3(0f, 0.05f, 0f);
            }
        }

        /// <summary>
        /// Returns (0, 0), as there is only one available spot
        /// </summary>
        /// <param name="worldPosition"><see cref="Vector3"/> indicating world space coordinates to convert.</param>
        /// <param name="sizeOffset"><see cref="IntVector2"/> indicating size of object to center.</param>
        public Vector2Int WorldToGrid(Vector3 worldPosition, Vector2Int sizeOffset)
        {
            return new Vector2Int(0, 0);
        }

        /// <summary>
        /// Returns transform.position, as there is only one available spot
        /// </summary>
        /// <param name="gridPosition">The coordinate in grid space</param>
        /// <param name="sizeOffset"><see cref="IntVector2"/> indicating size of object to center.</param>
        public Vector3 GridToWorld(Vector2Int gridPosition, Vector2Int sizeOffset)
        {
            return transform.position;
        }

        /// <summary>
        /// Tests whether the placement area is valid.
        /// </summary>
        /// <param name="gridPos">The grid location</param>
        /// <param name="size">The size of the item</param>
        public TowerFitStatus Fits(Vector2Int gridPos, Vector2Int size)
        {
            return m_IsOccupied ? TowerFitStatus.Overlaps : TowerFitStatus.Fits;
        }

        /// <summary>
        /// Occupies the area
        /// </summary>
        /// <param name="gridPos"></param>
        /// <param name="size"></param>
        public void Occupy(Vector2Int gridPos, Vector2Int size)
        {
            m_IsOccupied = true;

            if (m_SpawnedTile != null)
            {
                m_SpawnedTile.SetState(PlacementTileState.Filled);
            }
        }

        /// <summary>
        /// Clears the area
        /// </summary>
        /// <param name="gridPos"></param>
        /// <param name="size"></param>
        public void Clear(Vector2Int gridPos, Vector2Int size)
        {
            m_IsOccupied = false;

            if (m_SpawnedTile != null)
            {
                m_SpawnedTile.SetState(PlacementTileState.Empty);
            }
        }

#if UNITY_EDITOR
        /// <summary>
        /// Draw the spot as a smalls phere in the scene view.
        /// </summary>
        void OnDrawGizmos()
        {
            Color prevCol = Gizmos.color;
            Gizmos.color = Color.yellow;

            Matrix4x4 originalMatrix = Gizmos.matrix;
            Gizmos.matrix = transform.localToWorldMatrix;
            Vector3 size = new Vector3(2 , 0f , 2);
            Gizmos.DrawWireCube( Vector3.zero , size);

            Gizmos.matrix = originalMatrix;
            Gizmos.color = prevCol;

            // Draw icon too
            Gizmos.DrawIcon(transform.position + Vector3.up, "build_zone.png", true);
        }
#endif
    }
}