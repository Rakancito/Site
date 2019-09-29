using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Site.Models;

namespace Site.Controllers
{
    public class MockPlayerController : IPlayerController
    {
        private List<Players> _PlayerList;

        public MockPlayerController()
        {
            MySqlDataReader dr;
            dr = DBController.GetTopPlayers();
            if (dr != null)
            {
                _PlayerList = new List<Players>();
                while (dr.Read())
                {
                    _PlayerList.Add(new Players
                    {
                        pId = Convert.ToInt32(dr["id"]),
                        sName = dr["name"].ToString(),
                        iJob = Convert.ToInt32(dr["job"]),
                        iLevel = Convert.ToInt32(dr["level"]),
                        iExp = Convert.ToInt32(dr["exp"])
                    });
                }
            }
            else
            {
                _PlayerList.Add(new Players { sName = "Ha ocurrido un error" });
            }
        }

        public IEnumerable<Players> GetAllPlayers()
        {
            return _PlayerList;
        }
    }
}
