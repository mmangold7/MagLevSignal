using System.Collections.Generic;
using System.Threading.Tasks;
using MagLevSignal.Models;

namespace MagLevSignal.Hubs
{
    public interface IMagLevHub
    {
        #region ///  Methods  ///

        Task GameState(List<Body> bodies);
        Task Pause();
        Task Resume();
        Task TogglePaused();
        Task ResetSimulation();

        #endregion
    }
}