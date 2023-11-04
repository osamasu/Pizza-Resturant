using System;
using System.Collections.Generic;

namespace Pizza
{
    public partial class Pizza_Main : DevExpress.XtraEditors.XtraForm
    {

        #region Fields

        string FilePath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\PizzaOrders.txt";

        Random Rand = new Random();

        static public Queue<clsPizza> OrdersQueue = new Queue<clsPizza>();

        #endregion


        #region Constructor
        public Pizza_Main()
        {
            InitializeComponent();
        }
        #endregion


        #region Methods

        private float _CalcPizzaSizePrice()
        {
            string PriceInString = (string)((rb_Small.Checked) ? rb_Small.Tag : (rb_Medium.Checked) ? rb_Medium.Tag : rb_Large.Tag);
            float.TryParse(PriceInString, out float Result);
            return Result;
        }
        private float _CalcPizzaCrustType()
        {
            string CrustPriceInString = ((rb_ThinCrust.Checked) ? rb_ThinCrust.Tag : rb_ThickCrust.Tag).ToString();
            float.TryParse(CrustPriceInString, out float CrustPrice);
            return CrustPrice;
        }
        private float _CalculateToppingsPrice()
        {
            float TotalToppingsPrice = 0;

            if (ckb_Tomatoes.Checked)
                TotalToppingsPrice += float.Parse(ckb_Tomatoes.Tag.ToString());
            if (ckb_Onion.Checked)
                TotalToppingsPrice += float.Parse(ckb_Onion.Tag.ToString());
            if (ckb_Olive.Checked)
                TotalToppingsPrice += float.Parse(ckb_Olive.Tag.ToString());
            if (ckb_Mushrooms.Checked)
                TotalToppingsPrice += float.Parse(ckb_Mushrooms.Tag.ToString());
            if (ckb_Pepper.Checked)
                TotalToppingsPrice += float.Parse(ckb_Pepper.Tag.ToString());
            if (ckb_Cheese.Checked)
                TotalToppingsPrice += float.Parse(ckb_Cheese.Tag.ToString());

            return TotalToppingsPrice;
        }

        private float _CalcTotalOrderPrice()
        {
            return _CalcPizzaSizePrice() + _CalcPizzaCrustType() + _CalculateToppingsPrice();
        }


        #endregion



        #region Events  



        private void initializeControls()
        {
            LB_OrderNumber.Text = $"A{Rand.Next(3000)}";

            LB_DateAndTime.Text = string.Empty;
            LB_Size.Text = string.Empty;
            LB_Price.Text = string.Empty;
            LB_CrustType.Text = string.Empty;
            LB_EatPlace.Text = string.Empty;

            rb_EatIn.Checked = true;
            rb_ThinCrust.Checked = true;
            rb_Small.Checked = true;

            _Refresh();
        }

        private void Pizza_Main_Load(object sender, EventArgs e)
        {
            initializeControls();
        }

        private string _GetPizzaSizeString()
        {
            return (rb_Small.Checked) ? rb_Small.Text : (rb_Medium.Checked) ? rb_Medium.Text : rb_Large.Text;
        }

        private string _GetPizzaCrustTypeString()
        {
            return (rb_ThickCrust.Checked) ? rb_ThickCrust.Text : rb_ThinCrust.Text;
        }
        private string _GetPizzaEatPlaceString()
        {
            return (rb_EatIn.Checked) ? rb_EatIn.Text : rb_TakeOut.Text;
        }
        private string _GetPizzaToppingsInString()
        {

            string Seperator = " | ";
            string ToppingsString = Seperator;

            if (ckb_Cheese.Checked)
                ToppingsString += ckb_Cheese.Text + Seperator;
            if (ckb_Tomatoes.Checked)
                ToppingsString += ckb_Tomatoes.Text + Seperator;
            if (ckb_Onion.Checked)
                ToppingsString += ckb_Onion.Text + Seperator;
            if (ckb_Olive.Checked)
                ToppingsString += ckb_Olive.Text + Seperator;
            if (ckb_Mushrooms.Checked)
                ToppingsString += ckb_Mushrooms.Text + Seperator;
            if (ckb_Pepper.Checked)
                ToppingsString += ckb_Pepper.Text + Seperator;


            return ToppingsString.Substring(0, (ToppingsString.Length - Seperator.Length));
        }

        private void _Refresh()
        {
            LB_Price.Text = ((int)_CalcTotalOrderPrice()).ToString() + '$';

            LB_Size.Text = _GetPizzaSizeString();
            LB_CrustType.Text = _GetPizzaCrustTypeString();
            LB_EatPlace.Text = _GetPizzaEatPlaceString();
            LB_Toppings.Text = _GetPizzaToppingsInString();
        }

        private void _OrderPizzaControls()
        {
            LB_DateAndTime.Text = DateTime.Now.ToString("MM/dd/yyyy  -  hh:mm tt");
            GB_Size.Enabled = false;
            GB_Toppings.Enabled = false;
            GB_CrustType.Enabled = false;
            GB_EatPlace.Enabled = false;

        }

        private void Btn_NewOrder_Click(object sender, EventArgs e)
        {
            GB_Size.Enabled = true;
            GB_Toppings.Enabled = true;
            GB_CrustType.Enabled = true;
            GB_EatPlace.Enabled = true;

            initializeControls();

            ckb_Tomatoes.Checked = false;
            ckb_Onion.Checked = false;
            ckb_Olive.Checked = false;
            ckb_Mushrooms.Checked = false;
            ckb_Pepper.Checked = false;
            ckb_Cheese.Checked = false;


            Btn_OrderPizza.Enabled = true;
        }

        private void Btn_OrderPizza_Click(object sender, EventArgs e)
        {

            _OrderPizzaControls();
            _AddNewOrder();

            Btn_OrderPizza.Enabled = false;
        }

        private void AnyButton_CheckedChanged(object sender, EventArgs e)
        {
            _Refresh();
        }

        private void Btn_OrdersQueue_Click(object sender, EventArgs e)
        {
            OrdersQueueFrm OrdersQueue = new OrdersQueueFrm();
            this.Visible = false;
            OrdersQueue.ShowDialog();

            this.Visible = true;
        }

        #endregion



        private void _AddNewOrder()
        {
            clsPizza NewPizzaOrder = new clsPizza();

            NewPizzaOrder.OrderNumber = LB_OrderNumber.Text;
            NewPizzaOrder.PizzaSize = LB_Size.Text;
            NewPizzaOrder.CrustType = LB_CrustType.Text;
            NewPizzaOrder.Toppings = LB_Toppings.Text;
            NewPizzaOrder.WhereToEat = LB_EatPlace.Text;
            NewPizzaOrder.Price = short.Parse(LB_Price.Text.Substring(0, LB_Price.Text.Length - 1));
            NewPizzaOrder.OrderDate = LB_DateAndTime.Text;

            Pizza_Main.OrdersQueue.Enqueue(NewPizzaOrder);
        }
    }
}