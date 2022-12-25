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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LABA_8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            checkBox1.IsChecked = Properties.Settings.Default.DevelopMode;

            button2.IsEnabled = Properties.Settings.Default.LoadGame;

            slider1.Minimum = 2;
            slider2.Maximum = 10;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
           
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int player_count = (int)slider1.Value;
            Properties.Settings.Default.MoveSpeed = (int)numericUpDown2.Value;
            Properties.Settings.Default.DevelopMode = checkBox1.Checked;

            Properties.Settings.Default.Save();

            GameScence gameScence = new GameScence(player_count);
            gameScence.Show();
            this.Hide();
        }
    }
}

namespace Lab_7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Начальные настройки";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = 2;
            numericUpDown2.Value = 10;

            checkBox1.Checked = Properties.Settings.Default.DevelopMode;

            button2.Enabled = Properties.Settings.Default.LoadGame;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int player_count = (int)numericUpDown1.Value;
            Properties.Settings.Default.MoveSpeed = (int)numericUpDown2.Value;
            Properties.Settings.Default.DevelopMode = checkBox1.Checked;

            Properties.Settings.Default.Save();

            GameScence gameScence = new GameScence(player_count);
            gameScence.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataContractSerializer jsonF = new DataContractSerializer(typeof(SerializeGame));

            using (FileStream fileStream = new FileStream("GameSave.json", FileMode.Open))
            {
                SerializeGame loadgame = (SerializeGame)jsonF.ReadObject(fileStream);
                GameScence gameScence = new GameScence(loadgame);
                gameScence.Show();
                this.Hide();
            }

        }
    }
}