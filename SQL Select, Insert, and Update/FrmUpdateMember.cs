using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQL_Select__Insert__and_Update
{
    public partial class FrmUpdateMember : Form
    {
        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlAdapter;
        private SqlDataReader sqlReader;

        public FrmUpdateMember()
        {
            sqlConnect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\ultra\\Documents\\Visual Studio 2022\\Event Driven Program_ACT\\SQL Select, Insert, and Update\\SQL Select, Insert, and Update\\ClubDB.mdf\";Integrated Security=True;Connect Timeout=30");
            InitializeComponent();
        }

        private void FrmUpdateMember_Load(object sender, EventArgs e)
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
            ListOfStudentID();
        }
        public void ListOfStudentID()
        {
            sqlConnect.Open();
            sqlCommand = new SqlCommand("SELECT StudentID FROM ClubMembers", sqlConnect);
            sqlReader = sqlCommand.ExecuteReader();

            while (sqlReader.Read())
            {
                cbStudentID.Items.Add(sqlReader["StudentID"]);
            }

            sqlReader.Close();
            sqlConnect.Close();
        }

        private void cbStudentID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedStudentID = cbStudentID.SelectedItem.ToString();

            sqlConnect.Open();
            sqlCommand = new SqlCommand("SELECT * FROM ClubMembers WHERE StudentID = @StudentID", sqlConnect);
            sqlCommand.Parameters.AddWithValue("@StudentID", selectedStudentID);

            sqlReader = sqlCommand.ExecuteReader();

            if (sqlReader.Read())
            {
                tbLastname.Text = sqlReader["LastName"].ToString();
                tbFirstName.Text = sqlReader["FirstName"].ToString();
                tbMiddlename.Text = sqlReader["MiddleName"].ToString();
                tbAge.Text = sqlReader["Age"].ToString();
                cbGender.Text = sqlReader["Gender"].ToString();
                cbProgram.Text = sqlReader["Program"].ToString();
            }

            sqlReader.Close();
            sqlConnect.Close();
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string selectedStudentID = cbStudentID.SelectedItem.ToString();
            string lastName = tbLastname.Text;
            string firstName = tbFirstName.Text;
            string middleName = tbMiddlename.Text;
            int age = int.Parse(tbAge.Text);
            string gender = cbGender.Text;
            string program = cbProgram.Text;

            sqlConnect.Open();
            sqlCommand = new SqlCommand("UPDATE ClubMembers SET LastName = @LastName, FirstName = @FirstName, MiddleName = @MiddleName, Age = @Age, Gender = @Gender, Program = @Program WHERE StudentID = @StudentID", sqlConnect);

            sqlCommand.Parameters.AddWithValue("@StudentID", selectedStudentID);
            sqlCommand.Parameters.AddWithValue("@LastName", lastName);
            sqlCommand.Parameters.AddWithValue("@FirstName", firstName);
            sqlCommand.Parameters.AddWithValue("@MiddleName", middleName);
            sqlCommand.Parameters.AddWithValue("@Age", age);
            sqlCommand.Parameters.AddWithValue("@Gender", gender);
            sqlCommand.Parameters.AddWithValue("@Program", program);

            int rowsUpdate = sqlCommand.ExecuteNonQuery();
            if (rowsUpdate > 0)
            {
                MessageBox.Show("Update successful!");
            }
            else
            {
                MessageBox.Show("Update failed!");
            }

            sqlConnect.Close();
        }

    }
}
