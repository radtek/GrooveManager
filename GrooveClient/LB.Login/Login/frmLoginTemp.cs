﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LB.Controls;
using LB.WinFunction;
using System.Threading;

namespace LB.Login
{
    public partial class frmLoginTemp : LBForm
    {
        public bool IsLogin = false;
        public frmLoginTemp()
        {
            InitializeComponent();

            this.txtLoginName.DisplayMember = "LoginName";
            LoginInfo.IsVerifySuccess = false;
            LoginInfo.LoginName = "";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void btnLoginIn_Click(object sender, EventArgs e)
        {
            try
            {
                VerisyIsEmpty();

                string strLoginName = this.txtLoginName.Text.TrimEnd();
                string strPassword = this.txtPassword.Text.TrimEnd();
                bool bolPass = LoginInfo.VerifyLogin(strLoginName, strPassword);
                
                if (bolPass)
                {
                    IsLogin = true;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        /// <summary>
        /// 校验用户名称和密码是否为空
        /// </summary>
        private void VerisyIsEmpty()
        {
            if (this.txtLoginName.Text.TrimEnd() == "")
            {
                throw new Exception("用户名称不能为空！");
            }

            if (this.txtPassword.Text.TrimEnd() == "")
            {
                throw new Exception("用户密码不能为空！");
            }

        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.btnLoginIn.PerformClick();
                }
            }
            catch (Exception ex)
            {
                LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
    }
}
