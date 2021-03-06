
// This file has been generated by the GUI designer. Do not modify.
namespace BlinkStickClient
{
	public partial class OverviewWidget
	{
		private global::Gtk.VBox vbox4;
		
		private global::Gtk.HBox hboxMiniMenu;
		
		private global::Gtk.Label labelSelectBlinkStick;
		
		private global::BlinkStickClient.DeviceComboboxWidget deviceComboboxWidget;
		
		private global::Gtk.Button buttonRefresh;
		
		private global::Gtk.Button buttonConfigure;
		
		private global::Gtk.Button buttonDelete;
		
		private global::BlinkStickClient.BlinkStickEmulatorWidget blinkstickemulatorwidget1;
		
		private global::Gtk.Frame frame2;
		
		private global::Gtk.Alignment GtkAlignment8;
		
		private global::Gtk.HBox hbox1;
		
		private global::Gtk.Label GtkLabel11;
		
		private global::Gtk.HSeparator hseparator1;
		
		private global::BlinkStickClient.BlinkStickInfoWidget blinkstickinfowidget2;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget BlinkStickClient.OverviewWidget
			global::Stetic.BinContainer.Attach (this);
			this.Name = "BlinkStickClient.OverviewWidget";
			// Container child BlinkStickClient.OverviewWidget.Gtk.Container+ContainerChild
			this.vbox4 = new global::Gtk.VBox ();
			this.vbox4.Name = "vbox4";
			this.vbox4.Spacing = 6;
			// Container child vbox4.Gtk.Box+BoxChild
			this.hboxMiniMenu = new global::Gtk.HBox ();
			this.hboxMiniMenu.Name = "hboxMiniMenu";
			this.hboxMiniMenu.Spacing = 6;
			this.hboxMiniMenu.BorderWidth = ((uint)(12));
			// Container child hboxMiniMenu.Gtk.Box+BoxChild
			this.labelSelectBlinkStick = new global::Gtk.Label ();
			this.labelSelectBlinkStick.Name = "labelSelectBlinkStick";
			this.labelSelectBlinkStick.LabelProp = global::Mono.Unix.Catalog.GetString ("Select BlinkStick:");
			this.hboxMiniMenu.Add (this.labelSelectBlinkStick);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hboxMiniMenu [this.labelSelectBlinkStick]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child hboxMiniMenu.Gtk.Box+BoxChild
			this.deviceComboboxWidget = new global::BlinkStickClient.DeviceComboboxWidget ();
			this.deviceComboboxWidget.Events = ((global::Gdk.EventMask)(256));
			this.deviceComboboxWidget.Name = "deviceComboboxWidget";
			this.deviceComboboxWidget.AutoSelectDevice = true;
			this.hboxMiniMenu.Add (this.deviceComboboxWidget);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hboxMiniMenu [this.deviceComboboxWidget]));
			w2.Position = 1;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hboxMiniMenu.Gtk.Box+BoxChild
			this.buttonRefresh = new global::Gtk.Button ();
			this.buttonRefresh.CanFocus = true;
			this.buttonRefresh.Name = "buttonRefresh";
			this.buttonRefresh.UseUnderline = true;
			global::Gtk.Image w3 = new global::Gtk.Image ();
			w3.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-refresh", global::Gtk.IconSize.Menu);
			this.buttonRefresh.Image = w3;
			this.hboxMiniMenu.Add (this.buttonRefresh);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hboxMiniMenu [this.buttonRefresh]));
			w4.Position = 2;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hboxMiniMenu.Gtk.Box+BoxChild
			this.buttonConfigure = new global::Gtk.Button ();
			this.buttonConfigure.CanFocus = true;
			this.buttonConfigure.Name = "buttonConfigure";
			this.buttonConfigure.UseUnderline = true;
			global::Gtk.Image w5 = new global::Gtk.Image ();
			w5.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-preferences", global::Gtk.IconSize.Menu);
			this.buttonConfigure.Image = w5;
			this.hboxMiniMenu.Add (this.buttonConfigure);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hboxMiniMenu [this.buttonConfigure]));
			w6.Position = 3;
			w6.Expand = false;
			w6.Fill = false;
			// Container child hboxMiniMenu.Gtk.Box+BoxChild
			this.buttonDelete = new global::Gtk.Button ();
			this.buttonDelete.CanFocus = true;
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.UseUnderline = true;
			global::Gtk.Image w7 = new global::Gtk.Image ();
			w7.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-delete", global::Gtk.IconSize.Menu);
			this.buttonDelete.Image = w7;
			this.hboxMiniMenu.Add (this.buttonDelete);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hboxMiniMenu [this.buttonDelete]));
			w8.Position = 4;
			w8.Expand = false;
			w8.Fill = false;
			this.vbox4.Add (this.hboxMiniMenu);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.hboxMiniMenu]));
			w9.Position = 0;
			w9.Expand = false;
			w9.Fill = false;
			// Container child vbox4.Gtk.Box+BoxChild
			this.blinkstickemulatorwidget1 = new global::BlinkStickClient.BlinkStickEmulatorWidget ();
			this.blinkstickemulatorwidget1.HeightRequest = 200;
			this.blinkstickemulatorwidget1.Events = ((global::Gdk.EventMask)(256));
			this.blinkstickemulatorwidget1.Name = "blinkstickemulatorwidget1";
			this.vbox4.Add (this.blinkstickemulatorwidget1);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.blinkstickemulatorwidget1]));
			w10.Position = 1;
			// Container child vbox4.Gtk.Box+BoxChild
			this.frame2 = new global::Gtk.Frame ();
			this.frame2.Name = "frame2";
			this.frame2.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frame2.Gtk.Container+ContainerChild
			this.GtkAlignment8 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
			this.GtkAlignment8.Name = "GtkAlignment8";
			this.GtkAlignment8.LeftPadding = ((uint)(12));
			// Container child GtkAlignment8.Gtk.Container+ContainerChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			this.GtkAlignment8.Add (this.hbox1);
			this.frame2.Add (this.GtkAlignment8);
			this.GtkLabel11 = new global::Gtk.Label ();
			this.GtkLabel11.Name = "GtkLabel11";
			this.GtkLabel11.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Change Color</b>");
			this.GtkLabel11.UseMarkup = true;
			this.frame2.LabelWidget = this.GtkLabel11;
			this.vbox4.Add (this.frame2);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.frame2]));
			w13.Position = 2;
			// Container child vbox4.Gtk.Box+BoxChild
			this.hseparator1 = new global::Gtk.HSeparator ();
			this.hseparator1.Name = "hseparator1";
			this.vbox4.Add (this.hseparator1);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.hseparator1]));
			w14.Position = 3;
			w14.Expand = false;
			w14.Fill = false;
			// Container child vbox4.Gtk.Box+BoxChild
			this.blinkstickinfowidget2 = new global::BlinkStickClient.BlinkStickInfoWidget ();
			this.blinkstickinfowidget2.Events = ((global::Gdk.EventMask)(256));
			this.blinkstickinfowidget2.Name = "blinkstickinfowidget2";
			this.vbox4.Add (this.blinkstickinfowidget2);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.blinkstickinfowidget2]));
			w15.Position = 4;
			w15.Expand = false;
			w15.Fill = false;
			this.Add (this.vbox4);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
			this.buttonRefresh.Clicked += new global::System.EventHandler (this.OnButtonRefreshClicked);
			this.buttonConfigure.Clicked += new global::System.EventHandler (this.OnButtonConfigureClicked);
			this.buttonDelete.Clicked += new global::System.EventHandler (this.OnButtonDeleteClicked);
		}
	}
}
