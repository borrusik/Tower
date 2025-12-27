using TowerDefense.Core.Entities;

namespace TowerDefense.Core.Factories
{
    public enum EnemyType
    {
        Fast,
        Tank
    }

    public class EnemyFactory
    {
        public Enemy Create(EnemyType type, int x, int y)
        {
            return type switch
            {
                EnemyType.Fast => new FastEnemy(x, y),
                EnemyType.Tank => new TankEnemy(x, y),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Unknown enemy type")
            };
        }
    }
}
