using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LasMargaritas.UI.UserControls.PopUps
{
    /// <summary>
    /// Interaction logic for StatusProcess.xaml
    /// </summary>
    public partial class StatusProcess : UserControl
    {
        public StatusProcess()
        {
            InitializeComponent();
            switch (TypeProcess)
            {
                case "1":
                    Console.WriteLine("Case 1");
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }

            if(IsSuccess)
                btnCicle.Visibility= Visibility.Hidden;
        }

        public string TypeProcess
        {
            get { return (string)GetValue(TypeProcessProperty); }
            set { SetValue(TypeProcessProperty, value); }
        }

        public bool IsSuccess
        {
            get { return (bool)GetValue(IsSuccessProperty); }
            set { SetValue(IsSuccessProperty, value); }
        }

        public static readonly DependencyProperty TypeProcessProperty = DependencyProperty.Register("TypeProcess", typeof(string), typeof(string));
        public static readonly DependencyProperty IsSuccessProperty = DependencyProperty.Register("IsSuccess", typeof(bool), typeof(bool));


    }
}
