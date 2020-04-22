using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using P = AFN_WF_C.PCClient.Procesos;

namespace AFN_WF_C.PCClient.Vistas.Busquedas
{
    public partial class PriceQuantitySetter : AFN_WF_C.PCClient.FormBase
    {
        private int _Quantity;
        private decimal _UnitPrice;
        private decimal _TotalPrice;
        private string _UoE;
        public PriceQuantitySetter(int Quantity)
        {
            InitializeComponent();
            _Quantity = Quantity;
            tbCantidad.Text = Quantity.ToString();
            tbCantidad.Enabled = false;
            _UnitPrice = 0;
            _TotalPrice = 0;
            _UoE = string.Empty;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbUnitPrice.Text))
            {
                P.Mensaje.Advert("No ha establecido precio unitario");
                tbUnitPrice.Focus();
                return;
            }
            if (string.IsNullOrEmpty(tbTotalPrice.Text))
            {
                P.Mensaje.Advert("No ha establecido precio total");
                tbTotalPrice.Focus();
                return;
            }
            this.DialogResult = DialogResult.OK;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private bool CheckStts { get { return this.DialogResult == DialogResult.OK; } }

        public decimal UnitPrice { get { return (CheckStts ? _UnitPrice : 0); } }
        public decimal TotalPrice { get { return (CheckStts ? _TotalPrice : 0); } }
        public string UoE { get { return (CheckStts?_UoE:string.Empty); } }

        private void PriceSet_Leave(object sender, EventArgs e)
        {
            if (typeof(TextBox) == sender.GetType())
            {
                var Initial = (TextBox)sender;
                decimal EnterValue = (decimal)Initial.Tag;
                TextBox CounterPart;
                decimal thisAmount, otherAmount = 0;
                if (decimal.TryParse(Initial.Text, out thisAmount))
                {
                    if (Initial.Name == "tbUnitPrice")
                    {
                        CounterPart = tbTotalPrice;
                        if (EnterValue != thisAmount)
                            _UoE = "U";
                        _UnitPrice = thisAmount;
                        _TotalPrice = _Quantity * thisAmount;
                        otherAmount = _TotalPrice;
                    }
                    else
                    {
                        CounterPart = tbUnitPrice;
                        if (EnterValue != thisAmount)
                            _UoE = "E";
                        _TotalPrice = thisAmount;
                        _UnitPrice = thisAmount / _Quantity;
                        otherAmount = _UnitPrice;
                    }
                    Initial.Text = thisAmount.ToString("#,##0.0###");
                    CounterPart.Text = otherAmount.ToString("#,##0.0###");
                    //P.Mensaje.Info(_UoE);
                }
                else
                {
                    tbUnitPrice.Text = "0.0";
                    tbTotalPrice.Text = "0.0";
                } 
            }
        }


        private void PriceSet_Enter(object sender, EventArgs e)
        {
            if (typeof(TextBox) == sender.GetType())
            {
                var Initial = (TextBox)sender;
                decimal thisAmount;
                if (decimal.TryParse(Initial.Text, out thisAmount))
                {
                    Initial.Tag = thisAmount;
                    Initial.Text = thisAmount.ToString("0.0###");
                }
                else
                {
                    Initial.Text = string.Empty;
                    Initial.Tag = (decimal)0;
                }
                Initial.Select(0, Initial.Text.Length);
            }
        }
    }
}
