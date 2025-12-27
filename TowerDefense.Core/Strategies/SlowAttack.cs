using TowerDefense.Core.Entities;

namespace TowerDefense.Core.Strategies
{
    public class SlowAttack : IAttackStrategy
    {
        private readonly int _damage;
        private readonly int _slow;

        public SlowAttack(int damage, int slow)
        {
            _damage = damage;
            _slow = slow;
        }

        public void Attack(Enemy enemy)
        {
            enemy.TakeDamage(_damage);
            enemy.SlowDown(_slow);
        }
    }
}
