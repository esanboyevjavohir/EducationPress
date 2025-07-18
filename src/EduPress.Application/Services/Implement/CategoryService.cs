using AutoMapper;
using EduPress.Application.Models.CategoriesModel;
using EduPress.Application.Models;
using EduPress.Application.Services.Interface;
using EduPress.DataAccess.Persistence;
using EduPress.Core.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace EduPress.Application.Services.Implement
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _databaseContext;

        public CategoryService(IMapper mapper, DatabaseContext databaseContext)
        {
            _mapper = mapper;
            _databaseContext = databaseContext;
        }

        public async Task<ApiResult<CreateCategoriesResponseModel>> CreateAsync(CreateCategoriesModel create)
        {
            var createModel = _mapper.Map<Categories>(create);
            createModel.CreatedOn = DateTime.Now;

            _databaseContext.Categories.Add(createModel);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<CreateCategoriesResponseModel>.Success(new CreateCategoriesResponseModel
            {
                Id = createModel.Id
            });
        }

        public async Task<ApiResult<bool>> DeleteAsync(Guid id)
        {
            var delete = await _databaseContext.Categories
                .FirstOrDefaultAsync(c => c.Id == id);

            if (delete == null)
            {
                return ApiResult<bool>.Failure(new List<string> { "Categories not found" });
            }

            _databaseContext.Categories.Remove(delete);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<bool>.Success(true);
        }

        public async Task<ApiResult<List<CategoriesResponseModel>>> GetAllAsync()
        {
            var categories = await _databaseContext.Categories
                .AsNoTracking()
                .ProjectTo<CategoriesResponseModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return ApiResult<List<CategoriesResponseModel>>.Success(categories);
        }

        public async Task<ApiResult<CategoriesResponseModel>> GetByIdAsync(Guid id)
        {
            var category = await _databaseContext.Categories
                .AsNoTracking()
                .ProjectTo<CategoriesResponseModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return ApiResult<CategoriesResponseModel>.Failure(
                                new List<string> { "Categories not found" });
            }

            return ApiResult<CategoriesResponseModel>.Success(category);
        }

        public async Task<ApiResult<UpdateCategoriesResponseModel>> UpdateAsync(Guid id, 
            UpdateCategoriesModel update)
        {
            var updateModel = await _databaseContext.Categories
                .FirstOrDefaultAsync(c => c.Id == id);

            if (updateModel == null)
            {
                return ApiResult<UpdateCategoriesResponseModel>.Failure(
                    new List<string> { "Categories not found" });
            }

            _mapper.Map(update, updateModel);
            updateModel.UpdatedOn = DateTime.Now;
            _databaseContext.Categories.Update(updateModel);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<UpdateCategoriesResponseModel>.Success(new UpdateCategoriesResponseModel
            {
                Id = updateModel.Id
            });
        }
    }
}
