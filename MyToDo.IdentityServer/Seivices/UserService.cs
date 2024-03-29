﻿using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyToDo.IdentityServer.Seivices.Interfaces;
using MyToDo.Library.Entity;
using MyToDo.Library.Modes;

namespace MyToDo.IdentityServer.Seivices
{
    public class UserService : IUserSerivce
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ICustomJTMService customJTM;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ICustomJTMService customJTM) 
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.customJTM = customJTM;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<ApiResponse> LoginAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return new ApiResponse("用户名和密码不能为空");
            }
            User user = await unitOfWork.GetRepository<User>().GetFirstOrDefaultAsync(predicate:e => username.Equals(e.UserName) && password.Equals(e.Password));
            if (user == null)
            {
                return new ApiResponse("用户名或密码错误!");
            }
            string token = customJTM.GetToken(user);
            return new ApiResponse(true, token);
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<ApiResponse> RegisterAsync(User user)
        {
            if (user == null)
            {
                return new ApiResponse("用户信息不能为空");
            }
            if (string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Password))
            {
                return new ApiResponse("用户名和密码不能为空");
            }
            User findUser = unitOfWork.GetRepository<User>().GetFirstOrDefault(predicate: e => e.UserName.Equals(user.UserName));
            if (findUser != null)
            {
                return new ApiResponse("注册失败,该用户名已存在!");
            }
            await unitOfWork.GetRepository<User>().InsertAsync(user);
            if (await unitOfWork.SaveChangesAsync() > 0)
            {
                return new ApiResponse(true, user);
            }
            else
            {
                return new ApiResponse("注册失败!");
            }
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<ApiResponse> UpdateAsync(UserDto user)
        {
            if (user == null)
            {
                return new ApiResponse("用户信息不能为空");
            }
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                return new ApiResponse("用户名不能为空");
            }
            User findUser = unitOfWork.GetRepository<User>().GetFirstOrDefault(predicate: e => e.Id == user.Id);
            if (findUser == null)
            {
                return new ApiResponse("修改失败,用户不存在!");
            }
            findUser.UserName = user.UserName;
            findUser.Age = user.Age;
            findUser.Sex = user.Sex;
            findUser.Role = user.Role;
            findUser.ModifyDate = DateTime.Now;
            unitOfWork.GetRepository<User>().Update(findUser);
            if (await unitOfWork.SaveChangesAsync() > 0)
            {
                return new ApiResponse(true, findUser);
            }
            else
            {
                return new ApiResponse("注册失败!");
            }
        }
    }
}
