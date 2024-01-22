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