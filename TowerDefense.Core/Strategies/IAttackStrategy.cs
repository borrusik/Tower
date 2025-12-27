using TowerDefense.Core.Entities;

namespace TowerDefense.Core.Strategies
{
    public interface IAttackStrategy
    {
        void Attack(Enemy enemy);
    }
}
