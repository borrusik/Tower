namespace TowerDefense.Core.States
{
    public class GameContext
    {
        public IGameState CurrentState { get; private set; }

        public void SetState(IGameState state)
        {
            CurrentState?.Exit();
            CurrentState = state;
            CurrentState.Enter();
        }

        public void Update()
        {
            CurrentState?.Update();
        }

        public void Click(int x, int y)
        {
            CurrentState?.OnClick(x, y);
        }
    }
}
