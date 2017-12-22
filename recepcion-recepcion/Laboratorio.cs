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

namespace recepcion_recepcion
{
    public partial class Laboratorio : Form
    {
        public Laboratorio()
        {
            InitializeComponent();
        }
        Cconectar con = new Cconectar();
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt_usu = new DataTable();
        String fecha;
        String data;
        int idx;
        private GroupBox groupBox1;
        private ToolStrip toolStrip1;
        private ToolStripLabel toolStripLabel1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripLabel toolStripLabel2;
        private ToolStripTextBox toolStripTextBox1;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton toolStripButton1;
        private DataGridView dataGridView1;
        private TextBox textBox1;
        string ORDEN;
        string cod_repro;


        private void timer1_Tick(object sender, EventArgs e)
        {



            for (int i = 0; i < dt.Rows.Count; i++)
            {


                DataRow row = dt.Rows[i];

                string documento = Convert.ToString(row["marcacion"]);
                string fecha_ingreso = Convert.ToString(row["fecha"]);
                int caracteres = documento.Length;
                string ID_LAB = documento.Substring(0, 2);

                string ORDEN = documento.Substring(3, (caracteres - 3));
                string reproce = reproceso(ORDEN, "1");

                if (existe_marcacionbod(ORDEN))
                {
                    if (existe_marcacion(ORDEN, ID_LAB, reproce))
                    {

                        if (reproce.Substring(0, 1) == "R")
                        {
                            con.conectar("LESA");
                            SqlCommand cmd3 = new SqlCommand();
                            cmd3.Connection = con.cmdls;
                            Guid GuD = Guid.NewGuid();
                            cmd3.CommandText = "  INSERT INTO [LDN].[LDN].[LABORATORIO_SEG] ([ID_LAB],[ID_ORDER],[FECHA_ING],[REPROCESO],[FECHA_REPRO],[ROWID],[USER_INGRESO],[USER_AUTORIZA_REP]) values (@ID_LAB,@ID_ORDER,@FECHA_ING,@REPROCESO,@FECHA_REPRO,@ROWID,@USER_INGRESO,@USER_AUTORIZA_REP) ";
                            cmd3.Parameters.Add("@ID_LAB", SqlDbType.NVarChar).Value = ID_LAB;
                            cmd3.Parameters.Add("@ID_ORDER", SqlDbType.VarChar).Value = ORDEN;
                            cmd3.Parameters.Add("@FECHA_ING", SqlDbType.DateTime).Value = Convert.ToDateTime(fecha_ingreso);
                            cmd3.Parameters.Add("@REPROCESO", SqlDbType.NVarChar).Value = reproce;
                            cmd3.Parameters.Add("@FECHA_REPRO", SqlDbType.DateTime).Value = Convert.ToDateTime(fecha_ingreso);
                            cmd3.Parameters.Add("@ROWID", SqlDbType.UniqueIdentifier).Value = GuD;
                            cmd3.Parameters.Add("@USER_INGRESO", SqlDbType.NVarChar).Value = "NAVARROW";
                            cmd3.Parameters.Add("@USER_AUTORIZA_REP", SqlDbType.NVarChar).Value = "NA";


                            cmd3.ExecuteNonQuery();

                            con.Desconectar("LESA");
                        }
                    }
                }
                else
                {
                    con.conectar("LESA");
                    SqlCommand cmd3 = new SqlCommand();
                    cmd3.Connection = con.cmdls;
                    Guid GuD = Guid.NewGuid();
                    cmd3.CommandText = "  INSERT INTO [LDN].[LDN].[LABORATORIO_SEG] ([ID_LAB],[ID_ORDER],[FECHA_ING],[REPROCESO],[FECHA_REPRO],[ROWID],[USER_INGRESO],[USER_AUTORIZA_REP]) values (@ID_LAB,@ID_ORDER,@FECHA_ING,@REPROCESO,@FECHA_REPRO,@ROWID,@USER_INGRESO,@USER_AUTORIZA_REP) ";
                    cmd3.Parameters.Add("@ID_LAB", SqlDbType.NVarChar).Value = ID_LAB;
                    cmd3.Parameters.Add("@ID_ORDER", SqlDbType.VarChar).Value = ORDEN;
                    cmd3.Parameters.Add("@FECHA_ING", SqlDbType.DateTime).Value = Convert.ToDateTime(fecha_ingreso);
                    cmd3.Parameters.Add("@REPROCESO", SqlDbType.NVarChar).Value = 'N';
                    cmd3.Parameters.Add("@FECHA_REPRO", SqlDbType.DateTime).Value = Convert.ToDateTime(fecha_ingreso);
                    cmd3.Parameters.Add("@ROWID", SqlDbType.UniqueIdentifier).Value = GuD;
                    cmd3.Parameters.Add("@USER_INGRESO", SqlDbType.NVarChar).Value = "NAVARROW";
                    cmd3.Parameters.Add("@USER_AUTORIZA_REP", SqlDbType.NVarChar).Value = "NA";



                    cmd3.ExecuteNonQuery();

                    con.Desconectar("LESA");



                }
            }
        }

