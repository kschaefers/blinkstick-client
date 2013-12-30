#region License
// Copyright 2013 by Agile Innovative Ltd
//
// This file is part of BlinkStick.CLI application.
//
// BlinkStick.CLI application is free software: you can redistribute 
// it and/or modify it under the terms of the GNU General Public License as published 
// by the Free Software Foundation, either version 3 of the License, or (at your option) 
// any later version.
//		
// BlinkStick.CLI application is distributed in the hope that it will be useful, but 
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
// FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with 
// BlinkStick.CLI application. If not, see http://www.gnu.org/licenses/.
#endregion

using System;
using System.Collections.Generic;
using HidLibrary;
using BlinkStick.Hid;
using Plossum.CommandLine;

namespace BlinkStick.CLI
{
	class MainClass
	{
		public static int Main (string[] args)
		{
			Boolean Error = false;

			Options options = new Options();
            CommandLineParser parser = new CommandLineParser(options);
            parser.Parse();
            Console.WriteLine(parser.UsageInfo.GetHeaderAsString(78));

            if (options.Help)
            {
                Console.WriteLine(parser.UsageInfo.GetOptionsAsString(78));
                return 0;
            }
            else if (parser.HasErrors)
            {
                Console.WriteLine(parser.UsageInfo.GetErrorsAsString(78));
                Console.WriteLine(parser.UsageInfo.GetOptionsAsString(78));
                return -1;
            }

            //find devices
            List<AbstractBlinkstickHid> Devices = new List<AbstractBlinkstickHid>();

            foreach (AbstractBlinkstickHid device in BlinkstickDeviceFinder.FindDevices())
            {
                if (options.Device == null || device.Serial == options.Device || device.Serial == "first")
                {
                    Devices.Add(device);

                    if (device.Serial == "first")
                        break;
                }
            }

            if (Devices.Count == 0)
            {
                if (options.Device == null)
                {
                    Console.WriteLine("Error: No devices found...");
                }
                else
                {
                    Console.WriteLine("Error: Specified device not found...");
                }
                return -1;
            }

			foreach (AbstractBlinkstickHid device in Devices)
            {
                if (device.OpenDevice ()) {
					Console.WriteLine (String.Format ("Device {0} opened successfully", device.Serial));
					if (options.Info)
					{
						PrintDeviceInfo(device, false);
					}
					else if (options.GetColor)
					{
						PrintDeviceInfo(device, true);
					}
					else if (options.SetColor != null)
					{
						if (options.SetColor == "random")
						{
							Random r = new Random();
							((WindowsBlinkstickHid)device).SetLedIndexedColor((byte)options.Channel, (byte)options.Index, (byte)r.Next(256), (byte)r.Next(256), (byte)r.Next(256));
						}
						else
						{
							try
							{
								device.SetLedColor(options.SetColor);
							}
							catch (Exception e)
							{
								Console.WriteLine("Error: " + e.Message);
								Error = true;
							}
						}
					}
					else if (options.SetMode != -1)
					{
						((WindowsBlinkstickHid)device).SetLedMode((byte)options.SetMode);
					}
					else if (options.GetInfoBlock1)
					{
						PrintDeviceInfoBlock(device, 2);
					}
					else if (options.GetInfoBlock2)
					{
						PrintDeviceInfoBlock(device, 3);
					}
					else if (options.GetLedData)
					{
						PrintDeviceLedData(device);
					}
                    else if (options.SetInfoBlock1 != null)
                    {
                        device.SetInfoBlock(2, options.SetInfoBlock1);
                        Console.WriteLine("    Info block 1 updated");
                        PrintDeviceInfoBlock(device, 2);
                    }
                    else if (options.SetInfoBlock2 != null)
                    {
                        device.SetInfoBlock(3, options.SetInfoBlock2);
                        Console.WriteLine("    Info block 2 updated");
                        PrintDeviceInfoBlock(device, 3);
                    }

                    device.CloseDevice();
                } else {
                    Console.WriteLine ("Error: could not open a device");
                }
            }

			BlinkstickDeviceFinder.FreeUsbResources();

			if (Error)
				return -1;
			else
            	return 0;
		}

        public static void PrintDeviceInfo(AbstractBlinkstickHid device, Boolean colorOnly)
        {
            byte cr;
            byte cg;
            byte cb;

            device.GetLedColor(out cr, out cg, out cb);

            Console.WriteLine (String.Format ("    Device color: #{0:X2}{1:X2}{2:X2}", cr, cg, cb));

            if (colorOnly)
                return;

            Console.WriteLine ("    Serial:       " + device.Serial);
            Console.WriteLine ("    Manufacturer: " + device.ManufacturerName);
            Console.WriteLine ("    Product Name: " + device.ProductName);
			PrintDeviceInfoBlock(device, 3);
        }

        public static void PrintDeviceInfoBlock(AbstractBlinkstickHid device, byte blockId)
        {
            Console.Write (String.Format("    Info block{0}: ", blockId - 1));

            string deviceInfoBlock;
            if (device.GetInfoBlock(blockId, out deviceInfoBlock))
            {
                Console.WriteLine (deviceInfoBlock);
            }
            else
            {
                Console.WriteLine ("FAILED");
            }
        }

		public static void PrintDeviceLedData(AbstractBlinkstickHid device)
		{
			byte[] data;
			if (((WindowsBlinkstickHid)device).GetLedData(out data))
			{
				for (int j = 0; j < 8; j++)
				{
					for (int i = 0; i < 8; i++)
					{
						Console.Write(String.Format("{0:x2}{1:x2}{2:x2} ", data[j * 3 * 8 + i * 3 + 1], data[j * 3 * 8 + i * 3 + 0], data[j * 3 * 8 + i * 3 + 2]));
					}
					Console.WriteLine("");
				}
			}
			else
			{
				Console.WriteLine ("FAILED");
			}
		}
	}
}
