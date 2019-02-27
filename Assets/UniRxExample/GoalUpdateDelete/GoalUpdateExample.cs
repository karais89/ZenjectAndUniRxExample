using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

namespace Assets.UniRxExample.GoalUpdateDelete
{
    public class GoalUpdateExample : MonoBehaviour
    {
        public bool canPlayerMove = true;
        public bool isOnGrounded = true;
        public int ammoCount = 100;

        // Update is called once per frame
        void Update()
        {
            if (canPlayerMove)
            {
                var inputVector = (new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));

                if (inputVector.magnitude > 0.5f)
                {
                    Move(inputVector.normalized);
                }

                if (isOnGrounded && Input.GetButtonDown("Jump"))
                {
                    Jump();
                }
            }

            if (ammoCount > 0)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    Attack();
                }
            }
        }

        private void Attack()
        {
            Debug.Log("GoalUpdateExample Attack!");
        }

        public void Move(Vector3 direction)
        {
            Debug.Log("GoalUpdateExample Move" + direction);
        }

        public void Jump()
        {
            Debug.Log("GoalUpdateExample Jump");
        }
    }
}