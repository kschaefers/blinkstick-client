﻿using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using log4net;
using BlinkStickDotNet;

namespace BlinkStickClient.DataModel
{
    public class BlinkStickDeviceSettings
    {
        public event SendColorEventHandler SendColor;

        public Boolean OnSendColor(byte channel, byte index, byte r, byte g, byte b)
        {
            if (SendColor != null)
            {
                SendColor(this, new SendColorEventArgs(channel, index, r, g, b));
            }

            return true;
        }

        protected static readonly ILog log = LogManager.GetLogger("BlinkStickDeviceSettings");  

        [JsonIgnore]
        public Boolean Running { get; private set; }

        public String Serial { get; set; }

        public int LedsR { get; set; }
        public int LedsG { get; set; }
        public int LedsB { get; set; }

        public int BrightnessLimit { get; set; }

        private BackgroundWorker patternAnimator;

		public BlinkStickDeviceEnum BlinkStickDevice;

        [JsonIgnore]
        public Queue<TriggeredEvent> EventQueue = new Queue<TriggeredEvent>();

        private BlinkStick _Led;

        [JsonIgnore]
        public BlinkStick Led
        {
            get
            {
                return _Led;
            }
            set
            {
                if (_Led != value)
                {
                    _Led = value;

                    ApplyProperties();
                }
            }
        }

        [JsonIgnore]
        public Boolean Touched { get; set; }

        private Boolean[][] LedBusy = new Boolean[3][];
        private byte[][] LedFrame = new byte[3][];
        private Boolean NeedsLedUpdate = false;

        public BlinkStickDeviceSettings()
        {
            this.Touched = true;
            this.BrightnessLimit = 100;
            this.Running = false;

            this.LedFrame[0] = new byte[64 * 3];
            this.LedFrame[1] = new byte[64 * 3];
            this.LedFrame[2] = new byte[64 * 3];

            this.LedBusy[0] = new Boolean[64];
            this.LedBusy[1] = new Boolean[64];
            this.LedBusy[2] = new Boolean[64];
        }

        public BlinkStickDeviceSettings(BlinkStick led)
        {
            this.Led = led;
            this.Touched = true;
            this.Running = false;
            this.BrightnessLimit = 100;
        }

        private void ApplyProperties()
        {
            if (Led == null)
            {
                return;
            }

            this.Serial = Led.Serial;

            switch (this.Led.BlinkStickDevice) {
                case BlinkStickDeviceEnum.BlinkStick:
                    this.LedsR = 1;
                    this.LedsG = 1;
                    this.LedsB = 1;
                    break;
                case BlinkStickDeviceEnum.BlinkStickPro:
                    this.LedsR = 64;
                    this.LedsG = 64;
                    this.LedsB = 64;
                    break;
                case BlinkStickDeviceEnum.BlinkStickSquare:
                case BlinkStickDeviceEnum.BlinkStickStrip:
                    this.LedsR = 8;
                    this.LedsG = 0;
                    this.LedsB = 0;
                    break;
                case BlinkStickDeviceEnum.BlinkStickNano:
                    this.LedsR = 2;
                    this.LedsG = 0;
                    this.LedsB = 0;
                    break;
                case BlinkStickDeviceEnum.BlinkStickFlex:
                    this.LedsR = 32;
                    this.LedsG = 0;
                    this.LedsB = 0;
                    break;
                default:
                    break;
            }

            this.BlinkStickDevice = Led.BlinkStickDevice;
        }

        public override string ToString()
        {
            if (Led != null && Led.InfoBlock1.Trim() != "")
            {
                return Led.InfoBlock1;
            }
            else
            {
                return this.Serial;
            }
        }

        public void SetColor(byte channel, byte index, byte r, byte g, byte b)
        {
            if (BrightnessLimit < 100 && BrightnessLimit >= 0)
            {
                r = (byte)(BrightnessLimit / 100.0 * r);
                g = (byte)(BrightnessLimit / 100.0 * g);
                b = (byte)(BrightnessLimit / 100.0 * b);
            }

            lock (this)
            {
                LedFrame[channel][index * 3 + 0] = g;
                LedFrame[channel][index * 3 + 1] = r;
                LedFrame[channel][index * 3 + 2] = b;

                NeedsLedUpdate = true;
            }

            if (!Running)
            {
                this.Start();
            }

            OnSendColor(channel, index, r, g, b);
        }