        private bool existe_marcacion(string orden, string ID_LAB, string REMAR)
        {
            con.conectar("LESA");

            SqlCommand cmd = new SqlCommand("SELECT COUNT (*)  FROM [LDN].[LDN].[LABORATORIO_SEG] where ID_ORDER ='" + orden + "' and ID_LAB = '" + ID_LAB + "' and REPROCESO = '" + REMAR + "'", con.cmdls);
            //cmd.Parameters.AddWithValue("NUM_DOC_PREIMP", Convert.ToInt32(orden));

            int contar = Convert.ToInt32(cmd.ExecuteScalar());
            con.Desconectar("LESA");

            if (contar == 0)
            {
                return true;

            }
            else
            {
                return false;

            }
        }

        private bool existe_marcacionbod(string orden)
        {
            con.conectar("LESA");

            SqlCommand cmd = new SqlCommand("SELECT COUNT (*)  FROM [LDN].[LDN].[LABORATORIO_SEG] where ID_ORDER ='" + orden + "'", con.cmdls);
            //cmd.Parameters.AddWithValue("NUM_DOC_PREIMP", Convert.ToInt32(orden));

            int contar = Convert.ToInt32(cmd.ExecuteScalar());
            con.Desconectar("LESA");

            if (contar == 0)
            {
                return false;

            }
            else
            {
                return true;

            }
        }

        private string reproceso(string orden, string Repro)
        {
            //string Repro;
            con.conectar("LESA");
            SqlCommand comand7 = new SqlCommand("SELECT TOP 1[REPROCESO]  FROM[LDN].[LDN].[LABORATORIO_SEG] where ID_ORDER = '" + orden + "'  order by FECHA_ING desc", con.cmdls);
            SqlDataReader dr7 = comand7.ExecuteReader();

            while (dr7.Read())
            {
                Repro = Convert.ToString(dr7["REPROCESO"]);
            }
            dr7.Close();

            con.Desconectar("LESA");

            return Repro;
        }

        private void CopyDataTable(DataTable table)
        {
            // Create an object variable for the copy.
            DataTable copyDataTable;
            copyDataTable = table.Copy();

            // Insert code to work with the copy.
        }

