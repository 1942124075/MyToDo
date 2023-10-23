using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Library.Modes
{
	/// <summary>
	/// 汇总类
	/// </summary>
    public class SummaryDto : BindableBase
    {
		private int todoCount;
		private int todoFinshCount;
		private string todoFinshRatio;
		private int memoCount;
		private ObservableCollection<ToDoDto> todoList;
		private ObservableCollection<MemoDto> memoList;
        private UserDto user;

        /// <summary>
        /// 所属用户
        /// </summary>
        public UserDto User
        {
            get { return user; }
            set { user = value; }
        }

        /// <summary>
        /// 备忘录列表
        /// </summary>
        public ObservableCollection<MemoDto> MemoList
        {
			get { return memoList; }
			set { memoList = value; RaisePropertyChanged(); }
		}

		/// <summary>
		/// 待办列表
		/// </summary>
		public ObservableCollection<ToDoDto> TodoList
        {
			get { return todoList; }
			set { todoList = value; RaisePropertyChanged(); }
		}


		/// <summary>
		/// 备忘录总数
		/// </summary>
		public int MemoCount
        {
			get { return memoCount; }
			set { memoCount = value; RaisePropertyChanged(); }
		}

		/// <summary>
		/// 待办完成比例
		/// </summary>
		public string TodoFinshRatioRatio
        {
			get { return todoFinshRatio; }
			set { todoFinshRatio = value; RaisePropertyChanged(); }
		}

		/// <summary>
		/// 已完成待办总数
		/// </summary>
		public int TodoFinshCount
        {
			get { return todoFinshCount; }
			set { todoFinshCount = value; RaisePropertyChanged(); }
		}

		/// <summary>
		/// 待办总数
		/// </summary>
		public int TodoCount
        {
			get { return todoCount; }
			set { todoCount = value; RaisePropertyChanged(); }
		}
		/// <summary>
		/// 计算百分比
		/// </summary>
		/// <param name="num1"></param>
		/// <param name="num2"></param>
		/// <returns></returns>
		public string CalcRatio(int num1,int num2)
		{
			return (num1 /(double) num2).ToString("0%");
        }

	}
}
