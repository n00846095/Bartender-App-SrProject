using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BartenderApp.Controller;

namespace BartenderApp
{
    public partial class Default : System.Web.UI.Page
    {
        /// <summary>
        /// Make both the Orders queue and the order form invisible.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                divOrderQ.Visible = false;
                divPlaceOrder.Visible = false;
                LoadMenu();
            }
        }

        /// <summary>
        /// Load the drink menu into the dropdown in the order form.
        /// </summary>
        private void LoadMenu()
        {
            ddDrinkSelection.DataSource = GetDrinkMenu();
            ddDrinkSelection.DataValueField = "DrinkID";
            ddDrinkSelection.DataTextField = "DrinkName";
            ddDrinkSelection.DataBind();
        }

        /// <summary>
        /// Load any existing orders into the order grid.
        /// </summary>
        private void LoadOrders()
        {
            gvOrderQ.DataSource = GetDrinkOrders();
            gvOrderQ.DataBind();
        }

        /// <summary>
        /// Method to get the list of orders.
        /// </summary>
        /// <returns>Returns data table containing drink orders.</returns>
        protected DataTable GetDrinkOrders()
        {
            BartenderControl c = new BartenderControl();
            return c.GetDrinkOrders();
        }

        /// <summary>
        /// Method to get the list of drink menu options.
        /// </summary>
        /// <returns>Returns data table containing the list of menu options.</returns>
        protected DataTable GetDrinkMenu()
        {
            BartenderControl c = new BartenderControl();
            return c.GetDrinkMenu();
        }
        
        /// <summary>
        /// This controls the paging for the grid so that when the max
        /// number of items, a new grid page is created. This allows 
        /// toggling back and forth.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOrderQ.PageIndex = e.NewPageIndex;
            gvOrderQ.DataBind();
            LoadOrders();
            divOrderQ.Visible = true;
            divPlaceOrder.Visible = false; 
        }

        /// <summary>
        /// This is the button the server clicks to place an order.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            divPlaceOrder.Visible = true;
            divOrderQ.Visible = false;
        }

        /// <summary>
        /// This is the button the bartender cicks to view all orders.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnViewOrders_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            LoadOrders();
            divOrderQ.Visible = true;
            divPlaceOrder.Visible = false;
            if (gvOrderQ.Rows.Count == 0)
            {
                lblMessage.Text = "No orders found. Please submit an order.";
                btnReinitDb.Visible = false;
            }
            else
            {
                lblMessage.Text = "";
                btnReinitDb.Visible = true;
            }
        }

        /// <summary>
        /// Once the order form is completed, this button saves the order to the db.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BartenderControl c = new BartenderControl();
            int tableNum = Convert.ToInt32(ddTableNum.SelectedValue);
            int drinkId = Convert.ToInt32(ddDrinkSelection.SelectedValue);
            if (c.SaveOrder(tableNum, drinkId))
            {
                lblMessage.Text = "Order submitted successfully!";
                gvOrderQ.DataBind();
            }
            else
            {
                lblMessage.Text = "Error: Order was not submitted.";
            }
        }

        /// <summary>
        /// In case you want to start over testing the app,
        /// this button clears all orders from the database
        /// and starts fresh.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReinitDb_Click(object sender, EventArgs e)
        {
            BartenderControl c = new BartenderControl();
            if (c.ReinitializeDb())
            {
                lblMessage.Text = "Database reinitialized.";
                gvOrderQ.DataBind();
            }
            else
            {
                lblMessage.Text = "Error reinitializing the database.";
            }
        }
    }
}