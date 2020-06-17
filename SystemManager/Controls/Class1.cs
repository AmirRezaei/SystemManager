using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PluginInterface;

namespace SM.Controls
{
    public static class EntityConverter
    {
        public static ListViewItem FromEntity(this ListViewItem listViewItem, Entity entity)
        {
            listViewItem.Tag = entity;
            listViewItem.Name = entity.Path;
            listViewItem.Text = entity.Name; // Parameter Text = Name is used as key in ListViewItem, we are using Path on each item since it's unique
            listViewItem.ImageIndex = entity.IsDirectory ? 0 : 1; // set correct image based on folder and file
            return listViewItem;
        }
    }
}
