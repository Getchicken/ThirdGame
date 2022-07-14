using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EG 
{
    public class EnemyManager : MonoBehaviour
    {
        EnemyLocomotionManager enemyLocomotionManager;
        public bool isPerformingAction;
        
        [Header("A.I Settings")]
        public float detectionRadius = 20;
        // FOV widens with angle
        public float maximumDitectionAngle = 50;
        public float minimumDitectionAngle = -50;
        private void Awake()
        {
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
        }
        private void FixedUpdate()
        {
            HandleCurrentAction();
        }

        private void HandleCurrentAction()
        {
            if(enemyLocomotionManager.currentTarget == null)
            {
                enemyLocomotionManager.HandelDetection();
            }
        }  
    }
}