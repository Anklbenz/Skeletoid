using System;

public interface IInput {
	event Action Left, Right, Shot;
}
