using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BartenderApp.Model;

namespace BartenderApp.Controller
{
    public class BartenderControl
    {
        public DataTable GetDrinkOrders()
        {
            Db data = new Db();
            return data.GetDrinkOrderQueue();
        }

        public DrinkOrder GetDrinkOrder(int orderId)
        {
            Db data = new Db();
            return data.GetDrinkOrder(orderId);
        }

        public DataTable GetDrinkMenu()
        {
            Db data = new Db();
            return data.GetDrinkMenu();
        }

        public bool SaveOrder(int tableNumber, int drinkId)
        {
            bool saveOrder = true;
            Db data = new Db();
            try
            {
                data.SaveOrder(tableNumber, drinkId);
            }
            catch
            {
                saveOrder = false;
            }
            return saveOrder;
        }

        public bool ReinitializeDb()
        {
            bool initDb = true;
            Db data = new Db();
            try
            {
                data.ReinitializeDatabase();
            }
            catch
            {
                initDb = false;
            }
            return initDb;
        }
    }
}
