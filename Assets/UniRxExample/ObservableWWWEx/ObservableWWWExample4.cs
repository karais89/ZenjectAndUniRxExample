using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Assets.UniRxExample.ObservableWWWEx
{
    public class ObservableWWWExample4 : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var resoucePathURL = "http://torisoup.net/unirx-examples/resources/resourcepath.txt";
            ObservableWWW.Get(resoucePathURL)
                .SelectMany(resourceUrl => ObservableWWW.Get(resourceUrl))
                .Subscribe(Debug.Log);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}