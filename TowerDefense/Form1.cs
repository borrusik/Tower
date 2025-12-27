using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TowerDefense.Core.Entities;
using TowerDefense.Core.Factories;
using TowerDefense.Core.States;

namespace TowerDefense
{
    public partial class Form1 : Form
    {
        private readonly List<Enemy> _enemies = new();
        private readonly List<Tower> _towers = new();

        private readonly EnemyFactory _enemyFactory = new();
        private readonly TowerFactory _towerFactory = new();
        private readonly Random _rng = new();

        private readonly GameContext _gameContext = new();
        private BuildState _buildState;
        private WaveState _waveState;
        private GameOverState _gameOverState;

        private int _money;
        private int _lives;
        private int _waveNumber;

        private const int StartMoney = 150;
        private const int StartLives = 10;

        private const int BaseEnemies = 5;
        private const int EnemyHpPerWave = 10;
        private const int EnemyHpPerTower = 5;

        private TowerType _selectedTowerType = TowerType.Cannon;
        private readonly Dictionary<Tower, Enemy> _towerTargets = new();

        public Form1()
        {
            InitializeComponent();

            gamePanel.Paint += GamePanel_Paint;
            gamePanel.MouseClick += GamePanel_MouseClick;

            btnStart.Click += BtnStart_Click;
            gameTimer.Tick += GameTimer_Tick;

            cmbTowerType.DataSource = Enum.GetValues(typeof(TowerType));
            cmbTowerType.SelectedIndexChanged += (_, __) =>
            {
                _selectedTowerType = (TowerType)cmbTowerType.SelectedItem;
                UpdateHud();
            };

            _buildState = new BuildState(PlaceTower);
            _waveState = new WaveState(SpawnWave, () => _enemies.Count == 0, PlaceTower);
            _gameOverState = new GameOverState(OnGameOver);

            ResetGame();
        }

        private void ResetGame()
        {
            gameTimer.Stop();

            _enemies.Clear();
            _towers.Clear();

            _money = StartMoney;
            _lives = StartLives;
            _waveNumber = 1;

            btnStart.Text = "Start Wave";

            _gameContext.SetState(_buildState);
            UpdateHud();
            gamePanel.Invalidate();
        }

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            if (_gameContext.CurrentState == _gameOverState)
            {
                ResetGame();
                return;
            }

            _gameContext.SetState(_waveState);
            gameTimer.Start();
        }

        private int GetTowerCost(TowerType type)
        {
            return type switch
            {
                TowerType.Cannon => 50,
                TowerType.Slow => 70,
                _ => 50
            };
        }

        private void SpawnWave()
        {
            int enemyCount = BaseEnemies + _waveNumber + _towers.Count / 2;

            for (int i = 0; i < enemyCount; i++)
            {
                var type = _rng.Next(100) < 60
                    ? EnemyType.Fast
                    : EnemyType.Tank;

                var enemy = _enemyFactory.Create(type, -i * 60, 180);
                enemy.AddHealth(_waveNumber * EnemyHpPerWave);
                enemy.AddHealth(_towers.Count * EnemyHpPerTower);

                _enemies.Add(enemy);
            }
        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            foreach (var enemy in _enemies)
                enemy.Move();

            _towerTargets.Clear();
            foreach (var tower in _towers)
            {
                var target = _enemies.FirstOrDefault(e => tower.IsInRange(e));
                if (target != null)
                {
                    tower.Attack(target);
                    _towerTargets[tower] = target;
                }
            }

            int killed = _enemies.RemoveAll(e => e.IsDead);
            if (killed > 0)
            {
                _money += killed * 5;
                UpdateHud();
            }

            int escaped = _enemies.RemoveAll(e => e.X > gamePanel.Width);
            if (escaped > 0)
            {
                _lives -= escaped;
                UpdateHud();

                if (_lives <= 0)
                {
                    gameTimer.Stop();
                    _gameContext.SetState(_gameOverState);
                    return;
                }
            }

            if (_enemies.Count == 0 && _gameContext.CurrentState == _waveState)
            {
                gameTimer.Stop();
                _waveNumber++;
                _gameContext.SetState(_buildState);
                UpdateHud();
            }

            gamePanel.Invalidate();
        }

