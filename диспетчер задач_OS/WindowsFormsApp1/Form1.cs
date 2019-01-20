using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent(); 
            ShowProcess();   //вывод процессов
            AddButton();  //добавление колонки с кнопками
        }

        Process[] allProcesses;

        public Process[] AllProcesses
        {
            get => allProcesses;
            set => allProcesses = value;
        }

        public void ShowProcess()         //вывод процессов
        {
            
            AllProcesses = Process.GetProcesses();
            var allProcess = from pr in AllProcesses
                             orderby pr.Id
                             select pr;
            foreach (var proc in allProcess)
            {
                string[] bigtable = { "" + proc.Id, "" + proc.ProcessName,
                    "" + proc.WorkingSet64 /1000000 + " MB", "" + proc.VirtualMemorySize64 / 1000000 + " MB", "" + GetNameUser(proc.Id),
                     "" + proc.BasePriority };

                dataGridView1.Rows.Add(bigtable);

                dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;  //выравнивание заголовков
                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }

            label2.Text = allProcess.Count().ToString(); // количество процессов
        }

      


        public string GetNameUser(int ID)    //пользователь
        {
            string query = "Select * From Win32_Process Where ProcessID = " + ID;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection processList = searcher.Get(); 

            foreach (ManagementObject obj in processList)
            {
                string[] argList = new string[] { string.Empty, string.Empty };
                int returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", argList)); 
                if (returnVal == 0)
                {
                    return argList[0];
                }
            }
            return "User not found";
        }

        private void AddButton()    //колонка с кнопками
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.UseColumnTextForButtonValue = true;
            btn.HeaderText = "Information about Threads";
            btn.Name = "Info";
            btn.Text = "Click";
            dataGridView1.Columns.Add(btn);
        }

        private void Form1_Load(object sender, EventArgs e)

        {
            ShowProcess();
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)   //потоки
        {
            if (e.RowIndex < 0)   //показывает индекс строки для которой происходит событие 
                return;

            Form2 f2 = new Form2(AllProcesses[e.RowIndex]);
            f2.Show();
        }

        private void Button2_Click(object sender, EventArgs e)      //обновить список процессов
        {
            ShowProcess();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
