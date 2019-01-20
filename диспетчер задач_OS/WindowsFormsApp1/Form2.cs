using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2(Process process)
        {
            InitializeComponent();
            SmallTable(process);
        }

        public void SmallTable(Process computerProcess)
        {

            foreach (ProcessThread thr in computerProcess.Threads)
            {
                string[] info = new string[2];
                info[0] = thr.Id.ToString();
                info[1] = thr.BasePriority.ToString();

                dataGridView2.Rows.Add(info);

            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;    //выравнивание заголовков
            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }


            label1.Text =computerProcess.Threads.Count.ToString();   //количество потоков
        }


        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
