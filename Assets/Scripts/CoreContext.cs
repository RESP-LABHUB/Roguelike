using UnityEngine;

namespace Assets.Scripts
{
    public class CoreContext : MonoBehaviour
    {
        private void Awake()
        {
            var container = new DiContainer();
    
            container.InjectAll();
        }
    }

    public interface IEnemy
    {

    }

    public interface IWeapon
    {

    }

    public class  Enemy : IEnemy
    {
        [Inject] private IWeapon _weapon; 

    }

    public class ConcreteFactory : AbstractFactory
    {
        public IEnemy Create()
        {
            IEnemy enemy = new Enemy();

            Inject(enemy);

            return enemy;
        }
    }

    public abstract class AbstractFactory
    {
        [Inject] private DiContainer _container;

        protected void Inject(object obj)
        {
            _container.Bind(obj);
            _container.Inject(obj);
        }
    }
}