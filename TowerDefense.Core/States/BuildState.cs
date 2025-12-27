using System;

namespace TowerDefense.Core.States
{
    public class BuildState : IGameState
    {
        private readonly Action<int, int> _placeTower;

        public BuildState(Action<int, int> placeTower)
        {
            _placeTower = placeTower;
        }

        public void Enter() { }
        public void Exit() { }

        public void Update() { }

        public void OnClick(int x, int y)
        {
            _placeTower(x, y);
        }
    }
}
