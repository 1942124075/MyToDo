using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using MyToDo.Api.Services.Interfaces;
using MyToDo.Library.Entity;
using MyToDo.Library.Modes;

namespace MyToDo.Api.Services
{
    /// <summary>
    /// 待办服务
    /// </summary>
    public class ToDoService : IToDoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public ToDoService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="modeDto"></param>
        /// <returns></returns>
        public async Task<ApiResponse<ToDoDto>> AddAsync(ToDoDto modeDto)
        {
            if (modeDto == null)
                return new ApiResponse<ToDoDto>("添加失败！");
            try
            {
                var mode = mapper.Map<ToDo>(modeDto);
                mode.CreateDate = DateTime.Now;
                mode.ModifyDate = DateTime.Now;
                await unitOfWork.GetRepository<ToDo>().InsertAsync(mode);
                if (await unitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse<ToDoDto>(true, mapper.Map<ToDoDto>(mode), "添加成功！");
                }
                else
                {
                    return new ApiResponse<ToDoDto>("添加失败！");
                }

            }
            catch (Exception ex)
            {
                return new ApiResponse<ToDoDto>(ex.Message);
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
                var repository = unitOfWork.GetRepository<ToDo>();
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
        public async Task<ApiResponse<PageList<ToDoDto>>> GetAllAsync(QueryParameter query)
        {
            try
            {
                var repository = unitOfWork.GetRepository<ToDo>();
                var results = await repository.GetPagedListAsync(pageIndex: query.PageIndex,pageSize: query.PageSize,
                    predicate:e => string.IsNullOrWhiteSpace(query.Search) ? true : e.Title.Equals(query.Search));
                PageList<ToDoDto> pageList = new PageList<ToDoDto>();
                pageList.PageIndex = query.PageIndex;
                pageList.PageSize = query.PageSize;
                foreach (var item in results.Items)
                {
                    pageList.Lists.Add(mapper.Map<ToDoDto>(item));
                }
                if (results != null)
                {
                    return new ApiResponse<PageList<ToDoDto>>(true, pageList, "获取成功！");
                }
                else
                {
                    return new ApiResponse<PageList<ToDoDto>>("获取失败！");
                }

            }
            catch (Exception ex)
            {
                return new ApiResponse<PageList<ToDoDto>>(ex.Message);
            }
        }
        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResponse<ToDoDto>> GetSingleAsync(int id)
        {
            if (id < 0)
                return new ApiResponse<ToDoDto>("获取失败！");
            try
            {
                var repository = unitOfWork.GetRepository<ToDo>();
                var result = await repository.GetFirstOrDefaultAsync(predicate: e => e.Id == id);
                if (result != null)
                {
                    return new ApiResponse<ToDoDto>(true, mapper.Map<ToDoDto>(result), "获取成功！");
                }
                else
                {
                    return new ApiResponse<ToDoDto>("获取失败！");
                }

            }
            catch (Exception ex)
            {
                return new ApiResponse<ToDoDto>(ex.Message);
            }
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="modeDto"></param>
        /// <returns></returns>
        public async Task<ApiResponse<ToDoDto>> UpdateAsync(ToDoDto modeDto)
        {
            if (modeDto == null)
                return new ApiResponse<ToDoDto>("修改失败！");
            try
            {
                var mode = mapper.Map<ToDo>(modeDto);
                var repository = unitOfWork.GetRepository<ToDo>();
                var result = await repository.GetFirstOrDefaultAsync(predicate: e => e.Id == mode.Id);
                result.Title = string.IsNullOrEmpty(mode.Title) ? result.Title : mode.Title;
                result.Content = string.IsNullOrEmpty(mode.Content) ? result.Content : mode.Content;
                result.ModifyDate = DateTime.Now;
                repository.Update(result);
                if (await unitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse<ToDoDto>(true, mapper.Map<ToDoDto>(result), "修改成功！");
                }
                else
                {
                    return new ApiResponse<ToDoDto>("修改失败！");
                }

            }
            catch (Exception ex)
            {
                return new ApiResponse<ToDoDto>(ex.Message);
            }
        }
    }
}