        private void insert()
        {
            fecha = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            string ID_LAB = data.Substring(0, 2);
            int caracteres = data.Length;
            string ORDEN = data.Substring(3, (caracteres - 3));
            string reproce = reproceso(ORDEN, "1");


            if (existe_marcacionbod(ORDEN))
            {
                if (existe_marcacion(ORDEN, ID_LAB, reproce))
                {

                    if (reproce.Substring(0, 1) == "R")
                    {
                        con.conectar("LESA");
                        SqlCommand cmd3 = new SqlCommand();
                        cmd3.Connection = con.cmdls;
                        Guid GuD = Guid.NewGuid();
                        cmd3.CommandText = "  INSERT INTO [LDN].[LDN].[LABORATORIO_SEG] ([ID_LAB],[ID_ORDER],[FECHA_ING],[REPROCESO],[FECHA_REPRO],[ROWID],[USER_INGRESO],[USER_AUTORIZA_REP]) values (@ID_LAB,@ID_ORDER,@FECHA_ING,@REPROCESO,@FECHA_REPRO,@ROWID,@USER_INGRESO,@USER_AUTORIZA_REP) ";
                        cmd3.Parameters.Add("@ID_LAB", SqlDbType.NVarChar).Value = ID_LAB;
                        cmd3.Parameters.Add("@ID_ORDER", SqlDbType.VarChar).Value = ORDEN;
                        cmd3.Parameters.Add("@FECHA_ING", SqlDbType.DateTime).Value = Convert.ToDateTime(fecha);
                        cmd3.Parameters.Add("@REPROCESO", SqlDbType.NVarChar).Value = reproce;
                        cmd3.Parameters.Add("@FECHA_REPRO", SqlDbType.DateTime).Value = Convert.ToDateTime(fecha);
                        cmd3.Parameters.Add("@ROWID", SqlDbType.UniqueIdentifier).Value = GuD;
                        cmd3.Parameters.Add("@USER_INGRESO", SqlDbType.NVarChar).Value = "NAVARROW";
                        cmd3.Parameters.Add("@USER_AUTORIZA_REP", SqlDbType.NVarChar).Value = "NA";


                        cmd3.ExecuteNonQuery();

                        con.Desconectar("LESA");
                    }

                    else

                    {
                        con.conectar("LESA");
                        SqlCommand cmd3 = new SqlCommand();
                        cmd3.Connection = con.cmdls;
                        Guid GuD = Guid.NewGuid();
                        cmd3.CommandText = "  INSERT INTO [LDN].[LDN].[LABORATORIO_SEG] ([ID_LAB],[ID_ORDER],[FECHA_ING],[REPROCESO],[FECHA_REPRO],[ROWID],[USER_INGRESO],[USER_AUTORIZA_REP]) values (@ID_LAB,@ID_ORDER,@FECHA_ING,@REPROCESO,@FECHA_REPRO,@ROWID,@USER_INGRESO,@USER_AUTORIZA_REP) ";
                        cmd3.Parameters.Add("@ID_LAB", SqlDbType.NVarChar).Value = ID_LAB;
                        cmd3.Parameters.Add("@ID_ORDER", SqlDbType.VarChar).Value = ORDEN;
                        cmd3.Parameters.Add("@FECHA_ING", SqlDbType.DateTime).Value = Convert.ToDateTime(fecha);
                        cmd3.Parameters.Add("@REPROCESO", SqlDbType.NVarChar).Value = reproce;
                        cmd3.Parameters.Add("@FECHA_REPRO", SqlDbType.DateTime).Value = Convert.ToDateTime(fecha);
                        cmd3.Parameters.Add("@ROWID", SqlDbType.UniqueIdentifier).Value = GuD;
                        cmd3.Parameters.Add("@USER_INGRESO", SqlDbType.NVarChar).Value = "NAVARROW";
                        cmd3.Parameters.Add("@USER_AUTORIZA_REP", SqlDbType.NVarChar).Value = "NA";


                        cmd3.ExecuteNonQuery();

                        con.Desconectar("LESA");

                    }

                }

                else
                {


                }

            }
            else
            {
                con.conectar("LESA");
                SqlCommand cmd3 = new SqlCommand();
                cmd3.Connection = con.cmdls;
                Guid GuD = Guid.NewGuid();
                cmd3.CommandText = "  INSERT INTO [LDN].[LDN].[LABORATORIO_SEG] ([ID_LAB],[ID_ORDER],[FECHA_ING],[REPROCESO],[FECHA_REPRO],[ROWID],[USER_INGRESO],[USER_AUTORIZA_REP]) values (@ID_LAB,@ID_ORDER,@FECHA_ING,@REPROCESO,@FECHA_REPRO,@ROWID,@USER_INGRESO,@USER_AUTORIZA_REP) ";
                cmd3.Parameters.Add("@ID_LAB", SqlDbType.NVarChar).Value = ID_LAB;
                cmd3.Parameters.Add("@ID_ORDER", SqlDbType.VarChar).Value = ORDEN;
                cmd3.Parameters.Add("@FECHA_ING", SqlDbType.DateTime).Value = Convert.ToDateTime(fecha);
                cmd3.Parameters.Add("@REPROCESO", SqlDbType.NVarChar).Value = 'N';
                cmd3.Parameters.Add("@FECHA_REPRO", SqlDbType.DateTime).Value = Convert.ToDateTime(fecha);
                cmd3.Parameters.Add("@ROWID", SqlDbType.UniqueIdentifier).Value = GuD;
                cmd3.Parameters.Add("@USER_INGRESO", SqlDbType.NVarChar).Value = "NAVARROW";
                cmd3.Parameters.Add("@USER_AUTORIZA_REP", SqlDbType.NVarChar).Value = "NA";



                cmd3.ExecuteNonQuery();

                con.Desconectar("LESA");



            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void fill_datagrid()
        {
            dt2.Clear();

            con.conectar("LESA");

            SqlCommand cm2 = new SqlCommand("SELECT EST.[NOMBRE],[ID_ORDER],[FECHA_ING],[REPROCESO],[FECHA_REPRO],[USER_INGRESO],[USER_AUTORIZA_REP] FROM [LDN].[LDN].[LABORATORIO_SEG] as LAB LEFT JOIN [LDN].[LDN].[ETAPAS_LAB] as EST  ON LAB.ID_LAB = EST.ID_ETAPA  LEFT JOIN [LDN].[LDN].[PEDIDO_ENC] as ORD  on LAB.ID_ORDER= ORD.COD_ORDEN  WHERE ORD.ESTADO_LAB = 'LABORATORIO'", con.cmdls);
            SqlDataAdapter da = new SqlDataAdapter(cm2);
            da.Fill(dt2);


            dataGridView1.DataSource = dt2;
            if (dt2.Rows.Count >= 1)
            {
                dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[0];
                dataGridView1.Columns[2].DefaultCellStyle.Format = "MM/dd/yyyy hh:mm:ss tt";
                dataGridView1.Columns[4].DefaultCellStyle.Format = "MM/dd/yyyy hh:mm:ss tt";
            }
            con.Desconectar("LESA");
        }

        private void textBox1_LostFocus(object sender, System.EventArgs e)
        {
            textBox1.Focus();
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            dt2.DefaultView.RowFilter = string.Format("Convert(ID_ORDEN,'System.String') like '%{0}%'", this.toolStripTextBox1.Text);
            dataGridView1.DataSource = dt2;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idx = dataGridView1.CurrentRow.Index;
            ORDEN = Convert.ToString(dataGridView1.Rows[idx].Cells[1].Value);

        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Laboratorio));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Location = new System.Drawing.Point(21, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1011, 43);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripSeparator1,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.toolStripTextBox1,
            this.toolStripSeparator3,
            this.toolStripSeparator4,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(3, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1005, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(62, 22);
            this.toolStripLabel1.Text = "Busqueda:";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(43, 22);
            this.toolStripLabel2.Text = "Orden:";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 25);
//            this.toolStripTextBox1.Click += new System.EventHandler(this.toolStripTextBox1_Click);
            this.toolStripTextBox1.TextChanged += new System.EventHandler(this.textBox1_LostFocus);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Reproceso";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Location = new System.Drawing.Point(20, 99);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1012, 585);
            this.dataGridView1.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(47, 23);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(203, 20);
            this.textBox1.TabIndex = 4;
            // 
            // Laboratorio
            // 
            this.ClientSize = new System.Drawing.Size(1052, 707);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox1);
            this.Name = "Laboratorio";
            this.Text = "\'Laboraatorio\'";
            this.Load += new System.EventHandler(this.Laboratorio_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Laboratorio_Load(object sender, EventArgs e)
        {
            string cod_reproceso_ = menu.COD_REPRO;

            if (cod_reproceso_ == "S")
            {
                this.groupBox1.Enabled = true;
                this.textBox1.Enabled = false;

            }
            else if (cod_reproceso_ == "N")
            {
                this.groupBox1.Enabled = false;
            }

            dataGridView1.Enabled = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;

            textBox1.LostFocus += new EventHandler(textBox1_LostFocus);
            dt.Columns.Add("marcacion", typeof(string));
            dt.Columns.Add("fecha", typeof(string));
            // timer1.Interval = 10000;
            // timer1.Start();
            fill_datagrid();



        }

    }
    }
