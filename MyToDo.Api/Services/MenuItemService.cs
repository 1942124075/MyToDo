﻿using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using MyToDo.Api.Services.Interfaces;
using MyToDo.Library.Entity;
using MyToDo.Library.Modes;

namespace MyToDo.Api.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MenuItemService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> AddAsync(MenuItemDto modeDto)
        {
            if (modeDto == null)
                return new ApiResponse("添加失败！");
            try
            {
                var mode = mapper.Map<MenuItem>(modeDto);
                await unitOfWork.GetRepository<MenuItem>().InsertAsync(mode);
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

        public async Task<ApiResponse> GetAllAsync(QueryParameter query)
        {
            try
            {
                var repository = unitOfWork.GetRepository<MenuItem>();
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

        public async Task<ApiResponse> GetSingleAsync(int id)
        {
            if (id < 0)
                return new ApiResponse("获取失败！");
            try
            {
                var repository = unitOfWork.GetRepository<MenuItem>();
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

        public async Task<ApiResponse> UpdateAsync(MenuItemDto modeDto)
        {
            if (modeDto == null)
                return new ApiResponse("修改失败！");
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
