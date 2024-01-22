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
using AutoPilot.Actions;

namespace EditorTest.EditViews
{
    /// <summary>
    /// Interaction logic for DelayEdit.xaml
    /// </summary>
    public partial class DataInputEdit : Window
    {
        public DataInput zuBearbeiten { get; private set; }

        public DataInputEdit(DataInput editThisAktion)
        {
            InitializeComponent();
            zuBearbeiten = editThisAktion;

            ColumnTextBox.Text = Convert.ToString(editThisAktion.Column);
            CommentTextBox.Text = editThisAktion.Comment;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            zuBearbeiten.Column = Convert.ToInt32(ColumnTextBox.Text);
            zuBearbeiten.Comment = CommentTextBox.Text;
            this.Close();
        }
    }
}