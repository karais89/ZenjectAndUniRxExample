using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Assets.UniRxExample.ObservableWWWEx
{
    public class ObservableWWWExample3 : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var parallel = Observable.WhenAll(
                ObservableWWW.Get("http://google.com/"),
                ObservableWWW.Get("http://bing.com/"),
                ObservableWWW.Get("http://unity3d.com/"));

            parallel.Subscribe(xs =>
            {
                Debug.Log(xs[0].Substring(0, 100));
                Debug.Log(xs[1].Substring(0, 100));
                Debug.Log(xs[2].Substring(0, 100));
            });
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}