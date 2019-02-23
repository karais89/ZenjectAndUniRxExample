using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UniRxExample.ButtonClick
{
    public class ButtonClick : MonoBehaviour
    {
        public Button button;
        public Text text;

        private void Start()
        {
            button.onClick // Unity가 제공하는 클릭 이벤트
                .AsObservable() // 이벤트를 스트림으로 변경
                .Subscribe(_ => text.text += "Clicked\n"); // 스트림의 구독 (최종적으로 무엇을 할것인가를 작성)
        }
    }
}