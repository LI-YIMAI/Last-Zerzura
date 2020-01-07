package BinaryTree;

import java.util.*;


public class PreOrder {
    public List<Integer> preorderTraversal(TreeNode root) {
        Stack<TreeNode> nodeStack = new Stack<>();
        ArrayList<Integer> preorderList = new ArrayList<>();
        if (root == null){
            return preorderList;
        }
        nodeStack.push(root);
        while(!nodeStack.empty()){
            TreeNode tempNode = nodeStack.peek();
            preorderList.add(nodeStack.pop().val);

            if(tempNode.right != null){
                nodeStack.push(tempNode.right);
            }
            if(tempNode.left != null){
                nodeStack.push(tempNode.left);
            }
        }
        return preorderList;



    }
}
