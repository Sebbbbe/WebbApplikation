﻿using System;
using System.Collections.Generic;

namespace RestaurantReview.Domain.Models
{
    public class Restaurant
    {
        public Guid RestaurantID { get; set; }
        public string RestaurantName { get; set; }

        public string RestaurantLink { get; set; }

        public string Description { get; set; }

        public string MapURL { get; set; }

        public string TempImage { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<Category> Categories { get; set; }

        public ICollection<Image> Images { get; set; }



    }
}
