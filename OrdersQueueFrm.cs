using System;
using System.Data;
using System.Linq;

namespace Pizza
{
    public partial class OrdersQueueFrm : DevExpress.XtraEditors.XtraForm
    {
        DataTable OrdersDt = new DataTable();
        public OrdersQueueFrm()
        {
            InitializeComponent();
        }

        private void _LoadData()
        {

            OrdersDt.Columns.Add("OrderNumber", typeof(String));
            OrdersDt.Columns.Add("PizzaSize", typeof(String));
            OrdersDt.Columns.Add("Toppings", typeof(String));
            OrdersDt.Columns.Add("CrustType", typeof(String));
            OrdersDt.Columns.Add("WhereToEat", typeof(String));
            OrdersDt.Columns.Add("Price", typeof(short));
            OrdersDt.Columns.Add("OrderDate", typeof(string));

            Pizza_Main.OrdersQueue.Reverse<clsPizza>();

            foreach (clsPizza sPizzaOrder in Pizza_Main.OrdersQueue)
            {
                OrdersDt.Rows.Add(sPizzaOrder.OrderNumber, sPizzaOrder.PizzaSize, sPizzaOrder.Toppings
                    , sPizzaOrder.CrustType, sPizzaOrder.WhereToEat, sPizzaOrder.Price, sPizzaOrder.OrderDate);
            }

            gridControl1.DataSource = OrdersDt;
        }


        private void _initOrderInfo()
        {
            if (OrdersDt.Rows.Count == 0)
                return;


            LB_DateAndTime.Text = OrdersDt.Rows[0]["OrderDate"].ToString();
            LB_OrderNumber.Text = OrdersDt.Rows[0]["OrderNumber"].ToString();
            LB_Size.Text = OrdersDt.Rows[0]["PizzaSize"].ToString();
            LB_CrustType.Text = OrdersDt.Rows[0]["CrustType"].ToString();
            LB_Toppings.Text = OrdersDt.Rows[0]["Toppings"].ToString();
            LB_EatPlace.Text = OrdersDt.Rows[0]["WhereToEat"].ToString();
            LB_Price.Text = OrdersDt.Rows[0]["Price"].ToString();
        }


        private void OrdersQueueFrm_Load(object sender, EventArgs e)
        {
            _LoadData();

            _initOrderInfo();
        }

        private void Btn_OrderCooked_Click(object sender, EventArgs e)
        {
            if (OrdersDt.Rows.Count > 0)
            {
                OrdersDt.Rows.RemoveAt(0);
                _initOrderInfo();
            }
        }
    }
}