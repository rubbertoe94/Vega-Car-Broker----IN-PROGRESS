﻿using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using vega.ViewModels;

namespace vega.Models.ViewModels
{
    public class SaveVehicleViewModel
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public bool IsRegistered { get; set; }

        [Required]
        public ContactViewModel Contact { get; set; }
        public ICollection<int> Features { get; set; }

      
    }

    public class DisplayVehicleViewModel
    {
        public int Id { get; set; }
        public ModelViewModel Model { get; set; }
        public MakeViewModel Make { get; set; }
        public bool IsRegistered { get; set; }
        public ContactViewModel Contact { get; set; }
        public DateTime LastUpdated { get; set; }
        public ICollection<FeatureViewModel> Features { get; set; }

        public DisplayVehicleViewModel() 
        {
            Features = new Collection<FeatureViewModel>();
        }
    }
}