        private void GamePanel_MouseClick(object? sender, MouseEventArgs e)
        {
            _gameContext.Click(e.X, e.Y);
            gamePanel.Invalidate();
        }

        private void PlaceTower(int x, int y)
        {
            if (y >= 170 && y <= 230)
                return;

            int cost = GetTowerCost(_selectedTowerType);

            if (_money < cost)
                return;

            var tower = _towerFactory.Create(
                _selectedTowerType,
                x - 20,
                y - 20
            );

            _towers.Add(tower);
            _money -= cost;
            UpdateHud();
        }

        private void OnGameOver()
        {
            btnStart.Text = "Restart";
            MessageBox.Show("Game Over");
        }

        private void UpdateHud()
        {
            int cost = GetTowerCost(_selectedTowerType);

            lblMoney.Text = $"Money: {_money} | Tower cost: {cost}";
            lblLives.Text = $"Lives: {_lives} | Wave: {_waveNumber}";

            // Информация о выбранной башне
            string towerInfo = _selectedTowerType switch
            {
                TowerType.Cannon => "Cannon Tower\n━━━━━━━━\nDamage: 10\nRange: 100\nSpeed: Fast\n━━━━━━━━\nDeals direct damage",
                TowerType.Slow => "Slow Tower\n━━━━━━━━\nSlow: 2\nRange: 120\nSpeed: Medium\n━━━━━━━━\nReduces enemy speed",
                _ => ""
            };
            lblTowerInfo.Text = towerInfo;
        }

