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
            TreeGridItem child = new TreeGridItem("Balloon");
            List<TreeGridItem> children = new List<TreeGridItem>();
            children.Add(child);
            TreeGridItem topLevelItem = new TreeGridItem(children, "Crikey2");
            _collection.Add(topLevelItem);
            this.DataStore = _collection;
        }

    }
}
