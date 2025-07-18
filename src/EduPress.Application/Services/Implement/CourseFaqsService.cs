using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPress.Application.Models;
using EduPress.Application.Models.CourseFaqsModel;
using EduPress.Application.Services.Interface;
using EduPress.Core.Entities;
using EduPress.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EduPress.Application.Services.Implement
{
    public class CourseFaqsService :ICourseFaqsService
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _databaseContext;

        public CourseFaqsService(IMapper mapper, DatabaseContext databaseContext)
        {
            _mapper = mapper;
            _databaseContext = databaseContext;
        }

        public async Task<ApiResult<CreateCourseFaqsResponseModel>> CreateAsync(CreateCourseFaqsModel create)
        {
            var createModel = _mapper.Map<CourseFaqs>(create);
            createModel.CreatedOn = DateTime.Now;

            _databaseContext.CourseFaqs.Add(createModel);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<CreateCourseFaqsResponseModel>.Success(new CreateCourseFaqsResponseModel
            {
                Id = createModel.Id
            });
        }

        public async Task<ApiResult<bool>> DeleteAsync(Guid id)
        {
            var delete = await _databaseContext.CourseFaqs
                .FirstOrDefaultAsync(x => x.Id == id);

            if (delete == null)
            {
                return ApiResult<bool>.Failure(new List<string> { "CourseFaqs not found" });
            }

            _databaseContext.CourseFaqs.Remove(delete);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<bool>.Success(true);
        }

        public async Task<ApiResult<List<CourseFaqsResponseModel>>> GetAllAsync()
        {
            var courseFaqs = await _databaseContext.CourseFaqs
                .AsNoTracking()
                .ProjectTo<CourseFaqsResponseModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return ApiResult<List<CourseFaqsResponseModel>>.Success(courseFaqs);
        }

        public async Task<ApiResult<CourseFaqsResponseModel>> GetByIdAsync(Guid id)
        {
            var courseFaqs = await _databaseContext.CourseFaqs
                .AsNoTracking()
                .ProjectTo<CourseFaqsResponseModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (courseFaqs == null)
            {
                return ApiResult<CourseFaqsResponseModel>.Failure(
                                new List<string> { "CourseFaqs not found" });
            }

            return ApiResult<CourseFaqsResponseModel>.Success(courseFaqs);
        }

        public async Task<ApiResult<UpdateCourseFaqsResponseModel>> UpdateAsync(Guid id, 
            UpdateCourseFaqsModel update)
        {
            var updateModel = await _databaseContext.CourseFaqs
                .FirstOrDefaultAsync(c => c.Id == id);

            if (updateModel == null)
            {
                return ApiResult<UpdateCourseFaqsResponseModel>.Failure(
                    new List<string> { "CourseFaqs not found" });
            }

            _mapper.Map(update, updateModel);
            updateModel.UpdatedOn = DateTime.Now;
            _databaseContext.CourseFaqs.Update(updateModel);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<UpdateCourseFaqsResponseModel>.Success(new UpdateCourseFaqsResponseModel
            {
                Id = updateModel.Id
            });
        }
    }
}
