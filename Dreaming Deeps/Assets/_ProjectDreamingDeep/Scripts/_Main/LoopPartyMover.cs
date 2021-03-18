using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    public class LoopPartyMover : MonoBehaviour
    {
        [SerializeField] private ThingRuntimeSet loopPathTileSet;

        [SerializeField] private float moveSpeed = 40f;
        [SerializeField] private float distanceThreshold = 0.1f;

        [Header("Debug: ")]
        
        [SerializeField] private int currentLoopPathIndex = 0;
        [SerializeField] private int currentPathIndex = 0;
        
        private List<Vector3> pathVectorList;

        private void Update()
        {
            HandleMovement();

            if (Input.GetKeyDown(KeyCode.G))
                InitiateLoopMovement();
        }

        public void InitiateLoopMovement()
        {
            transform.position = loopPathTileSet.Items[0].transform.position;
            currentLoopPathIndex = 0;
            MoveToNextLoopPath();
        }

        public void MoveToNextLoopPath()
        {
            if(currentLoopPathIndex < loopPathTileSet.Items.Count - 1)
            {
                SetTargetPosition(loopPathTileSet.Items[currentLoopPathIndex + 1].transform.position);
            }
            else
            {
                SetTargetPosition(loopPathTileSet.Items[0].transform.position);
            }
        }

        private void HandleMovement()
        {
            if (pathVectorList != null)
            {
                Vector3 targetPosition = pathVectorList[currentPathIndex];
                if (Vector3.Distance(transform.position, targetPosition) > distanceThreshold)
                {
                    Vector3 moveDir = (targetPosition - transform.position).normalized;

                    float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                    transform.position = transform.position + moveDir * moveSpeed * Time.deltaTime;
                }
                else
                {
                    currentPathIndex++;
                    if (currentPathIndex >= pathVectorList.Count)
                    {
                        StopMoving();
                        MoveToNextLoopPath();
                    }
                }
            }
        }

        private void StopMoving()
        {
            pathVectorList = null;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void SetTargetPosition(Vector3 targetPosition)
        {
            if (currentLoopPathIndex < loopPathTileSet.Items.Count - 1)
                currentLoopPathIndex++;
            else
                currentLoopPathIndex = 0;

            currentPathIndex = 0;
            Debug.Log(GetPosition());
            Debug.Log(targetPosition);
            pathVectorList = Pathfinding.Instance.FindPathOnLoop(GetPosition(), targetPosition);

            Debug.Log(pathVectorList.Count);

            if (pathVectorList != null && pathVectorList.Count > 1)
            {
                pathVectorList.RemoveAt(0);
            }
        }
    }
}