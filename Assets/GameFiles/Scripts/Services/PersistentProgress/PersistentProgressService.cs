using Scripts.Data;

namespace Scripts.Services.PersistentProgress
{
	public class PersistentProgressService : IPersistentProgressService
	{
		public PlayerProgress Progress { get; set; }
	}
}