using System;

public interface IInput {
	event Action<float> HorizontalAxisChangedEvent;
	event Action  ShotEvent;
	bool Enabled { get; set; }
}
