using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BookRentalShop20
{
    public partial class RentalForm : MetroForm
    {
        //디비연결 string
        string mode = "";
        public RentalForm()
        {
            InitializeComponent();
        }

        //private void MemberForm_Load(object sender, EventArgs e)
        //{
        //    DtpRentalDate.CustomFormat = "yyyy-MM-dd";
        //    DtpRentalDate.Format = DateTimePickerFormat.Custom;

        //    UpdateData();   // 데이터그리드 DB 데이터 로딩하기
        //    UpdateCboDvision();
        //}

        private void RentalForm_Load(object sender, EventArgs e)
        {
            DtpRentalDate.CustomFormat = DtpReturnDate.CustomFormat = "yyyy-MM-dd";
            DtpRentalDate.Format = DtpReturnDate.Format = DateTimePickerFormat.Custom;

            UpdateData();
            UpdateCboMemberIdx();
            UpdateCboBookIdx();

        }

        private void UpdateCboMemberIdx()
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(Commons.CONNSTRING))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT Idx, Names FROM membertbl";
                SqlDataReader reader = cmd.ExecuteReader();
                Dictionary<string, string> temps = new Dictionary<string, string>();
                while (reader.Read())
                {
                    temps.Add(reader[0].ToString(), reader[1].ToString());
                }
                CboMemberIdx.DataSource = new BindingSource(temps, null);
                CboMemberIdx.DisplayMember = "Value";
                CboMemberIdx.ValueMember = "Key";
                CboMemberIdx.SelectedIndex = -1;
            }
        }
        private void UpdateCboBookIdx()
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(Commons.CONNSTRING))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT Idx, Names FROM bookstbl";
                SqlDataReader reader = cmd.ExecuteReader();
                Dictionary<string, string> temps = new Dictionary<string, string>();
                while (reader.Read())
                {
                    temps.Add(reader[0].ToString(), reader[1].ToString());
                }
                CboBookIdx.DataSource = new BindingSource(temps, null);
                CboBookIdx.DisplayMember = "Value";
                CboBookIdx.ValueMember = "Key";
                CboBookIdx.SelectedIndex = -1;
            }
        }

  

        //private void UpdateCboDvision()
        //{
        //    using( SqlConnection conn = new SqlConnection(Commons.CONNSTRING))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = conn;
        //        cmd.CommandText = "SELECT Division, Names FROM divtbl";
        //        SqlDataReader reader = cmd.ExecuteReader();
        //        Dictionary<string, string> temps = new Dictionary<string, string>();
        //        while(reader.Read())
        //        {
        //            temps.Add(reader[0].ToString(), reader[1].ToString());
        //        }
        //        CboMemberIdx.DataSource = new BindingSource(temps, null);
        //        CboMemberIdx.DisplayMember = "Value";
        //        CboMemberIdx.ValueMember = "Key";
        //        CboMemberIdx.SelectedIndex = -1;
        //    }
        //}

        private void UpdateData()
        {
            using (SqlConnection conn = new SqlConnection(Commons.CONNSTRING))   //using이 없으면 conn.Close() 해줘야함
            {
                conn.Open(); //DB 열기
                string strQuery = "    SELECT r.idx AS '대여번호', " +
                                  "           m.Names AS '회원이름', " +
                                  "           b.Names AS '책 이름', " +
                                  "           r.rentalDate AS '대여일', " +
                                  "           r.returnDate AS '반납일' " +
                                  "      FROM rentaltbl AS r " +
                                  "INNER JOIN membertbl AS m " +
                                  "        ON r.memberIdx = m.Idx " +
                                  "INNER JOIN bookstbl AS b " +
                                  "        ON r.bookIdx = b.Idx " +
                                  "INNER JOIN divtbl AS t " +
                                  "        ON b.division = t.division ";
                //SqlCommand cmd = new SqlCommand(strQuery, conn);  //sql문을 실행할때는 SqlCommand가 꼭 필요하다! update할때는 필요없음
                SqlDataAdapter dataAdapter = new SqlDataAdapter(strQuery, conn);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds, "rentaltbl");

                GrdRentalTbl.DataSource = ds;
                GrdRentalTbl.DataMember = "rentaltbl";
            }
        }

        private void GrdDivTbl_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if(e.RowIndex > -1)
            {               
                DataGridViewRow data = GrdRentalTbl.Rows[e.RowIndex];
                TxtIdx.Text = data.Cells[0].Value.ToString(); // ID
                TxtIdx.ReadOnly = true; //Division이 PK라서 변경하면 안 된다.
                TxtIdx.BackColor = Color.Red;

                CboMemberIdx.SelectedIndex = CboMemberIdx.FindString(data.Cells[1].Value.ToString());
                CboBookIdx.SelectedIndex = CboBookIdx.FindString(data.Cells[2].Value.ToString());
                //CboDivision.SelectedIndex = CboDivision.FindString(data.Cells[3].Value.ToString()); // 책 장르
                DtpRentalDate.Value = DateTime.Parse(data.Cells[3].Value.ToString());  // 발간일
                DtpReturnDate.Value = DateTime.Parse(data.Cells[4].Value.ToString());  // 반납일


                DtpRentalDate.CustomFormat = "yyyy-MM-dd";
                DtpRentalDate.Format = DateTimePickerFormat.Custom;

                DtpReturnDate.CustomFormat = "yyyy-MM-dd";
                DtpReturnDate.Format = DateTimePickerFormat.Custom;

                mode = "UPDATE"; // 수정은 UPDATE
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)   //New Button (추가)
        {
            ClearTextControls();
            mode = "INSERT"; // 신규는 INSERT
        }

        private void BtnSave_Click(object sender, EventArgs e)  //Save Button (저장)
        {
            if (string.IsNullOrEmpty(CboMemberIdx.Text) || (string.IsNullOrEmpty(CboBookIdx.Text)))
            {
                MetroMessageBox.Show(this, "빈값을 지정할 수 없습니다.", "경고",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; //메서드를 빠져나가서 더 이상 진행 안 함
            }
            SaveProcess();
            UpdateData();

            ClearTextControls();
        }

        private void ClearTextControls()
        {
            TxtIdx.Text = "";
            CboBookIdx.SelectedIndex = CboMemberIdx.SelectedIndex = -1;

            TxtIdx.ReadOnly = true; //txtIdx는 자동 증가
            TxtIdx.BackColor = Color.Beige;

            DtpRentalDate.CustomFormat = DtpReturnDate.CustomFormat = " ";
            DtpRentalDate.Format = DateTimePickerFormat.Custom;
            DtpReturnDate.Format = DateTimePickerFormat.Custom;


            CboMemberIdx.Focus();
        }

        private void SaveProcess()
        {
            if(String.IsNullOrEmpty(mode))
            {
                MetroMessageBox.Show(this, "신규버튼을 누르고 데이터를 저장하십시오.","경고",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //DB저장프로세스
            using (SqlConnection conn = new SqlConnection(Commons.CONNSTRING))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                string strQuery = "";

                if (mode == "UPDATE")
                {
                    strQuery = "UPDATE dbo.rentaltbl " +
                               "   SET memberIdx = @memberIdx, " +
                               "       bookIdx = @bookIdx, " +
                               "       rentalDate = @rentalDate, " +
                               "       returnDate = @returnDate, " +
                               " WHERE Idx = @Idx ";                  
                }
                else if (mode == "INSERT")
                {
                    //throw new NotImplementedException(); // 아직 구현 안 됬을 때 에러 발생
                    strQuery = "INSERT INTO dbo.rentaltbl(memberIdx, bookIdx, rentalDate, returnDate) " +
                               " VALUES(@memberIdx, @bookIdx, @rentalDate, @returnDate) ";
                }
                cmd.CommandText = strQuery;

                if(mode == "UPDATE")
                { 
                    // TxtIdx 설정 
                    SqlParameter pramIdx = new SqlParameter("@Idx", System.Data.SqlDbType.Int);//Idx 속성 글자타입을 Int, 길이를 Null이 아님으로 지정했음
                    pramIdx.Value = TxtIdx.Text;
                    cmd.Parameters.Add(pramIdx);
                }

                    //CboDivision 설정
                    SqlParameter parmMemberIdx = new SqlParameter("@memberIdx", System.Data.SqlDbType.Char, 4);// CboMemberIdx 속성 글자타입을 Char, 길이를 4로 지정했음
                    parmMemberIdx.Value = CboMemberIdx.SelectedValue;
                    cmd.Parameters.Add(parmMemberIdx);

                    //CboBookIdx 설정
                    SqlParameter parmBookIdx = new SqlParameter("@bookIdx", System.Data.SqlDbType.Char, 4);// CboBookIdx 속성 글자타입을 Char, 길이를 4로 지정했음
                    parmBookIdx.Value = CboBookIdx.SelectedValue;
                    cmd.Parameters.Add(parmBookIdx);

                    // DtpRentalDate 설정 
                    SqlParameter pramrentalDate = new SqlParameter("@rentalDate", System.Data.SqlDbType.Date);//rentalDate 속성 글자타입을 VarChar, 길이를 13로 지정했음
                    pramrentalDate.Value = DtpRentalDate.Value;
                    cmd.Parameters.Add(pramrentalDate);

                    // DtpReturnDate 설정 
                    SqlParameter pramReturnDate = new SqlParameter("@returnDate", System.Data.SqlDbType.Date);//returnDate 속성 글자타입을 VarChar, 길이를 13로 지정했음
                    pramReturnDate.Value = DtpReturnDate.Value;
                    cmd.Parameters.Add(pramReturnDate);





                cmd.ExecuteNonQuery();  //쿼리문을 실행 시켜주기 위해서, NonQuery를 사용하는것은 값을 돌려받지 않기 위해서
            }
        }

        private void TxtNames_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                BtnSave_Click(sender, new EventArgs());
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtIdx.Text) /* || (string.IsNullOrEmpty(TxtAuthor.Text))*/)
            {
                MetroMessageBox.Show(this, "빈값은 삭제할 수 없습니다.", "경고",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; //메서드를 빠져나가서 더 이상 진행 안 함
            }
            DeleteProcess();
        }

        private void DeleteProcess()
        {
            using (SqlConnection conn = new SqlConnection(Commons.CONNSTRING))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM dbo.divtbl WHERE Division = @Division";
                SqlParameter parmDivision = new SqlParameter("@Division", SqlDbType.Char, 4);
                parmDivision.Value = TxtIdx.Text;
                cmd.Parameters.Add(parmDivision);

                cmd.ExecuteNonQuery();
                UpdateData();
                ClearTextControls();
            }
        }

        private void DtpRentalDate_ValueChanged(object sender, EventArgs e)
        {
            DtpRentalDate.CustomFormat = "yyyy-MM-dd";
            DtpRentalDate.Format = DateTimePickerFormat.Custom;
        }

        private void DtpReturnDate_ValueChanged(object sender, EventArgs e)
        {
            DtpReturnDate.CustomFormat = "yyyy-MM-dd";
            DtpReturnDate.Format = DateTimePickerFormat.Custom;
        }

        private void GrdRentalTbl_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
