namespace TowerDefense
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private DoubleBufferedPanel gamePanel;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblMoney;
        private System.Windows.Forms.Label lblLives;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.ComboBox cmbTowerType;
        private System.Windows.Forms.Label lblTowerInfo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gamePanel = new DoubleBufferedPanel();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblMoney = new System.Windows.Forms.Label();
            this.lblLives = new System.Windows.Forms.Label();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.cmbTowerType = new System.Windows.Forms.ComboBox();
            this.lblTowerInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();

            this.gamePanel.BackColor = System.Drawing.Color.White;
            this.gamePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gamePanel.Location = new System.Drawing.Point(12, 12);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Size = new System.Drawing.Size(600, 400);
            this.gamePanel.TabIndex = 0;
            
            this.btnStart.Location = new System.Drawing.Point(630, 30);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(130, 40);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start Wave";
            this.btnStart.UseVisualStyleBackColor = true;
             
            this.lblMoney.AutoSize = true;
            this.lblMoney.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMoney.Location = new System.Drawing.Point(630, 100);
            this.lblMoney.Name = "lblMoney";
            this.lblMoney.Size = new System.Drawing.Size(83, 19);
            this.lblMoney.TabIndex = 2;
            this.lblMoney.Text = "Money: 0";
            
            this.lblLives.AutoSize = true;
            this.lblLives.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLives.Location = new System.Drawing.Point(630, 130);
            this.lblLives.Name = "lblLives";
            this.lblLives.Size = new System.Drawing.Size(68, 19);
            this.lblLives.TabIndex = 3;
            this.lblLives.Text = "Lives: 0";
            
            this.gameTimer.Interval = 50;

            this.cmbTowerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTowerType.Location = new System.Drawing.Point(630, 180);
            this.cmbTowerType.Name = "cmbTowerType";
            this.cmbTowerType.Size = new System.Drawing.Size(130, 23);

            this.lblTowerInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTowerInfo.Location = new System.Drawing.Point(630, 215);
            this.lblTowerInfo.Name = "lblTowerInfo";
            this.lblTowerInfo.Size = new System.Drawing.Size(140, 200);
            this.lblTowerInfo.TabIndex = 4;
            this.lblTowerInfo.Text = "";
            this.lblTowerInfo.AutoSize = false;

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 430);
            this.Controls.Add(this.lblLives);
            this.Controls.Add(this.lblMoney);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.gamePanel);
            this.Controls.Add(this.cmbTowerType);
            this.Controls.Add(this.lblTowerInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tower Defense";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}