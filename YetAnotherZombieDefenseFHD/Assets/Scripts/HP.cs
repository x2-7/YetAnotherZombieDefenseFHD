using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace YetAnotherZombieDefenceFHD
{
    [Serializable]
    public struct HP
    {
        [field: SerializeField]
        public int Max { get; private set; }

        [field: SerializeField]
        public int Min { get; private set; }

        public int Current { get; private set; }

        public readonly bool IsDead => Current <= Min;

        public event Action<int, Player> OnDamageReceived;
        public event Action<Player> OnDead;

        public void Add(int amount, Player player)
        {
            Current = Mathf.Clamp(Current + amount, Min, Max);
            OnDamageReceived?.Invoke(Current, player);
            if (Current <= 0)
            {
                OnDead?.Invoke(player);
            }
        }

        public void SetMin(int value)
        {
            Min = value + (value >= Max ? -1 : 0);
        }

        public void SetMax(int value)
        {
            Max = value + (value <= Min ? 1 : 0);
        }
    }
}