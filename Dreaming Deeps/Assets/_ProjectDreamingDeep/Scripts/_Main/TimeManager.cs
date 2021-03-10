using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    public class TimeManager : MonoBehaviour
    {
        [Header("Tick Settings: ")]

        [SerializeField] private float ticksPerSecond = 60;

        [Header("Debug: ")]

        [SerializeField] private int tick;

        private float tickTimer;

        private void Awake()
        {
            BeginTick();
        }

        private void OnEnable()
        {
            DelegateController.getTicksPerSecond += GetTicksPerSecond;
        }

        private void OnDisable()
        {
            DelegateController.getTicksPerSecond -= GetTicksPerSecond;
        }

        private void Update()
        {
            tickTimer += Time.deltaTime;
            float tickValue = 1f / ticksPerSecond;

            if (tickTimer >= tickValue)
            {
                tickTimer -= tickValue;
                tick++;
                DelegateController.tick?.Invoke(tick);
            }
        }

        public void BeginTick()
        {
            tick = 0;
        }

        public float GetTicksPerSecond()
        {
            return ticksPerSecond;
        }
    }
}