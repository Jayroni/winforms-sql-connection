using TestWins.Controller;
using TestWins.Model;

namespace TestWins
{
    public partial class Form1 : Form
    {
        private readonly StudentController controller = new StudentController();
        private DataGridViewRow? selectedRow;

        public Form1()
        {
            InitializeComponent();
            loadData();
        }

        private void loadData()
        {
            dataGridView1.DataSource = controller.getAll();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;
            var success = controller.add(GetStudent());
            ShowResult(success, "Added");
            ClearAndRefresh();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedRow == null) { MessageBox.Show("Select a row!"); return; }
            if (!ValidateFields()) return;
            var success = controller.update(GetStudent());
            ShowResult(success, "Updated");
            ClearAndRefresh();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedRow == null) { MessageBox.Show("Select a row!"); return; }
            if (MessageBox.Show("Delete?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No) return;
            
            var success = controller.delete(Convert.ToInt32(selectedRow.Cells["Id"].Value));
            ShowResult(success, "Deleted");
            ClearAndRefresh();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            selectedRow = dataGridView1.Rows[e.RowIndex];
            PopulateFields();
        }

        // Helpers
        private bool ValidateFields() => 
            !string.IsNullOrWhiteSpace(txtName.Text) && !string.IsNullOrWhiteSpace(txtCourse.Text);

        private Student GetStudent() => new()
        {
            Id = int.TryParse(txtId.Text, out int id) ? id : 0,
            Name = txtName.Text,
            Course = txtCourse.Text,
            Age = int.TryParse(txtAge.Text, out int age) ? age : 0
        };

        private void PopulateFields()
        {
            txtId.Text = selectedRow.Cells["Id"].Value?.ToString() ?? "";
            txtName.Text = selectedRow.Cells["Name"].Value?.ToString() ?? "";
            txtCourse.Text = selectedRow.Cells["Course"].Value?.ToString() ?? "";
            txtAge.Text = selectedRow.Cells["Age"].Value?.ToString() ?? "";
        }

        private void ClearAndRefresh()
        {
            ClearFields();
            loadData();
        }

        private void ClearFields()
        {
            txtId.Clear(); txtName.Clear(); txtCourse.Clear(); txtAge.Clear();
            selectedRow = null;
        }

        private void ShowResult(bool success, string action)
        {
            var msg = success ? $" {action} successfully!" : $" Failed to {action}!";
            MessageBox.Show(msg, success ? "Success" : "Error");
        }
    }
}
