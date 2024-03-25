using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice.Model;

namespace Practice.Controllers
{
    [Route("order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet("list")]
        public IActionResult GetListOrder() 
        {
            var ordprd = PracticStoreContext._context.OrderProducts.Include(x => x.ArticleProductNavigation)
                .Include(x => x.IdOrderNavigation).Include(x=>x.IdOrderNavigation.UserOrderNavigation);

            return Ok(ordprd);
        }

        [HttpPost("create")]
        public IActionResult CreateOrder(int idUser, int count, string article) 
        {
            int? countInStock = PracticStoreContext._context.Products.FirstOrDefault(x => x.ArticleProduct == article).CountInStockProduct;
            if (countInStock<count) 
            {
                return BadRequest("Товара не хватает");
            }
            int id = PracticStoreContext._context.Orders.Max(x => x.IdOrder) + 1;

            Order order = new Order
            {
                IdOrder = id,
                UserOrder = idUser,
                OrderDate = DateTime.UtcNow
            };

            PracticStoreContext._context.Orders.Add(order);

            OrderProduct ordprd = new OrderProduct
            {
                IdOrder = id,
                ArticleProduct = article,
                CountProduct = count,
            };

            PracticStoreContext._context.OrderProducts.Add(ordprd);

            var pr = PracticStoreContext._context.Products.FirstOrDefault(x => x.ArticleProduct == article);
            pr.CountInStockProduct = pr.CountInStockProduct - count;

            PracticStoreContext._context.SaveChanges();


            return Ok(true);
        }
    }
}
