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
    public class ProjectBase : OwningComponentBase<IGenericService<Project>>, IDisposable
    {
        public List<Project> projects { set; get; }
        public List<Project> dirtyprojects { set; get; }
        [Inject] public IJSRuntime JsRuntime { set; get; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        public bool activeButton { get; set; } = true;
        [Parameter] public Guid? contractorId { get; set; }

        private int pageSize { get; set; }
        private int PageNumber { get; set; }
        public int totalCount { get; set; }

        public async Task OnReadData(DataGridReadDataEventArgs<Project> e)
        {
            if (e.Page != PageNumber || e.PageSize != pageSize)
            {
                pageSize = e.PageSize;
                PageNumber = e.Page;
                if (contractorId.HasValue)
                {
                    projects = await Service.Get(PageNumber, pageSize, a => a.ContractorId == contractorId.Value);

                }
                else
                    projects = await Service.Get(PageNumber, pageSize);


                totalCount =
                    await Service
                        .GetTotalCount(); // this is used to tell datagrid how many items are available so that pagination will work
                dirtyprojects=new List<Project>(projects);
            }

            if (e.Columns.Any(a => !string.IsNullOrEmpty(a.SearchValue)))
            {
                int i = 0;
                int filtred = 0;
                foreach (var dataGridColumnInfo in e.Columns)
                {   if(i==0 && !string.IsNullOrEmpty(dataGridColumnInfo.SearchValue))
                    {
                        dirtyprojects = projects.Where(a =>a.ProjectName.Contains(dataGridColumnInfo.SearchValue,
                            StringComparison.CurrentCultureIgnoreCase)).Select(a => a).ToList();
                        filtred++;
                    }

                   else if (i ==2 && !string.IsNullOrEmpty(dataGridColumnInfo.SearchValue))
                    {
                        dirtyprojects = projects.Where(a => a.Contractor.Name.Contains(dataGridColumnInfo.SearchValue,
                            StringComparison.CurrentCultureIgnoreCase)).Select(a=>a).ToList();
                        filtred++;

                    }
                   else if (i == 1 && !string.IsNullOrEmpty(dataGridColumnInfo.SearchValue))
                   {
                       dirtyprojects = projects.Where(a => a.Reference.Contains(dataGridColumnInfo.SearchValue,
                           StringComparison.CurrentCultureIgnoreCase)).Select(a => a).ToList();
                       filtred++;

                   }

                    i++;
                }

                if (filtred == 0)
                {
                    dirtyprojects = projects.Select(a => a).ToList();

                }

                filtred = 0;
            }
            else
            {
                dirtyprojects = projects.Select(a => a).ToList();

            }
            // always call StateHasChanged!
            StateHasChanged();
        }





        public void Dispose()
        {
        }
        protected void updateRow(CancellableRowChange<Project, Dictionary<string, object>> value)
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
        public void NavigateTo(Guid guid)
        {
            NavigationManager.NavigateTo($"/manager/Movements/{guid}/{Constant.Parameter.Project}");
        }
        protected async Task Removeitem(CommandContext<Project> context)
        {
            activeButton = false;
            var p = projects.First(a => a.ID == context.Item.ID);
            if (p.Movements.Count > 0)
            {
                await JsRuntime.Alert("Cant reomve Project Has Movement. Remove Movement Before");
            }
            else if (await JsRuntime.Confirm($"Sure want remove {context.Item.ProjectName}\nAll Related projects and Movment Will be delete"))
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
        public void Change()
        {
            InvokeAsync(async () =>
            {
                if (contractorId.HasValue)
                {
                    projects = await Service.Get(PageNumber, pageSize, a => a.ContractorId == contractorId.Value);

                }
                else
                    projects = await Service.Get(PageNumber, pageSize);
                totalCount = await Service.GetTotalCount(); // this is used to tell datagrid how many items are available so that pagination will work
                dirtyprojects = new List<Project>(projects);

                StateHasChanged();
            });

        }

    }

}