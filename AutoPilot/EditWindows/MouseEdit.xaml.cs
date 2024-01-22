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
    /// Interaction logic for MouseEdit.xaml
    /// </summary>
    public partial class MouseEdit : Window
    {
        public MouseClick zuBearbeiten { get; private set; }

        public MouseEdit(MouseClick editThisClick)
        {
            InitializeComponent();
            zuBearbeiten = editThisClick;

            xTextBox.Text = Convert.ToString(editThisClick.X_Coordinate);
            yTextBox.Text = Convert.ToString(editThisClick.Y_Coordinate);
            NumberTextBox.Text = Convert.ToString(editThisClick.NumberOfClicks);
            CommentTextBox.Text = editThisClick.Comment;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            zuBearbeiten.X_Coordinate = Convert.ToInt32(xTextBox.Text);
            zuBearbeiten.Y_Coordinate = Convert.ToInt32(yTextBox.Text);
            zuBearbeiten.NumberOfClicks = Convert.ToInt32(NumberTextBox.Text);
            zuBearbeiten.Comment = CommentTextBox.Text;
            this.Close();
        }
    }
}
