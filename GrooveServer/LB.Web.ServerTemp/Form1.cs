﻿using LB.Web.Remoting;
using LB.Web.ServerTool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Windows.Forms;

namespace LB.Web.ServerTemp
{
    public partial class Form1 : Form
    {
        bool mbCloseFromNotifyIcon = false;
        public Form1()
        {
            InitializeComponent();

            this.notifyIcon1.Text = "入槽软件服务";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
           
            string strRemotingPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LBRemoting.Config");
            IniClass iniClass = new IniClass(strRemotingPath);
            string strPort = iniClass.ReadValue("Remoting", "Port");
            string strServerName = iniClass.ReadValue("Remoting", "ServerName");
            string strServerURL = iniClass.ReadValue("Remoting", "ServerURL");
            string strDBName = iniClass.ReadValue("Remoting", "DBName");
            string strDBServer = iniClass.ReadValue("Remoting", "DBServer");

            bool bolLoginSecure = iniClass.ReadValue("Remoting", "LoginSecure") == "1" ? true : false;
            string strDBUser = iniClass.ReadValue("Remoting", "DBUser");
            string strDBPW = iniClass.ReadValue("Remoting", "DBPW");
            //WriteLog("读取数据库名称："+ strDBName+" 地址："+ strAddress);
            int mPort;
            int.TryParse(strPort, out mPort);
            strServerName = "LRB";
            HttpChannel channel = new HttpChannel(mPort);
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(
             typeof(WebRemoting), strServerName, WellKnownObjectMode.Singleton);

            
            WebRemoting.SetRemotingInfo(strDBName, strDBServer, strDBUser);
            WebRemoting.LoadAllBLLFunction();
        }

        private void StartServer()
        {
            int mPort;
            //string strRemotingPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LBRemoting.Config");
            //IniClass iniClass = new IniClass(strRemotingPath);
            //string strPort = iniClass.ReadValue("Remoting", "Port");
            //string strAddress = iniClass.ReadValue("Remoting", "ServerName");
            //string strDBName = iniClass.ReadValue("Remoting", "DBName");

            //int.TryParse(strPort, out mPort);

            //ChannelServices.RegisterChannel(new HttpChannel(2017), false);
            ////ChannelServices.RegisterChannel(new TcpServerChannel(mPort), false);
            //RemotingConfiguration.RegisterWellKnownServiceType(
            //typeof(LB.Web.Remoting.WebRemoting), "LBProject", WellKnownObjectMode.Singleton);
            
            //WebRemoting.RemotingSendedEvent += WebRemoting_RemotingSendedEvent;


            //HttpChannel channel = new HttpChannel(2017);
            //ChannelServices.RegisterChannel(channel, false);
            //RemotingConfiguration.RegisterWellKnownServiceType(
            // typeof(WebRemoting), "LBProject", WellKnownObjectMode.Singleton);
            //WebRemoting.FaxSendedEvent += WebRemoting_FaxSendedEvent;
            //WebRemoting.RemotingSendedEvent += WebRemoting_RemotingSendedEvent;
            //WebRemoting.SetRemotingInfo(strDBName, strAddress);
            //WebRemoting.LoadAllBLLFunction();
        }
        
        private void itemExit_Click(object sender, EventArgs e)
        {
            try
            {
                mbCloseFromNotifyIcon = true;
                this.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void itemOpenForm_Click(object sender, EventArgs e)
        {
            try
            {
                this.Show();
                this.ShowInTaskbar = true;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                HardwareInfo hd = new HardwareInfo();
                string strHard = hd.GetHardDiskID();
                string strMac = hd.GetMacAddress();


                base.OnFormClosing(e);
                if (!mbCloseFromNotifyIcon && e.CloseReason == CloseReason.UserClosing)
                {
                    this.ShowInTaskbar = false;
                    this.Hide();
                    e.Cancel = true;
                }
                else if(MessageBox.Show("是否确认关闭软件服务器，关闭后入槽软件将无法使用？","提示", MessageBoxButtons.YesNo)!= DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    this.Show();
                    this.ShowInTaskbar = true;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void notifyIcon1_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
