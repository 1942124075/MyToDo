﻿using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using MyToDo.Api.Services.Interfaces;
using MyToDo.Library.Entity;
using MyToDo.Library.Modes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyToDo.Api.Services
{
    /// <summary>
    /// 备忘录服务
    /// </summary>
    public class MemoService : IMemoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public MemoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public async Task<ApiResponse<MemoDto>> AddAsync(Memo mode)
        {
            if (mode != null && mode.User != null)
            {
                mode.CreateDate = DateTime.Now;
                mode.ModifyDate = DateTime.Now;
                mode.UserId = mode.User.Id;
                mode.User = null;
                await unitOfWork.GetRepository<Memo>().InsertAsync(mode);
                if (await unitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse<MemoDto>(true, mapper.Map<MemoDto>(mode), "添加成功！");
                }
                else
                {
                    return new ApiResponse<MemoDto>("添加失败！");
                }
            }
            else
            {
                return new ApiResponse<MemoDto>("添加失败！");
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
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse<PageList<MemoDto>>> GetAllAsync(QueryParameter query, User user)
        {
            var repository = unitOfWork.GetRepository<Memo>();
            var results = await repository.GetPagedListAsync(pageIndex: query.PageIndex, pageSize: query.PageSize,
                predicate: e => e.UserId == user.Id && (string.IsNullOrWhiteSpace(query.Search) ? true : e.Title.Contains(query.Search)));
            PageList<MemoDto> pageList = new PageList<MemoDto>();
            pageList.PageIndex = query.PageIndex;
            pageList.PageSize = query.PageSize;
            foreach (var item in results.Items)
            {
                pageList.Lists.Add(mapper.Map<MemoDto>(item));
            }
            if (results != null)
            {
                return new ApiResponse<PageList<MemoDto>>(true, pageList, "获取成功！");
            }
            else
            {
                return new ApiResponse<PageList<MemoDto>>("获取失败！");
            }
        }
        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<ApiResponse<MemoDto>> GetSingleAsync(int id, User user)
        {
            if (id < 0)
                return new ApiResponse<MemoDto>("获取失败！");
            var repository = unitOfWork.GetRepository<Memo>();
            var result = await repository.GetFirstOrDefaultAsync(predicate: e => e.UserId == user.Id && e.Id == id);
            if (result != null)
            {
                return new ApiResponse<MemoDto>(true, mapper.Map<MemoDto>(result), "获取成功！");
            }
            else
            {
                return new ApiResponse<MemoDto>("获取失败！");
            }
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public async Task<ApiResponse<MemoDto>> UpdateAsync(Memo mode)
        {
            if (mode == null)
                return new ApiResponse<MemoDto>("修改失败！");
            var repository = unitOfWork.GetRepository<Memo>();
            var result = await repository.GetFirstOrDefaultAsync(predicate: e => e.Id == mode.Id);
            result.Title = string.IsNullOrEmpty(mode.Title) ? result.Title : mode.Title;
            result.Content = string.IsNullOrEmpty(mode.Content) ? result.Content : mode.Content;
            result.ModifyDate = DateTime.Now;
            repository.Update(result);
            if (await unitOfWork.SaveChangesAsync() > 0)
            {
                return new ApiResponse<MemoDto>(true, mapper.Map<MemoDto>(result), "修改成功！");
            }
            else
            {
                return new ApiResponse<MemoDto>("修改失败！");
            }
        }
    }
}
