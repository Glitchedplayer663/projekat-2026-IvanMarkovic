using System.Windows.Forms;
using System.Drawing;

namespace projekat_2026_IvanMarkovic
{
    internal static class InputBoxDialog
    {
        public static string Show(string prompt, string title, string defaultValue = "")
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button btnOk = new Button();
            Button btnCancel = new Button();

            form.Text = title;
            form.ClientSize = new Size(380, 140);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterParent;
            form.MaximizeBox = false;
            form.MinimizeBox = false;

            label.Text = prompt;
            label.Location = new Point(10, 10);
            label.Size = new Size(360, 20);
            label.AutoSize = false;

            textBox.Text = defaultValue;
            textBox.Location = new Point(10, 38);
            textBox.Size = new Size(360, 22);

            btnOk.Text = "OK";
            btnOk.Location = new Point(200, 78);
            btnOk.Size = new Size(80, 28);
            btnOk.DialogResult = DialogResult.OK;

            btnCancel.Text = "Otkazati";
            btnCancel.Location = new Point(290, 78);
            btnCancel.Size = new Size(80, 28);
            btnCancel.DialogResult = DialogResult.Cancel;

            form.Controls.AddRange(new Control[] { label, textBox, btnOk, btnCancel });
            form.AcceptButton = btnOk;
            form.CancelButton = btnCancel;

            if (form.ShowDialog() == DialogResult.OK)
                return textBox.Text;
            return null;
        }
    }
}
