﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReview.Domain.Models;

namespace RestaurantReview.Infrastructure.Configuration
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> modelBuilder)
        {
            modelBuilder
                 .HasKey(review => review.ReviewID);


            modelBuilder
                .HasOne(review => review.Restaurant)
                .WithMany(Restaurant => Restaurant.Reviews)
                .HasForeignKey(review => review.RestaurantID);

        }
    }
}