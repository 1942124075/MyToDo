using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using MyToDo.Api.Services.Interfaces;
using MyToDo.Library.Entity;
using MyToDo.Library.Modes;
using System.Collections.ObjectModel;

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
        public ToDoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        /// <summary>
        /// 获取汇总数据
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResponse<SummaryDto>> GetSummarySaync(User user)
        {
            SummaryDto summary = new SummaryDto();
            var memoList = await unitOfWork.GetRepository<Memo>().GetPagedListAsync(predicate: e => e.UserId == user.Id, orderBy: e => e.OrderBy(i => i.Id), pageSize: int.MaxValue);
            summary.MemoList = new ObservableCollection<MemoDto>(mapper.Map<List<MemoDto>>(memoList.Items));
            summary.MemoCount = summary.MemoList.Count;
            var todoList = await unitOfWork.GetRepository<ToDo>().GetPagedListAsync(predicate: e => e.UserId == user.Id, orderBy: e => e.OrderBy(i => i.Id), pageSize: int.MaxValue);
            summary.TodoList = new ObservableCollection<ToDoDto>(mapper.Map<List<ToDoDto>>(todoList.Items.Where(e => e.Status == 0)));
            summary.TodoFinshCount = todoList.Items.Count(e => e.Status == 1);
            summary.TodoCount = todoList.Items.Count;
            summary.TodoFinshRatioRatio = ((double)summary.TodoFinshCount / summary.TodoCount).ToString("0%");
            return new ApiResponse<SummaryDto>(true, summary);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public async Task<ApiResponse<ToDoDto>> AddAsync(ToDo mode)
        {
            if (mode != null && mode.User != null)
            {
                mode.CreateDate = DateTime.Now;
                mode.ModifyDate = DateTime.Now;
                mode.UserId = mode.User.Id;
                mode.User = null;
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
            else
            {
                return new ApiResponse<ToDoDto>("添加失败！");
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
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse<PageList<ToDoDto>>> GetAllAsync(QueryParameter query, User user)
        {
            var repository = unitOfWork.GetRepository<ToDo>();
            var results = await repository.GetPagedListAsync(pageIndex: query.PageIndex, pageSize: query.PageSize,
                predicate: e => e.UserId == user.Id && (string.IsNullOrWhiteSpace(query.Search) ? true : e.Title.Contains(query.Search))
                && (query.Status == null ? true : query.Status.Equals(e.Status)));
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
        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<ApiResponse<ToDoDto>> GetSingleAsync(int id, User user)
        {
            if (id < 0)
                return new ApiResponse<ToDoDto>("获取失败！");
            var repository = unitOfWork.GetRepository<ToDo>();
            var result = await repository.GetFirstOrDefaultAsync(predicate: e => e.UserId == user.Id && e.Id == id);
            if (result != null)
            {
                return new ApiResponse<ToDoDto>(true, mapper.Map<ToDoDto>(result), "获取成功！");
            }
            else
            {
                return new ApiResponse<ToDoDto>("获取失败！");
            }
        }



        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public async Task<ApiResponse<ToDoDto>> UpdateAsync(ToDo mode)
        {
            if (mode == null)
                return new ApiResponse<ToDoDto>("修改失败！");
            var repository = unitOfWork.GetRepository<ToDo>();
            var result = await repository.GetFirstOrDefaultAsync(predicate: e => e.Id == mode.Id);
            result.Title = string.IsNullOrEmpty(mode.Title) ? result.Title : mode.Title;
            result.Content = string.IsNullOrEmpty(mode.Content) ? result.Content : mode.Content;
            result.ModifyDate = DateTime.Now;
            result.Status = mode.Status;
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
    }
}
