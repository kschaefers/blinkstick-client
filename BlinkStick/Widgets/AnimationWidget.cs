﻿using System;
using BlinkStickClient.Classes;
using Gtk;

namespace BlinkStickClient
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class AnimationWidget : Gtk.Bin
    {
        public int Index {
            set {
                labelNumber.Text = "#" + value.ToString(); 
            }
        }


        public event EventHandler DeleteAnimation;

        protected void OnDeleteAnimation()
        {
            if (DeleteAnimation != null)
                DeleteAnimation(this, new EventArgs());
        }

        public event EventHandler MoveUp;

        protected void OnMoveUp()
        {
            if (MoveUp != null)
                MoveUp(this, new EventArgs());
        }

        public event EventHandler MoveDown;

        protected void OnMoveDown()
        {
            if (MoveDown != null)
                MoveDown(this, new EventArgs());
        }


        private Animation _AnimationObject;
        public Animation AnimationObject {
            get { 
                return _AnimationObject;
            }

            set {
                _AnimationObject = value;
                LoadAnimationObject();
            }
        }

        public AnimationWidget()
        {
            this.Build();

            comboboxMode.Active = 0;

            OnComboboxModeChanged(this, null);

            /*
            this.EnterNotifyEvent += (o, args) => {
                ButtonVisibility(true);
            };

            this.LeaveNotifyEvent += (o, args) =>  {
                ButtonVisibility(false);
            };
            */

            ButtonVisibility(true);

            /*
            foreach (Widget widget in this.AllChildren)
            {
                widget.EnterNotifyEvent += delegate(object o, EnterNotifyEventArgs args)
                {
                    args.RetVal = false;
                };
                widget.LeaveNotifyEvent += delegate(object o, LeaveNotifyEventArgs args)
                {
                    args.RetVal = false;
                };
            }
            */
        }

        private void ButtonVisibility(Boolean visible) 
        {
            buttonUp.Visible = visible;
            buttonUp.Sensitive = visible;
            buttonUp.NoShowAll = visible;

            buttonDown.Visible = visible;
            buttonDown.Sensitive = visible;
            buttonDown.NoShowAll = visible;

            buttonDelete.Visible = visible;
            buttonDelete.Sensitive = visible;
            buttonDelete.NoShowAll = visible;

            if (visible)
                this.Show();
        }

        protected void OnComboboxModeChanged (object sender, EventArgs e)
        {
            hboxSetColor.Visible = comboboxMode.Active == 0;
            hboxSetColor.Sensitive = comboboxMode.Active == 0;
            hboxSetColor.NoShowAll = !(comboboxMode.Active == 0);

            hboxBlink.Visible = comboboxMode.Active == 1;
            hboxBlink.Sensitive = comboboxMode.Active == 1;
            hboxBlink.NoShowAll = !(comboboxMode.Active == 1);

            hboxPulse.Visible = comboboxMode.Active == 2;
            hboxPulse.Sensitive = comboboxMode.Active == 2;
            hboxPulse.NoShowAll = !(comboboxMode.Active == 2);

            hboxMorph.Visible = comboboxMode.Active == 3;
            hboxMorph.Sensitive = comboboxMode.Active == 3;
            hboxMorph.NoShowAll = !(comboboxMode.Active == 3);

            this.Show();
        }

        private void LoadAnimationObject()
        {
            switch (AnimationObject.AnimationType)
            {
                case AnimationTypeEnum.SetColor:
                    comboboxMode.Active = 0;
                    break;
                case AnimationTypeEnum.Blink:
                    comboboxMode.Active = 1;
                    break;
                case AnimationTypeEnum.Pulse:
                    comboboxMode.Active = 2;
                    break;
                case AnimationTypeEnum.Morph:
                    comboboxMode.Active = 3;
                    break;
            }


            spinbuttonSetColorDelay.Value = AnimationObject.DelaySetColor;

            spinbuttonBlinkDelay.Value = AnimationObject.DurationBlink;
            spinbuttonBlinkRepeat.Value = AnimationObject.RepeatBlink;

            spinbuttonPulseDuration.Value = AnimationObject.DurationPulse;
            spinbuttonPulseRepeat.Value = AnimationObject.RepeatPulse;

            spinbuttonMorphDuration.Value = AnimationObject.DurationMorph;

            OnComboboxModeChanged(this, null);
        }

        protected void OnButtonDeleteClicked (object sender, EventArgs e)
        {
            OnDeleteAnimation();
        }
    }
}
