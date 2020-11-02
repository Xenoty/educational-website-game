using LibraryDeweyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryDeweyApp.Helpers
{
    public class FindNode
    {
        public void GetNode<T>(TreeNode<T> node, int level, FindingCallNumbersModel model)
        {
            //check level, and add value from tree and assign to model accordingly
          
            if (level == 0)
            {
                model.TopLevel.Add(node.Value.ToString());
            }
            else if (level == 1)
            {
                model.SecondLevel.Add(node.Value.ToString());
            }
            else if (level == 2)
            {
                model.ThirdLevel.Add(node.Value.ToString());
            }
            level++;
            node.Children.ForEach(p => GetNode(p, level, model));
        }
    }
}