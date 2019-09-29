using System.Collections.Generic;
using Site.Models;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Site.Controllers
{
    public interface IPlayerController
    {
        IEnumerable<Players> GetAllPlayers();
    }

}
