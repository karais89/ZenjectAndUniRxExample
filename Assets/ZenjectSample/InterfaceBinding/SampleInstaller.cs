using Zenject;

namespace Assets.ZenjectSample.InterfaceBinding
{
    public class SampleInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            bool isDevice = true; // test �뵵
            if (isDevice)
            {
                Container.Bind<ISample>().To<SampleDevice>().AsSingle();
            }
            else
            {
                Container.Bind<ISample>().To<SampleEditor>().AsSingle();  // ISample �������̽� ���κп� SampleEditor ���ε� �̱�
                // ���� ���� �����̰�, �������̽��� ������ ���ε� ���� �� �ִ� �κ�
                //Container.Bind(typeof(SampleEditor), typeof(ISample)).To<SampleEditor>().AsSingle();
            }
        }
    }
}