using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.ZenjectSample.InterfaceBinding
{
    public class SampleDevice : ISample
    {
        public void PrintMe()
        {
            Debug.Log("My Name is Sample Device");
        }
    }
}