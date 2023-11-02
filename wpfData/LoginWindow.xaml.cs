﻿using System;
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
using System.Windows.Shapes;
using wpfData_Step_4.Model;
using wpfData_Step_4.ViewModel;

namespace wpfData_Step_4
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            UserDB userDB = new UserDB();
            User user = new User();
            user.UserName = tbxID.Text;
            user.Password = tbxPassword.Password;
            user = userDB.login(user);
            if (user == null)
            {
                MessageBox.Show("Dont Exist");
                return;
            } user = userDB.login(user);
            if (!user.IsAdmin)
            {
                MessageBox.Show("Not manager");
                return;
            }
                    
            MainWindow mainWindow = new MainWindow();
            mainWindow.ShowDialog();
            tbxID.Text = tbxPassword.Password = string.Empty;
        }
    }
}
