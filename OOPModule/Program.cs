﻿using System;
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

            for (int i = 0; i < allowedDevicesAmount; i++)
            {
                var device = Console.ReadLine();
                if (device == "e")
                    break;
                int.TryParse(device, out int d);
                devicesToEnable.Add(d);
            }

            Console.WriteLine("Enabled device(s): " + string.Join(", ", new DeviceEnabler().ShowEnabledDevices(devicesToEnable)));

            Console.WriteLine("Total power consumption of enabled device(s) is " + new PowerCalculator().Calculator(devicesToEnable) + "W");

            Console.WriteLine("Enter power consumption to find its device:");
            DeviceFinderByConsumption.Finder();
        }
    }

    class DeviceManager
    {
        public List<int> DevicePower = new List<int>(new int[] { 2000, 1000, 100, 50, 35 });

        public enum DeviceList
        {
            Kettle, Microwave, TV, Lightbulb, Radio
        }
    }

    class PowerCalculator
    {
        public int Calculator(List<int> enabledDevices)
        {
            List<int> totalPower = new List<int>();

            foreach (int i in enabledDevices)
                totalPower.Add(new DeviceManager().DevicePower[i]);
            
            return totalPower.Sum();
        }
    }

    class DeviceFinderByConsumption
    {
        public static void Finder()
        {
            var inputPower = int.Parse(Console.ReadLine());
            var devicePowerList = new DeviceManager().DevicePower;

            var deviceIndex = int.Parse(devicePowerList.Count().ToString());

            for (int i = 0; i < deviceIndex; i++)
            {
                if (inputPower == devicePowerList[i])
                {
                    var device = (DeviceManager.DeviceList)i;
                    Console.WriteLine("Device with suitable power consumption is " + device.ToString());
                }
            }
        }
    }

    class DeviceEnabler
    {
        public List<string> ShowEnabledDevices(List<int> enabledDevices)
        {
            List<string> enabledDevicesNames = new List<string>();

            foreach (int i in enabledDevices)
            {
                var device = (DeviceManager.DeviceList)i;
                enabledDevicesNames.Add(device.ToString());
            }

            return enabledDevicesNames;
        }
    }
}