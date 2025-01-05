using System;
using System.Reflection;
using UnityEngine;
using YetAnotherZombieDefenceFHD.Resources;
using BigInt = System.Numerics.BigInteger;

namespace YetAnotherZombieDefenceFHD
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed = 20f;

        [SerializeField]
        private float rotateSpeed = 10f;

        [field: SerializeField]
        public HP HP { get; set; }

        public Point Point = default;

        private CameraManager cameraManager;

        private Quaternion q = default;
        private Rigidbody rb;

        public event Action<Quaternion> OnPlayerRotated;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            cameraManager = Camera.main.GetComponent<CameraManager>();
            cameraManager.Players.Add(this);

            Point.OnPointAdded += (totalAmount, addAmount) => Debug.Log($"Total: {totalAmount}, Add: {addAmount}");
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            // プレイヤーの移動
            var h = Input.GetAxisRaw(StringResource.HorizontalAxisName);
            var v = Input.GetAxisRaw(StringResource.VerticalAxisName);
            var moveVector = h * Vector3.right + v * Vector3.forward;
            if (moveVector.magnitude <= Mathf.Epsilon)
            {
                return;
            }
            rb.linearVelocity += Time.deltaTime * moveSpeed * moveVector;

            // オブジェクトを前方に向ける
            q.SetLookRotation(moveVector, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotateSpeed);

            OnPlayerRotated?.Invoke(q);
        }

        public void AddPoint(BigInt amount)
        {
            Debug.Log($"{GetType().FullName}.{MethodBase.GetCurrentMethod().Name}");

            Point.AddPoint(amount);
        }

        public static implicit operator GameObject(Player playerManager) => playerManager.gameObject;
    }
}