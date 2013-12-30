#region License
// Copyright 2013 by Agile Innovative Ltd
//
// This file is part of BlinkStick.HID library.
//
// BlinkStick.HID library is free software: you can redistribute it and/or modify 
// it under the terms of the GNU General Public License as published by the Free 
// Software Foundation, either version 3 of the License, or (at your option) any 
// later version.
//		
// BlinkStick.HID library is distributed in the hope that it will be useful, but 
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
// FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with 
// BlinkStick.HID library. If not, see http://www.gnu.org/licenses/.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using HidLibrary;
using System.Text.RegularExpressions;

namespace BlinkStick.Hid
{
	public class WindowsBlinkstickHid : AbstractBlinkstickHid, IDisposable
    {
        private HidDevice device;
        private bool attached = false;
        
        private bool disposed = false;

		protected override String GetSerial()
		{
			return device.Serial;
		}
		
		protected override String GetManufacturer()
		{
			return device.ManufacturerName;
		}

        /// Occurs when a BlinkStick device is attached.
        /// </summary>
        public event EventHandler DeviceAttached;

        /// <summary>
        /// Occurs when a BlinkStick device is removed.
        /// </summary>
        public event EventHandler DeviceRemoved;

        /// <summary>
        /// Initializes a new instance of the BlinkstickHid class.
        /// </summary>
        public WindowsBlinkstickHid()
        {
        }

        /// <summary>
        /// Attempts to connect to a BlinkStick device.
        /// 
        /// After a successful connection, a DeviceAttached event will normally be sent.
        /// </summary>
        /// <returns>True if a Blinkstick device is connected, False otherwise.</returns>
        public override bool OpenDevice ()
		{
			if (this.device == null) {
				HidDevice adevice = HidDevices.Enumerate (VendorId, ProductId).FirstOrDefault ();
				return OpenDevice (adevice);
			} else {
				return OpenCurrentDevice();
			}
        }

        public bool OpenDevice(HidDevice adevice)
        {
            if (adevice != null)
            {
                this.device = adevice;

                return OpenCurrentDevice();
            }

            return false;
        }

		private bool OpenCurrentDevice()
		{
		    connectedToDriver = true;
            device.OpenDevice();

            device.Inserted += DeviceAttachedHandler;
 			device.Removed += DeviceRemovedHandler;

			return true;
		}

        public static WindowsBlinkstickHid[] AllDevices ()
		{
			List<WindowsBlinkstickHid> result = new List<WindowsBlinkstickHid>();
			foreach (HidDevice device in HidDevices.Enumerate(VendorId, ProductId).ToArray<HidDevice>()) {
				WindowsBlinkstickHid hid = new WindowsBlinkstickHid();
				hid.device = device;

				result.Add(hid);
			}
			return result.ToArray();
        }

        /// <summary>
        /// Closes the connection to the device.
        /// </summary>
        public override void CloseDevice()
        {
            device.CloseDevice();
            connectedToDriver = false;
        }

        /// <summary>
        /// Closes the connection to the device.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
		private void DeviceAttachedHandler()
        {
            attached = true;

            if (DeviceAttached != null)
                DeviceAttached(this, EventArgs.Empty);

            device.ReadReport(OnReport);
        }

        private void DeviceRemovedHandler()
        {
            attached = false;

            if (DeviceRemoved != null)
                DeviceRemoved(this, EventArgs.Empty);
        }


        private void OnReport(HidReport report)
        {
            if (attached == false) { return; }

				/*
            if (report.Data.Length >= 6)
            {
                PowerMateState state = ParseState(report.Data);
                if (!state.IsValid)
                {
                    System.Diagnostics.Debug.WriteLine("Invalid PowerMate state");
                }
                else
                {
                    GenerateEvents(state);

                    if (debugPrintRawMessages)
                    {
                        System.Diagnostics.Debug.Write("PowerMate raw data: ");
                        for (int i = 0; i < report.Data.Length; i++)
                        {
                            System.Diagnostics.Debug.Write(String.Format("{0:000} ", report.Data[i]));
                        }
                        System.Diagnostics.Debug.WriteLine("");
                    }
                }
            }
*/

            //device.ReadReport(OnReport);
        }
  
        public override void SetLedColor(byte r, byte g, byte b)
        {
            if (connectedToDriver)
            {
				byte [] data = new byte[5];
                data[0] = 1;
                data[1] = r;
                data[2] = g;
                data[3] = b;
                data[4] = 0;

                HidReport report = new HidReport(5, new HidDeviceData(data, HidDeviceData.ReadStatus.Success));
                device.WriteFeatureData(data);
            }
        }

