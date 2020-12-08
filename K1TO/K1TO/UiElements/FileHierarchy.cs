using System;
using System.Collections.Generic;
using System.Text;
using Eto.Drawing;
using Eto.Forms;

namespace K1TO.UiElements
{
    class FileHierarchy : TreeGridView
    {
        public FileHierarchy()
        {
            this.Size = new Size(-1, 500);
            this.Columns.Add(new GridColumn { DataCell = new TextBoxCell(0) });

            TreeGridItemCollection _collection = new TreeGridItemCollection();
            TreeGridItem child = new TreeGridItem("Select a file to start");
            _collection.Add(child);
            this.DataStore = _collection;
        }

    }
}
