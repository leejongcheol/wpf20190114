using System.Collections.ObjectModel;
namespace DataBindingSortExam
{
    class Emps : ObservableCollection<Emp>
    {
        public Emps()
        {
            Add(new Emp() { Empno = 1, Ename = "김길동", Job = "Salesman" });
            Add(new Emp() { Empno = 2, Ename = "박길동", Job = "Clerk" });
            Add(new Emp() { Empno = 3, Ename = "정길동", Job = "Clerk" });
            Add(new Emp() { Empno = 4, Ename = "남길동", Job = "Clerk" });
            Add(new Emp() { Empno = 5, Ename = "황길동", Job = "Salesman" });
            Add(new Emp() { Empno = 6, Ename = "홍길동", Job = "Manager" });
        }
    }
}