        public void SetColor(DeviceNotification notification, byte r, byte g, byte b)
        {
            if (BrightnessLimit < 100 && BrightnessLimit >= 0)
            {
                r = (byte)(BrightnessLimit / 100.0 * r);
                g = (byte)(BrightnessLimit / 100.0 * g);
                b = (byte)(BrightnessLimit / 100.0 * b);
            }

            lock (this)
            {
                if (notification == null)
                {
                    int channel = 0;
                    for (int i = 0; i < this.LedsR; i++)
                    {
                        LedFrame[channel][i * 3] = g;
                        LedFrame[channel][i * 3 + 1] = r;
                        LedFrame[channel][i * 3 + 2] = b;
                        OnSendColor((byte)channel, (byte)i, r, g, b);
                    }
                }
                else
                {
                    int channel = notification.GetValidChannel();
                    //TODO: Copy first and last indexes to event
                    for (int i = notification.LedFirstIndex; i <= notification.LedLastIndex; i++)
                    {
                        LedFrame[channel][i * 3] = g;
                        LedFrame[channel][i * 3 + 1] = r;
                        LedFrame[channel][i * 3 + 2] = b;
                        OnSendColor((byte)channel, (byte)i, r, g, b);
                    }
                }

                NeedsLedUpdate = true;
            }

            if (!Running)
            {
                this.Start();
            }

        }

        public RgbColor GetColor(CustomNotification notification)
        {
            int index = 0;
            int channel = 0;

            if (notification is DeviceNotification)
            {
                index = ((DeviceNotification)notification).LedFirstIndex * 3;
                channel = ((DeviceNotification)notification).GetValidChannel();
            }

            return RgbColor.FromRgb(LedFrame[channel][index + 1], LedFrame[channel][index], LedFrame[channel][index + 2]);
        }

        public void SetColor(byte r, byte g, byte b)
        {
            if (BrightnessLimit < 100 && BrightnessLimit >= 0)
            {
                r = (byte)(BrightnessLimit / 100.0 * r);
                g = (byte)(BrightnessLimit / 100.0 * g);
                b = (byte)(BrightnessLimit / 100.0 * b);
            }

            lock (this)
            {
                NeedsLedUpdate = true;

                LedFrame[0][0] = g;
                LedFrame[0][1] = r;
                LedFrame[0][2] = b;
            }

            if (!Running)
            {
                this.Start();
            }

            OnSendColor(0, 0, r, g, b);
        }

        public void TurnOff()
        {
            for (byte i = 0; i < LedsR; i++)
            {
                this.SetColor(0, i, 0, 0, 0);
            }

            for (byte i = 0; i < LedsG; i++)
            {
                this.SetColor(1, i, 0, 0, 0);
            }

            for (byte i = 0; i < LedsB; i++)
            {
                this.SetColor(2, i, 0, 0, 0);
            }
        }

        public void Start()
        {
            if (Running)
                return;

            Running = true;

            patternAnimator = new BackgroundWorker ();
            patternAnimator.DoWork += new DoWorkEventHandler (patternAnimator_DoWork);
            patternAnimator.WorkerSupportsCancellation = true;
            patternAnimator.RunWorkerAsync ();
        }

