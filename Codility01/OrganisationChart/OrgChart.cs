using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.OrganisationChart
{
    public class OrgChart
    {
        private Node _root = new Node();
        private HashSet<string> _employees = new HashSet<string>();

        public void Add(string id, string name, string managerId)
        {
            if (id == null || _employees.Contains(id))
                return;

            else
            {
                var employee = new Node
                {
                    Id = id,
                    Name = name
                };

                if (!_employees.Contains(managerId))
                    _root.Subordinates.Add(employee);

                else
                {
                    var manager = _root.FindNode(managerId);
                    manager.Subordinates.Add(employee);
                    employee.Manager = manager;
                }

                _employees.Add(id);
            }
        }

        public void Print()
        {
            var output = _root.Output(0);
            Console.WriteLine(output);
        }

        public void Remove(string employeeId)
        {
            if (_employees.Contains(employeeId))
            {
                var employee = _root.FindNode(employeeId);
                var manager = employee.Manager ?? _root;

                //remove the employee
                manager.Subordinates.Remove(employee);
                employee.Manager = null;

                //copy subordinates
                foreach (var child in employee.Subordinates)
                {
                    manager.Subordinates.Add(child);
                    child.Manager = manager;
                }

                _employees.Remove(employeeId);
            }
        }

        public void Move(string employeeId, string newManagerId)
        {
            if (employeeId == null || newManagerId == null
                ||!_employees.Contains(newManagerId) 
                || !_employees.Contains(employeeId)
                || employeeId == newManagerId)
                return;

            var newManager = _root.FindNode(newManagerId);
            var employee = _root.FindNode(employeeId);

            if (employee.Manager == newManager)
                return;

            var oldManager = employee.Manager ?? _root;
            oldManager.Subordinates.Remove(employee);

            newManager.Subordinates.Add(employee);
            employee.Manager = newManager;
        }

        public int Count(string employeeId)
        {
            var employee = _root.FindNode(employeeId);

            return employee?.Count() ?? 0;
        }
    }

    public class Node
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsRoot { get; set; }

        public List<Node> Subordinates { get; } = new List<Node>();

        public Node Manager { get; set; }

        public Node FindNode(string id)
        {
            if (Id == id)
                return this;

            else
            {
                foreach(var child in Subordinates)
                {
                    var node = child.FindNode(id);
                    if (node != null)
                        return node;
                }
                return null;
            }
        }

        public string Output(int indent)
        {
            var sb = new StringBuilder();
            for (int cnt = 0; cnt < indent; cnt++)
                sb.Append("  ");

            if(!IsRoot)
                sb.Append(Name).Append(" [").Append(Id).Append("]");

            foreach(var child in Subordinates)
            {
                sb.AppendLine().Append(child.Output(indent + 1));
            }
            sb.Remove(0, 1);
            return sb.ToString();
        }

        public int Count()
        {
            var count = 1;
            foreach (var child in Subordinates)
                count += child.Count();

            return count;
        }
    }
}
