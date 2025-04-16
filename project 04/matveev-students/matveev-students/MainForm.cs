using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.ComponentModel;

namespace StudentManagement
{
    public partial class MainForm : Form
    {
        private DataManager _dataManager;
        private BindingSource _bindingSource;
        private DataGridView dgvStudents;
        private TextBox txtSearchLastName;
        private ComboBox cmbFilterCourse;
        private TextBox txtFilterGroup;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnImport;
        private Button btnExport;
        private Button btnSave;

        public MainForm()
        {
            InitializeComponent();
            _dataManager = new DataManager("students.json");
            _bindingSource = new BindingSource();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = "Управление студентами";
            this.Size = new Size(1000, 600);
            this.MinimumSize = new Size(800, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormClosing += MainForm_FormClosing;

            // Filter controls
            var filterPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                Padding = new Padding(10)
            };

            var lblSearchLastName = new Label
            {
                Text = "Поиск по фамилии:",
                Location = new Point(10, 20),
                AutoSize = true
            };

            txtSearchLastName = new TextBox
            {
                Location = new Point(120, 17),
                Width = 150
            };
            txtSearchLastName.TextChanged += FilterStudents;

            var lblFilterCourse = new Label
            {
                Text = "Курс:",
                Location = new Point(280, 20),
                AutoSize = true
            };

            cmbFilterCourse = new ComboBox
            {
                Location = new Point(320, 17),
                Width = 60,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbFilterCourse.Items.AddRange(new object[] { "", "1", "2", "3", "4", "5", "6" });
            cmbFilterCourse.SelectedIndexChanged += FilterStudents;

            var lblFilterGroup = new Label
            {
                Text = "Группа:",
                Location = new Point(390, 20),
                AutoSize = true
            };

            txtFilterGroup = new TextBox
            {
                Location = new Point(440, 17),
                Width = 100
            };
            txtFilterGroup.TextChanged += FilterStudents;

            filterPanel.Controls.AddRange(new Control[] {
                lblSearchLastName, txtSearchLastName,
                lblFilterCourse, cmbFilterCourse,
                lblFilterGroup, txtFilterGroup
            });

            // Buttons panel
            var buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                Padding = new Padding(10)
            };

            btnAdd = new Button
            {
                Text = "Добавить",
                Location = new Point(10, 15),
                Width = 100,
                Height = 30,
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnAdd.Click += BtnAdd_Click;

            btnEdit = new Button
            {
                Text = "Изменить",
                Location = new Point(120, 15),
                Width = 100,
                Height = 30,
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnEdit.Click += BtnEdit_Click;

            btnDelete = new Button
            {
                Text = "Удалить",
                Location = new Point(230, 15),
                Width = 100,
                Height = 30,
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnDelete.Click += BtnDelete_Click;

            btnImport = new Button
            {
                Text = "Импорт",
                Location = new Point(340, 15),
                Width = 100,
                Height = 30
            };
            btnImport.Click += BtnImport_Click;

            btnExport = new Button
            {
                Text = "Экспорт",
                Location = new Point(450, 15),
                Width = 100,
                Height = 30
            };
            btnExport.Click += BtnExport_Click;

            btnSave = new Button
            {
                Text = "Сохранить",
                Location = new Point(560, 15),
                Width = 100,
                Height = 30,
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSave.Click += BtnSave_Click;

            buttonPanel.Controls.AddRange(new Control[] {
                btnAdd, btnEdit, btnDelete,
                btnImport, btnExport, btnSave
            });

            // DataGridView
            dgvStudents = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                RowHeadersVisible = false,
                AllowUserToResizeRows = false,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            // Настройка столбцов
            var columns = new[]
            {
                new DataGridViewTextBoxColumn 
                { 
                    Name = "LastName",
                    HeaderText = "Фамилия",
                    DataPropertyName = "LastName",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    MinimumWidth = 100
                },
                new DataGridViewTextBoxColumn 
                { 
                    Name = "FirstName",
                    HeaderText = "Имя",
                    DataPropertyName = "FirstName",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    MinimumWidth = 100
                },
                new DataGridViewTextBoxColumn 
                { 
                    Name = "MiddleName",
                    HeaderText = "Отчество",
                    DataPropertyName = "MiddleName",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    MinimumWidth = 100
                },
                new DataGridViewTextBoxColumn 
                { 
                    Name = "Course",
                    HeaderText = "Курс",
                    DataPropertyName = "Course",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                    MinimumWidth = 60
                },
                new DataGridViewTextBoxColumn 
                { 
                    Name = "Group",
                    HeaderText = "Группа",
                    DataPropertyName = "Group",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                    MinimumWidth = 80
                },
                new DataGridViewTextBoxColumn 
                { 
                    Name = "DateOfBirth",
                    HeaderText = "Дата рождения",
                    DataPropertyName = "DateOfBirth",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                    MinimumWidth = 100,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" }
                },
                new DataGridViewTextBoxColumn 
                { 
                    Name = "Email",
                    HeaderText = "Email",
                    DataPropertyName = "Email",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    MinimumWidth = 150
                }
            };

            dgvStudents.Columns.AddRange(columns);
            dgvStudents.AutoGenerateColumns = false;

            // Настройка внешнего вида таблицы
            dgvStudents.EnableHeadersVisualStyles = false;
            dgvStudents.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 120, 215);
            dgvStudents.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvStudents.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStudents.ColumnHeadersDefaultCellStyle.Font = new Font(dgvStudents.Font, FontStyle.Bold);
            dgvStudents.ColumnHeadersHeight = 40;
            dgvStudents.RowTemplate.Height = 30;
            dgvStudents.DefaultCellStyle.Padding = new Padding(5);
            dgvStudents.RowPrePaint += DgvStudents_RowPrePaint;

            // TableLayoutPanel для правильного размещения элементов
            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1,
                BackColor = Color.White
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60)); // filterPanel
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // dgvStudents
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60)); // buttonPanel

