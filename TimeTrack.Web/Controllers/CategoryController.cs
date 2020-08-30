using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TimeTrack.Web.Handlers.Category;
using TimeTrack.Web.Models;

namespace TimeTrack.Web.Controllers
{
    [Route("/category")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMediator _mediator;
        public CategoryController(UserManager<IdentityUser> userManager, IMediator  mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }
        
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var resp = await _mediator.Send(new ListQuery
            {
                UserId = _userManager.GetUserId(User)
            });
            return View(resp);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var resp = await _mediator.Send(new FetchQuery
            {
                CategoryId = id,
                UserId = _userManager.GetUserId(User)
            });
            
            if (resp == null)
                return NotFound();
            
            return View("CreateOrEdit", resp);
        }
        
        [Route("{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] Category model)
        {
            ModelState.Remove(nameof(Category.UserId));
            ModelState.Remove(nameof(Category.User));
            
            model.UserId = _userManager.GetUserId(User);
            var resp = await _mediator.Send(new EditCommand
            {
                Category = model,
            });
            
            if (resp == null)
                return NotFound();
            
            return View("CreateOrEdit", resp);
        }
        
        [Route("Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View("CreateOrEdit");
        }
        
        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Category model)
        {
            ModelState.Remove(nameof(Category.UserId));
            ModelState.Remove(nameof(Category.User));
            if (!ModelState.IsValid)
            {
                return View("CreateOrEdit");
            }
            
            model.UserId = _userManager.GetUserId(User);
            var resp = await _mediator.Send(new CreateCommand
            {
                Category = model
            });

            return RedirectToAction("Edit", new {id = resp.Id});
        }
    }
}