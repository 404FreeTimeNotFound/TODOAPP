using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TODOAPP.Controllers.Data;
using TODOAPP.Data.Services;
using TODOAPP.Models;

namespace TODOAPP.Controllers.V1_0
{
    [Authorize]
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TODOController : ControllerBase
    {
        private readonly IItemService _itemService;
        public TODOController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("get-items")]
        public async Task<IActionResult> GetItems()
        {
            return Ok(await _itemService.GetItemsAsync());
        }
        
        [HttpGet("get-item/{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
           return Ok(await _itemService.GetItemByIdAsync(id));
        }

        [HttpPost("add-item")]
        public async Task<IActionResult> AddItem([FromBody] ItemData item)
        {
           return Ok(await _itemService.AddItemAsync(item));
        }

        [HttpPut("update-item/{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] ItemData item)
        {
            return Ok(await _itemService.UpdateItem(id, item));
        }

        [HttpDelete("delete-item/{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
           return Ok(await _itemService.DeleteItem(id));
        }

    }
}