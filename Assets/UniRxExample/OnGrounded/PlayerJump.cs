using System.Collections;
using System.Collections.Generic;
using Assets.UniRxExample.Common;
using UnityEngine;

namespace Assets.UniRxExample.OnGrounded
{
    [RequireComponent(typeof (ObjectMover))]
    public class PlayerJump : MonoBehaviour
    {
        private ObjectMover _objectMover;
        private CharacterController _characterController;

        private void Start()
        {
            _objectMover = GetComponent<ObjectMover>();
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Jump") && _characterController.isGrounded)
            {
                _objectMover.Jump(5);
            }
        }
    }
}