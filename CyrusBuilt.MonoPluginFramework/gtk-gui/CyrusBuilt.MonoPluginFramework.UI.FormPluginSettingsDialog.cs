
// This file has been generated by the GUI designer. Do not modify.
namespace CyrusBuilt.MonoPluginFramework.UI
{
	public partial class FormPluginSettingsDialog
	{
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		private global::Gtk.TreeView treeviewPlugins;
		private global::Gtk.Button buttonCancel;
		private global::Gtk.Button buttonSave;
		
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget CyrusBuilt.MonoPluginFramework.UI.FormPluginSettingsDialog
			this.Name = "CyrusBuilt.MonoPluginFramework.UI.FormPluginSettingsDialog";
			this.Title = global::Mono.Unix.Catalog.GetString ("Plugin Settings");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Internal child CyrusBuilt.MonoPluginFramework.UI.FormPluginSettingsDialog.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "formSettingsDialogVBox";
			w1.BorderWidth = ((uint)(2));
			// Container child formSettingsDialogVBox.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.treeviewPlugins = new global::Gtk.TreeView ();
			this.treeviewPlugins.CanFocus = true;
			this.treeviewPlugins.Name = "treeviewPlugins";
			this.GtkScrolledWindow.Add (this.treeviewPlugins);
			w1.Add (this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(w1 [this.GtkScrolledWindow]));
			w3.Position = 0;
			// Internal child CyrusBuilt.MonoPluginFramework.UI.FormPluginSettingsDialog.ActionArea
			global::Gtk.HButtonBox w4 = this.ActionArea;
			w4.Name = "formPluginsDialogActionArea";
			w4.Spacing = 10;
			w4.BorderWidth = ((uint)(5));
			w4.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child formPluginsDialogActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget (this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w5 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w4 [this.buttonCancel]));
			w5.Expand = false;
			w5.Fill = false;
			// Container child formPluginsDialogActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonSave = new global::Gtk.Button ();
			this.buttonSave.CanDefault = true;
			this.buttonSave.CanFocus = true;
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.UseStock = true;
			this.buttonSave.UseUnderline = true;
			this.buttonSave.Label = "gtk-ok";
			this.AddActionWidget (this.buttonSave, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w6 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w4 [this.buttonSave]));
			w6.Position = 1;
			w6.Expand = false;
			w6.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 528;
			this.DefaultHeight = 373;
			this.Show ();
			this.Realized += new global::System.EventHandler (this.OnRealized);
			this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
			this.treeviewPlugins.RowActivated += new global::Gtk.RowActivatedHandler (this.OnTreeviewPluginsRowActivated);
			this.buttonCancel.Clicked += new global::System.EventHandler (this.OnButtonCancelClicked);
			this.buttonSave.Clicked += new global::System.EventHandler (this.OnButtonSaveClicked);
		}
	}
}
