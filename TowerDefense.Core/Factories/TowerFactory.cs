using TowerDefense.Core.Entities;

namespace TowerDefense.Core.Factories
{
    public enum TowerType
    {
        Cannon,
        Slow
    }
    public class TowerFactory
    {
        public Tower Create(TowerType type, int x, int y)
        {
            return type switch
            {
                TowerType.Cannon => new CannonTower(x, y),
                TowerType.Slow => new SlowTower(x, y),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }
    }
}
