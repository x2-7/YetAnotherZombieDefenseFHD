using System.Numerics;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;

namespace YetAnotherZombieDefenceFHD
{
    public class Zombie : Enemy
    {
        protected override void Start()
        {
            base.Start();

            HP.OnDamageReceived += (damageAmount, player) => { };
            HP.OnDead += Die;
        }

        public override void Die(Player player)
        {
            Debug.Log($"{GetType().FullName}.{MethodBase.GetCurrentMethod().Name}");

            // 最後に攻撃を当てたプレイヤーにポイントを加算
            var point = BigInteger.TryParse(Point, out var value) ? value : 0;
            player.AddPoint(point);

            // TODO: 死亡エフェクトを追加
            Destroy(gameObject);
        }

        public override void Move()
        {
            Debug.Log($"{GetType().FullName}.{MethodBase.GetCurrentMethod().Name}");

            if (GetNearestPlayer() is Player nearestPlayer)
            {
                transform.Translate(Time.deltaTime * MoveSpeed * nearestPlayer.transform.position);
            }
        }

        void OnCollisionEnter(Collision collisionInfo)
        {
#if UNITY_EDITOR
            // プレイヤーが触れたら死亡処理(仮)
            if (collisionInfo.gameObject.TryGetComponent<Player>(out var player))
            {
                Die(player);
            }
#endif
        }
    }
}