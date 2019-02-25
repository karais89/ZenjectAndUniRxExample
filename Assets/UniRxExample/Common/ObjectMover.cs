using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.UniRxExample.Common
{
    [RequireComponent(typeof(CharacterController))]
    public class ObjectMover : MonoBehaviour
    {
        private CharacterController _characterController;
        private Vector3 _moveVector;

        void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _moveVector = new Vector3();
        }

        void Update()
        {
            var currentVelocity = _characterController.velocity;
            var nextVelocity =
                new Vector3(
                    _moveVector.x,
                    _moveVector.y + currentVelocity.y + Physics.gravity.y * Time.deltaTime,
                    _moveVector.z
                    );

            _characterController.Move(nextVelocity * Time.deltaTime);

            _moveVector = new Vector3();
        }

        public void Jump(float jumpPower)
        {
            _moveVector += new Vector3(0, jumpPower, 0);
        }

        public void MoveHorizontal(Vector3 moveVector)
        {
            Vector3 pos = moveVector;
            pos.y = 0;
            _moveVector = pos;
        }
    }
}