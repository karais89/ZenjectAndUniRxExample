using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UniRxExample.Buffer
{
    public class Buffer : MonoBehaviour
    {
        public Button button;
        public Text text;

        private void Start()
        {
            button.OnClickAsObservable()
                .Buffer(3)
                .SubscribeToText(text, _ => text.text + "clicked\n");
        }
    }
}