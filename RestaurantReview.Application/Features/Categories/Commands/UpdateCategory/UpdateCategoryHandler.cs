﻿using AutoMapper;
using RestaurantReview.Domain.IRepositories;
using RestaurantReview.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantReview.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryHandler : IUpdateCategoryService
    {
        protected readonly ICategoryRepository _categoryRepository;
        protected readonly IMapper _mapper;
        public UpdateCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }



        public async Task<UpdateCategoryResponse> UpdateCategory(UpdateCategoryCommand updateCategoryCommand)
        {
            var categoryResponse = new UpdateCategoryResponse();


            var validator = new UpdateCategoryCommandValidator();
            var validationResult = await validator.ValidateAsync(updateCategoryCommand);



            if (validationResult.Errors.Count > 0)
            {
                categoryResponse.Success = false;
                categoryResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    categoryResponse.ValidationErrors.Add(error.ErrorMessage);
                }

            }

            if (categoryResponse.Success)
            {
                var category = new Category()

                {
                    CategoryName = updateCategoryCommand.CategoryName,

                };



                await _categoryRepository.UpdateAsync(category);

                categoryResponse = _mapper.Map<UpdateCategoryResponse>(category);
            }
            return categoryResponse;



        }
    }
}
