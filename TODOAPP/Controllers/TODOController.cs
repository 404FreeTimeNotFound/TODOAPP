using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TODOAPP.Controllers.Data;
using TODOAPP.Models;

namespace TODOAPP.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TODOController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public TODOController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet("get-items")]
        public async Task<IActionResult> GetItems()
        {
            var items = await _context.Items.ToListAsync();
            return Ok(items);

        }
        [HttpGet("get-item/{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            return item != null ? Ok(item) : NotFound(new { message = "Item not found" });
        }

        [HttpPost("add-item")]
        public async Task<IActionResult> AddItem([FromBody] ItemData item)
        {
            await _context.Items.AddAsync(item);
            var state = await _context.SaveChangesAsync();
            return state > 0 ? CreatedAtAction(nameof(GetItems), new { id = item.id }, item) :
                BadRequest(new { message = "Failed to add item" });
        }

        [HttpPut("update-item/{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] ItemData item)
        {
            var existingItem = await _context.Items.FindAsync(id);
            if (existingItem == null)
            {
                return NotFound(new { message = "Item not found" });
            }

            existingItem.title = item.title;
            existingItem.description = item.description;
            existingItem.done = item.done;

            _context.Items.Update(existingItem);
            var state = await _context.SaveChangesAsync();
            return state == 1 ? Ok(existingItem) :
                BadRequest(new { message = "Failed to update item" });
        }

        [HttpDelete("delete-item/{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.id == id);
            if (item == null)
            {
                return NotFound(new { message = "Item not found" });
            }
            _context.Items.Remove(item);
            var state = await _context.SaveChangesAsync();
            return state == 1 ? Ok(new { message = "Item deleted successfully" }) :
                BadRequest(new { message = "Failed to delete item" });
        }

    }
}