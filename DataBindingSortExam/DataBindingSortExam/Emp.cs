using System;
using System.ComponentModel;
namespace DataBindingSortExam
{
    public class Emp : INotifyPropertyChanged
    {
        private int _empno;
        private string _ename;
        private string _job;
        public event PropertyChangedEventHandler PropertyChanged;

        public int Empno
        {
            get { return _empno; }
            set { _empno = value; OnPropertyChanged("Empno"); }
        }
        public string Ename
        {
            get { return _ename; }
            set { _ename = value; OnPropertyChanged("Ename"); }
        }
        public string Job
        {
            get { return _job; }
            set { _job = value; OnPropertyChanged("Job"); }
        }
        public void OnPropertyChanged(string PName)
        {
            PropertyChanged?.Invoke(this, new
           PropertyChangedEventArgs(PName));
        }
    }
}