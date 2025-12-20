using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODOAPP.Models;
using TODOAPP.Utilies;

namespace TODOAPP.Data.Services
{
    public interface IItemService
    {
        Task<BaseResponse> GetItemsAsync();
        Task<BaseResponse> GetItemByIdAsync(int id);
        Task<BaseResponse> AddItemAsync(ItemData item);
        Task<BaseResponse> UpdateItem(int id, ItemData item);
        Task<BaseResponse> DeleteItem(int id);
    }
}