using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TodoApp
{
    public partial class Form1 : Form
    {
        private string todoDirectoryPath;
        private string todoFilePath;

        public Form1()
        {
            InitializeComponent();
            todoDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Tasks");
            todoFilePath = Path.Combine(todoDirectoryPath, "todos.txt");

            Directory.CreateDirectory(todoDirectoryPath);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
            LoadTodos();
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
                SaveTodoToFile(textBox1.Text);
                textBox1.Clear();
            }
        }

        private void RemoveTodoItem(CheckBox task)
        {
            if (task.Checked)
            {
                flowLayoutPanel1.Controls.Remove(task);
                RemoveTodoFromFile(task.Text);
            }
        }

        private void LoadTodos()
        {
            if (File.Exists(todoFilePath))
            {
                string[] todos = File.ReadAllLines(todoFilePath);
                foreach (string todo in todos)
                {
                    CheckBox newTask = new CheckBox
                    {
                        Text = todo,
                        AutoSize = true,
                        Font = new Font("Arial", 12)
                    };
                    newTask.CheckedChanged += (s, e) => RemoveTodoItem(newTask);
                    flowLayoutPanel1.Controls.Add(newTask);
                }
            }
        }

        private void SaveTodoToFile(string todo)
        {
            using (StreamWriter writer = new StreamWriter(todoFilePath, true))
            {
                writer.WriteLine(todo);
            }
        }

        private void RemoveTodoFromFile(string todo)
        {
            if (File.Exists(todoFilePath))
            {
                var lines = File.ReadAllLines(todoFilePath).ToList();
                lines.Remove(todo);
                File.WriteAllLines(todoFilePath, lines);
            }
        }

    }
}
