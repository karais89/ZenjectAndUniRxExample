using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UniRxExample.Zip
{
    public class Zip : MonoBehaviour
    {
        public Button button1;
        public Button button2;
        public Text text;

        private void Start()
        {
            button1.OnClickAsObservable()
                .Zip(button2.OnClickAsObservable(), (b1, b2) => "Clicked!")
                .First() // 1번 동작한 후에
                .Repeat() // Zip내의 버퍼를 클리어 한다
                .SubscribeToText(text, x => text.text + x + "\n");
        }
    }
}