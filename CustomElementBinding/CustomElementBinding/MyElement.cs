using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
namespace CustomElementBinding
{
    /// //////////////////////////////////////////////////////////////
    // Number라는 double 타입의 프로퍼티를 정의하고
    // 이 프로퍼티는 NumberProperty라는 의존 프로퍼티의 지원을 받는다
    // FrameworkPropertyMetadata는 초기값 이이며, 프로퍼티에 변화가 생기면
    // OnRender를 호출하여 화면을 갱신(Number 프로퍼티를 노출)한다.
    //////////////////////////////////////////////////////////////////
    public class MyElement : FrameworkElement
    {
        // 의존 프로퍼티 선언
        // 데이터 바인딩 통보 메커니즘은 이를 통해 저절로 확보된다.
        public static DependencyProperty NumberProperty;
        // static 생성자를 통한 의존 프로퍼티 생성
        static MyElement()
        {
            NumberProperty =
            DependencyProperty.Register("Number", typeof(double),
            typeof(MyElement),
            new FrameworkPropertyMetadata(0.0,
            FrameworkPropertyMetadataOptions.AffectsRender));
        }

        // DependencyProperty를 CLR 프로퍼티로 노출
        public double Number
        {
            set { SetValue(NumberProperty, value); }
            get { return (double)GetValue(NumberProperty); }
        }
        // MeasureOverride를 오버라이딩해 Size를 리턴하는데 원래 이함수는 자식
        // 엘리먼트를 돌면서 크기를 계산하고 업데이트하는 메소드 이다.
        // 이함수를 재정의 안하면 자식 컨트롤의 크기가 이상해 질 수 있다.
        // MyElement의 크기를 측정해서 FrameworkElement 파생클래스(MyElement)
        // 의 Size를 리턴하는 것이다.레이아웃을 만들 때 자동으로 호출된다.
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            return new Size(200, 50);
        }
        // Number 프로퍼티를 보여주는 OnRender
        protected override void OnRender(DrawingContext dc)
        {
            dc.DrawText(
            new FormattedText(Number.ToString(),
            CultureInfo.CurrentCulture, FlowDirection.LeftToRight,      
            new Typeface("Times New Roman"), 12,
            SystemColors.WindowTextBrush),
            new Point(0, 0));
        }
    }
}