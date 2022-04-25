using GameFiles.Scripts.Services;
using Scripts.Data;

namespace Scripts.Services.PersistentProgress
{
	public interface IPersistentProgressService : IService
	{
		PlayerProgress Progress { get; set; }
	}
}