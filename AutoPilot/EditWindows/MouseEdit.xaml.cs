using System;
using System.Windows;
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
