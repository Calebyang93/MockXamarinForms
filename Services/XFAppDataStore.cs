using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XFApp1.Models;
using XFApp1.Services;

namespace XFAApp1.Services
{
    public class XFAppDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public XFAppDataStore()
        {
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "name of xfa object", Description="Enter the name of the xfa object" },
                new Item { Id = Guid.NewGuid().ToString(), Text = "type of xfa object", Description="Enter the category type of the xfa object" },
                new Item { Id = Guid.NewGuid().ToString(), Text = "cost of xfa object", Description="Enter the cost of the xfa objectr" },
                new Item { Id = Guid.NewGuid().ToString(), Text = "serial number of xfa object", Description="Enter this button for inserting of xfa object serial number" },
                new Item { Id = Guid.NewGuid().ToString(), Text = "access life of xfa object", Description="Enter the date of entry when xfa object was created" },
                new Item { Id = Guid.NewGuid().ToString(), Text = "access contributors of xfa object", Description="The access rights of the xfa object is given to the following contributors and users." },
                new Item { Id= Guid.NewGuid().ToString(), Text="roles for xfa object", Description="Enter the role for each {username} in this list. "},
                new Item { Id= Guid.NewGuid().ToString(), Text="permission rights for xfa objects", Description="{username} has these permission rights. They can either update data, read data or edit data or create new data objects."},
                new Item { Id= Guid.NewGuid().ToString(), Text="date of publishing for xfa objects", Description="{item} has been published on {dateTime}. Project 1 Details has been published in 3 May 2007 15:30:29."}
            };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);
            //await items.AddItemAsync(newitem);
            //return await Task.FromResult<Item>();
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
