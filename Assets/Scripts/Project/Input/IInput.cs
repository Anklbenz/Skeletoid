using System;

public interface IInput {
	event Action<float> HorizontalAxisChangedEvent;
	event Action ShotEvent, AnyPressedEvent;

	bool enabled { get; set; }
}
