using Zenject;

namespace Assets.ZenjectSample.InterfaceBinding
{
    public class SampleInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            bool isDevice = true; // test 용도
            if (isDevice)
            {
                Container.Bind<ISample>().To<SampleDevice>().AsSingle();
            }
            else
            {
                Container.Bind<ISample>().To<SampleEditor>().AsSingle();  // ISample 인터페이스 사용부분에 SampleEditor 바인딩 싱글
                // 위와 같은 로직이고, 인터페이스를 여러개 바인딩 해줄 수 있는 부분
                //Container.Bind(typeof(SampleEditor), typeof(ISample)).To<SampleEditor>().AsSingle();
            }
        }
    }
}