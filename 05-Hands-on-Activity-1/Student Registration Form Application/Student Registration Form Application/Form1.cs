using System;
using System.Collections;
using System.Globalization;
using System.Windows.Forms;

namespace Student_Registration_Form_Application
{
    public partial class frmStudentRegistration : Form
    {
        public frmStudentRegistration()
        {
            InitializeComponent();

            // Safer UX: restrict typing on combo boxes (optional but recommended)
            if (comboDay != null) comboDay.DropDownStyle = ComboBoxStyle.DropDownList;
            if (comboMonth != null) comboMonth.DropDownStyle = ComboBoxStyle.DropDownList;
            if (comboYear != null) comboYear.DropDownStyle = ComboBoxStyle.DropDownList;
            if (comboProgram != null) comboProgram.DropDownStyle = ComboBoxStyle.DropDownList;

            // ---- Day: 1..31 (loop) ----
            comboDay.Items.Clear();
            for (int i = 1; i <= 31; i++)
            {
                comboDay.Items.Add(i.ToString());
            }

            // ---- Month: string[] + foreach (per lab spec) ----
            comboMonth.Items.Clear();
            string[] months =
            {
                "January","February","March","April","May","June",
                "July","August","September","October","November","December"
            };
            foreach (var m in months)
            {
                comboMonth.Items.Add(m);
            }

            // ---- Year: 1900..current (loop) ----
            comboYear.Items.Clear();
            int currentYear = DateTime.Now.Year;
            for (int y = 1900; y <= currentYear; y++)
            {
                comboYear.Items.Add(y.ToString());
            }

            // ---- Program: ArrayList + foreach (per lab spec) ----
            comboProgram.Items.Clear();
            ArrayList programs = new ArrayList
            {
                "Bachelor of Science in Computer Science",
                "Bachelor of Science in Information Technology",
                "Bachelor of Science in Information Systems",
                "Bachelor of Science in Computer Engineering"
            };
            foreach (var p in programs)
            {
                comboProgram.Items.Add(p);
            }

            // ---- Sensible defaults ----
            if (comboDay.Items.Count > 0) comboDay.SelectedIndex = 0;
            if (comboMonth.Items.Count > 0) comboMonth.SelectedIndex = 0;
            if (comboYear.Items.Count > 0) comboYear.SelectedItem = currentYear.ToString();
            if (comboProgram.Items.Count > 0) comboProgram.SelectedIndex = 0;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Hooked by designer for txtFname; keep if needed
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // It's unusual to run logic on label click; left intentionally empty
        }

        private void frmStudentRegistration_Load(object sender, EventArgs e)
        {
            // Reserved for future use
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // --- Gather inputs ---
            string firstName = txtFname.Text?.Trim();
            string middleName = txtMname.Text?.Trim();
            string lastName = txtLname.Text?.Trim();

            // --- Validate names: all three required ---
            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(middleName) ||
                string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("Please fill in First, Middle, and Last Name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- Validate gender ---
            string gender = null;
            if (radioMale.Checked) gender = "Male";
            else if (radioFemale.Checked) gender = "Female";
            if (string.IsNullOrEmpty(gender))
            {
                MessageBox.Show("Please select a gender.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- Validate DOB selections ---
            if (string.IsNullOrWhiteSpace(comboDay.Text) ||
                string.IsNullOrWhiteSpace(comboMonth.Text) ||
                string.IsNullOrWhiteSpace(comboYear.Text))
            {
                MessageBox.Show("Please select a complete Date of Birth (Day, Month, Year).", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Convert month name -> month number
            int monthNumber = Array.IndexOf(new[]
            {
                "January","February","March","April","May","June",
                "July","August","September","October","November","December"
            }, comboMonth.Text) + 1;

            if (!int.TryParse(comboDay.Text, out int day) ||
                !int.TryParse(comboYear.Text, out int year) ||
                monthNumber < 1 || monthNumber > 12)
            {
                MessageBox.Show("Invalid date selection.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Build DateTime safely
            if (!DateTime.TryParseExact($"{year}-{monthNumber}-{day}", "yyyy-M-d",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dob))
            {
                MessageBox.Show("The selected Date of Birth is invalid. Please correct it.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dob > DateTime.Today)
            {
                MessageBox.Show("Date of Birth cannot be in the future.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- Validate Program selection ---
            string program = comboProgram.Text?.Trim();
            if (string.IsNullOrWhiteSpace(program))
            {
                MessageBox.Show("Please select a Program.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- Compose result ---
            string result =
                "Student Name: " + firstName + " " + middleName + " " + lastName +
                "\nGender: " + gender +
                "\nDate Of Birth: " + dob.ToString("dd/MM/yyyy") +
                "\nProgram: " + program;

            // --- Show confirmation ---
            MessageBox.Show(result, "Student Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}