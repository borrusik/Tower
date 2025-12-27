using TowerDefense.Core.Strategies;

namespace TowerDefense.Core.Entities
{
    public class SlowTower : Tower
    {
        public SlowTower(int x, int y)
            : base(x, y, 120, new SlowAttack(2, 2))
        {
        }
    }
}
