using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPress.Application.Models;
using EduPress.Application.Models.CategoriesModel;
using EduPress.Application.Models.CoursesModel;
using EduPress.Application.Services.Interface;
using EduPress.Core.Entities;
using EduPress.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using StatusGeneric;

namespace EduPress.Application.Services.Implement
{
    public class CoursesService : ICoursesService
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _databaseContext;

        public CoursesService(IMapper mapper, DatabaseContext databaseContext)
        {
            _mapper = mapper;
            _databaseContext = databaseContext;
        }

        public async Task<IStatusGeneric<CreateCoursesResponseModel>> CreateAsync(CreateCoursesModel create)
        {
            var status = new StatusGenericHandler<CreateCoursesResponseModel>();

            var createModel = _mapper.Map<Courses>(create);
            createModel.CreatedOn = DateTime.Now;

            _databaseContext.Courses.Add(createModel);
            await _databaseContext.SaveChangesAsync();

            status.SetResult(new CreateCoursesResponseModel
            {
                Id = createModel.Id
            });

            return status;
        }

        public async Task<IStatusGeneric<bool>> DeleteAsync(Guid id)
        {
            var status = new StatusGenericHandler<bool>();

            var delete = await _databaseContext.Courses
                .FirstOrDefaultAsync(c => c.Id == id);

            if (delete == null)
            {
                status.AddError("Courses not found");
                return status;
            }

            _databaseContext.Courses.Remove(delete);
            await _databaseContext.SaveChangesAsync();

            status.SetResult(true);

            return status;
        }

        public async Task<IStatusGeneric<List<CoursesResponseModel>>> GetAllAsync()
        {
            var status = new StatusGenericHandler<List<CoursesResponseModel>>();

            var getAll = await _databaseContext.Courses
                   .AsNoTracking()
                   .ProjectTo<CoursesResponseModel>(_mapper.ConfigurationProvider)
                   .ToListAsync();

            status.SetResult(getAll);

            return status;
        }

        public async Task<IStatusGeneric<CoursesResponseModel>> GetByIdAsync(Guid id)
        {
            var status = new StatusGenericHandler<CoursesResponseModel>();

            var getById = await _databaseContext.Courses
                .AsNoTracking()
                .ProjectTo<CoursesResponseModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == id);

            if(getById == null)
            {
                status.AddError("Courses not found");
                return status;
            }

            status.SetResult(getById);
            return status;
        }

        public async Task<IStatusGeneric<UpdateCoursesResponseModel>> UpdateAsync(Guid id, 
            UpdateCoursesModel update)
        {
            var status = new StatusGenericHandler<UpdateCoursesResponseModel>();

            var updateModel = await _databaseContext.Courses
                .FirstOrDefaultAsync(c => c.Id == id);

            if (updateModel == null)
            {
                status.AddError("Courses not found");
                return status;
            }

            _mapper.Map(update, updateModel);
            updateModel.UpdatedOn = DateTime.Now;
            _databaseContext.Courses.Update(updateModel);
            await _databaseContext.SaveChangesAsync();

            status.SetResult(new UpdateCoursesResponseModel
            {
                Id = updateModel.Id
            });

            return status;
        }
    }
}
