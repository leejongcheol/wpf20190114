using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Calculator
{
    public class Calc : INotifyPropertyChanged
    {
        //아래 두 필드는 속성으로 구현되어 있다.
        //출력될 문자들을 담아둘 변수
        string inputString = "";
        //계산기화면의 출력 텍스트박스에 대응되는 필드
        string displayText = "";
        //View와 바인딩된 속성값이 바뀔때 이를 WPF에게 알리기 위한 이벤트
        public event PropertyChangedEventHandler PropertyChanged;
        public Calc()
        {
            //이벤트 핸들러 정의
            //숫자 버튼을 클릭할 때 실행
            this.Append = new Append(this);

            //백스페이스 버튼을 클릭할 때 실행, 한글자 삭제
            this.Backspace = new Backspace(this);
            //출력화면 클리어
            this.Clear = new Clear(this);
            //+, -등 연산자 클릭할 때 실행
            this.Operator = new Operator(this);

            // ‘=’ 버튼을 클릭할 때 실행
            this.Calculate = new Calculate(this);
        }
        public string InputString
        {
            internal set
            {
                if (inputString != value)
                {
                    inputString = value;
                    OnPropertyChanged("InputString");
                    if (value != "")
                    {
                        DisplayText = value;
                    }
                }
            }
            get { return inputString; }
        }
        //계산기의 출력창과 바인딩된 속성
        public string DisplayText
        {
            internal set
            {
                if (displayText != value)
                {
                    displayText = value;
                    OnPropertyChanged("DisplayText");
                }
            }
            get { return displayText; }
        }
        public string Op { get; set; } // Opertaor
        public double? Op1 { get; set; } // Operand 1
        public ICommand Append { protected set; get; }
        public ICommand Backspace { protected set; get; }
        public ICommand Clear { protected set; get; }
        public ICommand Operator { protected set; get; }
        public ICommand Calculate { protected set; get; }
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new
               PropertyChangedEventArgs(propertyName));
            }
        }
    }

    class Append : ICommand
    {
        private Calc c;
        public event EventHandler CanExecuteChanged;
        public Append(Calc c)
        {
            this.c = c;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            c.InputString += parameter;
        }
    }
    class Backspace : ICommand
    {
        private Calc c;
        public Backspace(Calc c)
        {
            this.c = c;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object parameter)
        {
            return c.InputString.Length > 0;
        }
        public void Execute(object parameter)
        {
            int length = c.InputString.Length - 1;
            if (0 < length)
            {
                c.InputString = c.InputString.Substring(0, length);
            }
            else
            {
                c.InputString = c.DisplayText = "";
            }
        }
    }

    class Clear : ICommand
    {
        private Calc c;
        public Clear(Calc c)
        {
            this.c = c;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object parameter)
        {
            return c.InputString.Length > 0;
        }
        public void Execute(object parameter)
        {
            c.InputString = c.DisplayText = "";
            c.Op1 = null;
        }
    }
    class Operator : ICommand
    {
        private Calc c;
        public Operator(Calc c)
        {
            this.c = c;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object parameter)
        {
            return 0 < c.InputString.Length || parameter.ToString() == "-";
        }
        public void Execute(object parameter)
        {
            string op = parameter.ToString();
            double op1;
            if (double.TryParse(c.InputString, out op1))
            {
                c.Op1 = op1;
                c.Op = op;
                c.InputString = "";
            }
            else if (c.InputString == "" && op == "-")
            {
                c.InputString = "-";
            }
        }
    }


    class Calculate : ICommand
    {
        private Calc c;
        public Calculate(Calc c)
        {
        this.c = c;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object parameter)
        {
            double op2;
            return c.Op1 != null && double.TryParse(c.InputString, out op2)
            && (c.Op != "/" || op2 != 0);
        }
        public void Execute(object parameter)
        {
            double op2 = double.Parse(c.InputString);
            c.InputString = calculate(c.Op, (double)c.Op1, op2).ToString();
            c.Op1 = null;
        }
        private static double calculate(string op, double op1, double op2)
        {
            switch (op)
            {
                case "+": return op1 + op2;
                case "-": return op1 - op2;
                case "*": return op1 * op2;
                case "/": return op1 / op2;
            }
            return 0;
        }
    }

}

