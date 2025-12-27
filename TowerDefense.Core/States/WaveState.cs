using System;

namespace TowerDefense.Core.States
{
    public class WaveState : IGameState
    {
        private readonly Action _spawnWave;
        private readonly Func<bool> _waveFinished;
        private readonly Action<int, int> _placeTower;

        public WaveState(Action spawnWave, Func<bool> waveFinished, Action<int, int> placeTower = null)
        {
            _spawnWave = spawnWave;
            _waveFinished = waveFinished;
            _placeTower = placeTower;
        }

        public void Enter()
        {
            _spawnWave();
        }

        public void Exit() { }

        public void Update()
        {
            if (_waveFinished())
            {
                // переход обратно в BuildState делаем из UI
            }
        }

        public void OnClick(int x, int y)
        {
            _placeTower?.Invoke(x, y);
        }
    }
}
