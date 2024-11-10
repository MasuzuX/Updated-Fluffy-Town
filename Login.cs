using Fluffy_Town.UserControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fluffy_Town
{
    public partial class Login : Form
    {
        private ArrayList users = new ArrayList();

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );
        public Login()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';       

            //curve pare
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 50, 50));
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (createAccount.Text.Equals("Log in"))
            {
                users.Add(new string[] { username, password });
                MessageBox.Show("Registration successful!");
                txtUsername.Clear();
                txtPassword.Clear();
            }
            else
            {
                string defaultUsername = "Kim";
                string defaultPassword = "123";

                bool loginSuccess = (username.Equals(defaultUsername) && password.Equals(defaultPassword));               
                //register list
                foreach (var user in users)
                {
                    string[] credentials = (string[])user;
                    if (username.Equals(credentials[0]) && password.Equals(credentials[1]))
                    {
                        loginSuccess = true;
                        break;
                    }
                }
                if (loginSuccess)
                {
                    new Loading().Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrect username or password.");
                    txtUsername.Clear();
                    txtPassword.Clear();
                }
            }
        button1.Focus();
        }

        bool isPasswordVisible = false;
        private void viewpassword_Click(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;

            if (isPasswordVisible)
            {
                txtPassword.PasswordChar = '\0';
                viewpassword.BackgroundImage = Properties.Resources.passwordOpeneyes_removebg_preview;
                viewpassword.Size = new Size(32,32);
                viewpassword.Location = new Point(521, 634);
            }
            else
            {
                txtPassword.PasswordChar = '*';
                viewpassword.BackgroundImage = Properties.Resources.passwordClosedeyes_removebg_preview;
                viewpassword.Size = new Size(48, 39);
                viewpassword.Location = new Point(513, 632);
            }
            viewpassword.Focus();
        }

        bool clickedCreateAccount = false;
        private void createAccount_Click(object sender, EventArgs e)
        {
            clickedCreateAccount = !clickedCreateAccount;
            if (clickedCreateAccount)
            {
                Sign_up signup = new Sign_up();
                addUserControl(signup);

                createAccount.Text = "Log in";
            }
            else
            {
                panel2.Controls.Clear();
                createAccount.Text = "Sign up";
            }        
        }
        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panel2.Controls.Clear();
            panel2.Controls.Add(userControl);
            userControl.BringToFront();
        }
    }
}
