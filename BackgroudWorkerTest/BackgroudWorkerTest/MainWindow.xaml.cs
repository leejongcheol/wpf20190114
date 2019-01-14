/*
 * Windows 응용 프로그램 멀티 쓰레딩에서 가장 어려운 개념은 다른 스레드에서 UI를
변경할 수 없다는 것이다. 대신 UI스레드에서 메소드를 호출해야 원하는 변경이 이루
어 진다.
 백그라운드 워커(Background Worker)는 System.ComponentModel 아래의 클래스로
코드를 별도의 쓰레드에서 동시에 비동기적으로 실행하게 해 주는데 응용프로그램의
기본 쓰레드와 자동으로 동기화 해준다. 호출 쓰레드는 정상적으로 실행이 되고
Background Worker는 백그라운드에서 비동기적으로 실행된다.
 백그라운드에서 작업을 실행하고 UI 실행등을 연기하는데 사용되는데 사용자는 UI가
계속 반응하기를 원하면서 데이터를 다운로드 한다든지, 오래 걸리는 작업이 있어
진행사항을 표시해야 되는 경우, 데이터베이스 트랜잭션 처리 등에 유용하다.
 Background Worker에서 일어나는 작업에 대해 변경이 생길 때 호출되는
ProcessedChanged 이벤트, 작업이 완료되었을 때 무언가를 할 수 있도록 지원하는
RunWorkerCompledted 이벤트가 발생한다.
 DoWork 이벤트에서 백그라운드 쓰레드가 할일을 기술하는데 DoWork 이벤트 처리
메소드 내용은 다른 백그라운드, 다른 쓰레드에서 처리되므로 UI쪽을 접근할 수 없
는데 ReportProgress() 메소드를 호출하면 ProcessChanged 이벤트가 발생하여 UI를
업데이트하는 것이 가능하다.
 ProgressChanged 및 RunWorkerCompleted 이벤트는 BackgroundWorker가 만들
어지는 것과 동일한 스레드에서 실행된다.
 BackgroundWorker는 일반적으로 기본/UI 쓰레드이므로 UI를 업데이트 할 수 있다.
따라서 실행중인 백그라운드 작업과 UI간에 수행 할 수있는 유일한 통신방법은
ReportProgress() 메서드를 사용하는 것이다.
 DoWork 이벤트 처리 메소드 내부에서 파라미터가 필요하면 백그라운드 워커를 호출
하는 RunWorkerAsync() 메소드의 인수로 넣어주면 된다.
int count = (int)e.Argument;
 DoWork 이벤트 처리 메소드 내부에서 e.Result 등으로 어떤 결과값을 넣어두면
RunWorkerCompledted 이벤트 처리 메소드에서 e.Result 형태로 꺼내볼 수 있다.
 * 
 * */
using System;
using System.ComponentModel;
using System.Windows;
using System.Threading;
using System.Windows.Threading;
namespace BackgroundWorkerTest
{
    public partial class MainWindow : Window
    {
        //백그라운드 워커 선언
        private BackgroundWorker myThread;
        //짝수의 합을 저장할 인스턴스 변수
        int sum = 0;
        public MainWindow()
        {
            InitializeComponent();
        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            //백그라운드 워커 초기화
            //작업의 진행율이 바뀔때 ProgressChanged 이벤트 발생여부
            //작업취소 가능여부 true로 설정
            myThread = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            //백그라운드에서 실행될 콜백 이벤트 생성
            //For the performing operation in the background.
            //해야할 작업을 실행할 메소드 정의
            myThread.DoWork += myThread_DoWork;
            //UI쪽에 진행사항을 보여주기 위해
            //WorkerReportsProgress 속성값이 true 일때만 이벤트 발생
            myThread.ProgressChanged += myThread_ProgressChanged;
            //작업이 완료되었을 때 실행할 콜백메소드 정의
            myThread.RunWorkerCompleted += myThread_RunWorkerCompleted;
            MessageBox.Show("Worker 초기화");
        }
        //백그라운드 워커가 실행하는 작업
        //DoWork 이벤트 처리 메소드에서 lstNumber.Items.Add(i)를
        //직접 실행시키면 "InvalidOperationException" 오류발생
        private void myThread_DoWork(object sender, DoWorkEventArgs e)
        {
            int count = (int)e.Argument;
            for (int i = 1; i <= count; i++)
            {
                if (myThread.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                else
                {
                    //메인 UI쓰레드 UI를 변경하기 위해서는
                    //idle Time을 둬야한다.
                    Thread.Sleep(100);
                    this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    (ThreadStart)delegate ()
                    {
                        if (i % 2 == 0)
                        {
                            sum += i;
                            e.Result = sum;
                            lstNumber.Items.Add(i);
                        }
                    }
                    );

                    myThread.ReportProgress(i);

                }
            }
        }
        //작업의 진행률이 바뀔때 발생, ProgressBar에 변경사항을 출력
        //대체로 현재의 진행상태를 보여주는 코드 여기에 작성.
        private void myThread_ProgressChanged(object sender,
       ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }
        //작업완료
        private void myThread_RunWorkerCompleted(object sender,
       RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) MessageBox.Show("작업 취소...");
            else if (e.Error != null) MessageBox.Show("에러발생..." +
           e.Error);
            else
            {
                tblkSum.Text = ((int)e.Result).ToString();
                MessageBox.Show("작업 완료!!");
            }
        }
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            int num;
            if (!int.TryParse(txtNumber.Text, out num))
            {
                MessageBox.Show("숫자를 입력하세요.!");
                return;
            }
            progressBar.Maximum = num;
            lstNumber.Items.Clear();
            myThread.RunWorkerAsync(num);
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            myThread.CancelAsync();
        }
    }
}