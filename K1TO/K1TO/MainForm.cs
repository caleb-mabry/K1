using Eto.Drawing;
using Eto.Forms;
using K1TO.UiElements;
using System.Collections.Generic;

namespace K1TO
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// A file dialog that allows the user to select a file given all of the currently supported file types that
        /// are available for the program. This will set the current state of the program.
        /// </summary>
        /// <param name="currentState">The current state of the program</param>
        private void selectFile(ProgramState currentState)
        {
            OpenFileDialog folderDialog = new OpenFileDialog();
            folderDialog.ShowDialog(this);
            currentState.currentlySelectedFile = folderDialog.FileName;
            MessageBox.Show(currentState.currentlySelectedFile);
        }
        Control FileTreeView()
        {
            var control = new TreeGridView
            {
                Size = new Size(-1, 500)
            };
            control.Columns.Add(new GridColumn { DataCell = new TextBoxCell(0) });
            TreeGridItemCollection collection = new TreeGridItemCollection();
            TreeGridItem item = new TreeGridItem("Crikey");
            List<TreeGridItem> children = new List<TreeGridItem>();
            children.Add(item);
            TreeGridItem item2 = new TreeGridItem(children, "Crikey2");

            collection.Add(item2);
            control.DataStore = collection;
            return control;
        }
        public MainForm()
        {
            ProgramState currentState = new ProgramState();
            Label testing = new Label { Text = currentState.currentlySelectedFile };
            Title = "K1TO";
            ClientSize = new Size(500, 500);
            FileTreeView carpet = new FileTreeView();
            Content = new TableLayout
            {
                Spacing = new Size(5, 5), // space between each cell
                Padding = new Padding(10, 10, 10, 10), // space around the table's sides
                Rows =
    {
        new TableRow(
            // Selected File
            FileTreeView()
        ),
        new TableRow(
            new TextBox { Text = "Some text" },
            new DropDown { Items = { "Item 1", "Item 2", "Item 3" } },
            new CheckBox { Text = "A checkbox" }
        ),
		// by default, the last row & column will get scaled. This adds a row at the end to take the extra space of the form.
		// otherwise, the above row will get scaled and stretch the TextBox/ComboBox/CheckBox to fill the remaining height.
		new TableRow { ScaleHeight = true }
    }
            };
            // create a few commands that can be used for the menu and toolbar
            Command clickMe = new Command { MenuText = "Click Me!", ToolBarText = "Click Me!" };
            clickMe.Executed += (sender, e) => MessageBox.Show(FileInformation.testing());

            Command openFile = new Command { MenuText = "Open", ToolBarText = "Open" };
            openFile.Executed += (sender, e) =>
            {
                selectFile(currentState);
                
            };
            openFile.Executed += (sender, e) => testing.Text = currentState.currentlySelectedFile;

            Command quitCommand = new Command { MenuText = "Quit", Shortcut = Application.Instance.CommonModifier | Keys.Q };
            quitCommand.Executed += (sender, e) => Application.Instance.Quit();

            Command aboutCommand = new Command { MenuText = "About..." };
            aboutCommand.Executed += (sender, e) => new AboutDialog().ShowDialog(this);

            // create menu
            Menu = new MenuBar
            {
                Items =
                {
					// File submenu
					new ButtonMenuItem { Text = "&File", Items = { openFile } },
					// new ButtonMenuItem { Text = "&Edit", Items = { /* commands/items */ } },
					// new ButtonMenuItem { Text = "&View", Items = { /* commands/items */ } },
				},
                ApplicationItems =
                {
					// application (OS X) or file menu (others)
					new ButtonMenuItem { Text = "&Preferences..." },
                },
                QuitItem = quitCommand,
                AboutItem = aboutCommand
            };


            // create toolbar			
            ToolBar = new ToolBar { Items = { clickMe } };
        }
    }
}
