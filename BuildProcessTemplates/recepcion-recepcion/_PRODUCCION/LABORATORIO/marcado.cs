﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;


namespace LND
{
    public partial class marcado : Form
    {
        public marcado()
        {
            InitializeComponent();
        }
      //  private static System.Timers.Timer aTimer;
        int tiempo = 70;
        string error_;

        private void marcado_Load(object sender, EventArgs e)
        {
            error_ = Lab_Marcacion.error;


            if(error_ == "SI")
            {
                label1.Text = "Número de LMS";
                label2.Text = "No asignado";
                label3.Text = "Escanear, CODIGO DE ORDEN";
                this.groupBox1.BackColor = Color.Red;
                label1.ForeColor = Color.White;
                label2.ForeColor = Color.White;
                label3.ForeColor = Color.White;
                timer1.Interval = 240;
                timer1.Start();

            }
            else if(error_ == "NO")
            {
                label1.Text = Lab_Marcacion.ORDEN_SHOW;
                label2.Text = menu.usuario.ToUpper();
                label3.Text = DateTime.Now.ToString();
                this.groupBox1.BackColor = Color.LightGreen;
                timer1.Interval = 180;
                timer1.Start();
            }
            else if (error_ == "NOLAB")
            {
                label1.Text = Lab_Marcacion.ORDEN_SHOW;
                label2.Text = menu.usuario.ToUpper();
                label3.Text = DateTime.Now.ToString();
                this.groupBox1.BackColor = Color.LightGreen;
                timer1.Interval = 180;
                timer1.Start();

            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //while (tiempo > 0)
            //{
            //    tiempo = tiempo - 1;

            //    if (tiempo == 0)
            //    {
                    this.Close();
                //}
            //}
            
        }
        private void regresivo()
        {


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
