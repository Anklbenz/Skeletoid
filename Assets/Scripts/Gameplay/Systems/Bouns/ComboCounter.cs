public class ComboCounter {
	public int comboCount => _combo;
	private int _combo;

	public int Increase() {
		return ++_combo;
	}

	public void Reset() {
		_combo = 0;
	}
}