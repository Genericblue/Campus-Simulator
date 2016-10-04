using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CampusSimulator.Models
{
    public class Campus
    {
        public class Building
        {
            public string name;
            public string campusName;
            public Point point;
            public int width;
            public int height;
            public string hours;
            public List<Room> rooms = new List<Room>();
            public Building()
            {
                point = new Point();
            }
            public class Room
            {
                public string name;
                public Point point;
                public int width;
                public int height;
                public string person;
                public string hours;
                public Room()
                {
                    point = new Point();
                }
            }
        }
        public class Point
        {
            public int x;
            public int y;
            public int distance;
            public Point(int nX, int nY)
            {
                x = nX;
                y = nY;
            }
            public Point()
            {

            }
        }
        public class Edge
        {
            public Point p1;
            public Point p2;
            public Edge()
            {
                p1 = new Point();
                p2 = new Point();
            }
            public Edge(Point np1, Point np2)
            {
                p1 = np1;
                p2 = np2;
            }
        }
        public string name;
        public List<Building> buildings = new List<Building>();
        public List<Point> points = new List<Point>();
        public List<Edge> edges = new List<Edge>();
    }

    public class Campuses
    {
        public Campuses()
        {
            //initCampus();
        }
        public Campus getCampus(string campusName)
        {
            Campus returnCampus = new Campus();
            XmlDocument xml = new XmlDocument();
            try
            {
                xml.Load(campusName + "/campus");
            } 
            catch
            {
                return null;
            }
            returnCampus.name = campusName;
            int i = 0;
            foreach (XmlNode node in xml.SelectNodes("campus/building"))
            {
                
                /*
                AbstractCell cell = GetCell(Convert.ToInt32(node.SelectSingleNode("column").InnerText), Convert.ToInt32(node.SelectSingleNode("row").InnerText));
                cell.Text = node.SelectSingleNode("text").InnerText;
                cell.BGC = Convert.ToInt32(node.SelectSingleNode("bgc").InnerText);
                 * */
                Campus.Building newBuilding = new Campus.Building();
                newBuilding.name = node.SelectSingleNode("name").InnerText;
                newBuilding.campusName = returnCampus.name;
                returnCampus.buildings.Add(newBuilding);
                returnCampus.buildings[i].point.x = Convert.ToInt32(node.SelectSingleNode("x").InnerText);
                returnCampus.buildings[i].point.y = Convert.ToInt32(node.SelectSingleNode("y").InnerText);
                returnCampus.buildings[i].width = Convert.ToInt32(node.SelectSingleNode("width").InnerText);
                returnCampus.buildings[i].height = Convert.ToInt32(node.SelectSingleNode("height").InnerText);
                returnCampus.buildings[i].hours = node.SelectSingleNode("hours").InnerText;
                int j = 0;
                foreach (XmlNode node2 in xml.SelectNodes("campus/building/room"))
                {
                    returnCampus.buildings[i].rooms.Add(new Campus.Building.Room());
                    returnCampus.buildings[i].rooms[j].name = node2.SelectSingleNode("name").InnerText;
                    returnCampus.buildings[i].rooms[j].point.x = Convert.ToInt32(node2.SelectSingleNode("x").InnerText);
                    returnCampus.buildings[i].rooms[j].point.y = Convert.ToInt32(node2.SelectSingleNode("y").InnerText);
                    returnCampus.buildings[i].rooms[j].width = Convert.ToInt32(node2.SelectSingleNode("width").InnerText);
                    returnCampus.buildings[i].rooms[j].height = Convert.ToInt32(node2.SelectSingleNode("height").InnerText);
                    returnCampus.buildings[i].rooms[j].person = node2.SelectSingleNode("person").InnerText;
                    returnCampus.buildings[i].rooms[j].hours = node2.SelectSingleNode("hours").InnerText;
                    j++;
                }
                i++;
            }
            i = 0;
            foreach (XmlNode node in xml.SelectNodes("campus/point"))
            {
                Campus.Point newPoint = new Campus.Point();
                returnCampus.points.Add(newPoint);
                returnCampus.points[i].x = Convert.ToInt32(node.SelectSingleNode("x").InnerText);
                returnCampus.points[i].y = Convert.ToInt32(node.SelectSingleNode("y").InnerText);
                i++;
            }
            i = 0;
            foreach (XmlNode node in xml.SelectNodes("campus/edge"))
            {
                Campus.Edge newEdge = new Campus.Edge();
                returnCampus.edges.Add(newEdge);
                returnCampus.edges[i].p1 = returnCampus.points[Convert.ToInt32(node.SelectSingleNode("first").InnerText)];
                returnCampus.edges[i].p2 = returnCampus.points[Convert.ToInt32(node.SelectSingleNode("second").InnerText)];
                i++;
            }
            return returnCampus;
        }
        public List<Campus.Point> getPoints(string campusName)
        {
            List<Campus.Point> returnPoints = new List<Campus.Point>();
            XmlDocument xml = new XmlDocument();
            try
            {
                xml.Load(campusName + "/campus");
            } 
            catch
            {
                return null;
            }
            int i = 0;
            foreach (XmlNode node in xml.SelectNodes("campus/point"))
            {
                Campus.Point newPoint = new Campus.Point();
                returnPoints.Add(newPoint);
                returnPoints[i].x = Convert.ToInt32(node.SelectSingleNode("x").InnerText);
                returnPoints[i].y = Convert.ToInt32(node.SelectSingleNode("y").InnerText);
                i++;
            }
            return returnPoints;
        }
        public List<Campus.Edge> getEdges(string campusName, List<Campus.Point> points)
        {
            List<Campus.Edge> returnEdges = new List<Campus.Edge>();
            XmlDocument xml = new XmlDocument();
            try
            {
                xml.Load(campusName + "/campus");
            }
            catch
            {
                return null;
            }
            int i = 0;
            foreach (XmlNode node in xml.SelectNodes("campus/edge"))
            {
                Campus.Edge newEdge = new Campus.Edge();
                returnEdges.Add(newEdge);
                returnEdges[i].p1 = points[Convert.ToInt32(node.SelectSingleNode("first").InnerText)];
                returnEdges[i].p2 = points[Convert.ToInt32(node.SelectSingleNode("second").InnerText)];
                i++;
            }
            return returnEdges;
        }
    }
}
