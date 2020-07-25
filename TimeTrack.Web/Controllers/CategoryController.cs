using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TimeTrack.Web.Controllers
{
    [Route("/category")]
    public class CategoryController
    {
        [Route("/")]
        [HttpGet]
        public async Task<IActionResult> ListCategories()
        {
            
        }
    }
}