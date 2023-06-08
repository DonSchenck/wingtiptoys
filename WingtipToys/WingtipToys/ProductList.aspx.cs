using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WingtipToys.Models;
using System.Web.ModelBinding;
using System.Web.Routing;

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WingtipToys
{
    public partial class ProductList : System.Web.UI.Page
    {
        // Use microservice
        static HttpClient client = new HttpClient();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        static async Task<IQueryable<Product>> GetProductsAsync()
        {
            IQueryable<Product> products = null;
            Uri uri = new Uri("http://wttproductlist:8080/productlist");
            HttpResponseMessage response = await client.GetAsync(uri,HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
      
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                var p = JsonConvert.DeserializeObject<List<Product>>(content);
                products = p.AsQueryable();
            }
            return products;

        }
        public IQueryable<Product> GetProducts(
                        [QueryString("id")] int? categoryId,
                        [RouteData] string categoryName)
        {
            // use microservice
            ///IQueryable<Product> query = GetProductsAsync().Result;

            // Use EF
            var _db = new WingtipToys.Models.ProductContext();
            IQueryable<Product> query = _db.Products;

            if (categoryId.HasValue && categoryId > 0)
            {
                query = query.Where(p => p.CategoryID == categoryId);
            }

            if (!String.IsNullOrEmpty(categoryName))
            {
                query = query.Where(p =>
                                    String.Compare(p.Category.CategoryName,
                                    categoryName) == 0);
            }
            return query;
        }
    }
}