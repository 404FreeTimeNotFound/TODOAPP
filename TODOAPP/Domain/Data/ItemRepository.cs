using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TODOAPP.Controllers.Data;
using TODOAPP.Domain.Interfaces.IRepositories;
using TODOAPP.Models;

namespace TODOAPP.Domain.Data
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApiDbContext _context;
        public ItemRepository(ApiDbContext context)
        {
            _context = context;
        }
        public async Task<ItemData> AddItemAsync(ItemData item)
        {
             await _context.Items.AddAsync(item);
             await _context.SaveChangesAsync();
             return item;
        }

        public async Task<bool> DeleteItem(int id)
        {
            var item=await _context.Items.FindAsync(id);
            if(item==null)
            {
                return false;
            }
            _context.Items.Remove(item);
            return await _context.SaveChangesAsync()>0;
        }

        public async Task<ItemData> GetItemByIdAsync(int id)
        {
            var item= await _context.Items.FindAsync(id);
            if(item== null)
            {
                return null!;
            }
            return item;
        }

        public async Task<List<ItemData>> GetItemsAsync()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<bool> UpdateItem(int id, ItemData item)
        {
            var existingItem= await _context.Items.FindAsync(id);
            if(existingItem== null)
            {
                return false;
            }

            existingItem.title=item.title;
            existingItem.description=item.description;
            existingItem.done=item.done;
            return await _context.SaveChangesAsync()>0;

        }
    }
}