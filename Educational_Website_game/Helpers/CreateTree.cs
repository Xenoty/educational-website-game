using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;

namespace LibraryDeweyApp.Helpers
{
    public class CreateTree
    {
        //initalize variable
        public static Tree<string> tree = new Tree<string>();
        public Tree<string> GetTree()
        {  
            //get file with data of Dewey Decimal classificaiton
            string path = AppDomain.CurrentDomain.BaseDirectory + "Helpers\\Resources\\DeweyDatav2.json";
            string jsonString = ReturnStringJson(path);

            //deserialize json and assign to dictionary
            Dictionary<string, object> dic = deserializeJson(jsonString);

            //populate tree with dictionary values using recursion
            resolveEntry(dic);

            return tree;
        }

        public string ReturnStringJson(string FilePath)
        {
            string jsonString = "";
            //use stream reader to convert json to string
            using (StreamReader r = new StreamReader(FilePath))
            {
                jsonString = r.ReadToEnd();
            }

            return jsonString;
        }

        public Dictionary<string, object> deserializeJson(string json)
        {
            //use serializer to automatically convert json to nested dictionary
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Dictionary<string, object> dictionary =
                serializer.Deserialize<Dictionary<string, object>>(json);
            return dictionary;
        }

        public static void resolveEntry(Dictionary<string, object> dic)
        {
            // Each entry in the main dictionary is key and value
            foreach (KeyValuePair<string, object> entry in dic)
            {
                //if value is a dictionary, use for meta
                if (entry.Value is Dictionary<string, object>)
                    resolveEntry((Dictionary<string, object>)entry.Value);
                else
                //if value is a collection, it has children and therefor is a parent
                if (entry.Value is ICollection)
                {
                    foreach (var item in (ICollection)entry.Value)
                    {
                        //if value is dictionary, it is a parent
                        if (item is Dictionary<string, object>)
                        {
                            //we have found a new parent
                            //create new parent node
                            tree.Begin(entry.Key);
                            resolveEntry((Dictionary<string, object>)item);
                        }
                    }
                }
                else
                    //means we have reached the children of the final parent
                    //add nodes to parent
                    tree.Add(entry.Value.ToString());
            }

            //tree has reached leaf (no more children of current branch)
            tree.End();
        }

    }

    public class Tree<T>
    {
        private Stack<TreeNode<T>> m_Stack = new Stack<TreeNode<T>>();

        public List<TreeNode<T>> Nodes { get; } = new List<TreeNode<T>>();

        public Tree<T> Begin(T val)
        {
            if (m_Stack.Count == 0)
            {
                var node = new TreeNode<T>(val, null);
                Nodes.Add(node);
                m_Stack.Push(node);
            }
            else
            {
                var node = m_Stack.Peek().Add(val);
                m_Stack.Push(node);
            }

            return this;
        }

        public Tree<T> Add(T val)
        {
            m_Stack.Peek().Add(val);
            return this;
        }

        public Tree<T> End()
        {
            try
            {
                m_Stack.Pop();
            }
            catch (Exception ex)
            {
                return this;
            }

            return this;
        }
    }

    public class TreeNode<T>
    {
        public T Value { get; }
        public TreeNode<T> Parent { get; }
        public List<TreeNode<T>> Children { get; }

        public TreeNode(T val, TreeNode<T> parent)
        {
            Value = val;
            Parent = parent;
            Children = new List<TreeNode<T>>();
        }

        public TreeNode<T> Add(T val)
        {
            var node = new TreeNode<T>(val, this);
            Children.Add(node);
            return node;
        }
    }

    public class Node<T> : IEnumerable<Node<T>>
    {
        public T Value { get; set; }
        public List<Node<T>> Children { get; private set; }

        public Node(T value)
        {
            Value = value;
            Children = new List<Node<T>>();
        }

        public Node<T> Add(T value)
        {
            var childNode = new Node<T>(value);
            Children.Add(childNode);
            return childNode;
        }

        public IEnumerator<Node<T>> GetEnumerator()
        {
            return Children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}