        void patternAnimator_DoWork (object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;

            try
            {
                log.InfoFormat("[{0}] Starting pattern playback", this.Serial);

                List<TriggeredEvent> eventsPlaying = new List<TriggeredEvent>();

                while (!worker.CancellationPending)
                {
                    TriggeredEvent ev = null;

                    lock (EventQueue)
                    {
                        for (int i = 0; i < EventQueue.Count; i++)
                        {
                            ev = EventQueue.Dequeue();

                            if (CanPlayEvent(ev))
                            {
                                break;
                            }
                            else
                            {
                                EventQueue.Enqueue(ev);
                                ev = null;
                            }
                        }
                    }

                    if (ev != null && ev.NotificationSnapshot is PatternNotification)
                    {
                        ev.Started = DateTime.Now;

                        PatternNotification notification = ev.NotificationSnapshot as PatternNotification;
                        foreach (Animation animation in notification.Pattern.Animations)
                        {
                            Animation copyAnimation = new Animation();
                            copyAnimation.Assign(animation);
                            ev.Animations.Add(copyAnimation);
                        }

                        ev.Animations[0].ReferenceColor = GetColor(ev.NotificationSnapshot);

                        AssignBusyLeds(ev, true);
                        eventsPlaying.Add(ev);
                    }

                    //Prepare next frame of data to send to LEDs
                    for (int ii = eventsPlaying.Count - 1; ii >= 0; ii--)
                    {
                        TriggeredEvent evnt = eventsPlaying[ii];

                        RgbColor color = evnt.Animations[evnt.AnimationIndex].GetColor(evnt.Started.Value, DateTime.Now, evnt.Animations[evnt.AnimationIndex].ReferenceColor);

                        PatternNotification notification = evnt.NotificationSnapshot as PatternNotification;

                        lock (this)
                        {
                            SetColor(notification, color.R, color.G, color.B);
                        }

                        if (evnt.Animations[evnt.AnimationIndex].AnimationFinished)
                        {
                            evnt.AnimationIndex += 1;
                            if (evnt.AnimationIndex == evnt.Animations.Count)
                            {
                                eventsPlaying.RemoveAt(ii);
                                AssignBusyLeds(evnt, false);
                            }
                            else
                            {
                                evnt.Animations[evnt.AnimationIndex].ReferenceColor = GetColor(evnt.NotificationSnapshot);
                                evnt.Started = DateTime.Now;
                            }
                        }
                    }

                    if (Led != null && NeedsLedUpdate)
                    {
                        lock (this)
                        {
                            NeedsLedUpdate = false;
                        }

                        if (Led.BlinkStickDevice == BlinkStickDeviceEnum.BlinkStick || 
                            Led.BlinkStickDevice == BlinkStickDeviceEnum.BlinkStickPro && Led.Mode < 2)
                        {
                            Led.SetColor(LedFrame[0][1], LedFrame[0][0], LedFrame[0][2]);
                        }
                        else
                        {
                            byte[] frame = new byte[this.LedsR * 3];
                            Array.Copy(LedFrame[0], 0, frame, 0, frame.Length); 
                            Led.SetColors(0, frame);

                            if (Led.BlinkStickDevice == BlinkStickDeviceEnum.BlinkStickPro)
                            {
                                frame = new byte[this.LedsG * 3];
                                Array.Copy(LedFrame[1], 0, frame, 0, frame.Length); 
                                Led.SetColors(1, frame);

                                frame = new byte[this.LedsB * 3];
                                Array.Copy(LedFrame[2], 0, frame, 0, frame.Length); 
                                Led.SetColors(2, frame);
                            }
                        }
                    }

                    Thread.Sleep(40);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Pattern playback crash {0}", ex);
            }

            log.InfoFormat("[{0}] Pattern playback stopped", this.Serial);

            Running = false;
        }

        Boolean CanPlayEvent(TriggeredEvent ev)
        {
            PatternNotification notification = (PatternNotification)ev.NotificationSnapshot;
            int channel = notification.GetValidChannel();
            for (int i = notification.LedFirstIndex; i <= notification.LedLastIndex; i++)
            {
                //If there is at least one LED currently in use, event needs to wait
                if (LedBusy[channel][i])
                    return false;
            }

            return true;
        }

        void AssignBusyLeds(TriggeredEvent ev, Boolean busy)
        {
            PatternNotification notification = (PatternNotification)ev.NotificationSnapshot;
            int channel = notification.GetValidChannel();
            for (int i = notification.LedFirstIndex; i <= notification.LedLastIndex; i++)
            {
                LedBusy[channel][i] = busy;
            }
        }

        public void Stop()
        {
            if (patternAnimator != null && patternAnimator.IsBusy)
            {
                Led.Stop();
                patternAnimator.CancelAsync();
            }
        }
    }
}

