using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace YetAnotherZombieDefenceFHD
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField]
        private float moveTime = 0.2f;

        [SerializeField]
        private Vector3 positionOffset = new(0f, 20f, -6f);

        public List<PlayerManager> Players = new();

        private void LateUpdate()
        {
            Move();
        }

        private void Move()
        {
            // プレイヤーの中心座標に移動
            var center = CalcCenterPosition();
            var lerped = Vector3.Lerp(transform.position, new(center.x + positionOffset.x, positionOffset.y, center.z + positionOffset.z), Time.deltaTime * moveTime);
            transform.position = lerped; // + positionOffset;
        }

        /// <summary>
        /// 中心座標を算出
        /// </summary>
        private Vector3 CalcCenterPosition()
        {
            var sum = Vector3.zero;
            if (Players?.Count <= 0)
            {
                return sum;
            }

            foreach (var player in Players)
            {
                sum += player.transform.position;
            }
            return sum / Players.Count;
        }
    }
}