        private void GamePanel_Paint(object? sender, PaintEventArgs e)
        {
            // Включаем сглаживание
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Рисуем фон (трава)
            using (var grassBrush = new SolidBrush(Color.FromArgb(124, 252, 0)))
            {
                e.Graphics.FillRectangle(grassBrush, 0, 0, gamePanel.Width, gamePanel.Height);
            }

            // Добавляем текстуру травы (простые точки)
            for (int i = 0; i < 100; i++)
            {
                int x = (i * 47) % gamePanel.Width;
                int y = (i * 67) % gamePanel.Height;
                using (var darkGrassBrush = new SolidBrush(Color.FromArgb(100, 200, 0)))
                {
                    e.Graphics.FillEllipse(darkGrassBrush, x, y, 3, 3);
                }
            }

            // Рисуем дорогу
            using (var roadBrush = new SolidBrush(Color.FromArgb(169, 169, 169)))
            {
                e.Graphics.FillRectangle(roadBrush, 0, 170, gamePanel.Width, 60);
            }

            // Пунктирная линия по центру дороги
            using (var dashedPen = new Pen(Color.FromArgb(255, 255, 255), 2))
            {
                dashedPen.DashPattern = new float[] { 10, 5 };
                e.Graphics.DrawLine(dashedPen, 0, 200, gamePanel.Width, 200);
            }

            // Рисуем границы дороги
            using (var borderPen = new Pen(Color.FromArgb(105, 105, 105), 2))
            {
                e.Graphics.DrawLine(borderPen, 0, 170, gamePanel.Width, 170);
                e.Graphics.DrawLine(borderPen, 0, 230, gamePanel.Width, 230);
            }

            // Рисуем башни
            foreach (var tower in _towers)
            {
                Color towerColor = tower switch
                {
                    CannonTower => Color.FromArgb(65, 105, 225),  // Синий
                    SlowTower => Color.FromArgb(138, 43, 226),    // Фиолетовый
                    _ => Color.Blue
                };

                // Радиус атаки (полупрозрачный)
                using (var rangeBrush = new SolidBrush(Color.FromArgb(30, towerColor)))
                {
                    e.Graphics.FillEllipse(
                        rangeBrush,
                        tower.X + tower.Width / 2 - tower.Range,
                        tower.Y + tower.Height / 2 - tower.Range,
                        tower.Range * 2,
                        tower.Range * 2
                    );
                }

                // Башня
                using (var towerBrush = new SolidBrush(towerColor))
                using (var borderBrush = new SolidBrush(Color.FromArgb(Math.Max(0, towerColor.R - 40), 
                                                                       Math.Max(0, towerColor.G - 40), 
                                                                       Math.Max(0, towerColor.B - 40))))
                {
                    e.Graphics.FillRectangle(towerBrush, tower.X, tower.Y, tower.Width, tower.Height);
                    e.Graphics.DrawRectangle(new Pen(borderBrush, 2), tower.X, tower.Y, tower.Width, tower.Height);
                }

                // Линия выстрела
                if (_towerTargets.TryGetValue(tower, out var target))
                {
                    using (var shotPen = new Pen(Color.FromArgb(200, 255, 255, 0), 2))
                    {
                        e.Graphics.DrawLine(
                            shotPen,
                            tower.X + tower.Width / 2,
                            tower.Y + tower.Height / 2,
                            target.X + target.Width / 2,
                            target.Y + target.Height / 2
                        );
                    }
                }
            }

            // Рисуем врагов
            foreach (var enemy in _enemies)
            {
                Color enemyColor = enemy switch
                {
                    FastEnemy => Color.FromArgb(255, 69, 0),     // Красно-оранжевый (ярче)
                    TankEnemy => Color.FromArgb(178, 34, 34),    // Огненно-красный
                    _ => Color.Red
                };

                // Тень под врагом
                using (var shadowBrush = new SolidBrush(Color.FromArgb(80, 0, 0, 0)))
                {
                    e.Graphics.FillRectangle(shadowBrush, enemy.X + 2, enemy.Y + 2, enemy.Width, enemy.Height);
                }

                // Враг
                using (var enemyBrush = new SolidBrush(enemyColor))
                {
                    e.Graphics.FillRectangle(enemyBrush, enemy.X, enemy.Y, enemy.Width, enemy.Height);
                }
                
                // Толстая черная рамка
                using (var borderPen = new Pen(Color.Black, 3))
                {
                    e.Graphics.DrawRectangle(borderPen, enemy.X, enemy.Y, enemy.Width, enemy.Height);
                }
                
                // Белая внутренняя рамка для контраста
                using (var innerPen = new Pen(Color.White, 1))
                {
                    e.Graphics.DrawRectangle(innerPen, enemy.X + 1, enemy.Y + 1, enemy.Width - 2, enemy.Height - 2);
                }

                // Полоска здоровья
                int maxHealth = enemy is FastEnemy ? 20 : 50; // Базовое здоровье + бонусы
                maxHealth += _waveNumber * EnemyHpPerWave + _towers.Count * EnemyHpPerTower;
                
                int barWidth = enemy.Width;
                int barHeight = 4;
                int barX = enemy.X;
                int barY = enemy.Y - 8;

                // Фон полоски
                e.Graphics.FillRectangle(Brushes.Black, barX, barY, barWidth, barHeight);

                // Здоровье
                float healthPercent = Math.Min(1.0f, (float)enemy.Health / maxHealth);
                int healthWidth = Math.Min(barWidth, (int)(barWidth * healthPercent));
                
                Color healthColor = healthPercent > 0.6f ? Color.LimeGreen :
                                  healthPercent > 0.3f ? Color.Orange : Color.Red;
                
                using (var healthBrush = new SolidBrush(healthColor))
                {
                    e.Graphics.FillRectangle(healthBrush, barX, barY, healthWidth, barHeight);
                }

                // Рамка полоски
                e.Graphics.DrawRectangle(Pens.White, barX, barY, barWidth, barHeight);
            }
        }
    }
}
