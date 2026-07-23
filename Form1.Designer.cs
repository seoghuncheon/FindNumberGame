namespace FindNumber
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.display = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.TextBox();
            this.historyListBox = new System.Windows.Forms.ListBox();
            this.bestScoreLabel = new System.Windows.Forms.Label();
            this.rangeLabel = new System.Windows.Forms.Label();
            this.timerLabel = new System.Windows.Forms.Label();
            this.chanceProgressBar = new System.Windows.Forms.ProgressBar();
            this.difficultyComboBox = new System.Windows.Forms.ComboBox();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // display
            // 
            this.display.Dock = System.Windows.Forms.DockStyle.Top;
            this.display.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.display.Location = new System.Drawing.Point(0, 0);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(838, 52);
            this.display.TabIndex = 0;
            this.display.Text = "게임을 시작합니다";
            this.display.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.display.Click += new System.EventHandler(this.label1_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(492, 183);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 35);
            this.button1.TabIndex = 2;
            this.button1.Text = "입력";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button2.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button2.Location = new System.Drawing.Point(0, 401);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(838, 45);
            this.button2.TabIndex = 3;
            this.button2.Text = "게임시작";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox
            // 
            this.textBox.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox.Location = new System.Drawing.Point(125, 179);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(163, 39);
            this.textBox.TabIndex = 4;
            this.textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // historyListBox
            // 
            this.historyListBox.FormattingEnabled = true;
            this.historyListBox.ItemHeight = 12;
            this.historyListBox.Location = new System.Drawing.Point(706, 12);
            this.historyListBox.Name = "historyListBox";
            this.historyListBox.Size = new System.Drawing.Size(120, 100);
            this.historyListBox.TabIndex = 5;
            // 
            // bestScoreLabel
            // 
            this.bestScoreLabel.AutoSize = true;
            this.bestScoreLabel.Location = new System.Drawing.Point(687, 148);
            this.bestScoreLabel.Name = "bestScoreLabel";
            this.bestScoreLabel.Size = new System.Drawing.Size(38, 12);
            this.bestScoreLabel.TabIndex = 6;
            this.bestScoreLabel.Text = "label1";
            // 
            // rangeLabel
            // 
            this.rangeLabel.AutoSize = true;
            this.rangeLabel.Location = new System.Drawing.Point(543, 248);
            this.rangeLabel.Name = "rangeLabel";
            this.rangeLabel.Size = new System.Drawing.Size(38, 12);
            this.rangeLabel.TabIndex = 7;
            this.rangeLabel.Text = "label1";
            // 
            // timerLabel
            // 
            this.timerLabel.AutoSize = true;
            this.timerLabel.Location = new System.Drawing.Point(366, 195);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(38, 12);
            this.timerLabel.TabIndex = 8;
            this.timerLabel.Text = "label1";
            // 
            // chanceProgressBar
            // 
            this.chanceProgressBar.Location = new System.Drawing.Point(303, 284);
            this.chanceProgressBar.Name = "chanceProgressBar";
            this.chanceProgressBar.Size = new System.Drawing.Size(189, 34);
            this.chanceProgressBar.TabIndex = 9;
            // 
            // difficultyComboBox
            // 
            this.difficultyComboBox.FormattingEnabled = true;
            this.difficultyComboBox.Items.AddRange(new object[] {
            "쉬움",
            "보통",
            "어려움"});
            this.difficultyComboBox.Location = new System.Drawing.Point(303, 352);
            this.difficultyComboBox.Name = "difficultyComboBox";
            this.difficultyComboBox.Size = new System.Drawing.Size(190, 20);
            this.difficultyComboBox.TabIndex = 10;
            this.difficultyComboBox.SelectedIndexChanged += new System.EventHandler(this.difficultyComboBox_SelectedIndexChanged);
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 1000;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 446);
            this.Controls.Add(this.difficultyComboBox);
            this.Controls.Add(this.chanceProgressBar);
            this.Controls.Add(this.timerLabel);
            this.Controls.Add(this.rangeLabel);
            this.Controls.Add(this.bestScoreLabel);
            this.Controls.Add(this.historyListBox);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.display);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "숫자 맞추기 게임";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label display;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.ListBox historyListBox;
        private System.Windows.Forms.Label bestScoreLabel;
        private System.Windows.Forms.Label rangeLabel;
        private System.Windows.Forms.Label timerLabel;
        private System.Windows.Forms.ProgressBar chanceProgressBar;
        private System.Windows.Forms.ComboBox difficultyComboBox;
        private System.Windows.Forms.Timer gameTimer;
    }
}

