using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SourceCode
{
    public partial class DoiTac : Form
    {
        SqlConnection connect;
        public string temp;
        public string temp1;

        public bool MultipleActiveResultSets { get; private set; }

        public DoiTac()
        {
            InitializeComponent();
            tabControl1.Enabled = false;
        }

        public void display1()
        {
            string sqlselect1 = "SELECT * FROM HOPDONG WHERE MAHD = '" + textBox1.Text.Trim() + "'";
            SqlCommand cmd = new SqlCommand(sqlselect1, connect);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            dataGridView1.RowHeadersVisible = false;
        }

        public void displaySP()
        {
            string sqlselect1 = "SELECT MASP AS N'Mã SP', TENSP AS N'Tên sản phẩm', SLTON AS N'Số lượng tồn', CAST(DONGIA AS NUMERIC(10,0)) AS N'Đơn giá (đồng)' FROM SANPHAM WHERE MADT = '" + temp + "'";
            SqlCommand cmd = new SqlCommand(sqlselect1, connect);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView2.DataSource = dt;
            dataGridView2.RowHeadersVisible = false;
        }

        private void DoiTac_Load(object sender, EventArgs e)
        {
            connect = new SqlConnection(@"Data Source=DESKTOP-2V7JI18\MAYAO;Initial Catalog=QL_DH_GH_ONLINE;Integrated Security=True;MultipleActiveResultSets=true");
            connect.Open();

            label3.Text = temp;
            label5.Text = temp1;

            textBox3.Text = temp;
            textBox4.Text = temp1;

            string sqlselect3 = "SELECT SDT FROM DOITAC WHERE MADT = '" + temp + "'";
            SqlCommand cmd3 = new SqlCommand(sqlselect3, connect);
            textBox5.Text = Convert.ToString(cmd3.ExecuteScalar());

            string sqlselect4 = "SELECT DIACHI FROM DOITAC WHERE MADT = '" + temp + "'";
            SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
            textBox10.Text = Convert.ToString(cmd4.ExecuteScalar());

            string sqlselect7 = "SELECT QUAN FROM DOITAC WHERE MADT = '" + temp + "'";
            SqlCommand cmd7 = new SqlCommand(sqlselect7, connect);
            textBox6.Text = Convert.ToString(cmd7.ExecuteScalar());

            string sqlselect8 = "SELECT THANHPHO FROM DOITAC WHERE MADT = '" + temp + "'";
            SqlCommand cmd8 = new SqlCommand(sqlselect8, connect);
            comboBox2.Text = Convert.ToString(cmd8.ExecuteScalar());

            string sqlselect9 = "SELECT EMAIL FROM DOITAC WHERE MADT = '" + temp + "'";
            SqlCommand cmd9 = new SqlCommand(sqlselect9, connect);
            textBox11.Text = Convert.ToString(cmd9.ExecuteScalar());

            string sqlselect10 = "SELECT MST FROM DOITAC WHERE MADT = '" + temp + "'";
            SqlCommand cmd10 = new SqlCommand(sqlselect10, connect);
            textBox2.Text = Convert.ToString(cmd10.ExecuteScalar());

            string sqlselect5 = "SELECT USERNAME FROM LOGIN WHERE USERID = '" + temp + "'";
            SqlCommand cmd5 = new SqlCommand(sqlselect5, connect);
            textBox7.Text = Convert.ToString(cmd5.ExecuteScalar());

            string sqlselect6 = "SELECT PASSWORD FROM LOGIN WHERE USERID = '" + temp + "'";
            SqlCommand cmd6 = new SqlCommand(sqlselect6, connect);
            textBox8.Text = Convert.ToString(cmd6.ExecuteScalar());

            string sqlselect11 = "SELECT MANDD FROM DOITAC WHERE MADT = '" + temp + "'";
            SqlCommand cmd11 = new SqlCommand(sqlselect11, connect);
            textBox13.Text = Convert.ToString(cmd11.ExecuteScalar());

            string sqlselect12 = "SELECT TENNGUOIDAIDIEN FROM NGUOIDAIDIEN WHERE MANDD = '" + textBox13.Text.Trim() + "'";
            SqlCommand cmd12 = new SqlCommand(sqlselect12, connect);
            textBox17.Text = Convert.ToString(cmd12.ExecuteScalar());

            //display1();
            displaySP();
            displayHD();
            //displayDT();
            button7.Enabled = false;
            button3.Enabled = false;
            button16.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn có muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                connect.Close();
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            }
            else
            {
                return;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();

            if (comboBox1.Text.Trim() == "Còn hiệu lực")
            {
                dateTimePicker1.Enabled = false;
                comboBox1.Enabled = false;
                button1.Enabled = false;
            }
            else if (comboBox1.Text.Trim() == "Hết hạn")
            {
                comboBox1.Enabled = true;
                button1.Enabled = false;
                dateTimePicker1.Enabled = false;
            }

            if (comboBox1.Text.Trim() == "Đang chờ duyệt")
            {
                dateTimePicker1.Enabled = false;
                comboBox1.Enabled = false;
                button1.Enabled = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.Text.Trim() == "Gia hạn hợp đồng đến")
            {
                dateTimePicker1.Enabled = true;
                comboBox1.Enabled = true;
                button1.Enabled = true;
            }

            if (comboBox1.Text.Trim() == "Kết thúc hợp đồng")
            {
                dateTimePicker1.Enabled = false;
                comboBox1.Enabled = true;
                button1.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sqlselect4 = "";
            if (radioButton2.Checked || radioButton1.Checked)
            {
                if (comboBox1.Text.Trim() == "Gia hạn hợp đồng đến")
                {
                    sqlselect4 = "EXEC CAPNHAT_TTHD @MAHD, N'" + comboBox1.Text.Trim() + " " + dateTimePicker1.Value.ToString("dd/MM/yyyy") + "'";
                }

                if (comboBox1.Text.Trim() == "Kết thúc hợp đồng")
                {
                    sqlselect4 = "EXEC CAPNHAT_TTHD @MAHD, N'" + comboBox1.Text.Trim() + "'";
                }
            }

            SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
            cmd4.Parameters.Add(new SqlParameter("@MAHD", textBox1.Text.Trim()));
            cmd4.ExecuteNonQuery();
            display1();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sqlselect4 = "UPDATE DOITAC SET SDT = @SDT, QUAN = @QUAN, THANHPHO = @TP, DIACHI = @DC, EMAIL = @EM WHERE MADT = '" + temp + "'";
            SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
            cmd4.Parameters.Add(new SqlParameter("@SDT", textBox5.Text.Trim()));
            cmd4.Parameters.Add(new SqlParameter("@QUAN", textBox6.Text.Trim()));
            cmd4.Parameters.Add(new SqlParameter("@TP", comboBox2.Text.Trim()));
            cmd4.Parameters.Add(new SqlParameter("@EM", textBox11.Text.Trim()));
            cmd4.Parameters.Add(new SqlParameter("@DC", textBox10.Text.Trim()));
            cmd4.ExecuteNonQuery();

            DialogResult res = MessageBox.Show("Cập nhật thành công!", "Thông báo");
            if (res == DialogResult.OK)
            {
                return;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string sqlselect4 = "UPDATE LOGIN SET PASSWORD = @PW WHERE USERID = '" + temp + "'";
            SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
            cmd4.Parameters.Add(new SqlParameter("@PW", textBox8.Text.Trim()));
            cmd4.ExecuteNonQuery();

            DialogResult res = MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo");
            if (res == DialogResult.OK)
            {
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int count = 0;
            if (textBox12.Text == "")
            {
                MessageBox.Show("Tên sản phẩm còn trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                count++;
            }

            if (textBox14.Text == "")
            {
                MessageBox.Show("Giá sản phẩm còn trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                count++;
            }

            if (numericUpDown1.Value == 0)
            {
                MessageBox.Show("Số lượng sản phẩm nhập vào cần lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                count++;
            }

            if (count == 3)
            {
                string sqlselect4 = "EXEC SP_INSERT_SP '" + temp + "', @TENSP, @SL, @DG";
                SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
                cmd4.Parameters.Add(new SqlParameter("@TENSP", textBox12.Text.Trim()));
                cmd4.Parameters.Add(new SqlParameter("@SL", numericUpDown1.Text.Trim()));
                cmd4.Parameters.Add(new SqlParameter("@DG", textBox14.Text.Trim()));
                cmd4.ExecuteNonQuery();

                
                button2.Enabled = false;
                button3.Enabled = true;
                button7.Enabled = true;
                button8.Enabled = true;
            }
            displaySP();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked || radioButton1.Checked)
            {
                string sqlselect4 = "EXEC DT_CAPNHAT_GIASP @MASP, @DG";
                SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
                cmd4.Parameters.Add(new SqlParameter("@MASP", textBox9.Text.Trim()));
                cmd4.Parameters.Add(new SqlParameter("@DG", textBox14.Text.Trim()));
                cmd4.ExecuteNonQuery();
                displaySP();
            }
            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox9.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox12.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            numericUpDown1.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox14.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();

            button2.Enabled = false;
            button3.Enabled = true;
            button7.Enabled = true;
            button16.Enabled = true;
            textBox12.Enabled = false;
            numericUpDown1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn có muốn xóa sản phẩm " + textBox9.Text.Trim(), "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                string sqlselect4 = "DELETE SANPHAM WHERE MASP = '" + textBox9.Text.Trim() + "'";
                SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
                cmd4.ExecuteNonQuery();

                textBox9.Text = "";
                textBox12.Text = "";
                numericUpDown1.Value = 0;
                textBox14.Text = "";

                displaySP();
                button3.Enabled = false;
                button2.Enabled = true;
                button7.Enabled = false;
            }
            else
            {
                return;
            }    
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox9.Text = "";
            textBox12.Text = "";
            numericUpDown1.Value = 0;
            textBox14.Text = "";

            button2.Enabled = true;
            button3.Enabled = false;
            button7.Enabled = false;
            textBox12.Enabled = true;
            numericUpDown1.Enabled = true;
            button16.Enabled = false;
            displaySP();
        }

        public void displayHD()
        {
            string sqlselect1 = "SELECT MAHD AS N'Mã HĐ', MACN AS N'Mã CN', NGAYKY AS N'Ngày ký', NGAYKT AS N'Ngày kết thúc', PHANTRAMHOAHONG AS N'Phần trăm HH', TINHTRANGHD AS N'Tình trạng HĐ' FROM HOPDONG WHERE MADT = '" + temp + "' AND TINHTRANGHD = N'Đang chờ duyệt'";
            SqlCommand cmd = new SqlCommand(sqlselect1, connect);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView3.DataSource = dt;
            dataGridView3.RowHeadersVisible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string CN = textBox16.Text;
            string sqlselect = "SELECT * FROM dbo.CHINHANH WHERE MACN = '" + CN + "'";
            SqlDataAdapter sda = new SqlDataAdapter(sqlselect, connect);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                try
                {
                        string sqlselect4 = "EXEC THEM_HD @MADT, @MACN, @NK, @NKT";
                        SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
                        cmd4.Parameters.Add(new SqlParameter("@MADT", label3.Text.Trim()));
                        cmd4.Parameters.Add(new SqlParameter("@MACN", textBox16.Text.Trim()));
                        cmd4.Parameters.Add(new SqlParameter("@NK", dateTimePicker2.Value.Date));
                        cmd4.Parameters.Add(new SqlParameter("@NKT", dateTimePicker3.Value.Date));
                        cmd4.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    MessageBox.Show("Đã có lỗi xảy ra\nVui lòng kiểm tra thông tin đã nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Mã chi nhánh không hợp lệ\nVui lòng kiểm tra mã chi nhánh đã nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }    
            displayHD();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string sqlselect4 = "UPDATE NGUOIDAIDIEN SET TENNGUOIDAIDIEN = @NDD WHERE MANDD = '" + textBox13.Text.Trim() + "'";
            SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
            cmd4.Parameters.Add(new SqlParameter("@NDD", textBox17.Text.Trim()));
            cmd4.ExecuteNonQuery();

            DialogResult res = MessageBox.Show("Cập nhật thành công!", "Thông báo");
            if (res == DialogResult.OK)
            {
                return;
            }
        }

        public void displayDH()
        {
            string sqlselect1 = "SELECT DONHANG.MADH AS N'Mã ĐH', MAKH AS N'Mã khách hàng', TTDONHANG AS N'Tình trạng ĐH', CAST(PHIVC AS NUMERIC(10,0)) AS N'Phí vận chuyển', CAST(THANHTIEN AS NUMERIC(10,0)) AS N'Tổng tiền' FROM DONHANG, CTDONHANG, SANPHAM WHERE CTDONHANG.MADH = DONHANG.MADH AND CTDONHANG.MASP = SANPHAM.MASP AND SANPHAM.MADT = '" + label3.Text.Trim() + "'";
            SqlCommand cmd = new SqlCommand(sqlselect1, connect);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView4.DataSource = dt;
            dataGridView4.RowHeadersVisible = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            displayDH();
            button11.Enabled = false;
            button12.Enabled = true;
            label33.Visible = true;
            comboBox3.Visible = true;
            button13.Visible = true;
            label34.Visible = true;
            label35.Visible = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string sqlselect1 = "SELECT MALH AS N'Mã LH', TENLOAIHANG AS N'Tên loại hàng', MOTA AS N'Mô tả' FROM LHVANCHUYEN";
            SqlCommand cmd = new SqlCommand(sqlselect1, connect);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView4.DataSource = dt;
            dataGridView4.RowHeadersVisible = false;

            button12.Enabled = false;
            button11.Enabled = true;
            label33.Visible = false;
            comboBox3.Visible = false;
            button13.Visible = false;
            label34.Visible = false;
            label35.Visible = false;

        }

        private void button13_Click(object sender, EventArgs e)
        {
            string sqlselect4 = "UPDATE DONHANG SET TTDONHANG = @TT WHERE MADH = '" + label35.Text.Trim() + "'";
            SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
            cmd4.Parameters.Add(new SqlParameter("@TT", comboBox3.Text.Trim()));
            cmd4.ExecuteNonQuery();
            displayDH();
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            label35.Text = dataGridView4.Rows[e.RowIndex].Cells[0].Value.ToString();
            comboBox3.Text = dataGridView4.Rows[e.RowIndex].Cells[2].Value.ToString();

            if (comboBox3.Text.Trim() == "Đã nhận")
            {
                button13.Enabled = false;
                comboBox3.Enabled = false;
            }  
            else
            {
                button13.Enabled = true;
                comboBox3.Enabled = true;
            }    
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked || radioButton2.Checked)
            {
                tabControl1.Enabled = true;
            }
            else
            {
                tabControl1.Enabled = false;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập mã hợp đồng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (radioButton2.Checked)
                {
                    string sqlselect4 = "EXEC XEM_DS_MAHD @MAHD, @MADT";
                    SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
                    cmd4.Parameters.Add(new SqlParameter("@MADT", label3.Text.Trim()));
                    cmd4.Parameters.Add(new SqlParameter("@MAHD", textBox1.Text.Trim()));
                    SqlDataReader dr = cmd4.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dataGridView1.DataSource = dt;
                    dataGridView1.RowHeadersVisible = false;
                }
                else if (radioButton1.Checked)
                {
                    string sqlselect4 = "EXEC XEM_DS_MAHD_FIX @MAHD, @MADT";
                    SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
                    cmd4.Parameters.Add(new SqlParameter("@MADT", label3.Text.Trim()));
                    cmd4.Parameters.Add(new SqlParameter("@MAHD", textBox1.Text.Trim()));
                    SqlDataReader dr = cmd4.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dataGridView1.DataSource = dt;
                    dataGridView1.RowHeadersVisible = false;
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Mã hợp đồng không thuộc đối tác này", "Thông báo");
            }
            //displayHD();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                string sqlselect4 = "EXEC DT_CAPNHATSL @MASP, @SL";
                SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
                cmd4.Parameters.Add(new SqlParameter("@MASP", textBox9.Text.Trim()));
                cmd4.Parameters.Add(new SqlParameter("@SL", numericUpDown1.Text.Trim()));
                //SqlDataReader dr = cmd4.ExecuteReader();
                //DataTable dt = new DataTable();
                //dt.Load(dr);
                //dataGridView2.DataSource = dt;
                //dataGridView2.RowHeadersVisible = false;
                cmd4.ExecuteNonQuery();
            }
            else if (radioButton1.Checked)
            {
                string sqlselect4 = "EXEC DT_CAPNHATSL_FIX @MASP, @SL";
                SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
                cmd4.Parameters.Add(new SqlParameter("@MASP", textBox9.Text.Trim()));
                cmd4.Parameters.Add(new SqlParameter("@SL", numericUpDown1.Text.Trim()));
                //SqlDataReader dr = cmd4.ExecuteReader();
                //DataTable dt = new DataTable();
                //dt.Load(dr);
                //dataGridView2.DataSource = dt;
                //dataGridView2.RowHeadersVisible = false;
                cmd4.ExecuteNonQuery();
            }
            displaySP();
        }

        public void displayDT()
        {
            string sqlselect1 = "SELECT DATEPART(year, DONHANG.NGAY_DH) AS N'Năm', DATEPART(month, DONHANG.NGAY_DH) AS N'Tháng', SUM(THANHTIEN) AS 'Doanh thu' , (SUM(THANHTIEN)*0.1) AS N'Phí hoa hồng' FROM DONHANG, CTDONHANG, SANPHAM, HOPDONG, DOITAC WHERE CTDONHANG.MADH = DONHANG.MADH AND CTDONHANG.MASP = SANPHAM.MASP AND SANPHAM.MADT = HOPDONG.MADT AND HOPDONG.MADT = DOITAC.MADT AND DOITAC.MADT = '" + label3.Text.Trim() +
                                "' AND DONHANG.NGAY_DH >= HOPDONG.NGAYKY GROUP BY DATEPART(year, DONHANG.NGAY_DH), DATEPART(month, DONHANG.NGAY_DH) ORDER BY DATEPART(year, DONHANG.NGAY_DH), DATEPART(month, DONHANG.NGAY_DH)";
            SqlCommand cmd = new SqlCommand(sqlselect1, connect);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView5.DataSource = dt;
            dataGridView5.RowHeadersVisible = false;
        }

        public void displayTD()
        {
            string sqlselect1 = "SELECT TENTD as N'Tên thực đơn', SANPHAM.MASP as N'Mã sản phẩm', TENSP as N'Tên sản phẩm', DONGIA as N'Đơn giá' FROM SANPHAM, THUCDON WHERE SANPHAM.MASP = THUCDON.MASP AND THUCDON.MATD = '" + textBox18.Text + "'";
            SqlCommand cmd = new SqlCommand(sqlselect1, connect);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView6.DataSource = dt;
            dataGridView6.RowHeadersVisible = false;
            textBox19.Text = textBox18.Text;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            displayDT();
            button18.Enabled = false;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            displayTD();
        }
        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label37_Click(object sender, EventArgs e)
        {

        }


        private void label38_Click(object sender, EventArgs e)
        {

        }

        private void button22_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlselect4 = "EXEC THEM_TD @TENTD, @MASP";
                SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
                cmd4.Parameters.Add(new SqlParameter("@TENTD", textBox20.Text));
                cmd4.Parameters.Add(new SqlParameter("@MASP", textBox21.Text));
                cmd4.ExecuteNonQuery();
                DialogResult res = MessageBox.Show("Thêm thực đơn thành công!", "Thông báo!");
            }
            catch (SqlException)
            {
                MessageBox.Show("Đã có lỗi xảy ra\nVui lòng kiểm tra thông tin đã nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlselect4 = "EXEC THEM_MON_TD @MATD, @MASP";
                SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
                cmd4.Parameters.Add(new SqlParameter("@MATD", textBox19.Text));
                cmd4.Parameters.Add(new SqlParameter("@MASP", textBox21.Text));
                cmd4.ExecuteNonQuery();
                DialogResult res = MessageBox.Show("Thêm món ăn vào thực đơn thành công!", "Thông báo!");
            }
            catch (SqlException)
            {
                MessageBox.Show("Đã có lỗi xảy ra\nVui lòng kiểm tra thông tin đã nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlselect4 = "EXEC XOA_MON_TD @MATD, @MASP";
                SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
                cmd4.Parameters.Add(new SqlParameter("@MATD", textBox19.Text));
                cmd4.Parameters.Add(new SqlParameter("@MASP", textBox21.Text));
                cmd4.ExecuteNonQuery();
                DialogResult res = MessageBox.Show("Xóa món ăn trong thực đơn thành công!", "Thông báo!");
            }
            catch (SqlException)
            {
                MessageBox.Show("Đã có lỗi xảy ra\nVui lòng kiểm tra thông tin đã nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
