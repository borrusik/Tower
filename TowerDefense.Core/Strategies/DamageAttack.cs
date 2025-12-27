using TowerDefense.Core.Entities;

namespace TowerDefense.Core.Strategies
{
    public class DamageAttack : IAttackStrategy
    {
        private readonly int _damage;

        public DamageAttack(int damage)
        {
            _damage = damage;
        }

        public void Attack(Enemy enemy)
        {
            enemy.TakeDamage(_damage);
        }
    }
}
