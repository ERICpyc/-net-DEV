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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Loginbut_Click(object sender, EventArgs e)
        {
            string[] userIDpwd = File.ReadAllLines("login.txt");//readlines from login.txt file
            bool checker = false;//set a bool to check matched username pwd
            string type = "";//use a string to store usertype
            foreach (string item in userIDpwd)
            {
                string[] getnamepwd = item.Split(',');
                if (getnamepwd[0] == textBoxU.Text && getnamepwd[1] ==textBoxP.Text)
                {
                    checker = true;
                    type = getnamepwd[2];
                    break;
                }
            }
            if (checker)
            {
                MessageBox.Show("Login succseefully","login",MessageBoxButtons.OK,MessageBoxIcon.Information);
                Mainsystem form2 = new Mainsystem(type);
                PassValue.passvalue = textBoxU.Text;//pass the username to the passvalue
                form2.Show();
                this.Hide();
            }
            else if (String.IsNullOrEmpty(textBoxU.Text)|| String.IsNullOrEmpty(textBoxP.Text))//check the user input of username and pwd
            {
                MessageBox.Show("The username or password is empty, please re-enter", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else// uncorrect pwd or uname 
            {
                MessageBox.Show("User name or password is incorrect, please re-enter.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void Nwuser_btn_Click(object sender, EventArgs e)
        {
            NewUser newUser = new NewUser();
            newUser.Show();
            this.Hide();
        }

        private void Exitbut_Click(object sender, EventArgs e)
        {
            Application.Exit();//exit
        }

        private void Login_Load(object sender, EventArgs e)
        {
            textBoxU.Focus();
        }
    }
}
