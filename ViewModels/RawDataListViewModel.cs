using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SeiveIT.Entities;
using SeiveIT.Models;
using SeiveIT.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SeiveIT.ViewModels
{
    public partial class RawDataListViewModel : ObservableObject
    {
        IServiceManager _serviceManager = App.serviceManager;
        public long Pid { get; set; }
        public long Oid { get; set; }
        public ObservableCollection<RawDataViewModel> Rows { get; set; }
        [ObservableProperty]
        double _totalWeight;
        [ObservableProperty]
        double _totalInd;
                      
        public RawDataListViewModel(long pid, long oid)
        {
            Pid = pid;
            Oid = oid;
            Rows = new ObservableCollection<RawDataViewModel>( RawData.GetRows()
                .Select(r => new RawDataViewModel
                {
                    CummWeight = r.CummWeight,
                    IndWeight = r.IndWeight,
                    PhiScale = r.PhiScale,
                    RowNumber = r.RolNum,
                    Weight = r.Weight,
                })
            );
        }

        [RelayCommand]
        void Run()
        {
            TotalWeight = Math.Round(Rows.Sum(r => r.Weight), 1);
            double prevRow = 0;
            foreach (var row in Rows)
            {
                row.IndWeight = Math.Round( (100 * row.Weight) / TotalWeight, 3);
                row.CummWeight = 0;
                row.CummWeight = Math.Round(prevRow + row.IndWeight, 3);
                prevRow = row.CummWeight;
            }
        
            TotalInd = Math.Round(Rows.Sum(r => r.IndWeight));            
        }

        [RelayCommand]
        async Task Save()
        {
            try
            {
                foreach (var row in Rows)
                {
                    var data = await _serviceManager.RawDataService.UpsertSeiveData(new SeiveData
                    {
                        PhiScale = row.PhiScale,
                        Weight = row.Weight,
                        OutcropId = Oid,
                        ProjectId = Pid
                    });
                }
               

                await Toast.Make("Project saved").Show();
                //await Shell.Current.GoToAsync($"project?id={proj.Id}");
            }

            catch (SQLite.SQLiteException e)
            {
                var a = e.GetType();
                string errMsg = e.Message.Contains("UNIQUE constraint failed") ? "Title already exists" : "Unable to Save data, send us a mail";
                await Toast.Make(errMsg).Show();
            }
            catch (Exception e)
            {
                await Toast.Make("An error has occured, send us a mail").Show();
            }
        }
    }
}
