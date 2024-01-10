using System;
using Zenject;
using Zenject.Internal;
using System.Collections.Generic;

public sealed class TickableTracker : ITickable, IDisposable {
	private readonly List<ITickable> _tickables = new();

	[Preserve]
	public TickableTracker() {}

	public void Tick() =>
			_tickables.ForEach(m => m.Tick());

	public void Track(ITickable instance) =>
			_tickables.Add(instance);

	public void Dispose() =>
			_tickables.Clear();
}