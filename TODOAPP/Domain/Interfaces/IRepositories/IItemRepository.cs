using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODOAPP.Models;

namespace TODOAPP.Domain.Interfaces.IRepositories
{
    public interface IItemRepository
    {
        Task<List<ItemData>> GetItemsAsync();
        Task<ItemData> AddItemAsync(ItemData item);
        Task<bool>DeleteItem(int id);
        Task<ItemData> GetItemByIdAsync(int id);
        Task<bool> UpdateItem(int id, ItemData item);
    }
}