using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampusSimulator.Models;

namespace CampusSimulator.ViewModels
{
    class CampusVM
    {
        public Campuses campuses = new Campuses();
        public Campus campus = new Campus();
        public List<Campus.Point> tempPoints = new List<Campus.Point>();
        public List<Campus.Edge> tempEdges = new List<Campus.Edge>();
        public CampusVM(Campus newCampus)
        {
            campus = newCampus;
        }
        public Campus.Building getBuilding(int x, int y)
        {
            foreach (Campus.Building building in campus.buildings)
            {
                if (x >= building.point.x &&
                   x <= building.point.x + building.width &&
                   y >= building.point.y &&
                   y <= building.point.y + building.height)
                {
                    return building;
                }
            }
            return null;
        }
        public List<Campus.Edge> shortestPath(Campus.Point start, Campus.Point end)
        {
            List<Campus.Point> visited = new List<Campus.Point>();
            List<Campus.Edge> returnEdges = new List<Campus.Edge>();
            foreach (Campus.Point point in campus.points)
            {
                point.distance = 10000;
            }
            start.distance = 0;
            while (true)
            {
                int currentDistance = 10000;
                Campus.Point currentPoint = null;

                foreach (Campus.Point point in campus.points)
                {
                    if (point.distance < currentDistance &&
                        !visited.Contains(point))
                    {
                        currentDistance = point.distance;
                        currentPoint = point;
                        
                    }
                }
                if (currentPoint == null)
                    break;
                List<Campus.Point> connectedPoints = getConnectedPoints(currentPoint);
                foreach (Campus.Point point in connectedPoints)
                {
                    if (!visited.Contains(point) &&
                        (currentPoint.distance + getDistance(point, currentPoint)) < point.distance)
                    point.distance = currentPoint.distance + getDistance(point, currentPoint);
                }
                visited.Add(currentPoint);
            }
            Campus.Point reversePoint = end;
            while (true)
            {
                List<Campus.Point> connectedPoints = getConnectedPoints(reversePoint);
                foreach(Campus.Point point in connectedPoints)
                {
                    if (reversePoint.distance - getDistance(point, reversePoint) == point.distance)
                    {
                        returnEdges.Add(getEdge(reversePoint, point));
                        reversePoint = point;
                        break;
                    }
                }
                if (reversePoint == start)
                    break;
            }
            return returnEdges;
        }
        private Campus.Edge getEdge(Campus.Point p1, Campus.Point p2)
        {
            foreach (Campus.Edge edge in campus.edges)
            {
                if (edge.p1 == p1)
                {
                    if (edge.p2 == p2)
                    {
                        return edge;
                    }
                }
                if (edge.p2 == p1)
                {
                    if (edge.p1 == p2)
                    {
                        return edge;
                    }
                }
            }
            return null;

        }
        private int getDistance(Campus.Point p1, Campus.Point p2)
        {
            return (int)Math.Sqrt(Math.Pow(p1.x - p2.x, 2) + Math.Pow(p1.y - p2.y, 2));
        }
        private List<Campus.Point> getConnectedPoints(Campus.Point point)
        {
            List<Campus.Point> returnPoints = new List<Campus.Point>();
            foreach (Campus.Edge edge in campus.edges)
            {
                if(edge.p1 == point)
                {
                    returnPoints.Add(edge.p2);
                }
                if (edge.p2 == point)
                {
                    returnPoints.Add(edge.p1);
                }
            }
            return returnPoints;
        }
        public Campus.Point addPoint(int x, int y)
        {
            Campus.Point newPoint = new Campus.Point(x,y);
            double distance = 10000;
            Campus.Edge closestEdge = null;
            Campus.Point closestPoint = new Campus.Point();
            bool isPoint = false;
            double k = 0;
            int x4 = 0, y4 = 0;
            foreach (Campus.Edge edge in campus.edges)
            {
                double newDistance = findDistance(edge.p1, edge.p2, newPoint);
                if (newDistance < distance)
                {
                    double tempk = k;
                    int tempx4 = x4, tempy4 = y4;
                    k = (edge.p2.y - edge.p1.y) * (newPoint.x - edge.p2.x);
                    k -= ((edge.p2.x - edge.p1.x) * (newPoint.y - edge.p1.y));
                    k /= (Math.Pow(edge.p2.y - edge.p1.y, 2) + Math.Pow(edge.p2.x - edge.p1.x, 2));
                    x4 = (int)(newPoint.x - k * (edge.p2.y - edge.p1.y));
                    y4 = (int)(newPoint.y + k * (edge.p2.x - edge.p1.x));
                    if ((x4 < edge.p1.x-10 && x4 < edge.p2.x-10) || (x4 > edge.p1.x+10 && x4 > edge.p2.x+10) ||
                        (y4 < edge.p1.y-10 && y4 < edge.p2.y-10) || (y4 > edge.p1.y+10 && y4 > edge.p2.y+10))
                    {
                        k = tempk;
                        x4 = tempx4;
                        y4 = tempy4;
                    }
                    else
                    {
                        distance = newDistance;
                        closestEdge = edge;
                    }
                }
                
            }
            foreach (Campus.Point point in campus.points)
            {
                double newDistance = Math.Sqrt(Math.Pow(point.x-newPoint.x,2)+Math.Pow(point.y-newPoint.y,2));
                if (newDistance < distance)
                {
                    distance = newDistance;
                    closestPoint = point;
                    isPoint = true;
                }
            }
            if (isPoint == true)
                campus.edges.Add(new Campus.Edge(closestPoint, newPoint));
            else
            {
                Campus.Point linePoint = new Campus.Point(x4, y4);
                campus.points.Add(linePoint);
                campus.edges.Add(new Campus.Edge(newPoint, linePoint));
                campus.edges.Add(new Campus.Edge(closestEdge.p2, linePoint));
                closestEdge.p2 = linePoint;
            }
            campus.points.Add(newPoint);
            return newPoint;
        }
        public double findDistance(Campus.Point p1, Campus.Point p2, Campus.Point p3)
        {
            double answer;
            answer = (p2.y - p1.y) * p3.x;
            answer -= (p2.x - p1.x) * p3.y;
            answer += p2.x * p1.y;
            answer -= p2.y * p1.x;
            answer = Math.Abs(answer);
            double denominator;
            denominator = Math.Pow(p2.y - p1.y, 2);
            denominator += Math.Pow(p2.x - p1.x, 2);
            denominator = Math.Sqrt(denominator);
            answer /= denominator;
            return answer;
        }
        public void resetPoints()
        {
           campus.points = campuses.getPoints(campus.name);
           campus.edges = campuses.getEdges(campus.name, campus.points);
        }
    }
}
