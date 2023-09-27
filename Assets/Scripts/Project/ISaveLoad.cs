public interface ISaveLoad {
	public void Save<TData>(TData data);
	public TData Load<TData>();
}
