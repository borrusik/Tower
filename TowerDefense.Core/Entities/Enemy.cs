namespace TowerDefense.Core.Entities
{
    public abstract class Enemy : GameObject
    {
        public int Health { get; protected set; }
        public int Speed { get; protected set; }

        protected Enemy(int x, int y, int health, int speed)
            : base(x, y, 30, 30)
        {
            Health = health;
            Speed = speed;
        }

        public virtual void Move()
        {
            X += Speed; // пока враги идут вправо
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public void SlowDown(int value)
        {
            Speed = Math.Max(1, Speed - value);
        }

        public void AddHealth(int value)
        {
            Health += value;
        }


        public bool IsDead => Health <= 0;
    }
}
