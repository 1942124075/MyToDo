using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using MyToDo.Api.Services.Interfaces;
using MyToDo.Library.Entity;
using MyToDo.Library.Modes;

namespace MyToDo.Api.Services
{
    /// <summary>
    /// 菜单项服务
    /// </summary>
    public class MenuItemService : IMenuItemService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public MenuItemService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="modeDto"></param>
        /// <returns></returns>
        public async Task<ApiResponse<MenuItemDto>> AddAsync(MenuItemDto modeDto)
        {
            if (modeDto == null)
                return new ApiResponse<MenuItemDto>("添加失败！");
            try
            {
                var mode = mapper.Map<MenuItem>(modeDto);
                await unitOfWork.GetRepository<MenuItem>().InsertAsync(mode);
                if (await unitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse<MenuItemDto>(true, mapper.Map<MenuItemDto>(mode), "添加成功！");
                }
                else
                {
                    return new ApiResponse<MenuItemDto>("添加失败！");
                }

            }
            catch (Exception ex)
            {
                return new ApiResponse<MenuItemDto>(ex.Message);
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
                var repository = unitOfWork.GetRepository<MenuItem>();
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
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ApiResponse<PageList<MenuItemDto>>> GetAllAsync(QueryParameter query)
        {
            try
            {
                var repository = unitOfWork.GetRepository<MenuItem>();
                var results = await repository.GetPagedListAsync(pageIndex: query.PageIndex, pageSize: query.PageSize,
                    predicate: e => string.IsNullOrWhiteSpace(query.Search) ? true : e.Title.Equals(query.Search));
                PageList<MenuItemDto> pageList = new PageList<MenuItemDto>();
                pageList.PageIndex = query.PageIndex;
                pageList.PageSize = query.PageSize;
                foreach (var item in results.Items)
                {
                    pageList.Lists.Add(mapper.Map<MenuItemDto>(item));
                }
                if (results != null)
                {
                    return new ApiResponse<PageList<MenuItemDto>>(true, pageList, "获取成功！");
                }
                else
                {
                    return new ApiResponse<PageList<MenuItemDto>>("获取失败！");
                }

            }
            catch (Exception ex)
            {
                return new ApiResponse<PageList<MenuItemDto>>(ex.Message);
            }
        }
        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResponse<MenuItemDto>> GetSingleAsync(int id)
        {
            if (id < 0)
                return new ApiResponse<MenuItemDto>("获取失败！");
            try
            {
                var repository = unitOfWork.GetRepository<MenuItem>();
                var result = await repository.GetFirstOrDefaultAsync(predicate: e => e.Id == id);
                if (result != null)
                {
                    return new ApiResponse<MenuItemDto>(true, mapper.Map<MenuItemDto>(result), "获取成功！");
                }
                else
                {
                    return new ApiResponse<MenuItemDto>("获取失败！");
                }

            }
            catch (Exception ex)
            {
                return new ApiResponse<MenuItemDto>(ex.Message);
            }
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="modeDto"></param>
        /// <returns></returns>
        public async Task<ApiResponse<MenuItemDto>> UpdateAsync(MenuItemDto modeDto)
        {
            if (modeDto == null)
                return new ApiResponse<MenuItemDto>("修改失败！");
            try
            {
                var mode = mapper.Map<MenuItem>(modeDto);
                var repository = unitOfWork.GetRepository<MenuItem>();
                var result = await repository.GetFirstOrDefaultAsync(predicate: e => e.Id == mode.Id);
                result.Title = string.IsNullOrEmpty(mode.Title) ? result.Title : mode.Title;
                result.IsEnable = mode.IsEnable == result.IsEnable ? result.IsEnable : mode.IsEnable;
                result.ItemNameSpace = string.IsNullOrEmpty(mode.ItemNameSpace) ? result.ItemNameSpace : mode.ItemNameSpace;
                result.ItemType = string.IsNullOrEmpty(mode.ItemType) ? result.ItemType : mode.ItemType;
                result.IconName = string.IsNullOrEmpty(mode.IconName) ? result.IconName : mode.IconName;
                repository.Update(result);
                if (await unitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse<MenuItemDto>(true, mapper.Map<MenuItemDto>(result), "修改成功！");
                }
                else
                {
                    return new ApiResponse<MenuItemDto>("修改失败！");
                }

            }
            catch (Exception ex)
            {
                return new ApiResponse<MenuItemDto>(ex.Message);
            }
        }
    }
}
