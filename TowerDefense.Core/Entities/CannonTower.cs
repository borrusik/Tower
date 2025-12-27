using TowerDefense.Core.Strategies;

namespace TowerDefense.Core.Entities
{
    public class CannonTower : Tower
    {
        public CannonTower(int x, int y)
            : base(x, y, 140, new DamageAttack(10))
        {
        }
    }
}
