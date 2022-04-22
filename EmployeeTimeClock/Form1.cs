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
 
namespace EmployeeTimeClock//Employee Time Clock Script
{
    public partial class Form1 : Form
    {
        int empID;
        DateTime beginTime = DateTime.Now;
        DateTime endTime = DateTime.Now;
             
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();// Timer Method 
        }
        
             
        private void timer1_Tick(object sender, EventArgs e)//Timer Logic 
        {
            lblStatus.Text = DateTime.Now.ToString("T");
        }

      
        private void button1_Click(object sender, EventArgs e) //Clock In Logic 
        {
            beginTime = DateTime.Now;
            clockIN.Text = beginTime.ToShortTimeString();           
           
            SqlConnection conn1 = new SqlConnection(); //SQL Connection Logic 
            conn1.ConnectionString = "Data Source=DESKTOP-9IKPMC3\\GOBERTUTORIALSQL;Initial Catalog=empTimeClockDataBase;Integrated Security=True"; 
            conn1.Open();
            SqlCommand cmd = new SqlCommand("select id, employeename from data where id=@id " , conn1);
            cmd.Parameters.AddWithValue("id", textBox2.Text);
            SqlDataReader reader1;
            reader1 = cmd.ExecuteReader();
            
            if (reader1.Read())
            {
                textBox2.Text = reader1["employeename"].ToString();

                if (beginTime.Hour == 8) //on time
                {
                    MessageBox.Show("Congrats! You clocked in on time.:) ");
                }
                else if (beginTime.Hour > 8 && beginTime.Hour < 11)
                {
                    MessageBox.Show("You are late! ");
                }
               
                else if (beginTime.Hour > 11 || beginTime.Hour < 8 && endTime.Hour != 12)
                {
                    MessageBox.Show("Clocking In Early Not Permitted! ");
                }
            }
            else
            {
                MessageBox.Show("NO DATA FOUND");
            }
            
            conn1.Close();
          
        }

        private void button2_Click(object sender, EventArgs e)// Clock Out Logic 
        {
            endTime = DateTime.Now;
            clockOUT.Text = endTime.ToShortTimeString();
            if (endTime.Hour < 4 && beginTime.Hour > 2) //too early
            {
                MessageBox.Show("You punched out early! ");
            }
            if (endTime.Hour == 5) //on time
            {
                MessageBox.Show("Congrats! You clocked out on time.:) ");
            }          
        }

        
    }
}
