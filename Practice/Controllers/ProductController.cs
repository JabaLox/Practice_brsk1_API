using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice.Model;

namespace Practice.Controllers
{
    [Route("product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("list")]
        public ActionResult GetListProduct()
        {
            var list = PracticStoreContext._context.Products.Include(x => x.ManufactureNavigation).Include(x => x.CategoryNavigation);

            return Ok(list);
        }

        [Authorize]
        [HttpGet("info/{article}")]
        public ActionResult GetProduct(string article)
        {
            var list = PracticStoreContext._context.Products.Include(x => x.ManufactureNavigation)
                .Include(x => x.CategoryNavigation).FirstOrDefault(x => x.ArticleProduct == article);

            return Ok(list);
        }

        [Authorize]
        [HttpPost("create")]
        public ActionResult CreateProduct( Product product) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                PracticStoreContext._context.Products.Add(product);
                PracticStoreContext._context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok(true);
        }

        [Authorize]
        [HttpGet("{article}")]
        public ActionResult GetInfoProduct(string article)
        {
            var product = PracticStoreContext._context.Products.FirstOrDefault(x=>x.ArticleProduct==article);

            return Ok(product);
        }

        [Authorize]
        [HttpPut("edit/{article}")]
        public ActionResult PutProduct(string article, Product product)
        {
            var prd = PracticStoreContext._context.Products.FirstOrDefault(x => x.ArticleProduct == article);

            prd.DiscountProduct = product.DiscountProduct;
            prd.NameProduct = product.NameProduct;
            prd.DesriptionProduct = product.DesriptionProduct;
            prd.CostProduct = product.CostProduct;
            prd.Category = product.Category;
            prd.Manufacture = product.Manufacture;
            prd.CountInStockProduct = product.CountInStockProduct;

            PracticStoreContext._context.SaveChanges();

            return Ok(true);
        }

        [Authorize]
        [HttpDelete("delete/{article}")]
        public ActionResult DeleteProduct(string article)
        {
            var delete = PracticStoreContext._context.Products.FirstOrDefault(x => x.ArticleProduct == article);

            try
            {
                PracticStoreContext._context.Products.Remove(delete);
                PracticStoreContext._context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }
            return Ok(true);
        }

        [HttpGet("category")]
        public ActionResult GetCategory()
        {
            var list = PracticStoreContext._context.Categories;

            return Ok(list);
        }

        [HttpGet("manufacture")]
        public ActionResult GetManufacture()
        {
            var list = PracticStoreContext._context.Manufactures;

            return Ok(list);
        }

    }
}
