using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

namespace Assets.UniRxExample.MouseDoubleClick
{
    public class MouseDoubleClick : MonoBehaviour
    {
        public Text text;

        // Start is called before the first frame update
        void Start()
        {
            // UniRx Sample 08_DetectDoubleClick
            // _ = clickStream count
            //var clickStream = Observable.EveryUpdate()
            //    .Where(_ => Input.GetMouseButtonDown(0));

            var clickStream = this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonDown(0));

            clickStream.Buffer(clickStream.Throttle(TimeSpan.FromMilliseconds(200)))
                .Where(x => x.Count >= 2)
                .SubscribeToText(text, x => $"DoubleClick detected!\n Count:{x.Count}");
        }
    }
}