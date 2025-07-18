using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPress.Application.Models;
using EduPress.Application.Models.CategoriesModel;
using EduPress.Application.Models.ContactLocationsModel;
using EduPress.Application.Services.Interface;
using EduPress.Core.Entities;
using EduPress.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EduPress.Application.Services.Implement
{
    public class ContactLocationsService : IContactLocationsService
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _databaseContext;

        public ContactLocationsService(IMapper mapper, DatabaseContext databaseContext)
        {
            _mapper = mapper;
            _databaseContext = databaseContext;
        }

        public async Task<ApiResult<CreateLocationsResponseModel>> CreateAsync(CreateLocationsModel create)
        {
            var createModel = _mapper.Map<ContactLocations>(create);
            createModel.CreatedOn = DateTime.Now;

            _databaseContext.ContactLocations.Add(createModel);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<CreateLocationsResponseModel>.Success(new CreateLocationsResponseModel
            {
                Id = createModel.Id
            });
        }

        public async Task<ApiResult<bool>> DeleteAsync(Guid id)
        {
            var delete = await _databaseContext.ContactLocations
                .FirstOrDefaultAsync(x => x.Id == id);

            if (delete == null)
            {
                return ApiResult<bool>.Failure(new List<string> { "ContactLocation not found" });
            }

            _databaseContext.ContactLocations.Remove(delete);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<bool>.Success(true);
        }

        public async Task<ApiResult<List<LocationsResponseModel>>> GetAllAsync()
        {
            var locations = await _databaseContext.ContactLocations
                .AsNoTracking()
                .ProjectTo<LocationsResponseModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return ApiResult<List<LocationsResponseModel>>.Success(locations);
        }

        public async Task<ApiResult<LocationsResponseModel>> GetByIdAsync(Guid id)
        {
            var location = await _databaseContext.ContactLocations
                .AsNoTracking()
                .ProjectTo<LocationsResponseModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (location == null)
            {
                return ApiResult<LocationsResponseModel>.Failure(
                                new List<string> { "Categories not found" });
            }

            return ApiResult<LocationsResponseModel>.Success(location);
        }

        public async Task<ApiResult<UpdateLocationsResponseModel>> UpdateAsync(Guid id, 
            UpdateLocationsModel update)
        {
            var updateModel = await _databaseContext.ContactLocations
                .FirstOrDefaultAsync(l => l.Id == id);

            if (updateModel == null)
            {
                return ApiResult<UpdateLocationsResponseModel>.Failure(
                    new List<string> { "Location not found" });
            }

            _mapper.Map(update, updateModel);
            updateModel.UpdatedOn = DateTime.Now;
            _databaseContext.ContactLocations.Update(updateModel);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<UpdateLocationsResponseModel>.Success(new UpdateLocationsResponseModel
            {
                Id = updateModel.Id
            });
        }
    }
}
