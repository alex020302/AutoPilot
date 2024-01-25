using System;
using System.Windows;
using AutoPilot.Actions;

namespace EditorTest.EditViews
{
    /// <summary>
    /// Interaction logic for DelayEdit.xaml
    /// </summary>
    public partial class TextEmulationEdit : Window
    {
        public TextEmulation zuBearbeiten { get; private set; }

        public TextEmulationEdit (TextEmulation editThisAktion)
        {
            InitializeComponent();
            zuBearbeiten = editThisAktion;

            textTextBox.Text = Convert.ToString(editThisAktion.Text);
            CommentTextBox.Text = editThisAktion.Comment;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            zuBearbeiten.Text = Convert.ToString(textTextBox.Text);
            zuBearbeiten.Comment = CommentTextBox.Text;
            this.Close();
        }
    }
}