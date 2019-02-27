using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Assets.UniRxExample.GoalUpdateDelete
{
    public class GoalUpdateDeleteExample : MonoBehaviour
    {
        public bool canPlayerMove = true;
        public bool isOnGrounded = true;
        public int ammoCount = 100;

        // Start is called before the first frame update
        void Start()
        {
            // 이동 가능할때에 이동키가 일정 이상 입력 받으면 이동
            this.UpdateAsObservable()
                .Where(_ => canPlayerMove)
                .Select(_ => new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")))
                .Where(inputVector => inputVector.magnitude > 0.5f)
                .Subscribe(Move);

            // 이동 가능하고, 지면에 있을때에 점프 버튼이 눌리면 점프
            this.UpdateAsObservable()
                .Where(_ => canPlayerMove && isOnGrounded && Input.GetButtonDown("Jump"))
                .Subscribe(_ => Jump());

            // 총알이 있는 경우 공격 버튼이 눌리면 공격
            this.UpdateAsObservable()
                .Where(_ => ammoCount > 0 && Input.GetKeyDown(KeyCode.A))
                .Subscribe(_ => Attack());
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