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
    public class MovementBase : OwningComponentBase<IGenericService<Movement>>, IDisposable
    {
        public List<Movement> movments { set; get; }
        public List<Movement> dirtymovments { set; get; }
        [Inject] public IMovmentService MovmentService { get; set; }
        [Parameter]
        public Guid? FilterToId { get; set; }
        [Inject] public IPrintService<Movement> PrintService { get; set; }
        [Inject] public IJSRuntime JsRuntime { set; get; }
        public List<Movement> toPrint =new List<Movement>();
        public bool activeButton { get; set; } = true;
        private int pageSize { get; set; }
        private int PageNumber { get; set; }
        public int totalCount { get; set; }

        public async Task OnReadData(DataGridReadDataEventArgs<Movement> e)
        {
            if (e.Page != PageNumber || e.PageSize != pageSize)
            {
                pageSize = e.PageSize;
                PageNumber = e.Page;
                if (FilterToId.HasValue)
                    movments = await Service.Get(PageNumber, pageSize, a =>
                        a.ProjectId == FilterToId.Value || a.ContractorId == FilterToId.Value);
                else
                    movments = await Service.Get(PageNumber, pageSize);
                totalCount =
                    await Service
                        .GetTotalCount(); // this is used to tell datagrid how many items are available so that pagination will work


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
                    if (i == 1 && !string.IsNullOrEmpty(dataGridColumnInfo.SearchValue))
                    {
                        dirtymovments = movments.Where(a => a.Contractor.Name.Contains(dataGridColumnInfo.SearchValue,
                            StringComparison.CurrentCultureIgnoreCase)).Select(a => a).ToList();
                        filtred++;
                    }

                    else if (i == 2 && !string.IsNullOrEmpty(dataGridColumnInfo.SearchValue))
                    {
                        dirtymovments = movments.Where(a => a.Project.ProjectName.Contains(dataGridColumnInfo.SearchValue,
                            StringComparison.CurrentCultureIgnoreCase)).Select(a => a).ToList();
                        filtred++;

                    }
                    else if (i == 3 && !string.IsNullOrEmpty(dataGridColumnInfo.SearchValue))
                    {
                        dirtymovments = movments.Where(a => a.CertificateNumber.ToString().Contains(dataGridColumnInfo.SearchValue,
                            StringComparison.CurrentCultureIgnoreCase)).Select(a => a).ToList();
                        filtred++;

                    }
                    else if (i == 4 && !string.IsNullOrEmpty(dataGridColumnInfo.SearchValue))
                    {
                        dirtymovments = movments.Where(a => a.MovementDate.ToString("d").Contains(dataGridColumnInfo.SearchValue,
                            StringComparison.CurrentCultureIgnoreCase)).Select(a => a).ToList();
                        filtred++;

                    }
                    else if (i == 5 && !string.IsNullOrEmpty(dataGridColumnInfo.SearchValue))
                    {
                        dirtymovments = movments.Where(a => a.Reference.ToString().Contains(dataGridColumnInfo.SearchValue,
                            StringComparison.CurrentCultureIgnoreCase)).Select(a => a).ToList();
                        filtred++;

                    }
                    else if (i == 6 && !string.IsNullOrEmpty(dataGridColumnInfo.SearchValue))
                    {
                        dirtymovments = movments.Where(a => a.VehicleNo.Contains(dataGridColumnInfo.SearchValue,
                            StringComparison.CurrentCultureIgnoreCase)).Select(a => a).ToList();
                        filtred++;

                    }
                    else if (i == 7 && !string.IsNullOrEmpty(dataGridColumnInfo.SearchValue))
                    {
                        dirtymovments = movments.Where(a => a.DriverName.Contains(dataGridColumnInfo.SearchValue,
                            StringComparison.CurrentCultureIgnoreCase)).Select(a => a).ToList();
                        filtred++;

                    }
                    else if (i == 8 && !string.IsNullOrEmpty(dataGridColumnInfo.SearchValue))
                    {
                        dirtymovments = movments.Where(a => a.Place.Contains(dataGridColumnInfo.SearchValue,
                            StringComparison.CurrentCultureIgnoreCase)).Select(a => a).ToList();
                        filtred++;

                    }
                    else if (i == 9 && !string.IsNullOrEmpty(dataGridColumnInfo.SearchValue))
                    {
                        dirtymovments = movments.Where(a => a.MaterialTypes.Contains(dataGridColumnInfo.SearchValue,
                            StringComparison.CurrentCultureIgnoreCase)).Select(a => a).ToList();
                        filtred++;

                    }
                    else if (i == 10 && !string.IsNullOrEmpty(dataGridColumnInfo.SearchValue))
                    {
                        dirtymovments = movments.Where(a => a.Total.ToString().Contains(dataGridColumnInfo.SearchValue,
                            StringComparison.CurrentCultureIgnoreCase)).Select(a => a).ToList();
                        filtred++;

                    }
                    else if (i == 11 && !string.IsNullOrEmpty(dataGridColumnInfo.SearchValue))
                    {
                        dirtymovments = movments.Where(a => a.AddedValue.ToString().Contains(dataGridColumnInfo.SearchValue,
                            StringComparison.CurrentCultureIgnoreCase)).Select(a => a).ToList();
                        filtred++;

                    }
                    else if (i == 12 && !string.IsNullOrEmpty(dataGridColumnInfo.SearchValue))
                    {
                        dirtymovments = movments.Where(a => a.Net.ToString().Contains(dataGridColumnInfo.SearchValue,
                            StringComparison.CurrentCultureIgnoreCase)).Select(a => a).ToList();
                        filtred++;

                    }


                    i++;
                }

                if (filtred == 0)
                {
                    dirtymovments = movments.Select(a => a).ToList();

                }

                filtred = 0;
            }
            else
            {
                dirtymovments = movments.Select(a => a).ToList();

            }
            // always call StateHasChanged!
            StateHasChanged();
        }

        public void Print(Movement m)
        {
            PrintService.Print(new List<Movement>(){ m });
        }

        public void SelectItemToPrint(Movement m,bool selected)
        {

            if (selected && !toPrint.Contains(m))
                toPrint.Add(m);
            if (!selected && toPrint.Contains(m))
                toPrint.Remove(m);
        }
        public void Dispose()
        {
        }
      
        protected void updateRow(CancellableRowChange<Movement, Dictionary<string, object>> value)
        {
            if (value.Cancel) return;
            foreach (var valueValue in value.Item.GetType().GetProperties())
            {
                if (!value.Values.ContainsKey(valueValue.Name)|| valueValue.Name.Equals("number",StringComparison.CurrentCultureIgnoreCase))
                    continue;
                
                var current = valueValue.GetValue(value.Item);
                var newValue = value.Values[valueValue.Name];
                if (current != newValue && valueValue.CanWrite)
                    valueValue.SetValue(value.Item, newValue);
            }

            Service.Update(value.Item);
            StateHasChanged();
        }
        protected async Task Removeitem(CommandContext<Movement> context)
        {
            activeButton = false;

            if (await JsRuntime.Confirm($"Sure want remove Movment"))
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
                if (FilterToId.HasValue)
                    movments = await Service.Get(PageNumber, pageSize, a =>
                        a.ProjectId == FilterToId.Value || a.ContractorId == FilterToId.Value);
                else
                    movments = await Service.Get(PageNumber, pageSize); totalCount = await Service.GetTotalCount(); // this is used to tell datagrid how many items are available so that pagination will work
                dirtymovments = new List<Movement>(movments);

                StateHasChanged();
            });

        }

    }

}