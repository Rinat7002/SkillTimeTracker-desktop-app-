using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkillTimeTracker
{
    public partial class Form1 : Form
    {

        bool stopClicked = false;
        bool startStopClicked = false;

        int skill_hour = 0;
        string skill_name = "";
        private Timer timer;
        private int seconds;
        private int minutes;
        private int hours;
        string valueTimer = "";

        public Form1()
        {
            InitializeComponent();
            InitializeTimer();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            skill_name = textBox1.Text;
            skill_hour = int.Parse(textBox2.Text);
            listBox1.Items.Add(skill_name);
            //listView1.Items.Add(skill_name);

            // Создание нового элемента ListViewItem 
            ListViewItem newItem = new ListViewItem(skill_name);
            newItem.SubItems.Add(skill_hour.ToString() + ":00:00" );
            listViewSkills.Items.Add(newItem);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer1.Enabled) // Проверяем, активен ли таймер
            {
                seconds++;

                if (seconds >= 60)
                {
                    seconds = 0;
                    minutes++;

                    if (minutes >= 60)
                    {
                        minutes = 0;
                        hours++;
                    }
                }

                TimeSpan time = TimeSpan.FromSeconds(seconds);
                labelTimer.Text = time.ToString(@"hh\:mm\:ss");
            }
            //else
            //{
            //    seconds = 0;
            //    minutes = 0;
            //    hours = 0;
            //}
        }

        private void InitializeTimer()
        {
            timer1 = new Timer();
            timer1.Interval = 1000; // Интервал в миллисекундах (1 секунда)
            timer1.Tick += timer1_Tick;
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {

            if (timer1.Enabled) // Если таймер активен (работает)
            {
                timer1.Stop(); // Остановить таймер
                valueTimer = labelTimer.Text;
                listBox1.Items.Add(valueTimer);

            }
            else
            {
                timer1.Start(); // Возобновить таймер
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            valueTimer = labelTimer.Text;

            // превратить в часы 
            string[] parts = valueTimer.Split(':');
            int hourss = int.Parse(parts[0]);
            int minutess = int.Parse(parts[1]);
            int secondss = int.Parse(parts[2]);

            //double hoursDecimal = hours + (double)minutes / 60;

            // у выбранного айтема сложить текущее + что насчитал таймер
            if (listViewSkills.SelectedItems.Count > 0)
            {
                // Получение первого выбранного элемента
                ListViewItem selectedItem = listViewSkills.SelectedItems[0];
                // Доступ к данным выбранного элемента
                //string text = selectedItem.Text; // текст первой колонки
                string timeSkillSelected = selectedItem.SubItems[1].Text;


                DateTime time = DateTime.ParseExact(timeSkillSelected, "h:mm:ss", CultureInfo.InvariantCulture);

                int hoursSelectedSkill = time.Hour;
                int minutesSelectedSkill = time.Minute;
                int secondsSelectedSkill = time.Second;

                int hoursNew = hourss + hoursSelectedSkill;
                int minutesNew = minutess + minutesSelectedSkill;
                int secondsNew = secondss + secondsSelectedSkill;

                string formattedTime = $"{hoursNew}:{minutesNew:D2}:{secondsNew:D2}";

                //string dateNew = hoursNew.ToString() + ":" + minutesNew.ToString() + ":" + secondsNew.ToString();

                //selectedItem.SubItems[1].Text += hourss.ToString() + ":" + minutess.ToString() + ":" + secondss.ToString(); // текст второй колонки (если есть)
                selectedItem.SubItems[1].Text = formattedTime;
            }

            
            timer1.Stop();
            seconds = 0;
            minutes = 0;
            hours = 0;

            // Обновить текст label1
            labelTimer.Text = "00:00:00";

            listBox2.Items.Add(valueTimer);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listViewSkills.SelectedItems.Count > 0)
            {
                // Получение первого выбранного элемента
                ListViewItem selectedItem = listViewSkills.SelectedItems[0];
                // Доступ к данным выбранного элемента
                string text = selectedItem.Text; // текст первой колонки
                string subItemText = selectedItem.SubItems[1].Text; // текст второй колонки (если есть)
                label6.Text = text + " " + subItemText;

            }

        }
    }
}
