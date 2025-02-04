﻿using AutoMapper;
using RestaurantReview.Domain.IRepositories;
using RestaurantReview.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace RestaurantReview.Application.Features.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantHandler : ICreateRestaurantService
    {
        //Innehåller kod för metoder, sparar detta sedan genom att kalla på Repository för sin klass

        //Får inte returnera en vanlig "Model" Måste Returner en ResponsTyp med innehållet man vill visa.
        private readonly IMapper _mapper;
        private readonly IRestaurantRepository _restaurantRepository;


        public CreateRestaurantHandler(IMapper mapper, IRestaurantRepository restaurantRepository)
        {
            _mapper = mapper;
            _restaurantRepository = restaurantRepository;
        }

        public async Task<CreateRestaurantResponse> CreateRestaurant(CreateRestaurantCommand createRestaurantCommand)
        {

            var validator = new CreateRestaurantCommandValidator(_restaurantRepository);


            var validationResult = await validator.ValidateAsync(createRestaurantCommand);

            var createRestaurantResponse = new CreateRestaurantResponse();

            if (validationResult.Errors.Count > 0)

            {
                createRestaurantResponse.Success = false;
                createRestaurantResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createRestaurantResponse.ValidationErrors.Add(error.ErrorMessage);
                }

            }

            if (createRestaurantResponse.Success)
            {
                var restaurant = new Restaurant()
                {
                    RestaurantLink = createRestaurantCommand.RestaurantLink,
                    TempImage = createRestaurantCommand.TempImage,
                    RestaurantName = createRestaurantCommand.RestaurantName,
                    MapURL = createRestaurantCommand.MapURL,
                    Description = createRestaurantCommand.Description,
                    RestaurantID = new Guid(),
                    Categories = new List<Category>(),
                    Reviews = new List<Review>()

                };

                restaurant = await _restaurantRepository.AddAsync(restaurant);


                createRestaurantResponse = _mapper.Map<CreateRestaurantResponse>(restaurant);

            }

            return createRestaurantResponse;




        }

    }
}
