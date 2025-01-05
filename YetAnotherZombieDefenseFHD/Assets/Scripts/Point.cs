using System;
using System.Numerics;
using System.Reflection;
using UnityEngine;

namespace YetAnotherZombieDefenceFHD
{
    public struct Point
    {
        public BigInteger Amount { get; private set; }

        /// <summary>
        /// TotalAmount, AddAmount
        /// </summary>
        public event Action<BigInteger, BigInteger> OnPointAdded;

        public void AddPoint(BigInteger amount)
        {
            Amount += amount;
            OnPointAdded?.Invoke(Amount, amount);
        }
    }
}