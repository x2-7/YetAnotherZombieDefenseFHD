using System;
using System.Linq;
using UnityEngine;
using BigInt = System.Numerics.BigInteger;

namespace YetAnotherZombieDefenceFHD
{
    public abstract class Enemy : MonoBehaviour
    {
        public readonly HP HP;

        [field: SerializeField]
        protected float MoveSpeed { get; private set; }

        [field: SerializeField]
        protected string Point { get; private set; }

        protected CameraManager cameraManager;

        protected virtual void Start()
        {
            cameraManager = Camera.main.GetComponent<CameraManager>();
        }

        protected virtual void Update()
        {
            Move();
        }

        protected Player GetNearestPlayer()
        {
            var orderBy = from player in cameraManager.Players
                          where player
                          where !player.HP.IsDead
                          orderby Vector3.Distance(transform.position, player.transform.position)
                          select player;
            return orderBy.FirstOrDefault();
        }

        public abstract void Move();
        public abstract void Die(Player player);
    }
}