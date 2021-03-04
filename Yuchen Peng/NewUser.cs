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

namespace Yuchen_Peng_13147715_Assignment2
{
    
    public partial class NewUser : Form
    {
        
        public NewUser()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //close button 
            this.Close();
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //submit button
            //store all these user input to string
            string username = textBox1.Text.Trim();
            string pwd = textBox2.Text.Trim();
            string repwd = textBox3.Text.Trim();
            string fname = textBox4.Text.Trim();
            string lname = textBox5.Text.Trim();
            string birth = dateTimePicker1.Text;
            string usertype = comboBox1.Text.Trim();
            bool checker = false;//set a bool as a flag to check whether exist username
            string[] userIDpwd = File.ReadAllLines("login.txt");//readlines from login file
            foreach (string item in userIDpwd)
            {
                string[] getnamepwd = item.Split(',');
                if (getnamepwd[0] == username)//if username exists
                {
                    checker = true;//change the bool to true
                    break;
                }
            }
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(pwd) || String.IsNullOrEmpty(repwd) || String.IsNullOrEmpty(fname) || String.IsNullOrEmpty(lname) || String.IsNullOrEmpty(usertype))
            {
                MessageBox.Show("Registration information is required, please do not leave empty entry!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);//empty condition
            }
            else if (fname.Length > 15 || !fname.All(char.IsLetter))
            {
                MessageBox.Show("Invalid first name format, please re-enter", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);//first name should under 15 and all letter
                fname = "";
                textBox3.Focus();
                
            }
            else if (lname.Length > 15 || !lname.All(char.IsLetter))
            {
                MessageBox.Show("Invalid last name format, please re-enter", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);//last name should under 15 and all letter
                lname = "";
                textBox4.Focus();
            }
            else if (pwd != repwd)//check for re-enter password
            {
                MessageBox.Show("The two password entries are inconsistent, please re-enter", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (checker)//if have same username
            {
                MessageBox.Show("Username exists, please enter a new one.", "Existed Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
            }
            else//All information compliance 
            {
                DialogResult result = MessageBox.Show("Is the information correct?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    StreamWriter sw = new StreamWriter("login.txt",true);//write a new user login info to login.txt
                    sw.WriteLine(username + ',' + pwd + ',' + usertype + ',' + fname + ',' + lname + ',' + birth);//write to file login txt in this format
                    sw.Close();
                    
                    MessageBox.Show("New User Created!", "Created successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    Login login = new Login();
                    login.Show();//go back to login window
                    this.Hide();

                }
                
            }
            
        }
       
        private void NewUser_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
        }
    }
}
