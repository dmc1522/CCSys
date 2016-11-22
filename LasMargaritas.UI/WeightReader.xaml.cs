using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LasMargaritas.UI
{
    /// <summary>
    /// Interaction logic for WeightReader.xaml
    /// </summary>
    public partial class WeightReader : Window
    {
        private bool KeepThreadRunning
        {
            get { lock (this) { return _KeepThreadRunning; } }
            set { lock (this) { _KeepThreadRunning = value; } }
        }

        private SerialPort serialPort = new SerialPort();
        private bool _KeepThreadRunning;
        private Thread weightReaderThread;
        public WeightReader()
        {
            InitializeComponent();
          
        }
        
        public int Weight { get; set; }

        private void ReadPort()
        {
            Random randomizer = new Random();
            string[] parts;
            string lineRead;
            int weight;
            try
            {
                while (this.KeepThreadRunning)
                {
                    try
                    {
                        //lineRead = serialPort.ReadLine();                        
                        lineRead = string.Format("test,test,{0}kg\r", randomizer.Next(1000, 1010));
                        parts = lineRead.Split(',');
                        //                         a = a.Replace("ST,GS,", "");
                        //                         a = a.Replace("kg\r", "");
                        lineRead = parts[2];
                        lineRead = lineRead.Replace("kg\r", "");
                        if (this.KeepThreadRunning && int.TryParse(lineRead, out weight))
                        {
                            Weight = weight;
                            Dispatcher.Invoke(new Action(() =>
                                TextBoxCurrentWeight.Text = string.Format("{0:N0}", weight)                            
                            ));                            
                            Thread.Sleep(10);
                        }
                    }
                    catch (Exception ex)
                    {
                        //Error not logged
                    }                    
                }
            }
            catch (Exception ex)
            {
                //Error not logged
            }
        }

        private void ButtonCaptureWeight_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                this.KeepThreadRunning = false;
                //serialPort.Close();
                //serialPort.Dispose();
                while (weightReaderThread.IsAlive)
                {
                    Thread.Sleep(10);
                }              
            }
            catch
            {
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //serialPort = new SerialPort("COM1"); //TODO change this! 
                weightReaderThread = new Thread(new ThreadStart(ReadPort));
                KeepThreadRunning = true;
                weightReaderThread.Start();

            }
            catch (Exception ex)
            {

                //Error not logged
            }

        }
    }
}
