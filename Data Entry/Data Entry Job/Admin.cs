using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
namespace Data_Entry_Job
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
            error.Text = "";
            Name_User.Text = "";
            Image1.Visible = false;
            Image2.Visible = false;
            Field_1.Visible = false;
            Field_2.Visible = false;
            Add1.Visible = false;
            Add2.Visible = false;
            Delete_Field1.Visible = false;
            Delete_Field2.Visible = false;

        }
        int cropX1, cropY1, recW1, recH1;
        int cropX2, cropY2, recW2, recH2;

        bool f1 = false;
        bool f2 = false;

        bool f3 = false;

        public Pen crop_pen = new Pen(Color.Red);
        private void open_browser_Click(object sender, EventArgs e)
        {
            error.Text = "";
            OpenFileDialog browser = new System.Windows.Forms.OpenFileDialog();
            string path = "";
            if (browser.ShowDialog() == DialogResult.OK)
            {
                Name_User.Text = browser.SafeFileName;
                path = browser.FileName;
            }
            // Split Multi tiff 
            List<Image> images = Helper_Functions.Split_Multitiff(path);
            Image1.SizeMode = PictureBoxSizeMode.StretchImage;
            Image2.SizeMode = PictureBoxSizeMode.StretchImage;
            Image1.Image = images[0]; 
            Image2.Image = images[1];

            Image1.Visible = true;
            Image2.Visible = true;
            f1 = false;
            f2 = false;
            f3 = false;

            if (Field1.Image != null || Field2.Image != null || Field3.Image != null)
            {
                Field1.Image = null;
                Field2.Image = null;
                Field3.Image = null;
            }

            Field_1.Visible = false;
            Field_2.Visible = false;
            Add1.Visible = true;
            Add2.Visible = true;
            Delete_Field1.Visible = true;
            Delete_Field2.Visible = true;
        }

        private void Image1_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Cursor = Cursors.Cross;
                crop_pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                cropX1 = e.X;
                cropY1 = e.Y;

            }

        }

        private void Image1_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void Image1_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Image1.Refresh();
                recH1 = e.Y - cropY1;
                recW1 = e.X - cropX1;
                Graphics graph = Image1.CreateGraphics();
                graph.DrawRectangle(crop_pen, cropX1, cropY1, recW1, recH1);
                graph.Dispose();
            }
        }

        private void Image1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Image1_Click(object sender, EventArgs e)
        {
           


            
        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }

        private void Image2_MouseUp(object sender, MouseEventArgs e)
        {

           
        }

        private void Image2_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Cursor = Cursors.Cross;
                crop_pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                cropX2 = e.X;
                cropY2 = e.Y;

            }
        }

        private void Image2_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Image2.Refresh();
                recH2 = e.Y - cropY2;
                recW2 = e.X - cropX2;
                Graphics graph = Image2.CreateGraphics();
                graph.DrawRectangle(crop_pen, cropX2, cropY2, recW2, recH2);
                graph.Dispose();
            }
        }

        private void Image2_Click(object sender, EventArgs e)
        {



        }

        private void Image2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Save_Click(object sender, EventArgs e)
        {
            int AdminID = Fields.Get_AdminID(Login.username, Login.pword);
            
            Fields.Add_Fields(Field1.Image, Field2.Image, Field3.Image, AdminID);
            MessageBox.Show("Successful!");
        }

        private void Image1_MouseEnter(object sender, EventArgs e)
        {
            base.OnMouseEnter(e);
            Cursor = Cursors.Cross;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            
        }

        private void Add1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            Bitmap bmp = new Bitmap(Image1.Width, Image1.Height);
            Image1.DrawToBitmap(bmp, Image1.ClientRectangle);

            Bitmap crop = new Bitmap(recW1, recH1);

            for (int i = 0; i < recW1; i++)
            {
                for (int j = 0; j < recH1; j++)
                {
                    Color px = bmp.GetPixel(cropX1 + i, cropY1 + j);
                    crop.SetPixel(i, j, px);

                }
            }
            if (f1 == false)
            {
                Field1.Image = (Image)crop;
                Field1.SizeMode = PictureBoxSizeMode.CenterImage;

                f1 = true;
            }
            else if (f2 == false)
            {
                Field2.Image = (Image)crop;
                Field2.SizeMode = PictureBoxSizeMode.CenterImage;

                f2 = true;

            }
            else { error.Text = "Invalid!"; }
        }

        private void Add2_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            Bitmap bmp = new Bitmap(Image2.Width, Image2.Height);
            Image2.DrawToBitmap(bmp, Image2.ClientRectangle);

            Bitmap crop = new Bitmap(recW2, recH2);

            for (int i = 0; i < recW2; i++)
            {
                for (int j = 0; j < recH2; j++)
                {
                    Color px = bmp.GetPixel(cropX2 + i, cropY2 + j);
                    crop.SetPixel(i, j, px);

                }
            }
            
            if (f3 == false)
            {
                Field3.Image = (Image)crop;
                Field3.SizeMode = PictureBoxSizeMode.CenterImage;
                f3 = true;
            }
            else { error.Text = "Invalid!"; }
        }

        private void Delete_Field1_Click(object sender, EventArgs e)
        {
            Field_1.Visible = true;
            Field_2.Visible = true;

        }

        private void Field_1_Click(object sender, EventArgs e)
        {
            Field1.Image.Dispose();
            Field1.Image = null;
            f1 = false;
           
        }

        private void Field_2_Click(object sender, EventArgs e)
        {
            Field2.Image.Dispose();
            Field2.Image = null;
            f2 = false;
        }

        private void Delete_Field2_Click(object sender, EventArgs e)
        {
            Field3.Image.Dispose();
            Field3.Image = null;
            f3 = false;
        }

        private void label8_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you logout?", "Mood Test", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes:
                    {
                        Login l = new Login();
                        this.Hide();
                        l.Show();
                        break;
                    }
                case DialogResult.No:
                    break;
            }
        }
    
    }
}
