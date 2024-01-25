using System;
using System.Windows;
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