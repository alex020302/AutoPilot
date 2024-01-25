using System;
using System.Windows;
using AutoPilot.Actions;

namespace EditorTest.EditViews
{
    /// <summary>
    /// Interaction logic for DelayEdit.xaml
    /// </summary>
    public partial class DelayEdit : Window
    {
        public Delay zuBearbeiten { get; private set; }

        public DelayEdit(Delay editThisAktion)
        {
            InitializeComponent();
            zuBearbeiten = editThisAktion;

            timeTextBox.Text = Convert.ToString(editThisAktion.Milliseconds);
            CommentTextBox.Text = editThisAktion.Comment;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            zuBearbeiten.Milliseconds = Convert.ToInt32(timeTextBox.Text);
            zuBearbeiten.Comment = CommentTextBox.Text;
            this.Close();
        }
    }
}
