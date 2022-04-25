using Scripts.Data;

namespace GameFiles.Scripts.Services.SaveLoad
{
	public interface ISaveLoadService : IService
	{
		void SaveProgress();
		PlayerProgress LoadProgress();
	}
}