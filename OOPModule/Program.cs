using System;
using System.Collections.Generic;
using System.Linq;

namespace OOPModule
{
    // TASK 1, VARIANT 1
    class Program
    {
        static void Main(string[] args)
        {
            var allowedDevicesAmount = Enum.GetNames(typeof(DeviceManager.DeviceList)).Length;
            var devicesToEnable = new List<int>();

            Console.WriteLine("Press corresponding key to enable device(s): Hub (0), Microwave (1), TV (2), Lightbulb (3), Radio (4). Submit final list of device(s) by pressing (e) button.");

            for (int i = 0; i < allowedDevicesAmount; i++) // User is not able to enter more devices than listed in enumerator
            {
                var device = "";
                device = Console.ReadLine();

                bool isIntParsed = int.TryParse(device, out int d);

                if (isIntParsed && d > 0) // If parsed - device adds to the list
                    devicesToEnable.Add(d);
                else
                {
                    if (!isIntParsed && device != "e") // If not parsed and submitting not finished - repeat
                    {
                        Console.WriteLine("It is not an integer, try again.");
                        i--;
                    }

                    if (d < 0 && device != "e") // If parsed, but negative and submitting not finished - repeat
                    {
                        Console.WriteLine("Negative numbers are not allowed, try again.");
                        i--;
                    }
                }

                if (devicesToEnable.Count() == 0 && device == "e") // If user submitted empty device list - repeat
                {
                    Console.WriteLine("You have not entered anything, try again.");
                    i--;
                }

                if (devicesToEnable.Count() > 0 && device == "e") // If user submitted valid devices list - exit
                    break;
            }

            Console.WriteLine("Enabled device(s): " + string.Join(", ", new DeviceEnabler().ShowEnabledDevices(devicesToEnable)));

            Console.WriteLine("Total power consumption of enabled device(s) is " + new PowerCalculator().Calculator(devicesToEnable) + "W");

            Console.WriteLine("Enter power consumption to find its device:");
            DeviceFinderByConsumption.Finder();
        }
    }

    class DeviceManager
    {
        // I had to use dictionary, however when I realized it, it was too late. Device power and device list are mapped by order
        public List<int> DevicePower = new List<int>(new int[] { 2000, 1000, 100, 50, 35 });

        public enum DeviceList { Kettle, Microwave, TV, Lightbulb, Radio }
    }

    class PowerCalculator
    {
        public int Calculator(List<int> enabledDevices)
        {
            List<int> totalPower = new List<int>();

            foreach (int i in enabledDevices)
            {
                try
                {
                    totalPower.Add(new DeviceManager().DevicePower[i]);
                }

                catch(ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Index was not correct! Terminating stage.");
                }
            }

            return totalPower.Sum();
        }
    }

    class DeviceFinderByConsumption
    {
        public static void Finder()
        {
            var devicePowerList = new DeviceManager().DevicePower;

            var inputPower = "";

            for (int a = 0; a < 1; a++) // User is able to enter only one value for search
            {
                inputPower = Console.ReadLine();

                var parsedPower = int.TryParse(inputPower, out int p);

                if (parsedPower) // Check for integer, if true - continue with execution
                {
                    int roundedPower = devicePowerList.Min(i => (Math.Abs(p - i), i)).i; // Here I round value to the closest power value to avoid empty search result

                    var deviceIndex = int.Parse(devicePowerList.Count().ToString());

                    for (int i = 0; i < deviceIndex; i++)
                    {
                        if (roundedPower == devicePowerList[i])
                        {
                            var device = (DeviceManager.DeviceList)i;
                            Console.WriteLine("Device with suitable power consumption is " + device.ToString());
                        }
                    }
                }

                if (!parsedPower) // If check for integer is failed, user will be asked to enter it again
                {
                    Console.WriteLine("Only integers are acceptable! Try again.");
                    a--;
                }
            }
        }
    }

    class DeviceEnabler
    {
        public List<string> ShowEnabledDevices(List<int> enabledDevices)
        {
            List<string> enabledDevicesNames = new List<string>();

            try
            {
                foreach (int i in enabledDevices)
                {
                    var device = (DeviceManager.DeviceList)i;
                    enabledDevicesNames.Add(device.ToString());
                }
            }

            catch(ArgumentOutOfRangeException)
            {
                Console.WriteLine("Incorrect index! Terminating stage.");
            }

            return enabledDevicesNames;
        }
    }
}