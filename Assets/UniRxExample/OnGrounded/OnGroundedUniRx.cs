using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Assets.UniRxExample.OnGrounded
{
    public class OnGroundedUniRx : MonoBehaviour
    {
        public CharacterController characterController;
        public ParticleSystem particleSystem;

        // Start is called before the first frame update
        void Start()
        {
            this.UpdateAsObservable()
                .Select(_ => characterController.isGrounded)
                .DistinctUntilChanged()
                .Where(x => x)
                .Subscribe(_ => particleSystem.Play());
        }
    }
}