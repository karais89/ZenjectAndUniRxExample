using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UniRxExample.OnGrounded
{
    public class OnGroundedWithoutUniRx : MonoBehaviour
    {
        public CharacterController characterController;
        public ParticleSystem particleSystem;
        private bool _oldFlag;

        // Start is called before the first frame update
        void Start()
        {
            _oldFlag = characterController.isGrounded;
        }

        // Update is called once per frame
        void Update()
        {
            bool cntFlag = characterController.isGrounded;
            if (cntFlag && !_oldFlag)
            {
                particleSystem.Play();
            }
            _oldFlag = cntFlag;
        }
    }
}