using System;
using System.Windows.Forms;
using System.Drawing;

namespace StudentManagement
{
    public partial class StudentForm : Form
    {
        private Student _student;
        private bool _isNewStudent;

        public Student Student => _student;

        public StudentForm(Student student = null)
        {
            InitializeComponent();
            _student = student ?? new Student();
            _isNewStudent = student == null;
            InitializeControls();
        }

        private void InitializeComponent()
        {
            this.txtLastName = new TextBox();
            this.txtFirstName = new TextBox();
            this.txtMiddleName = new TextBox();
            this.numCourse = new NumericUpDown();
            this.txtGroup = new TextBox();
            this.dtpDateOfBirth = new DateTimePicker();
            this.txtEmail = new TextBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();

            // Form
            this.Text = "Студент";
            this.Size = new Size(400, 500);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.AutoScaleMode = AutoScaleMode.None;

            // Labels
            var lblLastName = new Label { Text = "Фамилия:", Location = new Point(20, 20), AutoSize = true };
            var lblFirstName = new Label { Text = "Имя:", Location = new Point(20, 70), AutoSize = true };
            var lblMiddleName = new Label { Text = "Отчество:", Location = new Point(20, 120), AutoSize = true };
            var lblCourse = new Label { Text = "Курс:", Location = new Point(20, 170), AutoSize = true };
            var lblGroup = new Label { Text = "Группа:", Location = new Point(20, 220), AutoSize = true };
            var lblDateOfBirth = new Label { Text = "Дата рождения:", Location = new Point(20, 270), AutoSize = true };
            var lblEmail = new Label { Text = "Email:", Location = new Point(20, 320), AutoSize = true };

            // Controls
            txtLastName.Location = new Point(150, 20);
            txtLastName.Size = new Size(200, 25);

            txtFirstName.Location = new Point(150, 70);
            txtFirstName.Size = new Size(200, 25);

            txtMiddleName.Location = new Point(150, 120);
            txtMiddleName.Size = new Size(200, 25);

            numCourse.Location = new Point(150, 170);
            numCourse.Size = new Size(200, 25);
            numCourse.Minimum = 1;
            numCourse.Maximum = 6;

            txtGroup.Location = new Point(150, 220);
            txtGroup.Size = new Size(200, 25);

            dtpDateOfBirth.Location = new Point(150, 270);
            dtpDateOfBirth.Size = new Size(200, 25);
            dtpDateOfBirth.Format = DateTimePickerFormat.Short;
            dtpDateOfBirth.MinDate = new DateTime(1992, 1, 1);
            dtpDateOfBirth.MaxDate = DateTime.Now;

            txtEmail.Location = new Point(150, 320);
            txtEmail.Size = new Size(200, 25);

            // Buttons
            var buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                Padding = new Padding(10)
            };

            btnSave.Text = "Сохранить";
            btnSave.DialogResult = DialogResult.OK;
            btnSave.Size = new Size(100, 30);
            btnSave.Location = new Point(80, 15);
            btnSave.Click += BtnSave_Click;
            btnSave.BackColor = Color.FromArgb(0, 120, 215);
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;

            btnCancel.Text = "Отмена";
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Size = new Size(100, 30);
            btnCancel.Location = new Point(200, 15);
            btnCancel.FlatStyle = FlatStyle.Flat;

            buttonPanel.Controls.AddRange(new Control[] { btnSave, btnCancel });

            // Add all controls
            this.Controls.AddRange(new Control[] {
                lblLastName, txtLastName,
                lblFirstName, txtFirstName,
                lblMiddleName, txtMiddleName,
                lblCourse, numCourse,
                lblGroup, txtGroup,
                lblDateOfBirth, dtpDateOfBirth,
                lblEmail, txtEmail,
                buttonPanel
            });

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }

        private void InitializeControls()
        {
            if (!_isNewStudent)
            {
                txtLastName.Text = _student.LastName;
                txtFirstName.Text = _student.FirstName;
                txtMiddleName.Text = _student.MiddleName;
                numCourse.Value = _student.Course;
                txtGroup.Text = _student.Group;
                dtpDateOfBirth.Value = _student.DateOfBirth;
                txtEmail.Text = _student.Email;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            _student.LastName = txtLastName.Text;
            _student.FirstName = txtFirstName.Text;
            _student.MiddleName = txtMiddleName.Text;
            _student.Course = (int)numCourse.Value;
            _student.Group = txtGroup.Text;
            _student.DateOfBirth = dtpDateOfBirth.Value;
            _student.Email = txtEmail.Text;

            string errorMessage;
            if (!_student.IsValid(out errorMessage))
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
            }
        }

        private TextBox txtLastName;
        private TextBox txtFirstName;
        private TextBox txtMiddleName;
        private NumericUpDown numCourse;
        private TextBox txtGroup;
        private DateTimePicker dtpDateOfBirth;
        private TextBox txtEmail;
        private Button btnSave;
        private Button btnCancel;
    }
} 