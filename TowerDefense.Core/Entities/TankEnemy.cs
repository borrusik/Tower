namespace TowerDefense.Core.Entities
{
    public class TankEnemy : Enemy
    {
        public TankEnemy(int x, int y) : base(x, y, health: 120, speed: 1) { }
    }
}
