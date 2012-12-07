using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace MovieOrganizer
{
    partial class WatchItem : Control
    {
        Movie theMovie;
        public WatchItem(Movie sentMovie)
        {
            FlowLayoutPanel recItemHolder = new FlowLayoutPanel();
            Button recItemAdd = new Button();
            recItemAdd.MouseClick +=new MouseEventHandler(recItemAdd_MouseClick);
            recItemAdd.FlatStyle = FlatStyle.Flat;
            recItemAdd.AutoSize = true;
            if (false) //in watchlist
            {
                recItemAdd.Enabled = false;
                recItemAdd.Text = "Already in watchlist";
            }
            else
            {
                recItemAdd.Text = "Add to Watchlist";
            }
            recItemHolder.FlowDirection = FlowDirection.TopDown;
            recItemHolder.Controls.Add(sentMovie.buildThumbnailPanel());
            recItemHolder.Controls.Add(recItemAdd);
            recItemHolder.AutoSize = true;
            recItemAdd.Margin = new Padding(30, 0, 0, 0);
        }

        public void recItemAdd_MouseClick(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            if (true)//(((Button)sender).Text.Equals(""))//If we want to add it
            {
                DBConnect adder = new DBConnect();
                adder.addToWatchList(theMovie.getMID(), HomeForm.UID);
                btn.Text = "Remove from Watchlist";

            }
            else
            {
                DBConnect adder = new DBConnect();
                //adder.addToWatchList(currMovie.getMID(), UID);//Remove from watchlist
                //btn.Text = "Add to Watchlist";

            }
        }
    }
}
