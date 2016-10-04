using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampusSimulator.Models;

namespace CampusSimulator.ViewModels
{
    class BuildingVM
    {
        Campus.Building building = new Campus.Building();
        public BuildingVM(Campus.Building newBuilding)
        {
            building = newBuilding;
        }
        public Campus.Building.Room getRoom(int x, int y)
        {
            foreach (Campus.Building.Room room in building.rooms)
            {
                if (x >= room.point.x &&
                   x <= room.point.x + room.width &&
                   y >= room.point.y &&
                   y <= room.point.y + room.height)
                {
                    return room;
                }
            }
            return null;
        }
    }
}