		public void SetLedIndexedColor(byte channel, byte index, byte r, byte g, byte b)
        {
            if (connectedToDriver)
            {
				byte [] data = new byte[6];
				data[0] = 5;
				data[1] = channel;
				data[2] = index;
				data[3] = r;
				data[4] = g;
				data[5] = b;

				//HidReport report = new HidReport(5, new HidDeviceData(data, HidDeviceData.ReadStatus.Success));
                device.WriteFeatureData(data);
            }
        }

		public void SetLedMode(byte mode)
		{
			if (connectedToDriver)
			{
				byte [] data = new byte[2];
				data[0] = 4;
				data[1] = mode;
				device.WriteFeatureData(data);
			}
		}


		public void SetLedColors(byte channel, byte[] reportData)
		{
			int max_leds = 64;
			byte reportId = 9;

			//Automatically determine the correct report id to send the data to
			if (reportData.Length <= 8 * 3)
			{
				max_leds = 8;
				reportId = 6;
			}
			else if (reportData.Length <= 16 * 3)
			{
				max_leds = 16;
				reportId = 7;
			}
			else if (reportData.Length <= 32 * 3)
			{
				max_leds = 32;
				reportId = 8;
			}
			else if (reportData.Length <= 64 * 3)
			{
				max_leds = 64;
				reportId = 9;
			}
			else if (reportData.Length <= 128 * 3)
			{
				max_leds = 64;
				reportId = 10;
			}

			byte [] data = new byte[max_leds * 3 + 2];
			data[0] = reportId;
			data[1] = channel; // chanel index

			for (int i = 0; i < Math.Min(reportData.Length, data.Length - 2); i++)
			{
				data[i + 2] = reportData[i];
			}

			for (int i = reportData.Length + 2; i < data.Length; i++)
			{
				data[i] = 0;
			}

			device.WriteFeatureData(data);

			if (reportId == 10)
			{
				//System.Threading.Thread.Sleep(1);

				for (int i = 0; i < Math.Min(data.Length - 2, reportData.Length - 64 * 3); i++)
				{
					data[i + 2] = reportData[64 * 3 + i];
				} 

				for (int i = reportData.Length + 2 - 64 * 3; i < data.Length; i++)
				{
					data[i] = 0;
				}

				data[0] = (byte)(reportId + 1);

				device.WriteFeatureData(data);
			}
		} 

		public override Boolean GetLedColor (out byte r, out byte g, out byte b)
		{
			byte[] report; 

			if (connectedToDriver && device.ReadFeatureData (1, out report)) {
				r = report [1];
				g = report [2];
				b = report [3];

				return true;
			} else {
				r = 0;
				g = 0;
				b = 0;

				return false;
			}
		}

		protected override void SetInfoBlock (byte id, byte[] data)
		{
			if (id == 2 || id == 3) {
				if (data.Length > 32)
				{
		            Array.Resize(ref data, 32);
				}
				else if (data.Length < 32)
				{
					int size = data.Length;

		            Array.Resize(ref data, 32);

					//pad with zeros
					for (int i = size; i < 32; i++)
					{
						data[i] = 0;
					}
				}

                Array.Resize(ref data, 33);


                for (int i = 32; i >0; i--)
                {
                    data[i] = data[i-1];
				}

                data[0] = id;

                HidReport report = new HidReport(33, new HidDeviceData(data, HidDeviceData.ReadStatus.Success));
                device.WriteFeatureData(data);
			} else {
				throw new Exception("Invalid info block id");
			}
		}

		public override Boolean GetInfoBlock (byte id, out byte[] data)
		{
			if (id == 2 || id == 3) {
				if (connectedToDriver && device.ReadFeatureData (id, out data))
				{
                    HidReport report = new HidReport(33, new HidDeviceData(data, HidDeviceData.ReadStatus.Success));
                    data = report.Data;
					return true;
				}
				else
				{
					data = new byte[0];
					return false;
				}
			} else {
				throw new Exception("Invalid info block id");
			}
		}

		public Boolean GetLedData (out byte[] data)
		{
			if (connectedToDriver)
			{
				if (device.ReadFeatureData(9, out data))
				{
					HidReport report = new HidReport(3 * 64 + 1, new HidDeviceData(data, HidDeviceData.ReadStatus.Success));
					data = report.Data;
					return true;
				}
				else
				{
					data = new byte[0];
					return false;
				}
			}
			else
			{
				data = new byte[0];
				return false;
			}

		}

        /// <summary>
        /// Closes any connected devices.
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    CloseDevice();
                }

                disposed = true;
            }
        }

        /// <summary>
        /// Destroys instance and frees device resources (if not freed already)
        /// </summary>
        ~WindowsBlinkstickHid()
        {
            Dispose(false);
        }

	}
}

