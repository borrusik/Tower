using System;

namespace TowerDefense.Core.States
{
    public class GameOverState : IGameState
    {
        private readonly Action _onGameOver;

        public GameOverState(Action onGameOver)
        {
            _onGameOver = onGameOver;
        }

        public void Enter()
        {
            _onGameOver();
        }

        public void Exit() { }

        public void Update() { }

        public void OnClick(int x, int y) { }
    }
}
