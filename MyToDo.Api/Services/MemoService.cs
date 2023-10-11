using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using MyToDo.Api.Services.Interfaces;
using MyToDo.Library.Entity;
using MyToDo.Library.Modes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyToDo.Api.Services
{
    public class MemoService : IMemoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MemoService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="blockItem"></param>
        /// <returns></returns>
        public async Task<ApiResponse> AddAsync(MemoDto modeDto)
        {
            if (modeDto == null)
                return new ApiResponse("添加失败！");
            try
            {
                var mode = mapper.Map<Memo>(modeDto);
                mode.CreateDate = DateTime.Now;
                mode.ModifyDate = DateTime.Now;
                await unitOfWork.GetRepository<Memo>().InsertAsync(mode);
                if (await unitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, mode, "添加成功！");
                }
                else
                {
                    return new ApiResponse("添加失败！");
                }

            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResponse> DeleteAsync(int id)
        {
            if (id < 1)
                return new ApiResponse("删除失败！");
            try
            {
                var repository = unitOfWork.GetRepository<Memo>();
                var blockItem = await repository.GetFirstOrDefaultAsync(predicate: e => e.Id == id);
                repository.Delete(blockItem);
                if (await unitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, blockItem, "删除成功！");
                }
                else
                {
                    return new ApiResponse("删除失败！");
                }

            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse> GetAllAsync(QueryParameter query)
        {
            try
            {
                var repository = unitOfWork.GetRepository<Memo>();
                var results = await repository.GetPagedListAsync(pageIndex: query.PageIndex, pageSize: query.PageSize,
                    predicate: e => string.IsNullOrWhiteSpace(query.Search) ? true : e.Title.Equals(query.Search));
                if (results != null)
                {
                    return new ApiResponse(true, results, "获取成功！");
                }
                else
                {
                    return new ApiResponse("获取失败！");
                }

            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="blockItem"></param>
        /// <returns></returns>
        public async Task<ApiResponse> GetSingleAsync(int id)
        {
            if (id < 0)
                return new ApiResponse("获取失败！");
            try
            {
                var repository = unitOfWork.GetRepository<Memo>();
                var result = await repository.GetFirstOrDefaultAsync(predicate: e => e.Id == id);
                if (result != null)
                {
                    return new ApiResponse(true, result, "获取成功！");
                }
                else
                {
                    return new ApiResponse("获取失败！");
                }

            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="blockItem"></param>
        /// <returns></returns>
        public async Task<ApiResponse> UpdateAsync(MemoDto modeDto)
        {
            if (modeDto == null)
                return new ApiResponse("修改失败！");
            try
            {
                var mode = mapper.Map<Memo>(modeDto);
                var repository = unitOfWork.GetRepository<Memo>();
                var result = await repository.GetFirstOrDefaultAsync(predicate: e => e.Id == mode.Id);
                result.Title = string.IsNullOrEmpty(mode.Title) ? result.Title : mode.Title;
                result.Content = string.IsNullOrEmpty(mode.Content) ? result.Content : mode.Content;
                result.ModifyDate = DateTime.Now;
                repository.Update(result);
                if (await unitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, result, "修改成功！");
                }
                else
                {
                    return new ApiResponse("修改失败！");
                }

            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
    }
}
