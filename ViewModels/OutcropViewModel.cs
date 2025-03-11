using CommunityToolkit.Mvvm.ComponentModel;
using SeiveIT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeiveIT.ViewModels
{
    public partial class OutcropViewModel : ObservableObject
    {
        [ObservableProperty]
        Outcrop _outcrop;

        public OutcropViewModel(Outcrop o)
        {
            _outcrop = o;
        }
    }
}
