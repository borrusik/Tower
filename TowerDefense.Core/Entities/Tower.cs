using TowerDefense.Core.Strategies;

namespace TowerDefense.Core.Entities
{
    public abstract class Tower : GameObject
    {
        protected IAttackStrategy AttackStrategy;

        public int Range { get; protected set; }

        protected Tower(int x, int y, int range, IAttackStrategy attackStrategy)
            : base(x, y, 40, 40)
        {
            Range = range;
            AttackStrategy = attackStrategy;
        }

        public void Attack(Enemy enemy)
        {
            AttackStrategy.Attack(enemy);
        }

        public bool IsInRange(Enemy enemy)
        {
            int dx = enemy.X - X;
            int dy = enemy.Y - Y;
            return dx * dx + dy * dy <= Range * Range;
        }
    }
}
