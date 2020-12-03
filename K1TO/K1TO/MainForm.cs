using Eto.Drawing;
using Eto.Forms;
using K1TO.Interfaces;
using K1TO.UiElements;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace K1TO
{
    public partial class MainForm : Form
    {
        ProgramState currentState = new ProgramState();
        FileHierarchy treeView = new FileHierarchy();
        FileInformation fileInformation = new FileInformation();
        private void SelectFile(ProgramState currentState)
        {
            // Create Folder Dialog
            OpenFileDialog folderDialog = new OpenFileDialog();
            
            // Set supported extensions (Manual for now)
            string[] extensions = new string[] { ".zip" };
            var zipExtension = new FileFilter("Zip", extensions);
            folderDialog.Filters.Add(zipExtension);

            // Show dialog and set state
            folderDialog.ShowDialog(this);
            currentState.currentlySelectedFile = folderDialog.FileName;
            this.updateTree();

        }
        private void updateTree()
        {
            fileInformation.getFileInformation(currentState.currentlySelectedFile);
            var item = new TreeGridItem(Path.GetFileName(currentState.currentlySelectedFile));
            var collection = new TreeGridItemCollection();
            collection.Add(item);
            this.treeView.DataStore = collection;
        }

        Control TableLayout()
        {
            var table = new TableLayout();
            table.Spacing = new Size(5, 5); // Set space between each cell
            table.Padding = new Padding(10, 10, 10, 10); // Set space around table's sides

            table.Rows.Add(TopRow());

            return table;
        }
        private TableRow TopRow()
        {
            return new TableRow(treeView);
        }
        
        MenuBar MainMenu()
        {
            var mainMenu = new MenuBar();
            
            // Create Commands
            Command openFile = new Command { MenuText = "Open File" };
            openFile.Executed += (sender, e) => SelectFile(currentState);

            Command quitCommand = new Command { MenuText = "Quit", Shortcut = Application.Instance.CommonModifier | Keys.Q };
            quitCommand.Executed += (sender, e) => Application.Instance.Quit();

            // Add commands and items
            mainMenu.Items.Add(new ButtonMenuItem { Text = "&File", Items = { openFile } });
            mainMenu.QuitItem = quitCommand;

            return mainMenu;
        }

        public MainForm()
        {
            Title = "K1TO";
            ClientSize = new Size(500, 500);
            
            Menu = MainMenu();

            Content = TableLayout();
        }
    }
}
