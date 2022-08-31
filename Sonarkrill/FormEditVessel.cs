using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SonarKrill
{
    public partial class FormEditVessel : Form
    {
        private FormMain fm;
        public FormEditVessel()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            foreach (object obj  in groupBox1.Controls)
            {
                if (obj.GetType() != typeof(TextBox))
                {
                    continue;
                }
                TextBox txt = (TextBox)obj;
                if (txt.Text=="")
                {
                    txt.Focus();
                    MessageBox.Show("信息框中不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                    return;
                }

            }

            DialogResult dr= MessageBox.Show("是否保存修改的渔船信息？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);


            

            if (dr == DialogResult.OK)
            {
                string strVessel = txtName.Text + "," +txtLength .Text + "," + txtWidth.Text + ","
                    + txtPower.Text + "," + txtTons.Text;
                SetConfigValue("vessel", strVessel);
                this.Close();


                //fm = (FormMain)this.Owner;
                //fm.RefreshVesselInfo();
            }
            else return;
            
        }

        private bool SetConfigValue(string key, string value)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (config.AppSettings.Settings[key] != null)
                    config.AppSettings.Settings[key].Value = value;
                else
                    config.AppSettings.Settings.Add(key, value);
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        private string GetConfigValue(string key)
        {
            string strValue;
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            strValue = config.AppSettings.Settings[key].Value.ToString();
            return strValue;


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormEditVessel_Load(object sender, EventArgs e)
        {
            //txtName.Text = GetConfigValue("vessel");
            string str = GetConfigValue("vessel").ToString();
            string[] parts = str.Split(new char[] { ',' });
            txtName.Text = parts[0];
            txtLength.Text= parts[1];
            txtWidth.Text = parts[2];
            txtPower.Text = parts[3];
            txtTons.Text = parts[4];


        }
    }
}
