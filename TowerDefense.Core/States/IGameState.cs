namespace TowerDefense.Core.States
{
    public interface IGameState
    {
        void Enter();
        void Exit();
        void Update();
        void OnClick(int x, int y);
    }
}
