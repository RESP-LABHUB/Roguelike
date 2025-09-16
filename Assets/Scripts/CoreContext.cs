using UnityEngine;

namespace Assets.Scripts
{
    public class CoreContext : MonoBehaviour
    {
        [SerializeField] private EcsManager _ecsManager;
        [SerializeField] private PlayerInputManager playerInputManager;

        private void Awake()
        {
            var container = new DiContainer();

            container.Bind(new EcsFactory(container));
            container.Bind(_ecsManager);
            container.Bind(playerInputManager);

            container.InjectAll();
        }
    }

    public class EcsFactory : AbstractFactory
    {
        public EcsFactory(DiContainer container) : base(container)
        {
        }

        public T Create<T>() where T : class, new()
        {
            T system = new T();

            Inject(system);

            return system;
        }
    }

    public abstract class AbstractFactory
    {
        private DiContainer _container;

        public AbstractFactory(DiContainer container)
        {
            _container = container;
        }

        protected void Inject(object obj)
        {
            _container.Bind(obj);
            _container.Inject(obj);
        }
    }
}