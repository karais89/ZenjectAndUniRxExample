using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Assets.UniRxExample.MouseDrag
{
    public class MouseDrag : MonoBehaviour
    {
        private float _rotationSpeed = 500.0f;
        void Start()
        {
            // OnMouseDown과 OnMouseUp의 조합으로 드래그 만 처리하는
            this.UpdateAsObservable() // Update ()의 타이밍을 알려 Observable
                .SkipUntil(this.OnMouseDownAsObservable()) // 마우스가 클릭 될 때까지 스트림을 무시
                .Select(_ => new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"))) // 마우스의 이동량을 스트림에 흐르는
                .TakeUntil(this.OnMouseUpAsObservable()) // 마우스를 놓을 때까지
                .Repeat() // TakeUntil 스트림이 종료하기 때문에 다시 Subscribe
                .Subscribe(move =>
                {
                    // 개체를 드래그하여 개체를 회전
                    transform.rotation =
                        Quaternion.AngleAxis(move.y * _rotationSpeed * Time.deltaTime, Vector3.right)
                        * Quaternion.AngleAxis(move.x * _rotationSpeed * Time.deltaTime, Vector3.up)
                        * transform.rotation;

                });
        }
    }
}