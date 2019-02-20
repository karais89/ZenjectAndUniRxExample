using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.ZenjectSample.InterfaceBinding
{
    public class SampleEditor : ISample
    {
        public void PrintMe()
        {
            Debug.Log("My name is Sample Editor");
        }
    }
}
