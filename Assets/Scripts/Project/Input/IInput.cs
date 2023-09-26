using System;

public interface IInput {
	event Action Left, Right, Shot;
	bool Enabled { get; set; }
}
