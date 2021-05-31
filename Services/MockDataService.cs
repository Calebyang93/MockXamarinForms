using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XFApp1.Models;

namespace XFApp1.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> EnteredOn;
        public List<Item> RemovedOn;
        public List<Item> UpdatedOn;

        public MockDataStore()
        {
            EnteredOn = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
            };

            RemovedOn = new List<Item>()
            {
                new Item {Id = Guid.NewGuid().ToString(), Text ="First item Entered on 2 December 2020", Description="First Item is removed on 1 December 2020" },
                new Item {Id = Guid.NewGuid().ToString(), Text = "Second item Entered on 1 August 2019", Description="Second Item is removed on 1 January 2020" },
                new Item {Id = Guid.NewGuid().ToString(), Text = "Third item entered on 5 July 2018", Description="Third Item is removed on 2 February 2019" },
                new Item {Id = Guid.NewGuid().ToString(), Text = "Fourth item entered on 3 August 2017", Description="Fourth Item is removed on 1 February 2018 " },
                new Item {Id = Guid.NewGuid().ToString(), Text = "Fifth item entered on 2 April 2015", Description="Fifth Item is removed on 5 October 2015" },
                new Item {Id = Guid.NewGuid().ToString(), Text = "Sixth item entered on 1 July 2014", Description="Sixth Item is removed on 2 December 2015" }
            };

            UpdatedOn = new List<Item>()
            {
                new Item {Id = Guid.NewGuid().ToString(), Text ="First item Entered on 2 December 2020", Description="First Item is updated on 1 December 2022" },
                new Item {Id = Guid.NewGuid().ToString(), Text = "Second item Entered on 1 August 2019", Description="Second Item is updated on 1 January 2023" },
                new Item {Id = Guid.NewGuid().ToString(), Text = "Third item entered on 5 July 2018", Description="Third Item is updated on 2 February 2020" },
                new Item {Id = Guid.NewGuid().ToString(), Text = "Fourth item entered on 3 August 2017", Description="Fourth Item is updated on 1 February 2019 " },
                new Item {Id = Guid.NewGuid().ToString(), Text = "Fifth item entered on 2 April 2015", Description="Fifth Item is updated on 5 October 2017" },
                new Item {Id = Guid.NewGuid().ToString(), Text = "Sixth item entered on 1 July 2014", Description="Sixth Item is updated on 2 December 2016" }
            };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            EnteredOn.Add(item);
            RemovedOn.Add(item);
            UpdatedOn.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = EnteredOn.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            EnteredOn.Remove(oldItem);
            EnteredOn.Add(item);
            RemovedOn.Remove(oldItem);
            RemovedOn.Add(item);
            UpdatedOn.Remove(oldItem);
            UpdatedOn.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = EnteredOn.Where((Item arg) => arg.Id == id).FirstOrDefault();
            EnteredOn.Remove(oldItem);
            var oldItem2 = RemovedOn.Where((Item arg) => arg.Id == id).FirstOrDefault();
            RemovedOn.Remove(oldItem);
            var oldItem3 = UpdatedOn.Where((Item arg) => arg.Id == id).FirstOrDefault();
            UpdatedOn.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(EnteredOn.FirstOrDefault(s => s.Id == id));
           // return await Task.FromResult(UpdatedOn.FirstOrDefault(s => s.Id == id));
           // return await Task.FromResult(RemovedOn.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(EnteredOn);
            //return await Task.FromResult(UpdatedOn);
            // return await Task.FromResult(RemovedOn);
        }
    }
}
