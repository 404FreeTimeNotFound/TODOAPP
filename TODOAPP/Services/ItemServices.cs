using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using TODOAPP.Data.Services;
using TODOAPP.Domain.Interfaces.IRepositories;
using TODOAPP.Models;
using TODOAPP.Utilies;

namespace TODOAPP.Repositories
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<BaseResponse> AddItemAsync(ItemData item)
        {
            var CreatedItem = await _itemRepository.AddItemAsync(item);
            if(CreatedItem == null)
            {
                return new BaseResponse(isSuccess:false, errors: new List<string> { "Failed to add item" });
            }

            return new BaseResponse(data:CreatedItem,errors: new List<string> { "Item added successfully" });
        }

        public async Task<BaseResponse> DeleteItem(int id)
        {
            var IsDeleted = await _itemRepository.DeleteItem(id);
            if(!IsDeleted)
            {
                return new BaseResponse(isSuccess:false, errors: new List<string> { "Failed to delete item" });
            }
            return new BaseResponse(errors: new List<string> { "Item deleted successfully" });
        }
        

        public async Task<BaseResponse> GetItemByIdAsync(int id)
        {

            var item= await _itemRepository.GetItemByIdAsync(id);
            if(item == null)
            {
                return new BaseResponse(isSuccess:false, errors: new List<string> { "Item not found" });
            }

            return new BaseResponse(data:item);
        }

        public async Task<BaseResponse> GetItemsAsync()
        {
            var items= await _itemRepository.GetItemsAsync();
            return new BaseResponse(data:items);
        }

        public async Task<BaseResponse> UpdateItem(int id, ItemData item)
        {
            var IsUpdated = await _itemRepository.UpdateItem(id, item);
            if(!IsUpdated)
            {
                return new BaseResponse(isSuccess:false, errors: new List<string> { "Failed to update item" });
            }
            return new BaseResponse(data:item, errors: new List<string> { "Item updated successfully" });
        }

        
    }
}