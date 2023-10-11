using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using MyToDo.Api.Services.Interfaces;
using MyToDo.Library.Entity;
using MyToDo.Library.Modes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyToDo.Api.Services
{
    /// <summary>
    /// 块服务
    /// </summary>
    public class BlockItemService : IBlockItemService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public BlockItemService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="modeDto"></param>
        /// <returns></returns>
        public async Task<ApiResponse<BlockItemDto>> AddAsync(BlockItemDto modeDto)
        {
            if (modeDto == null)
                return new ApiResponse<BlockItemDto>("添加失败！");
            try
            {
                var mode = mapper.Map<BlockItem>(modeDto);
                await unitOfWork.GetRepository<BlockItem>().InsertAsync(mode);
                if (await unitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse<BlockItemDto>(true, mapper.Map<BlockItemDto>(mode), "添加成功！");
                }
                else
                {
                    return new ApiResponse<BlockItemDto>("添加失败！");
                }
                
            }
            catch (Exception ex)
            {
                return new ApiResponse<BlockItemDto>(ex.Message);
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
                var repository = unitOfWork.GetRepository<BlockItem>();
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
        public async Task<ApiResponse<PageList<BlockItemDto>>> GetAllAsync(QueryParameter query)
        {
            try
            {
                var repository = unitOfWork.GetRepository<BlockItem>();
                var results = await repository.GetPagedListAsync(pageIndex: query.PageIndex, pageSize: query.PageSize,
                    predicate: e => string.IsNullOrWhiteSpace(query.Search) ? true : e.Title.Equals(query.Search));
                PageList<BlockItemDto> pageList = new PageList<BlockItemDto>();
                pageList.PageIndex = query.PageIndex;
                pageList.PageSize = query.PageSize;
                foreach ( var item in results.Items)
                {
                    pageList.Lists.Add(mapper.Map<BlockItemDto>(item));
                }
                if (results != null)
                {
                    return new ApiResponse<PageList<BlockItemDto>>(true, pageList, "获取成功！");
                }
                else
                {
                    return new ApiResponse<PageList<BlockItemDto>>("获取失败！");
                }

            }
            catch (Exception ex)
            {
                return new ApiResponse<PageList<BlockItemDto>>(ex.Message);
            }
        }
        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResponse<BlockItemDto>> GetSingleAsync(int id)
        {
            if (id < 0)
                return new ApiResponse<BlockItemDto>("获取失败！");
            try
            {
                var repository = unitOfWork.GetRepository<BlockItem>();
                var result = await repository.GetFirstOrDefaultAsync(predicate: e => e.Id == id);
                if (result != null)
                {
                    return new ApiResponse<BlockItemDto>(true, mapper.Map<BlockItemDto>(result), "获取成功！");
                }
                else
                {
                    return new ApiResponse<BlockItemDto>("获取失败！");
                }

            }
            catch (Exception ex)
            {
                return new ApiResponse<BlockItemDto>(ex.Message);
            }
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="modeDto"></param>
        /// <returns></returns>
        public async Task<ApiResponse<BlockItemDto>> UpdateAsync(BlockItemDto modeDto)
        {
            if (modeDto == null)
                return new ApiResponse<BlockItemDto>("修改失败！");
            try
            {
                var mode = mapper.Map<BlockItem>(modeDto);
                var repository = unitOfWork.GetRepository<BlockItem>();
                var result = await repository.GetFirstOrDefaultAsync(predicate: e => e.Id == mode.Id);
                result.Title = string.IsNullOrEmpty(mode.Title) ? result.Title : mode.Title;
                result.Value = string.IsNullOrEmpty(mode.Value) ? result.Value : mode.Value; 
                result.BackColor = string.IsNullOrEmpty(mode.BackColor) ? result.BackColor : mode.BackColor; 
                result.IconName = string.IsNullOrEmpty(mode.IconName) ? result.IconName : mode.IconName;
                repository.Update(result);
                if (await unitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse<BlockItemDto>(true, mapper.Map<BlockItemDto>(result), "修改成功！");
                }
                else
                {
                    return new ApiResponse<BlockItemDto>("修改失败！");
                }

            }
            catch (Exception ex)
            {
                return new ApiResponse<BlockItemDto>    (ex.Message);
            }
        }
    }
}
