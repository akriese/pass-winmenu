using System;

namespace PassWinmenu.Utilities
{
	/// <summary>
	/// Provides a simple wrapper around an <see cref="Action"/>, where the
	/// provided method is exposed as <see cref="IDisposable.Dispose"/>.
	/// </summary>
	public sealed class Disposable
		: IDisposable
	{
		private readonly Action dispose;
		private readonly bool allowMultipleDispose;
		private bool disposed;

		/// <summary>
		/// Creates an <see cref="IDisposable"/> wrapper around a specified
		/// <see cref="Action"/>.
		/// </summary>
		/// <param name="action">
		/// The method to be exposed as <see cref="IDisposable.Dispose"/>.
		/// </param>
		/// <param name="allowMultipleDispose">
		/// Whether calling <see cref="IDisposable.Dispose"/> multiple times
		/// is to result in the calling of <paramref name="action"/> multiple
		/// times.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="action"/> is null.
		/// </exception>
		public Disposable(Action action, bool allowMultipleDispose)
		{
			dispose = action ?? throw new ArgumentNullException(nameof(action));
			this.allowMultipleDispose = allowMultipleDispose;
			disposed = false;
		}
		/// <summary>
		/// Creates an <see cref="IDisposable"/> wrapper around a specified
		/// <see cref="Action"/>.
		/// </summary>
		/// <param name="action">
		/// The method to be exposed as <see cref="IDisposable.Dispose"/>.
		/// </param>
		/// <remarks>
		/// Calling <see cref="IDisposable.Dispose"/> multiple times on an
		/// instance created with this constructor will not result in multiple
		/// calls to <paramref name="action"/>.
		/// </remarks>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="action"/> is null.
		/// </exception>
		public Disposable(Action action)
			: this(action, allowMultipleDispose: false)
		{

		}

		/// <summary>
		/// Calls the <see cref="Action"/> provided during construction.
		/// </summary>
		public void Dispose()
		{
			if (!allowMultipleDispose && disposed)
			{
				return;
			}

			dispose();
			disposed = true;
		}
	}
}
