using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;
using Todo.Models.Domain;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Todo.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "LoginTodo");
        }

        private readonly ItemContext newItemContext;
        public ItemController(ItemContext newItemContext)
        {
            this.newItemContext = newItemContext;
        }
        [HttpGet]
        public async Task<IActionResult> Read()
        {
            var items = await newItemContext.ItemTodos.ToListAsync();
            return View(items);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var item = await newItemContext.ItemTodos.FindAsync(id);
            if (item != null)
            {
                return View(item);
            }
            return RedirectToAction("Read");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateItemTodo incomingItem)
        {
            var newItem = new ItemTodo()
            {
                Id = Guid.NewGuid(),
                Title = incomingItem.Title,
                Description = incomingItem.Description
            };
            await newItemContext.ItemTodos.AddAsync(newItem);
            await newItemContext.SaveChangesAsync();
            return RedirectToAction("Read");
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var item = await newItemContext.ItemTodos.FindAsync(id);

            if (item != null)
            {
                var updateItem = new UpdateItemTodo()
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description
                };
                return View(updateItem);
            }
            return RedirectToAction("Read");
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateItemTodo updateItem)
        {
            var item = await newItemContext.ItemTodos.FindAsync(updateItem.Id);
            if (item != null)
            {
                item.Title = updateItem.Title;
                item.Description = updateItem.Description;

                await newItemContext.SaveChangesAsync();
            }
            return RedirectToAction("Read");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var delItem = await newItemContext.ItemTodos.FindAsync(id);
            if (delItem != null)
            {
                newItemContext.ItemTodos.Remove(delItem);
                await newItemContext.SaveChangesAsync();
            }
            return RedirectToAction("Read");
        }
    }
}
