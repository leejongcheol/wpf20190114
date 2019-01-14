/*
의존속성은 읽기전용(readonly) 필드로 선언되는데 이것은 오직 FrameworkElement
클래스의 static 생성자에서만 설정될 수 있다는 것을 의미한다.
DependencyProperty 클래스에는 public 생성자가 없기 때문에 static 메소드인
DependencyProperty.Register()를 사용해서 등록한다.
Register 메서드의 입력 파라미터의 첫 번째 파라미터는 프로퍼티 이름이다. 여기서
는 “Test”, 두 번째 인자는 프로퍼티(Test)가 사용할 데이터 타입으로 여기에서
는 String 이다. 세 번째 인자는 프로퍼티를 소유하게 될 타입, 이 예제에서는
DPTest 클래스가 된다. 네 번째 인자는 실제로 어떻게 동작할 것인지에 대한
옵션을 설정을 할당해 준다. FrameworkPropertyMetadata 객체를 통하여 만약 값이
수정되었을 때의 알림을 어떻게 받을 것인가를 정의했으며 본 예제에서는
OnTestPropertyChanged 메소드가 알림을 받을 콜백 함수로 정의되었다. 선택적으로
new ValidateValueCallback을 사용하여 값의 유효성 검사를 어떻게 할 것인지 등을
설정하면 된다. 네 번째, 다섯 번째 파라미터는 옵션 파라미터이다.
*/
using System.Diagnostics;
using System.Windows;
namespace DependencyPropertyDataBindingTest
{
    public class DPTest : DependencyObject
    {
        //의존 속성을 정의
        public static readonly DependencyProperty TestProperty =
       DependencyProperty.Register(
        "Test",
        typeof(string),
        typeof(DPTest),
        new PropertyMetadata("Dependency Property Initial Value",
       OnTestPropertyChanged)); //Test 속성의 값이 바뀌면 OnTestPropertyChanged 호출
                                //TestProperty라는 의존속성을 래핑하고 있는 일반 속성,
                                //이값이 바뀜을 통지 받아서 OnTestPropertyChanged 메소드가 호출된다.
        public string Test
        {
            get
            {
                Debug.WriteLine("Test GetValue");
                return (string)GetValue(TestProperty);
            }
            set
            {
                Debug.WriteLine("Test SetValue");
                SetValue(TestProperty, value);
            }
        }
        private static void OnTestPropertyChanged(DependencyObject d,
       DependencyPropertyChangedEventArgs e)
        {
            Debug.WriteLine("Property Changed : {0}", e.NewValue);
        }
    }
}