            mainLayout.Controls.Add(filterPanel, 0, 0);
            mainLayout.Controls.Add(dgvStudents, 0, 1);
            mainLayout.Controls.Add(buttonPanel, 0, 2);

            // Очищаем и добавляем только TableLayoutPanel
            this.Controls.Clear();
            this.Controls.Add(mainLayout);
        }

        private void LoadData()
        {
            if (_dataManager.Students != null && _dataManager.Students.Any())
            {
                _bindingSource.DataSource = new BindingList<Student>(_dataManager.Students);
                dgvStudents.DataSource = _bindingSource;
                dgvStudents.Refresh();
            }
        }

        private void FilterStudents(object sender, EventArgs e)
        {
            int? course = null;
            if (int.TryParse(cmbFilterCourse.Text, out int selectedCourse))
            {
                course = selectedCourse;
            }

            var filteredStudents = _dataManager.FilterStudents(
                txtSearchLastName.Text,
                course,
                txtFilterGroup.Text
            );

            _bindingSource.DataSource = new BindingList<Student>(filteredStudents);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new StudentForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _dataManager.AddStudent(form.Student);
                    LoadData();
                }
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0) return;

            var student = (Student)dgvStudents.SelectedRows[0].DataBoundItem;
            using (var form = new StudentForm(student))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _dataManager.UpdateStudent(student, form.Student);
                    LoadData();
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0) return;

            var student = (Student)dgvStudents.SelectedRows[0].DataBoundItem;
            if (MessageBox.Show($"Удалить студента {student.LastName} {student.FirstName}?",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _dataManager.DeleteStudent(student);
                LoadData();
            }
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _dataManager.ImportFromCsv(dialog.FileName);
                        LoadData();
                        MessageBox.Show("Импорт успешно завершен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при импорте: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                dialog.FileName = "students.csv";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _dataManager.ExportToCsv(dialog.FileName);
                        MessageBox.Show("Экспорт успешно завершен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при экспорте: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _dataManager.SaveData();
                MessageBox.Show("Данные успешно сохранены", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_dataManager.HasUnsavedChanges)
            {
                var result = MessageBox.Show(
                    "Есть несохраненные изменения. Сохранить перед выходом?",
                    "Подтверждение",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                switch (result)
                {
                    case DialogResult.Yes:
                        _dataManager.SaveData();
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void DgvStudents_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvStudents.Rows[e.RowIndex].DefaultCellStyle.BackColor = 
                    e.RowIndex % 2 == 0 ? Color.White : Color.FromArgb(240, 240, 240);
            }
        }
    }
} 