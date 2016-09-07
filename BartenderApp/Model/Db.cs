using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace BartenderApp.Model
{
    public class Db
    {
        #region Database
            //private string Conn = ConfigurationManager.ConnectionStrings["bdbWk"].ConnectionString;
            private string Conn = ConfigurationManager.ConnectionStrings["bdb"].ConnectionString;
        #endregion
        #region Database Queries
        private string DrinkMenuQuery()
        {
            return @"Select DrinkID, DrinkName from Drink";
        }
        /// <summary>
        /// This is the query used for the Orders queue.
        /// </summary>
        /// <returns>Returns the SQL for the query.</returns>
        private string OrderQueueQuery()
        {
            return @" select  o.DrinkOrderID as [Order No.]
                            , o.DrinkID as [Code]
		                    , d.DrinkName as [Cocktail]
		                    , o.TableNumber as [Table No.]
		                    , o.OrderDateTime as [Order Date/Time]
                      from	DrinkOrder o
                      join	Drink d
                      on	o.DrinkID=d.DrinkID
                      order by o.DrinkOrderID";
        }

        /// <summary>
        /// This is the query used for a single order.
        /// </summary>
        /// <returns>Returns the SQL for the query.</returns>
        private string OrderQuery(int orderId)
        {
            return String.Format(@" select  o.DrinkOrderID
                                            o.DrinkID
		                                  , o.OrderDateTime
		                                  , o.TableNumber
		                                  , d.DrinkName
                                      from	DrinkOrder o
                                      join	Drink d
                                      on	o.DrinkID=d.DrinkID
                                      where d.DrinkOrderID={0}",orderId);
        }

        /// <summary>
        /// This is the query used to insert Orders into the Order table.
        /// </summary>
        /// <returns>Returns the SQL for the query.</returns>
        private string OrderInsertQuery(int tableNumber, int drinkId)
        {
            return String.Format("insert DrinkOrder (OrderDateTime, TableNumber, DrinkID) values (getdate(),{0},{1})", tableNumber.ToString(), drinkId.ToString());
        }

        /// <summary>
        /// This is the query used for the Ingredients list.
        /// </summary>
        /// <returns>Returns the SQL for the query.</returns>
        private string DrinkIngredientsQuery(int drinkId)
        {
            return String.Format("select IngredientName from Ingredient i join DrinkIngredient di on i.IngredientId=di.IngredientId where di.DrinkID={1})", drinkId.ToString());
        }     
        #endregion

        /// <summary>
        /// This resets the database, removing all orders.
        /// </summary>
        public void ReinitializeDatabase()
        {
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                using (SqlCommand command = new SqlCommand("spInitializeDb", conn))
                {
                    command.CommandType = CommandType.Text;
                    conn.Open();
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        public DataTable GetDrinkMenu()
        {
            DataTable dtMenu = new DataTable();

            using (SqlConnection conn = new SqlConnection(Conn))
            {
                using (SqlCommand command = new SqlCommand(DrinkMenuQuery(), conn))
                {
                    command.CommandType = CommandType.Text;
                    conn.Open();
                    dtMenu.Load(command.ExecuteReader());
                }
                conn.Close();
            }

            return dtMenu;
        }
        
        public DataTable GetDrinkOrderQueue()
        {
            DataTable dtOrders = new DataTable();

            using (SqlConnection conn = new SqlConnection(Conn))
            {
                using (SqlCommand command = new SqlCommand(OrderQueueQuery(), conn))
                {
                    command.CommandType = CommandType.Text;
                    conn.Open();
                    dtOrders.Load(command.ExecuteReader());
                }
                conn.Close();
            }

            return dtOrders;
        }

        public void SaveOrder(int tableNumber, int drinkId)
        {
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                using (SqlCommand command = new SqlCommand(OrderInsertQuery(tableNumber, drinkId), conn))
                {
                    command.CommandType = CommandType.Text;
                    conn.Open();
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }

        }

        public DrinkOrder GetDrinkOrder(int orderID)
        {
            DataTable dtDrinks = new DataTable();
            DataTable dtIngredients = new DataTable();
            DrinkOrder d = new DrinkOrder();

            using (SqlConnection conn = new SqlConnection(Conn))
            {
                using (SqlCommand command1 = new SqlCommand(OrderQuery(orderID), conn))
                {
                    command1.CommandType = CommandType.Text;
                    conn.Open();
                    dtDrinks.Load(command1.ExecuteReader());

                    foreach (DataRow row in dtDrinks.Rows) // should only be one row.
                    {
                        d.DrinkOrderID = (int)row["DrinkOrderID"];
                        d.DrinkID = (int)row["DrinkID"];
                        d.OrderDateTime = (DateTime)row["OrderDateTime"];
                        d.DrinkName = row["DrinkName"].ToString();
                    }
                }
                using (SqlCommand command2 = new SqlCommand(DrinkIngredientsQuery(d.DrinkID), conn))
                {
                    command2.CommandType = CommandType.Text;
                    dtIngredients.Load(command2.ExecuteReader());
                    
                    StringBuilder s = new StringBuilder();

                    foreach (DataRow row in dtIngredients.Rows)
                    {
                        s.AppendFormat(row["IngredientName"].ToString());
                    }

                    d.Ingredients = s.ToString();
                }
                conn.Close();
            }

            return d;
        }
    }
}
