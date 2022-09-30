using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Security.Cryptography;
using System.IO.Compression;
using Ionic.Zip;

namespace SJCommonLib
{
    /// <summary>
    /// 登入結束使用UserControl.GetAuthorityLevel() 獲取權限等級 , 在這之前將Control Modifier設為Public
    /// </summary>
    public partial class SJLogIn : UserControl
    {
        private List<TAccount> AccountList { get; set; } = new List<TAccount>();
        /// <summary>
        /// 權限等級
        /// </summary>
        public int AuthorityLevel { get; set; } = 0;
        /// <summary>
        /// Constructor
        /// </summary>
        public SJLogIn()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 取得權限等級
        /// </summary>
        /// <returns></returns>
        public  int GetAuthorityLevel()
        {
            return AuthorityLevel;
        }
       

        private void chkAccountManager_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAccountManager.Checked)
            {
                chkAccountManager.Text = "儲存並返回";
                dgvAccountList.Visible = true;
            }
            else
            {
                chkAccountManager.Text = "帳號管理";
                dgvAccountList.Visible = false;
                Save();
                m_lblStatus.Text = "";
                chkAccountManager.Enabled = false;
            }
        }

        private void m_btnLogin_Click(object sender, EventArgs e)
        {
            bool IsPass = false; ;
            for (int i = 0; i < AccountList.Count; i++)
            {
                if (AccountList[i].Account == null) AccountList[i].Account = string.Empty;
                if (AccountList[i].PassWord == null) AccountList[i].PassWord = string.Empty;
                if (txtAccount.Text == AccountList[i].Account)
                {
                    if (txtPassWord.Text == AccountList[i].PassWord)
                    {
                        AuthorityLevel = AccountList[i].Level;
                        IsPass = true;
                    }
                }
            }
            if (!IsPass)
            {
                m_lblStatus.Text = "錯誤的帳號密碼";
                return;
            }
            if (AuthorityLevel == 3)
            {
                chkAccountManager.Enabled = true;
                m_lblStatus.Text = "管理帳號權限";
            }
            else
            {
                chkAccountManager.Enabled = false;
                ((Form)Parent).Close();
            }

        }

        private void m_btnCancel_Click(object sender, EventArgs e)
        {
            ((Form)(this.Parent)).Close();
        }

        private void m_btnLogOut_Click(object sender, EventArgs e)
        {
            AuthorityLevel = 0;
            ((Form)(this.Parent)).Close();
        }
        /// <summary>
        /// 載入設定檔
        /// </summary>
        /// <returns></returns>
        public static List<TAccount> LoadAccountList()
        {
            List<TAccount> la = new List<TAccount>();
            XmlSerializer mySerializer = new XmlSerializer(typeof(List<TAccount>));
            try
            {
                if (File.Exists(Environment.CurrentDirectory + "\\Account.md"))
                {
                    using (var zip = new ZipFile(Environment.CurrentDirectory + "\\Account.md"))
                    {
                        zip.Password = "SJ";
                        zip.ExtractAll(Environment.CurrentDirectory, ExtractExistingFileAction.OverwriteSilently);

                    }
                }
                if (File.Exists(Environment.CurrentDirectory + "\\Account.xml"))
                {
                    using (FileStream myFileStream = new FileStream(Environment.CurrentDirectory + "\\Account.xml", FileMode.Open))
                    {
                        la = (List<TAccount>)mySerializer.Deserialize(myFileStream);
                    }
                    using (var zip = new ZipFile())
                    {
                        zip.Password = "SJ";
                        zip.AddFile("Account.xml");
                        zip.Save(Environment.CurrentDirectory + "\\Account.md");
                        File.Delete(Environment.CurrentDirectory + "\\Account.xml");
                    }
                }
            }
            catch(Exception e)
            {
                return new List<TAccount>();
            }
            return la;
        }
        /// <summary>
        /// 儲存設定檔
        /// </summary>
        public void Save()
        {
             XmlSerializer ser = new XmlSerializer(typeof(List<TAccount>));
            using (TextWriter writer = new StreamWriter(Environment.CurrentDirectory + "\\Account.xml"))
            {
                ser.Serialize(writer, AccountList);
            }
           
            using(var zip = new ZipFile())
            {
                zip.Password = "SJ";
                zip.AddFile("Account.xml");
                zip.Save(Environment.CurrentDirectory + "\\Account.md");
                File.Delete(Environment.CurrentDirectory + "\\Account.xml");
            }
        }
        private void SJLogIn_Load(object sender, EventArgs e)
        {
            chkAccountManager.Enabled = false;
            dgvAccountList.Visible = false;
            AccountList = LoadAccountList();
            if (AccountList.Count == 0)
            {
                AccountList.Add(new TAccount("Admin", "0000", 3));
                AccountList.Add(new TAccount("", "0000", 2));
            }
            bsAccount.DataSource = AccountList;
            bsAccount.ResetBindings(false);
        }

        private void dgvAccountList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.Value != null)
            {
                e.Value = new String('*', e.Value.ToString().Length);
            }
        }

        private void dgvAccountList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvAccountList.CurrentCell.ColumnIndex == 1)
            {
                TextBox textBox = e.Control as TextBox;
                if (textBox != null)
                {
                    //textBox.UseSystemPasswordChar = true;
                    byte[] Star = new byte[1];
                    Star[0] = 0x2A;
                    char[] pwdChar = Encoding.ASCII.GetChars(Star);
                    textBox.PasswordChar = pwdChar[0];
                }
            }
            else
            {
                TextBox textBox = e.Control as TextBox;
                if (textBox != null)
                {
                    textBox.UseSystemPasswordChar = false;
                }
            }
            //var txtBox = e.Control as TextBox;
            //txtBox.KeyDown -= new KeyEventHandler(underlyingTextBox_KeyDown);
            //txtBox.KeyDown += new KeyEventHandler(underlyingTextBox_KeyDown);
        }

        private void 刪除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountList.RemoveAt(dgvAccountList.CurrentRow.Index);
            bsAccount.ResetBindings(false);
        }

        private void dgvAccountList_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dgvAccountList.CurrentCell.ColumnIndex == 1)
            {
                dgvAccountList.CurrentCell.Value = string.Empty;
            }
        }
    }
    /// <summary>
    /// TAccount
    /// </summary>
    [Serializable]
    public class TAccount
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="passWord"></param>
        /// <param name="level"></param>
        public TAccount(string account, string passWord, int level)
        {
            Account = account;
            PassWord = passWord;
            Level = level;
            
        }
        /// <summary>
        /// 
        /// </summary>
        public TAccount()
        {

        }
        /// <summary>
        /// md5加密
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        static public string Encode(string Value)
        {
            if ("" == Value) return "";

            var md5 = new MD5CryptoServiceProvider();
            Value = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(Value)), 0, Value.Length);

            return Value;
        }
        /// <summary>
        /// Account
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// PassWordByte
        /// </summary>
        public byte[] PassWordByte { get; set; }
        [XmlIgnore]
        private string _PassWord = string.Empty;
        [XmlIgnore]
        public string PassWord
        {
            get
            {
                if (PassWordByte != null)
                    _PassWord = Encoding.ASCII.GetString(PassWordByte);
                return _PassWord;
            }
            set
            {
                _PassWord = value;
                if(_PassWord != null)
                PassWordByte = Encoding.ASCII.GetBytes(_PassWord);
            }
        }
        public int Level { get; set; } = 0;
    }
}
