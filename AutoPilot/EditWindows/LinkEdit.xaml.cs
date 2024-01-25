using AutoPilot.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AutoPilot.EditWindows
{
    /// <summary>
    /// Interaction logic for LinkEdit.xaml
    /// </summary>
    public partial class LinkEdit : Window
    {
        public Link zuBearbeiten { get; private set; }

        public LinkEdit(Link editThisAktion)
        {
            InitializeComponent();
            zuBearbeiten = editThisAktion;

            urlTextBox.Text = Convert.ToString(editThisAktion.Url);
            CommentTextBox.Text = editThisAktion.Comment;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            zuBearbeiten.Url = urlTextBox.Text;
            zuBearbeiten.Comment = CommentTextBox.Text;
            this.Close();
        }
    }
}
