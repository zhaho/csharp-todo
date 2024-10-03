using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TodoApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                AddNewTodo();
            }
        }

        private void AddNewTodo()
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                CheckBox newTask = new CheckBox
                {
                    Text = textBox1.Text,
                    AutoSize = true,
                    Font = new Font("Arial", 12)
                };

                newTask.CheckedChanged += (s, e) => RemoveTodoItem(newTask);

                flowLayoutPanel1.Controls.Add(newTask);

                textBox1.Clear();
            }
        }

        private void RemoveTodoItem(CheckBox task)
        {
            if (task.Checked)
            {
                flowLayoutPanel1.Controls.Remove(task);
            }
        }

    }
}
