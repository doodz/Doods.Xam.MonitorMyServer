using Doods.Framework.Repository.Std.Interfaces;
using Doods.Framework.Repository.Std.Tables;
using Doods.Framework.Std;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Services
{
    public interface IDataProvider
    {
        //IRepository Repository { get; }
        Task<long> InsertHostAsync(Host host);
        Task<Host> FindHostAsync(Host host);
        Task<Host> FindHostAsync(long hostId);
        Task UpdateHostAsync(Host host);
        Task UpdateCustomCommandAsync(CustomCommandSsh customCommandSsh);
        Task<int> InsertCustomCommandAsync(CustomCommandSsh customCommandSsh);
        Task<IEnumerable<Host>> GetHostsAsync();
        Task<int> CountHostAsync();
        Task DeleteHostAsync(Host host);
        Task<IEnumerable<T>> GetItemsAsync<T>() where T : TableBase, new();
        Task DeleteItemAsync(TableBase item);
    }

    public class MessengerKeys
    {
        public const string AddItem = "AddItem";
        public const string RemoveItem = "RemoveItem";
        public const string UpdateItem = "UpdateItem";
        public const string ItemChanged = "ItemChanged";

    }


    public class DataProvider: NotifyPropertyChangedBase, IDataProvider
    {
        private IRepository _repository;
        private readonly ITimeWatcher _timer;
        public DataProvider(IRepository repository)
        {
            _timer = new TimeWatcher();
            _repository = repository;
        }

        public async Task<IEnumerable<Host>> GetHostsAsync()
        {
            return await _repository.GetAllAsync<Host>(_timer);
        }
        public async Task<IEnumerable<T>> GetItemsAsync<T>() where T: TableBase,new()
        {
            return await _repository.GetAllAsync<T>(_timer);
        }
        public async Task<long> InsertHostAsync(Host host)
        {
            var result = await _repository.InsertAsync(_timer, host).ConfigureAwait(false);
            MessagingCenter.Send(this, MessengerKeys.AddItem, host);
            MessagingCenter.Send(this, MessengerKeys.ItemChanged, host);
            return result;

        }

        public Task<int> InsertCustomCommandAsync(CustomCommandSsh customCommandSsh)
        {
            return  InsertItemAsync(customCommandSsh);
           

        }
        public  Task<Host> FindHostAsync(Host host)
        {
            return  FindHostAsync(host.Id.Value);
        }

        public  Task<Host> FindHostAsync(long hostId)
        {
            return _repository.FindAsync<Host>(_timer, hostId);
        }

        public Task<int> CountHostAsync()
        {
            return _repository.CountAsync<Host>(_timer);
        }
        public async Task UpdateHostAsync(Host host)
        {
            await UpdateItemAsync(host).ConfigureAwait(false);


        }

        public async Task UpdateCustomCommandAsync(CustomCommandSsh customCommandSsh)
        {
            await UpdateItemAsync(customCommandSsh).ConfigureAwait(false);

        }

        public async Task DeleteHostAsync(Host host)
        {
            await DeleteItemAsync(host).ConfigureAwait(false);
           
        }

        public async Task<int> InsertItemAsync(TableBase item)
        {
            var result =await _repository.InsertAsync(_timer, item).ConfigureAwait(false);
            MessagingCenter.Send(this, MessengerKeys.AddItem, item);
            MessagingCenter.Send(this, MessengerKeys.ItemChanged, item);
            return result;
        }

        public async Task UpdateItemAsync(TableBase item)
        {
            await _repository.UpdateAsync(_timer, item);
            MessagingCenter.Send(this, MessengerKeys.UpdateItem, item);
            MessagingCenter.Send(this, MessengerKeys.ItemChanged, item);
        }

        public async Task DeleteItemAsync(TableBase item)
        {
            await _repository.DeleteAsync(_timer, item).ConfigureAwait(false);
            MessagingCenter.Send(this, MessengerKeys.RemoveItem, item);
            MessagingCenter.Send(this, MessengerKeys.ItemChanged, item);
        }
    }
}
