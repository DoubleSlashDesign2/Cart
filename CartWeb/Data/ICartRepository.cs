using CartWeb.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartWeb.Data
{
    public interface ICartRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);

        IEnumerable<Order> GetAllOrders(bool includeItems);
        IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems);

        Order GetOrderById(string username, int id);
        void AddOrder(Order newOrder);

        bool SaveAll();
        void AddEntity(object model);
    }
}
