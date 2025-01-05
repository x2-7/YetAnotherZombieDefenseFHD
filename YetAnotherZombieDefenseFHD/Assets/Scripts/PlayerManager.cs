using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using YetAnotherZombieDefenceFHD.Resources;

namespace YetAnotherZombieDefenceFHD
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed = 20f;

        [SerializeField]
        private float rotateSpeed = 10f;

        private CameraManager cameraManager;

        private Quaternion q = default;
        private Rigidbody rb;

        public event Action<Quaternion> OnPlayerMoved;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            cameraManager = Camera.main.GetComponent<CameraManager>();
            cameraManager.Players.Add(this);
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

            OnPlayerMoved?.Invoke(q);
        }
    }
}