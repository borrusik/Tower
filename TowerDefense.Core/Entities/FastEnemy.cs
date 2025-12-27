namespace TowerDefense.Core.Entities
{
    public class FastEnemy : Enemy
    {
        public FastEnemy(int x, int y) : base(x, y, health: 50, speed: 4) { }
    }
}
