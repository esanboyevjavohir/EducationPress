using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPress.Application.Models;
using EduPress.Application.Models.CategoriesModel;
using EduPress.Application.Models.CourseLessonsModel;
using EduPress.Application.Models.InstructorsModel;
using EduPress.Application.Services.Interface;
using EduPress.Core.Entities;
using EduPress.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EduPress.Application.Services.Implement
{
    public class InstructorsService : IInstructorsService
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _databaseContext;

        public InstructorsService(IMapper mapper, DatabaseContext databaseContext)
        {
            _mapper = mapper;
            _databaseContext = databaseContext;
        }

        public async Task<ApiResult<CreateInstructorsResponseModel>> CreateAsync(CreateInstructorsModel create)
        {
            var createModel = _mapper.Map<Instructors>(create);
            createModel.CreatedOn = DateTime.Now;

            _databaseContext.Instructors.Add(createModel);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<CreateInstructorsResponseModel>.Success(new CreateInstructorsResponseModel
            {
                Id = createModel.Id
            });
        }

        public async Task<ApiResult<bool>> DeleteAsync(Guid id)
        {
            var delete = await _databaseContext.Instructors
                .FirstOrDefaultAsync(c => c.Id == id);

            if (delete == null)
            {
                return ApiResult<bool>.Failure(new List<string> { "Instructors not found" });
            }

            _databaseContext.Instructors.Remove(delete);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<bool>.Success(true);
        }

        public async Task<ApiResult<List<InstructorsResponseModel>>> GetAllAsync()
        {
            var getAll = await _databaseContext.Instructors
                .AsNoTracking()
                .ProjectTo<InstructorsResponseModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return ApiResult<List<InstructorsResponseModel>>.Success(getAll);
        }

        public async Task<ApiResult<InstructorsResponseModel>> GetByIdAsync(Guid id)
        {
            var getById = await _databaseContext.Instructors
                .AsNoTracking()
                .ProjectTo<InstructorsResponseModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (getById == null)
            {
                return ApiResult<InstructorsResponseModel>.Failure(
                                new List<string> { "Instructors not found" });
            }

            return ApiResult<InstructorsResponseModel>.Success(getById);
        }

        public async Task<ApiResult<UpdateInstructorsResponseModel>> UpdateAsync(Guid id, UpdateInstructorsModel update)
        {
            var updateModel = await _databaseContext.Instructors
                .FirstOrDefaultAsync(c => c.Id == id);

            if (updateModel == null)
            {
                return ApiResult<UpdateInstructorsResponseModel>.Failure(
                    new List<string> { "Categories not found" });
            }

            _mapper.Map(update, updateModel);
            updateModel.UpdatedOn = DateTime.Now;
            _databaseContext.Instructors.Update(updateModel);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<UpdateInstructorsResponseModel>.Success(new UpdateInstructorsResponseModel
            {
                Id = updateModel.Id
            });
        }
    }
}
