using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using cm.frontend.core.Domain.Interfaces;
using cm.frontend.core.Domain.Objects;
using cm.frontend.core.Domain.Services.Realms.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Realms;

namespace cm.frontend.core.Domain.Services.Sync.Jobs.Base
{
    public abstract class Core<TModel> : ISyncJob
        where TModel : RealmObject, ISyncableEntity, new()
    {
        public string TargetApi { get; }

        protected Core(string targetApi)
        {
            TargetApi = targetApi;
        }

        // Sync the specified model type using the Realm db and rest services
        public async Task SyncGet(string token)
        {
            var restService = new Services.Rest.Base.Core<TModel>(TargetApi);
            var items = await restService.GetAsync(token);
            await SyncGet(items);
        }

        public async Task SyncPost(string token)
        {
            var realmService = new AsyncRealm<TModel>();
            var realmResults = realmService.GetRealmResults();
            var items = realmResults.Where(x => !x.Synced).ToList();

            var restService = new Services.Rest.Base.Core<TModel>(TargetApi);
            
            for (var i = 0; i < items.Count; i++)
            {
                if (items[i] == null) continue;
                await UpdateModel(items[i].LocalId);
                HttpResponseMessage reply;
                try
                {
                    reply = await restService.PostAsync(items[i], token);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    continue;
                }

                if (reply == null) continue;

                //var response = await restService.ParseHttpResponse(reply);
                var model = await restService.ParseResponseItem(reply);
                var id = model.Id;
                var itemLocalId = items[i].LocalId;
                
                await realmService.WriteAsync(tempRealm =>
                {
                    var localItem = tempRealm.Get(itemLocalId);
                    localItem.Id = id;
                    localItem.Synced = true;
                });
            }
        }

        protected async Task SyncGet(List<TModel> items)
        {

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                await SyncGet(item);
            }
        }

        protected async Task SyncGet(TModel item)
        {
            var realmService = new AsyncRealm<TModel>();
            if (item == null) return;

            var itemId = item.Id;
            TModel localItem;
            try
            {
                localItem = realmService.Get(x => x.Id == itemId);
            }
            catch (InvalidOperationException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                localItem = null;
            }

            if (localItem != null) // the item exists in the database and matches with the remote item
            {
                await realmService.WriteAsync(tempRealm =>
                {
                    var existingItem = tempRealm.Get(x => x.Id == itemId);
                    var mapper = new Utilities.PropertyMapper<TModel>();
                    mapper.Map(existingItem, item);
                    existingItem.Synced = true;
                });
            }
            else // the remote item is a new one
            {
                item.Synced = true;
                var localId = 0;
                await realmService.WriteAsync(tempRealm =>
                {
                    tempRealm.Manage(item);
                    localId = item.LocalId;
                });
                await RebuildModel(localId);
            }
        }

        public virtual async Task RebuildModel(int localId) { }

        public virtual async Task UpdateModel(int localId) { }
    }
}
