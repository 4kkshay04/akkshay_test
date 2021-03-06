using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Ports;
using System.Runtime.InteropServices;

namespace CSharpLibrary
{
    public class ExampleObject
    {
        private int _trackedInt;
        SerialPort port;

        public ExampleObject()
        {
            _trackedInt = 0;
        }

        /// <summary>
        /// Prints message to console.
        /// 0 - Performed without error
        /// 1 - String was null or empty
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public int Print(string message)
        {
            if (string.IsNullOrEmpty(message))
                return 1;

            Console.WriteLine(message);
            return 0;
        }

        public int Add(int num)
        {
            _trackedInt += num;
            checkAndFireEvent();

            return _trackedInt;
        }

        public int Subtract(int num)
        {
            _trackedInt -= num;
            checkAndFireEvent();

            return _trackedInt;
        }

        public event EventHandler EvenNumberAchieved;
        public event EventHandler OddNumberAchieved;
        public int CurrentInt { get { return _trackedInt; } }

        /// <summary>
        /// fires a simple event in order to test them in unmanaged code
        /// </summary>
        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Console.WriteLine(port.ReadExisting());
        }

    private void checkAndFireEvent()
        {
            if (_trackedInt % 2 == 0)
            {
                EvenNumberAchieved?.Invoke(this, new CustomArgs("Even Number!"));
            }
            else
                OddNumberAchieved?.Invoke(this, new CustomArgs("Odd Number!"));
        }
    }
}