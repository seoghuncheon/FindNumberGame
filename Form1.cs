using System;
using System.Drawing;
using System.Media; // 소리 기능을 쓰기 위해 가져와야 하는 도구 상자
using System.Windows.Forms;

namespace FindNumber
{
    public partial class Form1 : Form
    {
        // ==============================================
        // [1. 전역 변수 구역]
        // 함수 바깥에 선언해서, 개임 내내 모든 버튼이 이 값들을 기억하고 공유하게 함
        // ==============================================

        private int findNumber = 0; // 컴퓨터가 몰래 뽑은 '정답 숫자'를 저장할 변수
        private int chance = 0; // 플레이어에게 현재 남은 '기회 횟수'
        private int maxChance = 0; // 처음에 주어진 '최대 기회' (프로그레스 바 게이지 계산용)

        private int bestScore = 999; // 최고 기록 저장용 (시도 횟수가 적을수록 잘한 거라 초기 값을 엄청 큰 수로 세팅)
        private int currentTryCount = 0; // 이번 판에 내가 몇 번 만에 맞췄는지 세는 카운터

        private int minRange = 1; // 힌트를 줄 때 "현재 범위의 최소값"을 표시할 변수
        private int maxRange = 50; // 힌트를 줄 때 "현재 범위의 최대값"을 표시할 변수

        private int timeLeft = 30; // 제한시간을 저장할 변수 (난이도 마다 달라짐)

        // 효과음 파일 경로 세팅
        // 경로 문자열 앞에 '@'를 붙여야 백슬래시를 특수문자가 아닌 일반 문자로 인식함
        private string successSoundPath = @"C:\Users\byc14\OneDrive\바탕 화면\숫자 맞추기 게임\성공.wav";
        private string failSoundPath = @"C:\Users\byc14\OneDrive\바탕 화면\숫자 맞추기 게임\실패.wav";
        // =====================================================================
        //  [2. 폼 생성자 (앱이 처음 켜질 때 딱 한 번 실행되는 곳)]
        // =====================================================================
        public Form1()
        {
            // 주의: 절대 지우지 말 것 디자인(버튼, 텍스트박스 등)을 화면에 렌더링하는 용도
            InitializeComponent();

            // 앱이 켜지자마자 난이도 콤보박스의 1번 인덱스(두 번째 항목인 "보통")를 기본으로 선택함
            if (difficultyComboBox.Items.Count > 0)
                difficultyComboBox.SelectedIndex = 1; // 기본 '보통'
        }

        // =====================================================================
        // [3. 게임 시작 버튼 클릭 이벤트]
        // =====================================================================
        private void button2_Click(object sender, EventArgs e)
        {
            var rand = new Random(); // 랜덤 숫자를 뽑아주는 객체
            currentTryCount = 0; // 새 게임이라 시도 횟수 카운터는 0으로 초기화
            historyListBox.Items.Clear(); // 리스트박스에 남아있던 이전 게임의 텍스트 기록도 지움
            
            // [난이도 판별 로직]
            // 선택된 항목의 글자를 가져와서 difficulty 변수에 담음 (선택 안 됐으면 "보통"으로 간주)
            string difficulty = difficultyComboBox.SelectedItem?.ToString() ?? "보통";

            // 난이도에 따라 범위, 기회, 제한시간을 다르게 세팅
            if (difficulty == "쉬움")
            {
                minRange = 1; maxRange = 30; // 숫자 범위 1 ~ 30
                chance = 15; // 기회 15번
                timeLeft = 30; // 제한시간 30초
                findNumber = rand.Next(1, 31); // 1부터 30까지 중 랜덤 뽑기 (31 미만)
            }
            else if (difficulty == "어려움")
            {
                minRange = 1; maxRange = 100; // 범위 1~100 (어려움 난이도)
                chance = 7; // 기회 7번 만 줌
                timeLeft = 50; // 대신 시간은 50초로 넉넉하게 줌
                findNumber = rand.Next(1, 101);
            }
            else // 보통
            {
                minRange = 1; maxRange = 50;
                chance = 10;
                timeLeft = 30;
                findNumber = rand.Next(1, 51);
            }

            maxChance = chance; // 계산을 위해 최대 기회 횟수를 저장해 둠

            // [UI 초기화 구역]
            // 프로그레스 바의 최대치를 방금 설정한 기회(maxChance)로 맞추고, 현재 값도 꽉 채움
            chanceProgressBar.Maximum = maxChance;
            chanceProgressBar.Value = chance;

            // 안내 멘트와 글자색 리셋
            display.Text = $"{minRange}~{maxRange} 사이 숫자를 맞춰보세요!";
            display.ForeColor = Color.Black;

            // 좁혀지는 범위 힌트 라벨 리셋
            rangeLabel.Text = $"현재 범위: {minRange} ~ {maxRange}";

            // [타이머 가동]
            timerLabel.Text = $"남은 시간: {timeLeft}초"; // 타이머 텍스트 업데이트
            gameTimer.Start(); // 타이머 시작!

            // 게임 시작과 동시에 숫자를 바로 칠 수 있게 텍스트 상자를 비우고 커서를 깜박이게 만듬!
            textBox.Clear();
            textBox.Focus();
        }

