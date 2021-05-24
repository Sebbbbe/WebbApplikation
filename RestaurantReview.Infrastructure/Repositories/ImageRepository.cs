﻿
using AutoMapper;
using RestaurantReview.Domain.IRepositories;
using RestaurantReview.Domain.Models;
using System.Threading.Tasks;

namespace RestaurantReview.Infrastructure.Repositories
{
    public class ImageRepository : BaseRepository<Image>, IImageRepository
    {
        protected new readonly MyDbContext _myDbContext;
        public ImageRepository(MyDbContext myDbContext) : base(myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public Image Add(Image image)
        {
             _myDbContext.Images.Add(image);
             _myDbContext.SaveChangesAsync();

            return image;
        }


    }
}
