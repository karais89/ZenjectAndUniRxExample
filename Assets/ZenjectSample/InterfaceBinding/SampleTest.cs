using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.ZenjectSample.InterfaceBinding
{
    public class SampleTest : MonoBehaviour
    {
        // sample 인젝트 부분은 SampleInstaller에서 설정 해줌.
        [Inject] private ISample sample;

        // Start is called before the first frame update
        void Start()
        {
            sample.PrintMe();
        }
    }
}