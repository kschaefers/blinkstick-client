
// This file has been generated by the GUI designer. Do not modify.
namespace BlinkStick
{
	public partial class BlinkStickTestForm
	{
		private global::Gtk.VBox vbox4;
		private global::Gtk.HBox hbox2;
		private global::Gtk.Label labelBlinkStick;
		private global::Gtk.ComboBox comboboxDevices;
		private global::Gtk.ColorSelection colorSelection;
		private global::Gtk.HBox hbox1;
		private global::Gtk.CheckButton cbAutoSetColor;
		private global::Gtk.Button buttonSetColor;
		private global::Gtk.Button buttonSwitchToColor;
		private global::Gtk.Button buttonPulseColor;
		private global::Gtk.Button buttonAllOff;
		private global::Gtk.Button buttonCancel;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget BlinkStick.BlinkStickTestForm
			this.Name = "BlinkStick.BlinkStickTestForm";
			this.Title = global::Mono.Unix.Catalog.GetString ("Test BlinkStick");
			this.WindowPosition = ((global::Gtk.WindowPosition)(1));
			// Internal child BlinkStick.BlinkStickTestForm.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.vbox4 = new global::Gtk.VBox ();
			this.vbox4.Name = "vbox4";
			this.vbox4.Spacing = 6;
			this.vbox4.BorderWidth = ((uint)(12));
			// Container child vbox4.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.labelBlinkStick = new global::Gtk.Label ();
			this.labelBlinkStick.Name = "labelBlinkStick";
			this.labelBlinkStick.LabelProp = global::Mono.Unix.Catalog.GetString ("Select BlinkStick:");
			this.hbox2.Add (this.labelBlinkStick);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.labelBlinkStick]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.comboboxDevices = global::Gtk.ComboBox.NewText ();
			this.comboboxDevices.Name = "comboboxDevices";
			this.hbox2.Add (this.comboboxDevices);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.comboboxDevices]));
			w3.Position = 1;
			w3.Expand = false;
			w3.Fill = false;
			this.vbox4.Add (this.hbox2);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.hbox2]));
			w4.Position = 0;
			w4.Expand = false;
			w4.Fill = false;
			// Container child vbox4.Gtk.Box+BoxChild
			this.colorSelection = new global::Gtk.ColorSelection ();
			this.colorSelection.Name = "colorSelection";
			this.colorSelection.HasPalette = true;
			this.colorSelection.HasOpacityControl = true;
			this.vbox4.Add (this.colorSelection);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.colorSelection]));
			w5.Position = 1;
			// Container child vbox4.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.cbAutoSetColor = new global::Gtk.CheckButton ();
			this.cbAutoSetColor.CanFocus = true;
			this.cbAutoSetColor.Name = "cbAutoSetColor";
			this.cbAutoSetColor.Label = global::Mono.Unix.Catalog.GetString ("Automatically set color");
			this.cbAutoSetColor.Active = true;
			this.cbAutoSetColor.DrawIndicator = true;
			this.cbAutoSetColor.UseUnderline = true;
			this.hbox1.Add (this.cbAutoSetColor);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.cbAutoSetColor]));
			w6.Position = 0;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonSetColor = new global::Gtk.Button ();
			this.buttonSetColor.CanFocus = true;
			this.buttonSetColor.Name = "buttonSetColor";
			this.buttonSetColor.UseUnderline = true;
			this.buttonSetColor.Label = global::Mono.Unix.Catalog.GetString ("Set Color");
			this.hbox1.Add (this.buttonSetColor);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.buttonSetColor]));
			w7.Position = 1;
			w7.Expand = false;
			w7.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonSwitchToColor = new global::Gtk.Button ();
			this.buttonSwitchToColor.CanFocus = true;
			this.buttonSwitchToColor.Name = "buttonSwitchToColor";
			this.buttonSwitchToColor.UseUnderline = true;
			this.buttonSwitchToColor.Label = global::Mono.Unix.Catalog.GetString ("Switch to Color");
			this.hbox1.Add (this.buttonSwitchToColor);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.buttonSwitchToColor]));
			w8.Position = 2;
			w8.Expand = false;
			w8.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonPulseColor = new global::Gtk.Button ();
			this.buttonPulseColor.CanFocus = true;
			this.buttonPulseColor.Name = "buttonPulseColor";
			this.buttonPulseColor.UseUnderline = true;
			this.buttonPulseColor.Label = global::Mono.Unix.Catalog.GetString ("Pulse Color");
			this.hbox1.Add (this.buttonPulseColor);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.buttonPulseColor]));
			w9.Position = 3;
			w9.Expand = false;
			w9.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonAllOff = new global::Gtk.Button ();
			this.buttonAllOff.CanFocus = true;
			this.buttonAllOff.Name = "buttonAllOff";
			this.buttonAllOff.UseUnderline = true;
			this.buttonAllOff.Label = global::Mono.Unix.Catalog.GetString ("All Off");
			this.hbox1.Add (this.buttonAllOff);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.buttonAllOff]));
			w10.Position = 4;
			w10.Expand = false;
			w10.Fill = false;
			this.vbox4.Add (this.hbox1);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.hbox1]));
			w11.Position = 2;
			w11.Expand = false;
			w11.Fill = false;
			w1.Add (this.vbox4);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(w1 [this.vbox4]));
			w12.Position = 0;
			// Internal child BlinkStick.BlinkStickTestForm.ActionArea
			global::Gtk.HButtonBox w13 = this.ActionArea;
			w13.Name = "dialog1_ActionArea";
			w13.Spacing = 10;
			w13.BorderWidth = ((uint)(5));
			w13.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-close";
			this.AddActionWidget (this.buttonCancel, -7);
			global::Gtk.ButtonBox.ButtonBoxChild w14 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w13 [this.buttonCancel]));
			w14.Expand = false;
			w14.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 533;
			this.DefaultHeight = 395;
			this.Show ();
			this.comboboxDevices.Changed += new global::System.EventHandler (this.OnComboboxDevicesChanged);
			this.colorSelection.ColorChanged += new global::System.EventHandler (this.OnColorSelectionColorChanged);
			this.cbAutoSetColor.Toggled += new global::System.EventHandler (this.OnCbAutoSetColorToggled);
			this.buttonSetColor.Clicked += new global::System.EventHandler (this.OnButtonSetColorClicked);
			this.buttonSwitchToColor.Clicked += new global::System.EventHandler (this.OnButtonSwitchToColorClicked);
			this.buttonPulseColor.Clicked += new global::System.EventHandler (this.OnButtonPulseColorClicked);
			this.buttonAllOff.Clicked += new global::System.EventHandler (this.OnButtonAllOffClicked);
		}
	}
}
