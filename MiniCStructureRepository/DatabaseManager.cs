using MiniCStructureDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniCStructureRepository
{
    class DatabaseManager
    {
        static DatabaseManager()
        {
            Instance = new MiniCStructureContext();
        }
        public static MiniCStructureContext Instance { get; private set; }
    }
}