        // =====================================================================
        // [4. 입력 버튼 클릭 이벤트 (게임의 핵심 로직)]
        // =====================================================================
        private void button1_Click(object sender, EventArgs e)
        {
            // 방어막: 텍스트박스가 비어있거나 기회가 없으면 아무것도 안 하고 함수 탈출(return)!
            if (string.IsNullOrEmpty(textBox.Text) || chance <= 0) return;

            // 기본 입력 효과음 재생 (윈도우 기본 소리)
            SystemSounds.Asterisk.Play();

            // 텍스트박스에 입력된 글자(string)를 숫자(int)로 변환!
            int inputNumber = Int32.Parse(textBox.Text);

            currentTryCount++; // 한 번 시도했으니 시도 카운터 1 증가
            chance--; // 기회는 1 차감

            // 남은 기회를 프로그레스 바에 반영 (혹시 0 미만으로 내려가면 에러 나니까 Math.Max로 0 밑으론 안 가게 방어)
            chanceProgressBar.Value = Math.Max(0, chance);

            // 기회가 3번 이하로 남으면 글자색을 빨간색으로
            if (chance <= 3)
            {
                display.ForeColor = Color.Red;
            }

            // 조건 1 : 정답을 맞췄을 때!
            if (inputNumber == findNumber)
            {
                gameTimer.Stop(); // 정답 맞췄으니 타이머 정지

                // 커스텀 사운드 재생!
                // try-catch 로 만약 파일 이름이 틀렸거나 없어서 에러가 나도 게임이 안튕기게 함
                try
                {
                    SoundPlayer successPlayer = new SoundPlayer(successSoundPath);
                    successPlayer.Play();
                } catch { }

                // 화면에 텍스트 띄우기
                display.Text = $"정답입니다!! ({currentTryCount}번 만에 성공!)";
                display.ForeColor = Color.Blue; // 파란색으로 변경
                historyListBox.Items.Add($"[정답] {inputNumber}");

                if (currentTryCount < bestScore)
                {
                    bestScore = currentTryCount; // 더 적은 숫자로 최고 기록 넣기
                    bestScoreLabel.Text = $"최고 기록: {bestScore}회 성공!";
                    MessageBox.Show("축하합니다! 최고 기록을 갱신하셨습니다!", "신기록");
                }
            }
            // 조건 2: 틀렸을 때 (UP & DOWN 힌트)
            else
            {
                // 내가 입력한 숫자가 정답보다 클 때 -> 더 작은 숫자를 찾아야 함 (DOWN)
                if (inputNumber > findNumber)
                {
                    display.Text = $"더 작은 숫자입니다! (남은 기회: {chance}번)";
                    historyListBox.Items.Add($"{inputNumber} -> [DOWN]");

                    // 범위 힌트 좁히기 : 현재 최대 범위보다 내가 부른 숫자가 작으면, 최대 범위를 갱신
                    if (inputNumber < maxRange) maxRange = inputNumber - 1;
                }
                // 내가 입력한 숫자가 정답보다 작을 때 -> 더 큰 숫자를 찾아야 함 (UP)
                else
                {
                    display.Text = $"더 큰 숫자입니다! (남은 기회 {chance}번";
                    historyListBox.Items.Add($"{inputNumber} -> [UP]");

                    // 범위 힌트 좁히기: 현재 최소 범위보다 내가 부른 숫자가 크면, 최소 범위를 갱신!
                    if (inputNumber > minRange) minRange = inputNumber + 1;
                }

                // 좁혀진 범위를 화면에 즉시 갱신
                rangeLabel.Text = $"현재 범위: {minRange} ~ {maxRange}";
            }
            // ---------------------------------------------------------
            // ☠️ [조건 3: 기회를 모두 잃고 패배했을 때]
            // ---------------------------------------------------------
            if (chance <= 0 && inputNumber != findNumber)
            {
                GameOver($"실패했습니다!! 정답은 {findNumber}이였습니다.");
            }

            // 사용자가 마우스를 안 움직여도 되게 텍스트박스 안의 글자를 전체 선택하고 포커스 추가
            textBox.SelectAll();
            textBox.Focus();
        }

        // =====================================================================
        // [5. 타이머 틱(Tick) 이벤트 (설정한 간격-보통 1초-마다 실행됨)]
        // =====================================================================
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            timeLeft--; // 남은 시간 1초 차감
            timerLabel.Text = $"남은 시간: {timeLeft}초"; // 깎인 시간을 화면에 표시

            // 시간이 다 끝났다면
            if (timeLeft <= 0)
            {
                GameOver($"제한시간 초과! 정답은 {findNumber}였습니다.");
            }
        }

        // =====================================================================
        // [6. 게임 오버 공통 처리 함수 (중복 코드를 줄이기 위해 만듦)]
        // =====================================================================
        private void GameOver(string message)
        {
            gameTimer.Stop(); // 타이머 정지

            // 다운로드 한 커스텀 실패 사운드 재생! (경로 틀려도 안 튕기게 tey-catch)
            try
            {
                SoundPlayer failPlayer = new SoundPlayer(failSoundPath);
                failPlayer.Play();
            } catch { }

            // 매개변수로 받은 메시지(실패 이유)를 화면에 띄우고 진한 빨간색으로 변경
            display.Text = message;
            display.ForeColor = Color.DarkRed;
        }

        // =====================================================================
        // [7. 텍스트 박스 키보드 이벤트 (엔터 키 치트키)]
        // =====================================================================
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            // 키보드에서 친 키가 엔터(Enter) 키라면
            if (e.KeyCode == Keys.Enter)
            {
                // 마우스로 입력 버튼(button1)을 클릭한 것처럼 클릭 함수를 실행
                button1_Click(sender, e);
            }
        }

        // 에러 방지용 빈 껍데기 메서드들
        // 실수로 더블클릭해서 만들어진 이벤트들이 디자인 창과 연결이 끊겨서
        // 폼이 튕기는 걸 막아주는 용도

        private void difficultyComboBox_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
    }
}