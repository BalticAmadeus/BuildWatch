using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BalticAmadeus.BuildWatch.Infrastructure
{
	public class AsyncCollection<T> : ObservableCollection<T>
	{
		private readonly Func<Task<IEnumerable<T>>> _refreshAction;
		private bool _isExecuting;

		public event EventHandler IsExecutingChanged;

		public AsyncCollection(Func<Task<IEnumerable<T>>> refreshAction)
		{
			_refreshAction = refreshAction;
		}

		protected virtual void OnIsExecutingChanged()
		{
			var handler = IsExecutingChanged;
			if (handler == null)
				return;

			handler(this, new EventArgs());
		}

		public bool IsExecuting
		{
			get { return _isExecuting; }
			private set
			{
				_isExecuting = value;

				OnIsExecutingChanged();
				OnPropertyChanged(new PropertyChangedEventArgs("IsExecuting"));
			}
		}

		public async void Refresh()
		{
			IsExecuting = true;

			var itemsResult = await _refreshAction();

			Clear();
			foreach (var item in itemsResult)
				Add(item);

			IsExecuting = false;
		}
	}
}
