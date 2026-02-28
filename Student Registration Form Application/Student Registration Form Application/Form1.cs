using System;
using System.Windows.Forms;

namespace Student_Registration_Form_Application
{
    public partial class frmStudentRegistration : Form
    {
        public frmStudentRegistration()
        {
            InitializeComponent();

            for (int i = 1; i <= 31; i++)
            {
                comboDay.Items.Add(i.ToString());
            }

            for (int i = 1; i <= 12; i++)
            {
                comboMonth.Items.Add(i.ToString());
            }

            int currentYear = DateTime.Now.Year;

            for (int i = 1900; i <= currentYear; i++)
            {
                comboYear.Items.Add(i.ToString());
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            String firstName = txtFname.Text;
            String lastName = txtLname.Text;
            String middleName = txtMname.Text;
            String gender = "";
            String dateOfBirth = comboDay.Text + "/" + comboMonth.Text + "/" + comboYear.Text;

            if (String.IsNullOrEmpty(firstName) && String.IsNullOrEmpty(lastName) && String.IsNullOrEmpty(middleName))
            {
                MessageBox.Show("Fill the Blanks", "Errors");
                return;
            }
            else
            {
                if (radioMale.Checked == true)
                {
                    gender = "Male";
                }
                else if (radioFemale.Checked == true)
                {
                    gender = "Female";
                }

                String result = "Student Name: " + firstName + " " + middleName + " " + lastName +
                "\nGender: " + gender +
                "\nDate Of Birth: " + dateOfBirth;
                MessageBox.Show(result, "Student Info");

            }
        }

        private void frmStudentRegistration_Load(object sender, EventArgs e)
        {

        }
    }
}

