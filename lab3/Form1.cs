using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3
{
    public partial class Form1 : Form
    {
        bool help = true;
        string biNumber;
        List<int> FirstLine;
        List<int> line = new List<int>(new int[20]);
        List<int> otherLine = new List<int>(new int[20]);

        int NumCells = 20;
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
            InitializeTable();
        }

        private void InitializeTable()
        {
            table.RowCount = NumCells;
            table.ColumnCount = NumCells;
            for (int i = 0; i < NumCells; i++)
            {
                table.Columns[i].Width = 20;
                table.Rows[i].Cells[0].Value = i;
            }
        }

        private void InitializeFirstLine()
        {
            FirstLine = new List<int>(new int[NumCells]);
            for (int i = 1; i < FirstLine.Count; i++)
            {
                int k = rnd.Next(0, 2);
                if (k == 1)
                {
                    FirstLine[i] = 1;
                    table.Rows[0].Cells[i].Style.BackColor = Color.LightBlue;
                }
                else
                    FirstLine[i] = 0;
            }
        }

        private void btStart_Click(object sender, EventArgs e)
        {

            if (help)
            {
                table.Refresh();

                biNumber = Convert.ToString((int)edRule.Value, 2).PadLeft(8, '0');

                if (biNumber.Length < 8)
                {
                    int n = 8 - biNumber.Length;
                    for (int i = 0; i < n; i++) biNumber = biNumber.Insert(0, "0");
                }

                InitializeFirstLine();

                for (int i = 0; i < otherLine.Count; i++) otherLine[i] = 0;

                table.Refresh();
                line = FirstLine;
            }

            help = false;

            if (btStart.Text == "Start")
            {
                btStart.Text = "Stop";
                timer1.Start();
            }
            else
            {
                btStart.Text = "Start";
                timer1.Stop();
            }
            table.Refresh();
        }

        public int check(string help)
        {
            switch (help)
            {
                case "111":
                    return biNumber[0] - 48;
                case "110":
                    return biNumber[1] - 48;
                case "101":
                    return biNumber[2] - 48;
                case "100":
                    return biNumber[3] - 48;
                case "011":
                    return biNumber[4] - 48;
                case "010":
                    return biNumber[5] - 48;
                case "001":
                    return biNumber[6] - 48;
                case "000":
                    return biNumber[7] - 48;
                default:
                    return biNumber[0] - 48;
            }
        }

        int iter = 1;
        bool First = true;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (First == true)
            {
                First = false;
            }
            else
            {
                for (int i = 0; i < otherLine.Count; i++)
                {
                    line[i] = otherLine[i];
                }
            }

            for (int i = 0; i < line.Count; i++)
            {
                if (i != 0 && i != line.Count - 1)
                {
                    string Result = "";
                    Result = line[i - 1].ToString() + line[i].ToString() + line[i + 1].ToString();
                    int result = check(Result);
                    otherLine[i] = result;
                    if (otherLine[i] == 1)
                    {
                        table.Rows[iter].Cells[i].Style.BackColor = Color.LightBlue;
                    }
                }
            }

            table.Refresh();
            iter++;

            if (iter == 20)
            {
                timer1.Stop();
            }
            
        }
    }
}
