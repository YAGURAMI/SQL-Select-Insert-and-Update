using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQL_Select__Insert__and_Update
{
    public partial class FrmClubRegistration : Form
    {
        private ClubRegistrationQuery clubRegistrationQuery;
        private int ID, Age, count;
        private string FirstName, MiddleName, LastName, Gender, Program;
        private long StudentId;
        public FrmClubRegistration()
        {
            InitializeComponent();
        }
        public void RefreshListOfClubMembers()
        {
            clubRegistrationQuery.DisplayList();
            dataGridView1.DataSource = clubRegistrationQuery.bindingSource;
        }
        private void FrmClubRegistration_Load(object sender, EventArgs e)
        {
            ArrayList list_of_Programs = new ArrayList();
            list_of_Programs.Add("BS in Information Technology");
            list_of_Programs.Add("BS in Computer Science");
            list_of_Programs.Add("BS in Business Administration");
            list_of_Programs.Add("BS in Hospitality Management");
            foreach (string program in list_of_Programs)
            {
                cbProgram.Items.Add(program);
            }

            ArrayList list_of_Gender = new ArrayList();
            list_of_Gender.Add("Male");
            list_of_Gender.Add("Female");
            foreach (string gender in list_of_Gender)
            {
                cbGender.Items.Add(gender);
            }
            clubRegistrationQuery = new ClubRegistrationQuery();
            RefreshListOfClubMembers();
        }
        public int RegistrationID()
        {
            return +count;
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            StudentId = long.Parse(tbStudentID.Text);
            FirstName = tbFirstName.Text;
            MiddleName = tbMiddlename.Text;
            LastName = tbLastname.Text;
            Age = int.Parse(tbAge.Text);
            Gender = cbGender.Text;
            Program = cbProgram.Text;

            ID = RegistrationID();
            clubRegistrationQuery.RegisterStudent(ID, StudentId, FirstName, MiddleName, LastName, Age, Gender, Program);
            clubRegistrationQuery.DisplayList();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshListOfClubMembers();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FrmUpdateMember frmUpdate = new FrmUpdateMember();
            frmUpdate.ShowDialog();
        }
    }
}
