using System;
using HidLibrary;
using BlinkStick.Hid;
using System.Collections.Generic;
using System.Threading;

namespace BlinkStick.Test
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");

			List<AbstractBlinkstickHid> Devices = new List<AbstractBlinkstickHid>();

			foreach (AbstractBlinkstickHid device in BlinkstickDeviceFinder.FindDevices())
			{
				Devices.Add(device);
			}

			if (Devices [0].OpenDevice ()) {
				//Devices [0].SetLedColor (255, 255, 255);
				//((WindowsBlinkstickHid)Devices [0]).SetLedMode (1);
				//Devices [0].SetLedColor (255, 0, 255);

				//((WindowsBlinkstickHid)Devices [0]).SetLedMode (2);
				//((WindowsBlinkstickHid)Devices [0]).SetLedIndexedColor (0, 63, 32, 0, 0);

				/* 
				for (int j = 0; j < 255; j++) {
					for (int i = 0; i < 8; i++) {
						((WindowsBlinkstickHid)Devices [0]).SetLedIndexedColor ((byte)(7 - i), (byte)j, 0, 0);
					}
				}

				for (int j = 0; j < 255; j++) {
					for (int i = 0; i < 8; i++) {
						((WindowsBlinkstickHid)Devices [0]).SetLedIndexedColor ((byte)(7 - i), (byte)(255 - j), 0, 0);
					}
				}
				/* */

				/* sample - run left right 

				int i = 0;
				int sign = 1;

				//int maxLeds = 64 + 8;
				//int maxLeds = 8;
				int maxLeds = 16;
				byte channel = 2;

				while (true) {
					i = i + sign;
					if (i >= maxLeds - 1)
						sign = -1;

					if (i <= 0)
						sign = 1;

					((WindowsBlinkstickHid)Devices [0]).SetLedIndexedColor (channel, (byte)i, 255, 0, 0);
					Thread.Sleep (20);
					((WindowsBlinkstickHid)Devices [0]).SetLedIndexedColor (channel, (byte)i, 0, 0, 0);
					Thread.Sleep (2);
				}
				/* */

				/* single channel packet 
				int leds1 = 64;

				int i1 = 0;
				int sign1 = 1;
				int index1 = 0;
				byte channel = 0;

				byte[] data1 = new byte[leds1 * 3];

				int sleep = 5;

				const byte colorValue = 32;
				const byte fadeSpeed = 4;

				while (true) {
					i1 = i1 + sign1;
					if (i1 >= leds1 - 1)
						sign1 = -1;

					if (i1 <= 0) {
						sign1 = 1;
						index1++;
						if (index1 > 2)
							index1 = 0;
					}

					data1 [i1 * 3 + index1] = colorValue;
					((WindowsBlinkstickHid)Devices [0]).SetLedColors (channel, data1);
					//data1 [i1 * 3 + index1] = 0;

					for (int i = 0; i < leds1; i++) {
						if (data1 [i * 3] <= fadeSpeed) {
							data1 [i * 3] = 0;
						} else {
							data1 [i * 3] = (byte)(data1 [i * 3] - fadeSpeed);
						}

						if (data1 [i * 3 + 1] <= fadeSpeed) {
							data1 [i * 3 + 1] = 0;
						} else {
							data1 [i * 3 + 1] = (byte)(data1 [i * 3 + 1] - fadeSpeed);
						}

						if (data1 [i * 3 + 2] <= fadeSpeed) {
							data1 [i * 3 + 2] = 0;
						} else {
							data1 [i * 3 + 2] = (byte)(data1 [i * 3 + 2] - fadeSpeed);
						}
					}

					if (sleep > 0) {
						Thread.Sleep (sleep);
					}
				}

				/* */


				/* Send packet of data */
				int i1 = 0;
				int sign1 = 1;
				int leds1 = 64;
				int index1 = 0;

				int i2 = 0;
				int sign2 = 1;
				int leds2 = 8;
				int index2 = 0;

				int i3 = 0;
				int sign3 = 1;
				int leds3 = 16;
				int index3 = 0;

				byte[] data1 = new byte[leds1 * 3];
				byte[] data2 = new byte[leds2 * 3];
				byte[] data3 = new byte[leds3 * 3];

				const byte colorValue = 32;
				const byte fadeSpeed = 16;

				int speed = 3;

				while (true) {
					i1 = i1 + sign1;
					if (i1 >= leds1 - 1)
						sign1 = -1;

					if (i1 <= 0) {
						sign1 = 1;
						index1++;
						if (index1 > 2)
							index1 = 0;
					}

					data1 [i1 * 3 + index1] = colorValue;
					((WindowsBlinkstickHid)Devices [0]).SetLedColors (0, data1);
					//data1 [i1 * 3 + index1] = 0;

					for (int i = 0; i < leds1; i++) {
						if (data1 [i * 3] <= fadeSpeed) {
							data1 [i * 3] = 0;
						} else {
							data1 [i * 3] = (byte)(data1 [i * 3] - fadeSpeed);
						}

						if (data1 [i * 3 + 1] <= fadeSpeed) {
							data1 [i * 3 + 1] = 0;
						} else {
							data1 [i * 3 + 1] = (byte)(data1 [i * 3 + 1] - fadeSpeed);
						}

						if (data1 [i * 3 + 2] <= fadeSpeed) {
							data1 [i * 3 + 2] = 0;
						} else {
							data1 [i * 3 + 2] = (byte)(data1 [i * 3 + 2] - fadeSpeed);
						}
					}

					Thread.Sleep (speed);

					//  --------

					i2 = i2 + sign2;
					if (i2 >= leds2 - 1)
						sign2 = -1;

					if (i2 <= 0) {
						sign2 = 1;
						index2++;
						if (index2 > 2)
							index2 = 0;
					}

					data2 [i2 * 3 + index2] = colorValue;
					((WindowsBlinkstickHid)Devices [0]).SetLedColors (1, data2);
					data2 [i2 * 3 + index2] = 0;
					Thread.Sleep (speed);

					//  --------

					i3 = i3 + sign3;
					if (i3 > leds3 - 1) {
						//sign3 = -1;
						i3 = 0;
					}

					if (i3 <= 0) {
						sign3 = 1;
						index3++;
						if (index3 > 2)
							index3 = 0;
					}

					data3 [i3 * 3 + index3] = colorValue;
					((WindowsBlinkstickHid)Devices [0]).SetLedColors (2, data3);
					data3 [i3 * 3 + index3] = 0;
					Thread.Sleep (speed);

					/*
					i3 = i3 + sign3;
					if (i3 >= 8) {
						//sign3 = -1;
						i3 = 0;
					}

					if (i3 <= 0) {
						sign3 = 1;

						index3++;
						if (index3 > 2)
							index3 = 0;
					}

					for (int i = 0; i < 8; i++) {
						data3 [i3 * 8 * 3 + i * 3 + index3] = colorValue;
					}

					((WindowsBlinkstickHid)Devices [0]).SetLedColors (2, data3);

					for (int i = 0; i < 8; i++) {
						data3 [i3 * 8 * 3 + i * 3 + index3] = 0;
					}
					*/
					Thread.Sleep (speed);

					//Thread.Sleep (10);
				}

				/* */

				/* pulse 
				byte[] data = new byte[64 * 3];

				while (true) {

					for (int j = 0; j < 255; j++) {
						for (int i = 0; i < 8; i++) {
							data [i * 3] = (byte)j;
							//data [i * 3 + 1] = (byte)(256 / (i + 1));

						}
						((WindowsBlinkstickHid)Devices [0]).SetLedColors (data);
					}

					for (int j = 0; j < 255; j++) {
						for (int i = 0; i < 8; i++) {
							data [i * 3] = (byte)(255 - j);
						}
						((WindowsBlinkstickHid)Devices [0]).SetLedColors (data);
					}
				}
				/* */

				/* Fire 
				int leds1 = 64;

				byte[] data1 = new byte[leds1 * 3];

				int height = 8;
				int width = 8;

				byte channel = 0;

				Random r = new Random ();

				while (true) {
					for (int i = 0; i < 8; i++) {
						data1 [i * 3 + 1] = (byte)(r.Next (16) * r.Next(2));
					}

					for (int j = 1; j < 8; j++) {
						for (int i = 1; i < 7; i++) {
							data1 [j * 3 * 8 + i * 3 + 1] =
								(byte)((//data1 [j * 3 * 8 + i * 3 + 1] +
										data1 [(j - 1) * 3 * 8 + (i - 1) * 3 + 1] +
										data1 [(j - 1) * 3 * 8 + i * 3 + 1] +
										data1 [(j - 1) * 3 * 8 + (i + 1) * 3 + 1]) / 4);

						}
					}

					((WindowsBlinkstickHid)Devices [0]).SetLedColors (channel, data1);
					Thread.Sleep (5);
				}

				/*				 */

			}

			Console.WriteLine ("Press Enter to exit!");
			//Console.ReadLine ();
		}
	}
}
