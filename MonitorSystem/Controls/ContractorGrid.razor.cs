using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MonitorSystem.Entity;
using MonitorSystem.Extention;
using MonitorSystem.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitorSystem.Controls
{
    public class ContractorBase : OwningComponentBase<IGenericService<Contractor>>, IDisposable
    {
        [Inject] private IJSRuntime JSRuntime { get; set; }
        [Inject] private NavigationManager NavigationManager  { get; set; }
        public List<MonitorSystem.Entity.Contractor> Contractors { set; get; }
        public List<MonitorSystem.Entity.Contractor> dirtyContractors { set; get; }

        public bool activeButton { get; set; } = true;

        private int pageSize { get; set; }
        private int PageNumber { get; set; }
        public int totalCount { get; set; }

        public async Task<Task> OnReadData(DataGridReadDataEventArgs<Contractor> e)
        {
            if(e.Page!=PageNumber|| e.PageSize!= pageSize|| dirtyContractors.Count<pageSize)
            {
                pageSize = e.PageSize;
                PageNumber = e.Page;
                Contractors = await Service.Get(e.Page, e.PageSize);
                dirtyContractors = Contractors.Select(a => a).ToList();

                totalCount =
                    await Service
                        .GetTotalCount(); // this is used to tell datagrid how many items are available so that pagination will work
            }
            if (e.Columns.Any(a => !string.IsNullOrEmpty(a.SearchValue)))
            {
                int i = 0;
                int filtred = 0;
                foreach (var dataGridColumnInfo in e.Columns)
                {
                    if (i == 0 && !string.IsNullOrEmpty(dataGridColumnInfo.SearchValue))
                    {
                        dirtyContractors = Contractors.Where(a => a.Name.Contains(dataGridColumnInfo.SearchValue,
                            StringComparison.CurrentCultureIgnoreCase)).Select(a => a).ToList();
                        filtred++;
                    }

                    else if (i == 1 && !string.IsNullOrEmpty(dataGridColumnInfo.SearchValue))
                    {
                        dirtyContractors = Contractors.Where(a =>  a.Phone.Contains(dataGridColumnInfo.SearchValue,
                            StringComparison.CurrentCultureIgnoreCase)).Select(a => a).ToList();
                        filtred++;

                    }


                    i++;
                }

                if (filtred == 0)
                {
                    dirtyContractors = Contractors.Select(a => a).ToList();

                }

                filtred = 0;
            }
            else
            {
                dirtyContractors = Contractors.Select(a => a).ToList();

            }
            // always call StateHasChanged!
            StateHasChanged();
            return Task.CompletedTask;
        }
        protected void updateRow(CancellableRowChange<Contractor, Dictionary<string, object>> value)
        {
            if (value.Cancel) return;
            foreach (var valueValue in value.Item.GetType().GetProperties())
            {
                if (!value.Values.ContainsKey(valueValue.Name))
                    continue;
                ;
                var current = valueValue.GetValue(value.Item);
                var newValue = value.Values[valueValue.Name];
                if (current != newValue && valueValue.CanWrite)
                    valueValue.SetValue(value.Item, newValue);
            }

            Service.Update(value.Item);
            StateHasChanged();
        }

        public void Dispose()
        {
        }

     
        public void Change()
        {
            InvokeAsync(async () =>
            {
                Contractors = await Service.Get(PageNumber, pageSize);
                totalCount = await Service.GetTotalCount(); // this is used to tell datagrid how many items are available so that pagination will work
                dirtyContractors = new List<Contractor>(Contractors);

                StateHasChanged();
            });

        }

        public void NavigateTo(Guid guid,char x)
        {
            switch (x)
            {
                case 'M':
                    NavigationManager.NavigateTo($"/Movements/{guid}/{Constant.Parameter.Contractor}");
                    break;
                case 'P':
                    NavigationManager.NavigateTo($"/Projects/{guid}");
                    break;

            }
        }
        protected async Task Removeitem(CommandContext<Contractor> context)
        {
            activeButton = false;
            if (await JSRuntime.Confirm($"Sure want remove {context.Item.Name}"))
            {
                if (await Service.Delete(context.Item.ID))
                {
                    activeButton = true;

                    Change();
                    StateHasChanged();
                }
                else
                {
                    activeButton = true;

                }
            }
            else
            {
                activeButton = true;

            }
        }

    }

}